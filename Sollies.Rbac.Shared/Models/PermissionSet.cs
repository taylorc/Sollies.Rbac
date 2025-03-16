namespace Sollies.Rbac.Shared
{
    namespace Models
    {

        public class PermissionSet
        {
            private string id;
            private string name;



            public class SetupEntity
            {

                //    APEX_CLASS("01p", "ApexClass", "Apex Classes"),
                //TABSET("02u", "AppMenuItem", "Apps"),
                //CONN_APP("0H4", "AppMenuItem", "Connected Apps"),
                //APEX_PAGE("066", "ApexPage", "Visualforce Pages");

                // private readonly string prefix;
                // private readonly string apiFieldName;
                // private readonly string displayName;
                private readonly Dictionary<string, Tuple<string, string, string>> dictionary;

                //private SetupEntityTypes(string prefix, string apiFieldName, string displayName)
                //{
                //    this.prefix = prefix;
                //    this.apiFieldName = apiFieldName;
                //    this.displayName = displayName;
                //}

                public SetupEntity()
                {

                    dictionary = new Dictionary<string, Tuple<string, string, string>>
                    {
                        { "APEX_CLASS", new Tuple<string, string, string>("01p", "ApexClass", "Apex Classes") },
                        { "TABSET", new Tuple<string, string, string>("02u", "AppMenuItem", "Apps") },
                        { "CONN_APP", new Tuple<string, string, string>("0H4", "AppMenuItem", "Connected Apps") },
                        { "APEX_PAGE", new Tuple<string, string, string>("066", "ApexPage", "Visualforce Pages") }
                    };
                }

                // Tuple 1: Prefix, 2: API Field Name, 3: Display Name

                public string GetPrefix(SetupEntityTypes setupEntityTypes)
                {
                    return dictionary[setupEntityTypes.ToString()].Item1;
                }

                public string GetApiName(SetupEntityTypes setupEntityTypes)
                {
                    return dictionary[setupEntityTypes.ToString()].Item2;
                }

                public string GetDisplayName(SetupEntityTypes setupEntityTypes)
                {
                    return dictionary[setupEntityTypes.ToString()].Item3;
                }
            }

            private HashSet<string> userPerms;
            private HashSet<string> uniqueUserPerms;
            private HashSet<string> commonUserPerms;
            private HashSet<string> differenceUserPerms;

            private Dictionary<ObjPermCategory, Dictionary<string, HashSet<ObjectPermissions>>> objPermMap;
            private Dictionary<string, HashSet<ObjectPermissions>> emptyMap;

            public Dictionary<ObjPermCategory, Dictionary<SetupEntityTypes, HashSet<string>>> seaPermMap;
            private Dictionary<SetupEntityTypes, HashSet<string>> emptySeaMap;

            public PermissionSet() { }

            public PermissionSet(string permsetId)
            {
                id = permsetId;
                userPerms = new HashSet<string>();
                uniqueUserPerms = userPerms;
                commonUserPerms = userPerms;
                differenceUserPerms = userPerms;

                objPermMap = new Dictionary<ObjPermCategory, Dictionary<string, HashSet<ObjectPermissions>>>();
                emptyMap = new Dictionary<string, HashSet<ObjectPermissions>>();
                objPermMap[ObjPermCategory.Original] = emptyMap;
                objPermMap[ObjPermCategory.Unique] = emptyMap;
                objPermMap[ObjPermCategory.Common] = emptyMap;
                objPermMap[ObjPermCategory.Differing] = emptyMap;

                seaPermMap = new Dictionary<ObjPermCategory, Dictionary<SetupEntityTypes, HashSet<string>>>();
                emptySeaMap = new Dictionary<SetupEntityTypes, HashSet<string>>();
                foreach (SetupEntityTypes type in Enum.GetValues(typeof(SetupEntityTypes)))
                {
                    emptySeaMap[type] = new HashSet<string>();
                }
                seaPermMap[ObjPermCategory.Original] = new Dictionary<SetupEntityTypes, HashSet<string>>(emptySeaMap);
                seaPermMap[ObjPermCategory.Unique] = new Dictionary<SetupEntityTypes, HashSet<string>>(emptySeaMap);
                seaPermMap[ObjPermCategory.Common] = new Dictionary<SetupEntityTypes, HashSet<string>>(emptySeaMap);
                seaPermMap[ObjPermCategory.Differing] = new Dictionary<SetupEntityTypes, HashSet<string>>(emptySeaMap);
            }

            public string Id
            {
                get { return id; }
                set { id = value; }
            }

            public string Name
            {
                get { return name; }
                set { name = value; }
            }

            public HashSet<string> UserPerms
            {
                get { return userPerms; }
                set { userPerms = value; }
            }

            public HashSet<string> UniqueUserPerms
            {
                get { return uniqueUserPerms; }
                set { uniqueUserPerms = value; }
            }

            public HashSet<string> CommonUserPerms
            {
                get { return commonUserPerms; }
                set { commonUserPerms = value; }
            }

            public HashSet<string> DifferenceUserPerms
            {
                get { return differenceUserPerms; }
                set { differenceUserPerms = value; }
            }

            public Dictionary<string, HashSet<ObjectPermissions>> GetObjPermMap(ObjPermCategory category)
            {
                if (objPermMap.ContainsKey(category))
                {
                    return objPermMap[category];
                }
                else
                {
                    return null!;
                }
            }

            public Dictionary<SetupEntityTypes, HashSet<string>> GetSeaPermMap(ObjPermCategory category)
            {
                if (seaPermMap.ContainsKey(category))
                {
                    return seaPermMap[category];
                }
                else
                {
                    return null!;
                }
            }

            public void SetObjPermMap(ObjPermCategory category, Dictionary<string, HashSet<ObjectPermissions>> objPermMap)
            {
                this.objPermMap[category] = objPermMap;
            }

            public void SetSeaPermMap(ObjPermCategory category, Dictionary<SetupEntityTypes, HashSet<string>> seaPermMap)
            {
                this.seaPermMap[category] = seaPermMap;
            }
        }
    }
}
