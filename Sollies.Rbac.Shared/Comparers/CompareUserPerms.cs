namespace Sollies.Rbac.Shared.Comparers
{
    using Microsoft.Extensions.Logging;
    using Models;
    using NetCoreForce.Client.Models;
    using Newtonsoft.Json.Linq;
    using Sollies.Rbac.Shared.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public class CompareUserPermissions : BaseCompare, ICompareUserPermissions
    {
        private readonly IRetrieveData retrieveData;
        private readonly ILogger<BaseCompare> logger;

        public CompareUserPermissions(IRetrieveData retrieveData, ILogger<BaseCompare> logger) : base(retrieveData, logger)
        {
            this.retrieveData = retrieveData;
            this.logger = logger;
        }
        public async Task<string> CompareUserPerms(bool retry, params string[] ids)
        {
            var permsets = await GetPermsetArray(retry, USER_PERMS, ids);
            ClassifyUserPerms(permsets);

            return GeneratePermsJson(permsets, USER_PERMS);
        }

        public async Task<HashSet<string>> RetrieveValidUserPerms()
        {
            var permsetData = await retrieveData.GetUserPerms(true);

            if (permsetData == null)
            {
                logger.LogError("No data retrieved when fetching user perms");
            }

            if (permsetData != null)
            {
                int validUserPermsLength = permsetData.Count;
                var userPerms = new HashSet<string>();

                foreach (SObjectFieldMetadata item in permsetData)
                {
                    string permName = item.Name;
                    if (permName.StartsWith("Permissions"))
                    {
                        userPerms.Add(permName);
                    }
                }
                for (int i = 0; i < validUserPermsLength; i++)
                {
                    string permName = permsetData[i].Name;
                    if (permName.StartsWith("Permissions"))
                    {
                        userPerms.Add(permName);
                    }
                }
                return userPerms;
            }
            else
            {
                logger.LogWarning("PermsetFields was null");
                return null;
            }
        }

        public async Task AddPermsToPermset(PermissionSet permset, bool retry)
        {
            var userPerms = await RetrieveValidUserPerms();

            List<UserPermissionsDto> permsetInfo = await retrieveData.GetPermsetUserPerms(permset.Id, userPerms, retry);

            if (permsetInfo == null)
            {
                logger.LogWarning("PermsetInfo is null after query in getPermissionSet.");
                return;
            }

            foreach (var perm in userPerms)
            {
                if (perm == null)
                    continue;

                if (permsetInfo.GetType().GetProperty(perm) != null)
                {
                    if (permsetInfo.GetType().GetProperty(perm).GetValue(permsetInfo).ToString().Equals("true", StringComparison.Ordinal))
                    {
                        permset.UserPerms.Add(perm);
                    }
                }
                //if (permsetInfo.Contains(perm))
                //{
                //    if (permsetInfo[perm].toobject<bool>())
                //    {
                //        permset.permsetInfo.add(perm);
                //    }
                //}
                //if (permsetInfo.Contains(perm))
                //{
                //    if (permsetInfo[perm].toobject<bool>())
                //    {
                //        permset.userperms.add(perm);
                //    }
                //}
            }
        }

        public void ClassifyUserPerms(PermissionSet[] permsets)
        {
            int numberOfPermsets = permsets.Length;

            for (int i = 0; i < numberOfPermsets; i++)
            {
                if (permsets[i] != null)
                {
                    var uniquePerms = new HashSet<string>(permsets[i].UserPerms);
                    var commonPerms = new HashSet<string>(permsets[i].UserPerms);

                    for (int j = 0; j < numberOfPermsets; j++)
                    {
                        if (permsets[j] != null && j != i)
                        {
                            uniquePerms.ExceptWith(permsets[j].UserPerms);
                            commonPerms.IntersectWith(permsets[j].UserPerms);
                        }
                    }
                    permsets[i].UniqueUserPerms = uniquePerms;
                    permsets[i].CommonUserPerms = commonPerms;

                    var differencePerms = new HashSet<string>(permsets[i].UserPerms);
                    differencePerms.ExceptWith(commonPerms);
                    permsets[i].DifferenceUserPerms = differencePerms;
                }
            }
        }

        public void AddUserPermResultsToJson(PermissionSet permset, StringBuilder jsonBuild, string permCategory, int i)
        {
            jsonBuild.Append(", \"permset").Append(i + 1);

            jsonBuild.Append(USER);

            HashSet<string> userPerms = new HashSet<string>();
            if (permCategory.Equals(UNIQUE))
            {
                userPerms = permset.UniqueUserPerms;
                jsonBuild.Append(UNIQUE);
            }
            else if (permCategory.Equals(COMMON))
            {
                userPerms = permset.CommonUserPerms;
                jsonBuild.Append(COMMON);
            }
            else if (permCategory.Equals(DIFFERENCES))
            {
                userPerms = permset.DifferenceUserPerms;
                jsonBuild.Append(DIFFERENCES);
            }

            jsonBuild.Append("\": [");

            var sortedPerms = new SortedSet<string>(userPerms);

            int endOfPermissionsIndex = 11;
            foreach (var perm in sortedPerms)
            {
                jsonBuild.Append("{\"name\": \"");

                var permLabelSubstrings = perm.Substring(endOfPermissionsIndex).Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var substring in permLabelSubstrings)
                {
                    jsonBuild.Append(substring).Append(" ");
                }
                jsonBuild.Append("\", \"enabled\": true }");
                if (!perm.Equals(sortedPerms.Last()))
                {
                    jsonBuild.Append(", ");
                }
            }
            jsonBuild.Append("]");
        }
    }

}
