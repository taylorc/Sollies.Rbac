using Sollies.Rbac.Shared.Models;
using System.Text;
using System.Threading.Tasks;

namespace Sollies.Rbac.Shared.Interfaces
{
    public interface ICompareSetupEntityPermissions
    {
        abstract void ClassifySetupEntityPerms(PermissionSet[] permsets);
        Task AddPermsToPermset(PermissionSet permset, bool retry);
        void AddSEAPermResultsToJson(PermissionSet permset, StringBuilder jsonBuild, string permCategory, int i);
        Task<string> CompareSetupEntityPerms(bool retry, params string[] ids);
    }
}