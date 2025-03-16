using Microsoft.Extensions.Logging;
using Sollies.Rbac.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Sollies.Rbac.Shared.Models.PermissionSet;
using Sollies.Rbac.Shared.Interfaces;

namespace Sollies.Rbac.Shared.Comparers
{
        public class CompareSetupEntityPermissions : BaseCompare, ICompareSetupEntityPermissions
        {
            private readonly IRetrieveData retrieveData;
            private readonly ILogger<BaseCompare> logger;
            private readonly SetupEntity entity;

            public CompareSetupEntityPermissions(IRetrieveData retrieveData, ILogger<BaseCompare> logger) : base(retrieveData, logger)
            {
                this.retrieveData = retrieveData;
                this.logger = logger;
                entity = new SetupEntity();
            }
            public async Task<string> CompareSetupEntityPerms(bool retry, params string[] ids)
            {
                var permsets = await GetPermsetArray(retry, SETUP_ENTITY_PERMS, ids);
                ClassifySetupEntityPerms(permsets);

                return GeneratePermsJson(permsets, SETUP_ENTITY_PERMS);
            }

            public async Task AddPermsToPermset(PermissionSet permset, bool retry)
            {
                var seaInfo = await retrieveData.GetSetupAccessIds(permset.Id, retry);
                if (seaInfo == null)
                {
                    logger.LogWarning("PermsetInfo is null after trying to get SetupEntityAccess Ids.");
                }

                var typeToEntityMap = new Dictionary<string, List<string>>();

                foreach (SetupEntityTypes type in Enum.GetValues(typeof(SetupEntityTypes)))
                {
                    typeToEntityMap[entity.GetPrefix(type)] = new List<string>();
                }

                foreach (var sea in seaInfo)
                {
                    string id = sea["SetupEntityId"].ToString();
                    string prefix = id.Substring(0, 3);
                    if (typeToEntityMap.ContainsKey(prefix))
                    {
                        typeToEntityMap[prefix].Add(id);
                    }
                }

                foreach (SetupEntityTypes type in Enum.GetValues(typeof(SetupEntityTypes)))
                {
                    bool isAppType = type == SetupEntityTypes.CONN_APP || type == SetupEntityTypes.TABSET;
                    var idList = typeToEntityMap[entity.GetPrefix(type)];
                    var apiName = entity.GetApiName(type);
                    var results = await retrieveData.GetSetupEntityNames(isAppType, apiName, idList, retry);
                    if (results == null)
                    {
                        logger.LogWarning("Type: {0} skipped", apiName);
                        continue;
                    }
                    foreach (var result in results)
                    {
                        permset.GetSeaPermMap(ObjPermCategory.Original)[type].Add(result[isAppType ? "Label" : "Name"].ToString());
                    }
                }
            }

            public void ClassifySetupEntityPerms(PermissionSet[] permsets)
            {
                int numberOfPermsets = permsets.Length;

                for (int i = 0; i < numberOfPermsets; i++)
                {
                    if (permsets[i] != null)
                    {
                        var origMap = new Dictionary<SetupEntityTypes, HashSet<string>>(permsets[i].GetSeaPermMap(ObjPermCategory.Original));
                        var uniquePerms = GetNewHashMap(origMap);
                        var commonPerms = GetNewHashMap(origMap);

                        var uniqueKeysToRemove = new HashSet<SetupEntityTypes>();
                        var commonKeysToRemove = new HashSet<SetupEntityTypes>();

                        for (int j = 0; j < numberOfPermsets; j++)
                        {
                            if (permsets[j] != null && j != i)
                            {
                                var compPermsetMap = permsets[j].GetSeaPermMap(ObjPermCategory.Original);

                                foreach (SetupEntityTypes type in Enum.GetValues(typeof(SetupEntityTypes)))
                                {
                                    if (compPermsetMap.ContainsKey(type))
                                    {
                                        if (uniquePerms.ContainsKey(type))
                                        {
                                            uniquePerms[type].ExceptWith(compPermsetMap[type]);
                                            if (!uniquePerms[type].Any())
                                            {
                                                uniqueKeysToRemove.Add(type);
                                            }
                                        }
                                    }
                                    if (commonPerms.ContainsKey(type) && compPermsetMap.ContainsKey(type))
                                    {
                                        commonPerms[type].IntersectWith(compPermsetMap[type]);
                                        if (!commonPerms[type].Any())
                                        {
                                            commonKeysToRemove.Add(type);
                                        }
                                    }
                                    else
                                    {
                                        commonKeysToRemove.Add(type);
                                    }
                                }
                            }
                        }
                        RemoveKeys(uniquePerms, uniqueKeysToRemove);
                        permsets[i].SetSeaPermMap(ObjPermCategory.Unique, uniquePerms);

                        RemoveKeys(commonPerms, commonKeysToRemove);
                        permsets[i].SetSeaPermMap(ObjPermCategory.Common, commonPerms);

                        var differencePerms = GetNewHashMap(origMap);
                        var keysToRemove = new HashSet<SetupEntityTypes>();
                        foreach (var type in commonPerms.Keys)
                        {
                            if (differencePerms.ContainsKey(type))
                            {
                                differencePerms[type].ExceptWith(commonPerms[type]);
                                if (!differencePerms[type].Any())
                                {
                                    keysToRemove.Add(type);
                                }
                            }
                        }
                        RemoveKeys(differencePerms, keysToRemove);
                        permsets[i].SetSeaPermMap(ObjPermCategory.Differing, differencePerms);
                    }
                }
            }

