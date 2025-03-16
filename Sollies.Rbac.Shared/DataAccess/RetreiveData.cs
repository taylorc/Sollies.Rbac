
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using NetCoreForce.Client.Models;
using Newtonsoft.Json.Linq;
using Sollies.Rbac.Shared.Comparers;
using Sollies.Rbac.Shared.Interfaces;
using Sollies.Rbac.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Sollies.Rbac.Shared.DataAccess
{
    public class RetrieveData : IRetrieveData
    {
        private static readonly long MEM_THRESHOLD_MB = 10;
        private readonly IForceClientFactory forceClientFactory;
        private readonly ILogger<BaseCompare> logger;
        private readonly IMemoryCache memoryCache;

        public RetrieveData(IForceClientFactory forceClientFactory, ILogger<BaseCompare> logger, IMemoryCache memoryCache)
        {
            this.forceClientFactory = forceClientFactory;
            this.logger = logger;
            this.memoryCache = memoryCache;
        }

        public async Task<List<JObject>> Query(string query, bool retry)
        {
            var client = await forceClientFactory.Build();

            return await client.Query<JObject>(query);

        }

        public async Task<List<JObject>> GetItems(string itemType, string search, int queryLimit, bool retry)
        {
            var items = await Query(GenerateQuery(itemType, search, queryLimit), retry);

            foreach (var item in items)
            {
                var itemJson = item;
                switch (itemType)
                {
                    case "PermissionSet":
                        itemJson["Label"] = HttpUtility.HtmlEncode(itemJson["Label"].ToString());
                        break;
                    case "ProfilePermissionSet":
                        itemJson["Profile"]["Name"] = HttpUtility.HtmlEncode(itemJson["Profile"]["Name"].ToString());
                        break;
                    default:
                        itemJson["Name"] = HttpUtility.HtmlEncode(itemJson["Name"].ToString());
                        break;
                }
            }
            return items;
        }

        private string GenerateQuery(string itemType, string search, int itemLimit)
        {
            var queryBuilder = new StringBuilder();
            queryBuilder.Append("SELECT Id, ");
            bool isProfilePermset = itemType.Equals("ProfilePermissionSet");

            string displayField = "";
            if (itemType.Equals("User"))
            {
                displayField = "Name";

                queryBuilder.Append(displayField).Append(" ,AccountId, Username FROM User WHERE IsActive=true ");
            }
            else if (itemType.Equals("PermissionSet"))
            {
                displayField = "Label";
                queryBuilder.Append(displayField).Append(" FROM PermissionSet WHERE IsOwnedByProfile=false ");
            }
            else if (isProfilePermset)
            {
                displayField = "Profile.Name";
                queryBuilder.Append(displayField).Append(" FROM PermissionSet WHERE IsOwnedByProfile=true ");
            }

            if (!string.IsNullOrEmpty(search))
            {
                string safeSearch = search.Replace("'", "\\'");
                if (!safeSearch.StartsWith("%") && !safeSearch.EndsWith("%"))
                {
                    safeSearch = "%" + safeSearch + "%";
                }
                queryBuilder.Append("AND ").Append(displayField).Append(" LIKE '").Append(safeSearch).Append("' ");
            }
            queryBuilder.Append("ORDER BY ").Append(displayField);
            queryBuilder.Append(" ASC NULLS LAST LIMIT ").Append(itemLimit);

            logger.LogInformation("QUERY: {0}", queryBuilder.ToString());
            return queryBuilder.ToString();
        }

        public async Task<List<SObjectFieldMetadata>> GetUserPerms(bool retry)
        {
            var client = await forceClientFactory.Build();

            string cacheKey = "-userperms";
            var userPerms = memoryCache.Get(cacheKey) as List<SObjectFieldMetadata>;
            if (userPerms != null)
            {
                memoryCache.Set(cacheKey, userPerms, DateTimeOffset.Now.AddMinutes(6));
                return userPerms;
            }
            else
            {
                var response = await client.GetObjectDescribe("PermissionSet");
                memoryCache.Set(cacheKey, response.Fields, DateTimeOffset.Now.AddMinutes(6));

                return response.Fields;
            }

        }

        protected string UserPermQueryBuilder(string permsetId, HashSet<string> userPerms)
        {
            var queryBuild = new StringBuilder();
            queryBuild.Append("SELECT ");
            var baseCompare = new BaseCompare(this, logger);
            baseCompare.AppendParamsToQuery(queryBuild, BaseCompare.USER_PERMS, userPerms);
            queryBuild.Append(" FROM PermissionSet WHERE Id='").Append(permsetId).Append("'");

            return queryBuild.ToString();
        }

        public async Task<List<UserPermissionsDto>> GetPermsetUserPerms(string permsetId, HashSet<string> userPerms, bool retry)
        {
            string cacheKey = "-userperms-" + permsetId;

            List<UserPermissionsDto>  userPermCacheString = memoryCache.Get(cacheKey) as List<UserPermissionsDto>;

            if (userPermCacheString != null)
            {
                memoryCache.Set(cacheKey, userPermCacheString, DateTimeOffset.Now.AddMinutes(3));
                return userPermCacheString;
            }
            else
            {
                var userPermJsonObject = await Query(UserPermQueryBuilder(permsetId, userPerms), retry);
                if (!FreeMemoryBelowThreshold())
                {
                    memoryCache.Set(cacheKey, userPermJsonObject.ToString(), DateTimeOffset.Now.AddMinutes(3));
                }

                userPermCacheString = new List<UserPermissionsDto>();

                for (var i = 0; i < userPermJsonObject.Count; i++)
                {
                    var userPerm = UserPermissionsDto.FromJson(userPermJsonObject[i].ToString());
                    userPermCacheString.Add(userPerm);
                }
                return userPermCacheString;
            }
        }

        public async Task<List<JObject>> GetSetupAccessIds(string permsetId, bool retry)
        {
            var results = new List<JObject>();
            JArray resultsArray = new JArray();

            string cacheKey = "-setupaccess-" + permsetId;
            string setupAccessString = memoryCache.Get(cacheKey) as string;
            if (setupAccessString != null)
            {
                memoryCache.Set(cacheKey, setupAccessString, DateTimeOffset.Now.AddMinutes(3));
                resultsArray = JArray.Parse(setupAccessString);
            }
            else
            {
                string seaQuery = BuildSeaPermQuery(permsetId);
                var tResults = await Query(seaQuery, retry);
                resultsArray = new JArray(tResults);

                if (!FreeMemoryBelowThreshold())
                {
                    memoryCache.Set(cacheKey, resultsArray.ToString(), DateTimeOffset.Now.AddMinutes(3));
                }
            }
            RemoveCacheElementIfMemoryLow(cacheKey);

            foreach (var jsElement in resultsArray)
            {
                results.Add(jsElement as JObject);
            }
            return results;
        }

        private string BuildSeaPermQuery(string parentId)
        {
            var query = new StringBuilder("SELECT SetupEntityId FROM SetupEntityAccess WHERE ParentId");
            if (parentId.StartsWith(BaseCompare.PERMSET_ID_PREFIX))
            {
                query.Append("='").Append(parentId).Append("'");
            }
            else
            {
                query.Append(" IN (SELECT PermissionSetId from PermissionSetAssignment WHERE ");
                if (parentId.StartsWith(BaseCompare.USER_ID_PREFIX))
                {
                    query.Append("AssigneeId='");
                }
                else if (parentId.StartsWith(BaseCompare.PROFILE_ID_PREFIX))
                {
                    query.Append("ProfileId='");
                }
                else
                {
                    logger.LogWarning("Invalid parentId prefix.  ParentId: {0}", parentId);
                }
                query.Append(parentId).Append("')");
            }
            return query.ToString();
        }

        public async Task<List<JObject>> GetSetupEntityNames(bool isAppType, string apiName, List<string> idList, bool retry)
        {
            var results = new List<JObject>();
            var uncachedIds = new List<string>();

            logger.LogInformation("[{0}] Number of Ids in idList: {1}", apiName, idList.Count);

            string cacheKeyPrefix = "-setupentity-";
            foreach (var id in idList)
            {
                string cacheKey = cacheKeyPrefix + id;

                string setupEntityNameJson = memoryCache.Get(cacheKey) as string;
                if (setupEntityNameJson != null)
                {
                    results.Add(JObject.Parse(setupEntityNameJson));
                    memoryCache.Set(cacheKey, setupEntityNameJson, DateTimeOffset.Now.AddMinutes(3));
                }
                else
                {
                    uncachedIds.Add(id);
                }
            }

            if (uncachedIds.Any())
            {
                logger.LogInformation("[{0}] Number of uncached Ids: {1}", apiName, uncachedIds.Count);

                int BATCH_SIZE = 200;
                int numNamesToRetrieve = uncachedIds.Count;

                while (numNamesToRetrieve > 0)
                {
                    var namesToRetrieve = new List<string>();
                    if (numNamesToRetrieve < BATCH_SIZE)
                    {
                        namesToRetrieve.AddRange(uncachedIds);
                        uncachedIds.Clear();
                    }
                    else
                    {
                        namesToRetrieve.AddRange(uncachedIds.Take(BATCH_SIZE));
                        uncachedIds.RemoveRange(0, BATCH_SIZE);
                    }
                    numNamesToRetrieve = uncachedIds.Count;
                    string labelQuery = string.Format("SELECT Id, {0} FROM {1} WHERE Id IN ({2})", isAppType ? "Label" : "Name", apiName, BuildIdListString(namesToRetrieve));

                    var uncachedResults = await Query(labelQuery, retry);

                    foreach (var jsonElement in uncachedResults)
                    {
                        var jsonObj = jsonElement;
                        //if (!FreeMemoryBelowThreshold())
                        //{
                        //    MemoryCache.Default.Set(cacheKeyPrefix + jsonObj["Id"].ToString(), jsonObj.ToString(), DateTimeOffset.Now.AddMinutes(3));
                        //}
                        results.Add(jsonObj);
                    }
                }
            }

            return results;
        }

        private string BuildIdListString(List<string> idList)
        {
            var builder = new StringBuilder();
            if (!idList.Any())
            {
                return "''";
            }
            foreach (var id in idList)
            {
                builder.Append("'").Append(id).Append("',");
            }
            builder.Length--; // Remove the last comma
            return builder.ToString();
        }

        private void RemoveCacheElementIfMemoryLow(string cacheKey)
        {
            if (FreeMemoryBelowThreshold())
            {
                logger.LogError("Free Memory below threshold. Clearing cache element: {0}.", cacheKey);
                memoryCache.Remove(cacheKey);
            }
        }

        private bool FreeMemoryBelowThreshold()
        {
            long freeMemory = GC.GetTotalMemory(false) / (1024 * 1024);
            bool freeMemoryLessThanThreshold = freeMemory < MEM_THRESHOLD_MB;
            if (freeMemoryLessThanThreshold)
            {
                logger.LogWarning("Free Memory less than specified threshold.");
            }
            return freeMemoryLessThanThreshold;
        }
    }

}
