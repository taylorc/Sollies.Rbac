using Microsoft.Extensions.Configuration;
using NetCoreForce.Client;
using Newtonsoft.Json.Linq;
using Sollies.Rbac.Shared.Interfaces;
using System.Threading.Tasks;

namespace Sollies.Rbac.Shared.Factories
{
    public class ForceClientFactory : IForceClientFactory
    {
        private string Username { get; }
        private string Password { get; }

        private string Token { get; }

        private string ClientId { get; }
        private string ClientSecret { get; }


        private string LoginEndpoint { get; }
        private string ApiEndpoint { get; }

        public ForceClientFactory(IConfiguration configuration)
        {
            Username = configuration.GetSection("Salesforce")["Username"];
            Password = configuration.GetSection("Salesforce")["Password"];
            Token = configuration.GetSection("Salesforce")["Token"];
            ClientSecret = configuration.GetSection("Salesforce")["ClientSecret"];
            ClientId = configuration.GetSection("Salesforce")["ClientId"];
            LoginEndpoint = configuration.GetSection("Salesforce")["LoginEndpoint"];
            ApiEndpoint = configuration.GetSection("Salesforce")["ApiEndpoint"];

        }

        public async Task<ForceClient> Build()
        {
            AuthenticationClient auth = new AuthenticationClient();

            //Pass in the login information
            await auth.UsernamePasswordAsync(ClientId, ClientSecret, Username, $"{Password}{Token}", LoginEndpoint);

            //the AuthenticationClient object will then contain the instance URL and access token to be used in each of the API calls
            ForceClient client = new ForceClient(auth.AccessInfo.InstanceUrl, auth.ApiVersion, auth.AccessInfo.AccessToken);

            return client;
        }
    }
}
