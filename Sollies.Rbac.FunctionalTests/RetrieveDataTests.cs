using Castle.Core.Logging;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.Routing.AutoValues;
using Sollies.Rbac.FunctionalTests.Factories;
using Sollies.Rbac.Shared.Comparers;
using Sollies.Rbac.Shared.DataAccess;
using Sollies.Rbac.Shared.Factories;
using Sollies.Rbac.Shared.Interfaces;

namespace Sollies.Rbac.FunctionalTests
{
    public class CompareUserPermsTests
    {
        [Fact]
        public async Task CompareUserPerms_Works_As_Expected()
        {
            var configuration = Substitute.For<IConfiguration>();
            var logger = Substitute.For<ILogger<BaseCompare>>();
            var memoryCache = Substitute.For<IMemoryCache>();

            IForceClientFactory forceClientFactory = new TestForceClientFactory();
            IRetrieveData retrieveData = new RetrieveData(forceClientFactory, logger, memoryCache);
            ICompareUserPermissions compareUserPermissions = new CompareUserPermissions(retrieveData, logger);

            //compare Aaron 0052j000000WkzFAAS and Alicia 0052j000000UJw0AAG

            var result = await compareUserPermissions.CompareUserPerms(true, "0052j000000WkzFAAS", "0052j000000UJw0AAG");

            Console.WriteLine(result);
        }

        [Fact]
        public async Task CompareObjectPerms_Works_As_Expected()
        {
            var configuration = Substitute.For<IConfiguration>();
            var logger = Substitute.For<ILogger<BaseCompare>>();
            var memoryCache = Substitute.For<IMemoryCache>();

            IForceClientFactory forceClientFactory = new TestForceClientFactory();
            IRetrieveData retrieveData = new RetrieveData(forceClientFactory, logger, memoryCache);
            ICompareObjectPermissions compareUserPermissions = new CompareObjectPermissions(retrieveData, logger);

            //compare Aaron 0052j000000WkzFAAS and Alicia 0052j000000UJw0AAG

            var result = await compareUserPermissions.CompareObjPerms(true, "0052j000000WkzFAAS", "0052j000000UJw0AAG");

            Console.WriteLine(result);
        }

        [Fact]
        public async Task CompareSetupEntityPerms_Works_As_Expected()
        {
            var configuration = Substitute.For<IConfiguration>();
            var logger = Substitute.For<ILogger<BaseCompare>>();
            var memoryCache = Substitute.For<IMemoryCache>();

            IForceClientFactory forceClientFactory = new TestForceClientFactory();
            IRetrieveData retrieveData = new RetrieveData(forceClientFactory, logger, memoryCache);
            ICompareSetupEntityPermissions compareUserPermissions = new CompareSetupEntityPermissions(retrieveData, logger);

            //compare Aaron 0052j000000WkzFAAS and Alicia 0052j000000UJw0AAG

            var result = await compareUserPermissions.CompareSetupEntityPerms(true, "0052j000000WkzFAAS", "0052j000000UJw0AAG");

            Console.WriteLine(result);
        }
    }
}