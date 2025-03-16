using NetCoreForce.Client.Models;
using Newtonsoft.Json.Linq;
using Sollies.Rbac.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sollies.Rbac.Shared.Interfaces
{
    public interface IRetrieveData
    {
        Task<List<JObject>> GetItems(string itemType, string search, int queryLimit, bool retry);
        Task<List<UserPermissionsDto>> GetPermsetUserPerms(string permsetId, HashSet<string> userPerms, bool retry);
        Task<List<JObject>> GetSetupAccessIds(string permsetId, bool retry);
        Task<List<JObject>> GetSetupEntityNames(bool isAppType, string apiName, List<string> idList, bool retry);
        Task<List<SObjectFieldMetadata>> GetUserPerms(bool retry);
        Task<List<JObject>> Query(string query, bool retry);
    }
}