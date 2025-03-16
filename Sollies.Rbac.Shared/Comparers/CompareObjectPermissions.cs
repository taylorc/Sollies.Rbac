namespace Sollies.Rbac.Shared.Comparers
{
    using Microsoft.Extensions.Logging;
    using Models;
    using Newtonsoft.Json.Linq;
    using Sollies.Rbac.Shared.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public class CompareObjectPermissions : BaseCompare, ICompareObjectPermissions
        {
            private readonly IRetrieveData retrieveData;
            private readonly ILogger<BaseCompare> logger;

            public CompareObjectPermissions(IRetrieveData retrieveData, ILogger<BaseCompare> logger) : base(retrieveData, logger)
            {
                this.retrieveData = retrieveData;
                this.logger = logger;
            }

            public async Task AddPermsToPermset(PermissionSet permset, bool retry)
            {
                var opm = await GetObjectPermsMap(permset.Id, retry);

                permset.SetObjPermMap(ObjPermCategory.Original, opm);
            }

            private string BuildObjPermQuery(string parentId)
            {
                var query = new StringBuilder("SELECT SobjectType,");
                AppendParamsToQuery(query, OBJECT_PERMS, null);
                query.Append(" FROM ObjectPermissions WHERE ParentId");
                if (parentId.StartsWith(PERMSET_ID_PREFIX))
                {
                    query.Append("='").Append(parentId).Append("'");
                }
                else
                {
                    query.Append(" IN (SELECT PermissionSetId from PermissionSetAssignment WHERE ");
                    if (parentId.StartsWith(USER_ID_PREFIX))
                    {
                        query.Append("AssigneeId='");
                    }
                    else if (parentId.StartsWith(PROFILE_ID_PREFIX))
                    {
                        query.Append("ProfileId='");
                    }
                    else
                    {
                        logger.LogWarning("Invalid parentId prefix.  ParentId: {0}", parentId);
                    }
                    query.Append(parentId).Append("')");
                }
                query.Append(" ORDER BY SobjectType ASC NULLS FIRST");
                return query.ToString();
            }

            protected async Task<Dictionary<string, HashSet<ObjectPermissions>>> GetObjectPermsMap(string parentId, bool retry)
            {
                string query = BuildObjPermQuery(parentId);
                var objPermMap = new Dictionary<string, HashSet<ObjectPermissions>>();
                List<JObject> objPermResults = await retrieveData.Query(query, retry);
                JArray objPermRows = null;
                if (objPermResults != null)
                {
                    objPermRows = new JArray(objPermResults);
                }
                else
                {
                    logger.LogWarning("ObjPermResults was null. Query: {0}", query);
                }

                foreach (var objRow in objPermRows)
                {
                    var row = objRow as JObject;
                    string objName = row["SobjectType"].ToString();
                    var objPerms = new HashSet<ObjectPermissions>();

                    foreach (ObjectPermissions objPerm in Enum.GetValues(typeof(ObjectPermissions)))
                    {
                        if (row[objPerm.ToString()].ToObject<bool>())
                        {
                            objPerms.Add(objPerm);
                        }
                    }

                    if (objPermMap.ContainsKey(objName))
                    {
                        objPerms.UnionWith(objPermMap[objName]);
                    }
                    objPermMap[objName] = objPerms;
                }
                return objPermMap;
            }

            public async Task<string> CompareObjPerms(bool retry, params string[] ids)
            {
                var permsets = await GetPermsetArray(retry, OBJECT_PERMS, ids);
                ClassifyObjectPerms(permsets);

                return GeneratePermsJson(permsets, OBJECT_PERMS);
            }

            public void ClassifyObjectPerms(PermissionSet[] permsets)
            {
                for (int i = 0; i < permsets.Length; i++)
                {
                    if (permsets[i] != null)
                    {
                        FindAndSetUniqueObjPerms(permsets, i);
                        FindAndSetCommonObjPerms(permsets, i);
                        FindAndSetDifferingObjPerms(permsets, i);
                    }
                }
            }

            private void FindAndSetUniqueObjPerms(PermissionSet[] permsets, int currentIndex)
            {
                var uniqueObjectPermMap = new Dictionary<string, HashSet<ObjectPermissions>>();
                CopyOriginalObjectMapIntoMap(uniqueObjectPermMap, permsets, currentIndex);
                var objectKeysToRemove = new HashSet<string>();

                for (int j = 0; j < permsets.Length; j++)
                {
                    if (permsets[j] != null && j != currentIndex)
                    {
                        var compPermsetMap = permsets[j].GetObjPermMap(ObjPermCategory.Original);
                        foreach (var objectName in uniqueObjectPermMap.Keys.ToList())
                        {
                            if (compPermsetMap.ContainsKey(objectName))
                            {
                                uniqueObjectPermMap[objectName].ExceptWith(compPermsetMap[objectName]);
                                if (!uniqueObjectPermMap[objectName].Any())
                                {
                                    objectKeysToRemove.Add(objectName);
                                }
                            }
                        }
                    }
                }
                foreach (var objectKeyToRemove in objectKeysToRemove)
                {
                    uniqueObjectPermMap.Remove(objectKeyToRemove);
                }
                permsets[currentIndex].SetObjPermMap(ObjPermCategory.Unique, uniqueObjectPermMap);
            }

            private void FindAndSetCommonObjPerms(PermissionSet[] permsets, int currentIndex)
            {
                var commonObjectPermMap = new Dictionary<string, HashSet<ObjectPermissions>>();
                var objKeysToRemove = new HashSet<string>();

                CopyOriginalObjectMapIntoMap(commonObjectPermMap, permsets, currentIndex);

                for (int j = 0; j < permsets.Length; j++)
                {
                    if (permsets[j] != null && j != currentIndex)
                    {
                        var compPermsetMap = permsets[j].GetObjPermMap(ObjPermCategory.Original);
                        foreach (var objectName in commonObjectPermMap.Keys.ToList())
                        {
                            if (compPermsetMap.ContainsKey(objectName))
                            {
                                commonObjectPermMap[objectName].IntersectWith(compPermsetMap[objectName]);
                                if (!commonObjectPermMap[objectName].Any())
                                {
                                    objKeysToRemove.Add(objectName);
                                }
                            }
                            else
                            {
                                commonObjectPermMap[objectName].Clear();
                                objKeysToRemove.Add(objectName);
                            }
                        }
                    }
                }

                foreach (var objKeyToRemove in objKeysToRemove)
                {
                    commonObjectPermMap.Remove(objKeyToRemove);
                }
                permsets[currentIndex].SetObjPermMap(ObjPermCategory.Common, commonObjectPermMap);
            }

            private void FindAndSetDifferingObjPerms(PermissionSet[] permsets, int currentIndex)
            {
                var differingObjPermMap = new Dictionary<string, HashSet<ObjectPermissions>>();
                CopyOriginalObjectMapIntoMap(differingObjPermMap, permsets, currentIndex);

                var commonObjPermMap = new Dictionary<string, HashSet<ObjectPermissions>>();
                CopyOriginalObjectMapIntoMap(commonObjPermMap, ObjPermCategory.Common, permsets, currentIndex);

                var objKeysToRemove = new HashSet<string>();

                foreach (var objectName in commonObjPermMap.Keys)
                {
                    if (!differingObjPermMap.ContainsKey(objectName))
                    {
                        objKeysToRemove.Add(objectName);
                    }
                    else
                    {
                        differingObjPermMap[objectName].ExceptWith(commonObjPermMap[objectName]);
                        if (!differingObjPermMap[objectName].Any())
                        {
                            objKeysToRemove.Add(objectName);
                        }
                    }
                }
                foreach (var objKeyToRemove in objKeysToRemove)
                {
                    differingObjPermMap.Remove(objKeyToRemove);
                }

                permsets[currentIndex].SetObjPermMap(ObjPermCategory.Differing, differingObjPermMap);
            }

            public void AddObjPermResultsToJson(PermissionSet permset, StringBuilder jsonBuild, string permCategory, int i)
            {
                var objPermMap = new Dictionary<string, HashSet<ObjectPermissions>>();
                var permsetRoot = new StringBuilder();
                permsetRoot.Append("permset").Append(i + 1).Append(OBJECT);

                if (permCategory.Equals(UNIQUE))
                {
                    objPermMap = permset.GetObjPermMap(ObjPermCategory.Unique);
                    permsetRoot.Append(UNIQUE);
                }
                else if (permCategory.Equals(COMMON))
                {
                    objPermMap = permset.GetObjPermMap(ObjPermCategory.Common);
                    permsetRoot.Append(COMMON);
                }
                else if (permCategory.Equals(DIFFERENCES))
                {
                    objPermMap = permset.GetObjPermMap(ObjPermCategory.Differing);
                    permsetRoot.Append(DIFFERENCES);
                }

                string permsetLabel = permsetRoot.ToString();
                jsonBuild.Append(", \"").Append(permsetLabel).Append("\": [");

                var alphabeticalKeySet = new SortedSet<string>(objPermMap.Keys);
                if (alphabeticalKeySet.Any())
                {
                    jsonBuild.Append("{ \"success\": \"true\", \"text\": \"Objects\", \"").Append(permsetLabel).Append("\": [ ");
                }

                foreach (var objName in alphabeticalKeySet)
                {
                    jsonBuild.Append("{\"success\": \"true\", \"text\": \"").Append(objName).Append("\", \"").Append(permsetLabel).Append("\": [");

                    foreach (var objPerm in objPermMap[objName])
                    {
                        jsonBuild.Append("{\"success\": \"true\", \"text\": \"").Append(objPerm.ToString().Substring(11)).Append("\", \"leaf\":\"true\", \"icon\":\"../../resources/themes/images/default/tree/checkMark.png\", \"loaded\":\"true\" }");
                        if (!objPerm.Equals(objPermMap[objName].Last()))
                        {
                            jsonBuild.Append(", ");
                        }
                    }
                    jsonBuild.Append("], \"leaf\":\"false\", \"expanded\":\"true\", \"loaded\":\"true\"}");
                    if (!objName.Equals(alphabeticalKeySet.Last()))
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

            protected void CopyOriginalObjectMapIntoMap(Dictionary<string, HashSet<ObjectPermissions>> map, PermissionSet[] permsets, int currentIndex)
            {
                CopyOriginalObjectMapIntoMap(map, ObjPermCategory.Original, permsets, currentIndex);
            }

            private void CopyOriginalObjectMapIntoMap(Dictionary<string, HashSet<ObjectPermissions>> map, ObjPermCategory category, PermissionSet[] permsets, int currentIndex)
            {
                var originalMap = permsets[currentIndex].GetObjPermMap(category);
                foreach (var key in originalMap.Keys)
                {
                    map[key] = new HashSet<ObjectPermissions>(originalMap[key]);
                }
            }
        }

}
