namespace Sollies.Rbac.Shared.Comparers
{
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json.Linq;
    using Sollies.Rbac.Shared.Models;
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Sollies.Rbac.Shared.Interfaces;
    using Sollies.Rbac.Shared;

    public class BaseCompare
        {
            protected readonly string USER = "_User";
            protected readonly string OBJECT = "_Object";
            protected readonly string SEA = "_Sea";

            protected readonly string UNIQUE = "_Unique";
            protected readonly string COMMON = "_Common";
            protected readonly string DIFFERENCES = "_Differences";

            public static readonly string USER_ID_PREFIX = "005";
            public static readonly string PROFILE_ID_PREFIX = "00e";
            public static readonly string PERMSET_ID_PREFIX = "0PS";

            public static readonly string USER_PERMS = "UserPerms";
            public static readonly string OBJECT_PERMS = "ObjectPerms";
            public static readonly string SETUP_ENTITY_PERMS = "SetupEntityPerms";
            private readonly IRetrieveData retrieveData;
            private readonly ILogger<BaseCompare> logger;

            public BaseCompare(IRetrieveData retrieveData, ILogger<BaseCompare> logger)
            {
                this.retrieveData = retrieveData;
                this.logger = logger;
            }

            public void AppendParamsToQuery(StringBuilder queryBuild, string permCategory, HashSet<string> userPerms)
            {
                string[] perms = new string[] { };
                if (permCategory.Equals(USER_PERMS))
                {
                    if (userPerms != null)
                    {
                        perms = userPerms.ToArray();
                    }
                }
                else if (permCategory.Equals(OBJECT_PERMS))
                {
                    var enumValues = Enum.GetValues(typeof(ObjectPermissions)).Cast<ObjectPermissions>();
                    perms = enumValues.Select(e => e.ToString()).ToArray();
                }

                for (int i = 0; i < perms.Length; i++)
                {
                    queryBuild.Append(perms[i]);
                    if (i < perms.Length - 1)
                        queryBuild.Append(", ");
                }
            }

            public async Task<PermissionSet> GetPermissionSet(string permsetId, string compareType, bool retry)
            {
                var permset = new PermissionSet(permsetId);

                if (USER_PERMS.Equals(compareType))
                {
                    await new CompareUserPermissions(retrieveData, logger).AddPermsToPermset(permset, retry);
                }
                else if (OBJECT_PERMS.Equals(compareType))
                {
                    await new CompareObjectPermissions(retrieveData, logger).AddPermsToPermset(permset, retry);
                }
                else if (SETUP_ENTITY_PERMS.Equals(compareType))
                {
                    await new CompareSetupEntityPermissions(retrieveData, logger).AddPermsToPermset(permset, retry);
                }

                return permset;
            }

            public async Task<ImmutableHashSet<string>> GetUserPermsetIds(string userId, bool retry)
            {
                var idsBuilder = ImmutableHashSet.CreateBuilder<string>();

                string query = $"SELECT PermissionSet.Id FROM PermissionSetAssignment WHERE AssigneeId='{userId}'";
                var permsetInfo = await retrieveData.Query(query, retry);
                if (permsetInfo.Count == 0)
                {
                    logger.LogWarning("No permsets assigned to user found - should have >= 1");
                }

                foreach (var returnedPermset in permsetInfo)
                {
                    idsBuilder.Add(returnedPermset["PermissionSet"]["Id"].ToString());
                }

                return idsBuilder.ToImmutable();
            }

            public async Task<PermissionSet[]> GetPermsetArray(bool retry, string compareType, params string[] ids)
            {
                int numberOfIds = ids.Length;
                var permsets = new PermissionSet[numberOfIds];

                for (int i = 0; i < numberOfIds; i++)
                {
                    string id = ids[i];
                    if (id.Contains("blank"))
                    {
                        permsets[i] = null;
                    }
                    else
                    {
                        if (id.StartsWith(USER_ID_PREFIX))
                        {
                            permsets[i] = await GetEffectiveUserPermset(id, compareType);
                        }
                        else
                        {
                            permsets[i] = await GetPermissionSet(id, compareType, retry);
                        }
                    }
                }
               return permsets;
            }