            private static void RemoveKeys(Dictionary<SetupEntityTypes, HashSet<string>> map, HashSet<SetupEntityTypes> typesToRemove)
            {
                foreach (var type in typesToRemove)
                {
                    map.Remove(type);
                }
            }

            private static Dictionary<SetupEntityTypes, HashSet<string>> GetNewHashMap(Dictionary<SetupEntityTypes, HashSet<string>> mapToCopy)
            {
                var newMap = new Dictionary<SetupEntityTypes, HashSet<string>>();
                foreach (var key in mapToCopy.Keys)
                {
                    newMap[key] = new HashSet<string>(mapToCopy[key]);
                }
                return newMap;
            }

            public void AddSEAPermResultsToJson(PermissionSet permset, StringBuilder jsonBuild, string permCategory, int i)
            {
                jsonBuild.Append(", \"permset").Append(i + 1).Append(SEA);

                Dictionary<SetupEntityTypes, HashSet<string>> seaPermMap = new Dictionary<SetupEntityTypes, HashSet<string>>();

                if (permCategory.Equals(UNIQUE))
                {
                    seaPermMap = permset.GetSeaPermMap(ObjPermCategory.Unique);
                    jsonBuild.Append(UNIQUE);
                }
                else if (permCategory.Equals(COMMON))
                {
                    seaPermMap = permset.GetSeaPermMap(ObjPermCategory.Common);
                    jsonBuild.Append(COMMON);
                }
                else if (permCategory.Equals(DIFFERENCES))
                {
                    seaPermMap = permset.GetSeaPermMap(ObjPermCategory.Differing);
                    jsonBuild.Append(DIFFERENCES);
                }

                jsonBuild.Append("\": [");

                var alphabeticalKeySet = new SortedSet<SetupEntityTypes>(seaPermMap.Keys);

                if (alphabeticalKeySet.Any())
                {
                    jsonBuild.Append("{ \"success\": \"true\", \"text\": \"Setup Entities\", \"").Append("permset").Append(i + 1).Append(SEA).Append(permCategory).Append("\": [ ");
                }

                foreach (var seaName in alphabeticalKeySet)
                {
                    jsonBuild.Append("{\"success\": \"true\", \"text\": \"").Append(entity.GetDisplayName(seaName)).Append("\", \"").Append("permset").Append(i + 1).Append(SEA).Append(permCategory).Append("\": [");

                    var alphabeticalEntitySet = new SortedSet<string>(seaPermMap[seaName]);

                    foreach (var entity in alphabeticalEntitySet)
                    {
                        jsonBuild.Append("{\"success\": \"true\", \"text\": \"").Append(entity).Append("\", \"leaf\":\"true\", \"icon\":\"../../resources/themes/images/default/tree/checkMark.png\", \"loaded\":\"true\" }");
                        if (!entity.Equals(alphabeticalEntitySet.Last()))
                        {
                            jsonBuild.Append(", ");
                        }
                    }
                    jsonBuild.Append("], \"leaf\":\"false\", \"expanded\":\"true\", \"loaded\":\"true\"}");
                    if (!seaName.Equals(alphabeticalKeySet.Last()))
                    {
                        jsonBuild.Append(", ");
                    }
                    else
                    {
                        jsonBuild.Append(" ], \"leaf\":\"false\", \"expanded\":\"true\", \"loaded\":\"true\"}");
                    }
                }
                jsonBuild.Append("]");
            }
        }
   
}
