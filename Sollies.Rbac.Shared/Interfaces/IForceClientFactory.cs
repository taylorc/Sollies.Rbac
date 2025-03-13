using NetCoreForce.Client;
using System.Threading.Tasks;

namespace Sollies.Rbac.Shared.Interfaces
{
    public interface IForceClientFactory
    {
        Task<ForceClient> Build();
    }
}