            private async Task<PermissionSet> GetEffectiveUserPermset(string userId, string compareType)
            {
                var permsetSetBuilder = ImmutableHashSet.CreateBuilder<PermissionSet>();
                bool retry = true;

                if (OBJECT_PERMS.Equals(compareType))
                {
                    return await GetPermissionSet(userId, compareType, retry);
                }
                else
                {
                    var permsetIds = await GetUserPermsetIds(userId, retry);
                    foreach (var permsetId in permsetIds)
                    {
                        permsetSetBuilder.Add(await GetPermissionSet(permsetId, compareType, retry));
                    }
                    return AggregatePermissionSets(permsetSetBuilder.ToImmutable(), compareType);
                }
            }

            public PermissionSet AggregatePermissionSets(ImmutableHashSet<PermissionSet> permsets, string compareType)
            {
                var permset = new PermissionSet("aggregatePermset_FakeId");

                var aggregateUserPerms = new HashSet<string>();

                foreach (var tempPermset in permsets)
                {
                    if (USER_PERMS.Equals(compareType))
                    {
                        foreach (var item in tempPermset.UserPerms)
                        {
                            aggregateUserPerms.Add(item);
                        }
                    }
                    else if (SETUP_ENTITY_PERMS.Equals(compareType))
                    {
                        foreach (var type in Enum.GetValues(typeof(SetupEntityTypes)).Cast<SetupEntityTypes>())
                        {
                            foreach (var item in tempPermset.seaPermMap[ObjPermCategory.Original][type])
                            {
                                permset.seaPermMap[ObjPermCategory.Original][type].Add(item);
                            }
                            
                        }
                    }
                }

                if (USER_PERMS.Equals(compareType))
                {
                    permset.UserPerms = aggregateUserPerms;
                }

                return permset;
            }

            protected string GeneratePermsJson(PermissionSet[] permsets, string permType)
            {
                int numberOfPermsets = permsets.Length;

                var jsonBuild = new StringBuilder();
                jsonBuild.Append("{\"numberOfPermsets\": ").Append(numberOfPermsets);

                for (int i = 0; i < numberOfPermsets; i++)
                {
                    if (permsets[i] != null)
                    {
                        if (permType.Contains(USER_PERMS))
                        {
                            var CompareUserPerms = new CompareUserPermissions(retrieveData, logger);
                            CompareUserPerms.AddUserPermResultsToJson(permsets[i], jsonBuild, UNIQUE, i);
                            CompareUserPerms.AddUserPermResultsToJson(permsets[i], jsonBuild, COMMON, i);
                            CompareUserPerms.AddUserPermResultsToJson(permsets[i], jsonBuild, DIFFERENCES, i);
                        }
                        else if (permType.Contains(OBJECT_PERMS))
                        {
                            var CompareObjectPerms = new CompareObjectPermissions(retrieveData, logger);
                            CompareObjectPerms.AddObjPermResultsToJson(permsets[i], jsonBuild, UNIQUE, i);
                            CompareObjectPerms.AddObjPermResultsToJson(permsets[i], jsonBuild, COMMON, i);
                            CompareObjectPerms.AddObjPermResultsToJson(permsets[i], jsonBuild, DIFFERENCES, i);
                        }
                        else if (permType.Contains(SETUP_ENTITY_PERMS))
                        {
                            var CompareSetupEntityPerms = new CompareSetupEntityPermissions(retrieveData, logger);
                            CompareSetupEntityPerms.AddSEAPermResultsToJson(permsets[i], jsonBuild, UNIQUE, i);
                            CompareSetupEntityPerms.AddSEAPermResultsToJson(permsets[i], jsonBuild, COMMON, i);
                            CompareSetupEntityPerms.AddSEAPermResultsToJson(permsets[i], jsonBuild, DIFFERENCES, i);
                        }
                    }
                }
                jsonBuild.Append(" }");
                return jsonBuild.ToString();
            }
        }
  
}
