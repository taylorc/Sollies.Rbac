using Sollies.Rbac.Shared.Models;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sollies.Rbac.Shared.Interfaces
{
    public interface ICompareUserPermissions
    {
        Task AddPermsToPermset(PermissionSet permset, bool retry);
        void AddUserPermResultsToJson(PermissionSet permset, StringBuilder jsonBuild, string permCategory, int i);
        void ClassifyUserPerms(PermissionSet[] permsets);
        Task<string> CompareUserPerms(bool retry, params string[] ids);
        Task<HashSet<string>> RetrieveValidUserPerms();
    }
}