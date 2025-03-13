using Sollies.Rbac.Shared.Models;
using System.Text;
using System.Threading.Tasks;

namespace Sollies.Rbac.Shared.Interfaces
{
    public interface ICompareObjectPermissions
    {
        void AddObjPermResultsToJson(PermissionSet permset, StringBuilder jsonBuild, string permCategory, int i);
        Task AddPermsToPermset(PermissionSet permset, bool retry);
        void ClassifyObjectPerms(PermissionSet[] permsets);
        Task<string> CompareObjPerms(bool retry, params string[] ids);
    }
}