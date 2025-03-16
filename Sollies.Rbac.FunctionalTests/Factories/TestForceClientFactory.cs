using Microsoft.Extensions.Configuration;
using NetCoreForce.Client;
using Sollies.Rbac.Shared.Interfaces;

namespace Sollies.Rbac.FunctionalTests.Factories
{
    public class TestForceClientFactory: IForceClientFactory
    {

        private string Username { get; }
        private string Password { get; }

        private string Token { get; }

        private string ClientId { get; }
        private string ClientSecret { get; }


        private string LoginEndpoint { get; }
        private string ApiEndpoint { get; }

        public TestForceClientFactory()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddUserSecrets<TestForceClientFactory>();



            var configuration = builder.Build();
            Username = configuration["SFUsername"];
            Password = configuration["SFPassword"];
            Token = configuration["SFToken"];
            ClientSecret = configuration["SFClientSecret"];
            ClientId = configuration["SFClientId"];
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
