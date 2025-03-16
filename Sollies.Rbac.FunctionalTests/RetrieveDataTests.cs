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

            //compare Aaron and Alicia

            //Aaron userperms - {"numberOfPermsets": 4, "permset1_User_Unique": [{"name": "Activate Contract ", "enabled": true }, {"name": "Activate Order ", "enabled": true }, {"name": "Allow Universal Search ", "enabled": true }, {"name": "Api Enabled ", "enabled": true }, {"name": "Author Apex ", "enabled": true }, {"name": "Can Insert Feed System Fields ", "enabled": true }, {"name": "Can Use New Dashboard Builder ", "enabled": true }, {"name": "Chatter File Link ", "enabled": true }, {"name": "Chatter Internal User ", "enabled": true }, {"name": "Chatter Invite External Users ", "enabled": true }, {"name": "Chatter Own Groups ", "enabled": true }, {"name": "Connect Org To Environment Hub ", "enabled": true }, {"name": "Content Administrator ", "enabled": true }, {"name": "Convert Leads ", "enabled": true }, {"name": "Create Packaging ", "enabled": true }, {"name": "Create Workspaces ", "enabled": true }, {"name": "Customize Application ", "enabled": true }, {"name": "Data Export ", "enabled": true }, {"name": "Delete Activated Contract ", "enabled": true }, {"name": "Distribute From Pers Wksp ", "enabled": true }, {"name": "Edit Activated Orders ", "enabled": true }, {"name": "Edit Billing Info ", "enabled": true }, {"name": "Edit Brand Templates ", "enabled": true }, {"name": "Edit Case Comments ", "enabled": true }, {"name": "Edit Event ", "enabled": true }, {"name": "Edit Html Templates ", "enabled": true }, {"name": "Edit Knowledge ", "enabled": true }, {"name": "Edit Opp Line Item Unit Price ", "enabled": true }, {"name": "Edit Public Documents ", "enabled": true }, {"name": "Edit Public Templates ", "enabled": true }, {"name": "Edit Readonly Fields ", "enabled": true }, {"name": "Edit Task ", "enabled": true }, {"name": "Email Administration ", "enabled": true }, {"name": "Email Mass ", "enabled": true }, {"name": "Email Single ", "enabled": true }, {"name": "Email Template Management ", "enabled": true }, {"name": "Enable Notifications ", "enabled": true }, {"name": "Export Report ", "enabled": true }, {"name": "Flow U F L Required ", "enabled": true }, {"name": "Import Leads ", "enabled": true }, {"name": "Import Personal ", "enabled": true }, {"name": "Inbound Migration Tools User ", "enabled": true }, {"name": "Install Packaging ", "enabled": true }, {"name": "Manage Analytic Snapshots ", "enabled": true }, {"name": "Manage Auth Providers ", "enabled": true }, {"name": "Manage Business Hour Holidays ", "enabled": true }, {"name": "Manage Call Centers ", "enabled": true }, {"name": "Manage Cases ", "enabled": true }, {"name": "Manage Categories ", "enabled": true }, {"name": "Manage Content Permissions ", "enabled": true }, {"name": "Manage Content Properties ", "enabled": true }, {"name": "Manage Content Types ", "enabled": true }, {"name": "Manage Custom Report Types ", "enabled": true }, {"name": "Manage Data Categories ", "enabled": true }, {"name": "Manage Data Integrations ", "enabled": true }, {"name": "Manage Dynamic Dashboards ", "enabled": true }, {"name": "Manage Email Client Config ", "enabled": true }, {"name": "Manage Exchange Config ", "enabled": true }, {"name": "Manage Interaction ", "enabled": true }, {"name": "Manage Knowledge ", "enabled": true }, {"name": "Manage Knowledge Import Export ", "enabled": true }, {"name": "Manage Leads ", "enabled": true }, {"name": "Manage Mobile ", "enabled": true }, {"name": "Manage Networks ", "enabled": true }, {"name": "Manage Partner Net Conn ", "enabled": true }, {"name": "Manage Partners ", "enabled": true }, {"name": "Manage Quotas ", "enabled": true }, {"name": "Manage Remote Access ", "enabled": true }, {"name": "Manage Solutions ", "enabled": true }, {"name": "Manage Synonyms ", "enabled": true }, {"name": "Manage Users ", "enabled": true }, {"name": "Mass Inline Edit ", "enabled": true }, {"name": "Moderate Chatter ", "enabled": true }, {"name": "Modify All Data ", "enabled": true }, {"name": "New Report Builder ", "enabled": true }, {"name": "Outbound Migration Tools User ", "enabled": true }, {"name": "Override Forecasts ", "enabled": true }, {"name": "Publish Packaging ", "enabled": true }, {"name": "Reset Passwords ", "enabled": true }, {"name": "Run Flow ", "enabled": true }, {"name": "Run Reports ", "enabled": true }, {"name": "Schedule Job ", "enabled": true }, {"name": "Schedule Reports ", "enabled": true }, {"name": "Send Sit Requests ", "enabled": true }, {"name": "Solution Import ", "enabled": true }, {"name": "Transfer Any Case ", "enabled": true }, {"name": "Transfer Any Entity ", "enabled": true }, {"name": "Transfer Any Lead ", "enabled": true }, {"name": "Use Team Reassign Wizards ", "enabled": true }, {"name": "View All Data ", "enabled": true }, {"name": "View All Forecasts ", "enabled": true }, {"name": "View All Users ", "enabled": true }, {"name": "View Data Categories ", "enabled": true }, {"name": "View My Teams Dashboards ", "enabled": true }, {"name": "View Setup ", "enabled": true }], "permset1_User_Common": [{"name": "Activate Contract ", "enabled": true }, {"name": "Activate Order ", "enabled": true }, {"name": "Allow Universal Search ", "enabled": true }, {"name": "Api Enabled ", "enabled": true }, {"name": "Author Apex ", "enabled": true }, {"name": "Can Insert Feed System Fields ", "enabled": true }, {"name": "Can Use New Dashboard Builder ", "enabled": true }, {"name": "Chatter File Link ", "enabled": true }, {"name": "Chatter Internal User ", "enabled": true }, {"name": "Chatter Invite External Users ", "enabled": true }, {"name": "Chatter Own Groups ", "enabled": true }, {"name": "Connect Org To Environment Hub ", "enabled": true }, {"name": "Content Administrator ", "enabled": true }, {"name": "Convert Leads ", "enabled": true }, {"name": "Create Packaging ", "enabled": true }, {"name": "Create Workspaces ", "enabled": true }, {"name": "Customize Application ", "enabled": true }, {"name": "Data Export ", "enabled": true }, {"name": "Delete Activated Contract ", "enabled": true }, {"name": "Distribute From Pers Wksp ", "enabled": true }, {"name": "Edit Activated Orders ", "enabled": true }, {"name": "Edit Billing Info ", "enabled": true }, {"name": "Edit Brand Templates ", "enabled": true }, {"name": "Edit Case Comments ", "enabled": true }, {"name": "Edit Event ", "enabled": true }, {"name": "Edit Html Templates ", "enabled": true }, {"name": "Edit Knowledge ", "enabled": true }, {"name": "Edit Opp Line Item Unit Price ", "enabled": true }, {"name": "Edit Public Documents ", "enabled": true }, {"name": "Edit Public Templates ", "enabled": true }, {"name": "Edit Readonly Fields ", "enabled": true }, {"name": "Edit Task ", "enabled": true }, {"name": "Email Administration ", "enabled": true }, {"name": "Email Mass ", "enabled": true }, {"name": "Email Single ", "enabled": true }, {"name": "Email Template Management ", "enabled": true }, {"name": "Enable Notifications ", "enabled": true }, {"name": "Export Report ", "enabled": true }, {"name": "Flow U F L Required ", "enabled": true }, {"name": "Import Leads ", "enabled": true }, {"name": "Import Personal ", "enabled": true }, {"name": "Inbound Migration Tools User ", "enabled": true }, {"name": "Install Packaging ", "enabled": true }, {"name": "Manage Analytic Snapshots ", "enabled": true }, {"name": "Manage Auth Providers ", "enabled": true }, {"name": "Manage Business Hour Holidays ", "enabled": true }, {"name": "Manage Call Centers ", "enabled": true }, {"name": "Manage Cases ", "enabled": true }, {"name": "Manage Categories ", "enabled": true }, {"name": "Manage Content Permissions ", "enabled": true }, {"name": "Manage Content Properties ", "enabled": true }, {"name": "Manage Content Types ", "enabled": true }, {"name": "Manage Custom Report Types ", "enabled": true }, {"name": "Manage Data Categories ", "enabled": true }, {"name": "Manage Data Integrations ", "enabled": true }, {"name": "Manage Dynamic Dashboards ", "enabled": true }, {"name": "Manage Email Client Config ", "enabled": true }, {"name": "Manage Exchange Config ", "enabled": true }, {"name": "Manage Interaction ", "enabled": true }, {"name": "Manage Knowledge ", "enabled": true }, {"name": "Manage Knowledge Import Export ", "enabled": true }, {"name": "Manage Leads ", "enabled": true }, {"name": "Manage Mobile ", "enabled": true }, {"name": "Manage Networks ", "enabled": true }, {"name": "Manage Partner Net Conn ", "enabled": true }, {"name": "Manage Partners ", "enabled": true }, {"name": "Manage Quotas ", "enabled": true }, {"name": "Manage Remote Access ", "enabled": true }, {"name": "Manage Solutions ", "enabled": true }, {"name": "Manage Synonyms ", "enabled": true }, {"name": "Manage Users ", "enabled": true }, {"name": "Mass Inline Edit ", "enabled": true }, {"name": "Moderate Chatter ", "enabled": true }, {"name": "Modify All Data ", "enabled": true }, {"name": "New Report Builder ", "enabled": true }, {"name": "Outbound Migration Tools User ", "enabled": true }, {"name": "Override Forecasts ", "enabled": true }, {"name": "Publish Packaging ", "enabled": true }, {"name": "Reset Passwords ", "enabled": true }, {"name": "Run Flow ", "enabled": true }, {"name": "Run Reports ", "enabled": true }, {"name": "Schedule Job ", "enabled": true }, {"name": "Schedule Reports ", "enabled": true }, {"name": "Send Sit Requests ", "enabled": true }, {"name": "Solution Import ", "enabled": true }, {"name": "Transfer Any Case ", "enabled": true }, {"name": "Transfer Any Entity ", "enabled": true }, {"name": "Transfer Any Lead ", "enabled": true }, {"name": "Use Team Reassign Wizards ", "enabled": true }, {"name": "View All Data ", "enabled": true }, {"name": "View All Forecasts ", "enabled": true }, {"name": "View All Users ", "enabled": true }, {"name": "View Data Categories ", "enabled": true }, {"name": "View My Teams Dashboards ", "enabled": true }, {"name": "View Setup ", "enabled": true }], "permset1_User_Differences": [] }

            var result = await compareUserPermissions.CompareUserPerms(true, "0052j000000WkzFAAS", "0052j000000UJw0AAG");

            Console.WriteLine(result);
        }
    }
}