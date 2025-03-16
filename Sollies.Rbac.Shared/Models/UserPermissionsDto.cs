using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Sollies.Rbac.Shared.Models
{
        public partial class UserPermissionsDto
        {
            [JsonProperty("attributes")]
            public Attributes Attributes { get; set; }

            [JsonProperty("PermissionsEmailSingle")]
            public bool PermissionsEmailSingle { get; set; }

            [JsonProperty("PermissionsEmailMass")]
            public bool PermissionsEmailMass { get; set; }

            [JsonProperty("PermissionsEditTask")]
            public bool PermissionsEditTask { get; set; }

            [JsonProperty("PermissionsEditEvent")]
            public bool PermissionsEditEvent { get; set; }

            [JsonProperty("PermissionsExportReport")]
            public bool PermissionsExportReport { get; set; }

            [JsonProperty("PermissionsImportPersonal")]
            public bool PermissionsImportPersonal { get; set; }

            [JsonProperty("PermissionsDataExport")]
            public bool PermissionsDataExport { get; set; }

            [JsonProperty("PermissionsManageUsers")]
            public bool PermissionsManageUsers { get; set; }

            [JsonProperty("PermissionsEditPublicFilters")]
            public bool PermissionsEditPublicFilters { get; set; }

            [JsonProperty("PermissionsEditPublicTemplates")]
            public bool PermissionsEditPublicTemplates { get; set; }

            [JsonProperty("PermissionsModifyAllData")]
            public bool PermissionsModifyAllData { get; set; }

            [JsonProperty("PermissionsEditBillingInfo")]
            public bool PermissionsEditBillingInfo { get; set; }

            [JsonProperty("PermissionsManageCases")]
            public bool PermissionsManageCases { get; set; }

            [JsonProperty("PermissionsMassInlineEdit")]
            public bool PermissionsMassInlineEdit { get; set; }

            [JsonProperty("PermissionsEditKnowledge")]
            public bool PermissionsEditKnowledge { get; set; }

            [JsonProperty("PermissionsManageKnowledge")]
            public bool PermissionsManageKnowledge { get; set; }

            [JsonProperty("PermissionsManageSolutions")]
            public bool PermissionsManageSolutions { get; set; }

            [JsonProperty("PermissionsCustomizeApplication")]
            public bool PermissionsCustomizeApplication { get; set; }

            [JsonProperty("PermissionsEditReadonlyFields")]
            public bool PermissionsEditReadonlyFields { get; set; }

            [JsonProperty("PermissionsRunReports")]
            public bool PermissionsRunReports { get; set; }

            [JsonProperty("PermissionsViewSetup")]
            public bool PermissionsViewSetup { get; set; }

            [JsonProperty("PermissionsTransferAnyEntity")]
            public bool PermissionsTransferAnyEntity { get; set; }

            [JsonProperty("PermissionsNewReportBuilder")]
            public bool PermissionsNewReportBuilder { get; set; }

            [JsonProperty("PermissionsManageCssUsers")]
            public bool PermissionsManageCssUsers { get; set; }

            [JsonProperty("PermissionsActivateContract")]
            public bool PermissionsActivateContract { get; set; }

            [JsonProperty("PermissionsActivateOrder")]
            public bool PermissionsActivateOrder { get; set; }

            [JsonProperty("PermissionsImportLeads")]
            public bool PermissionsImportLeads { get; set; }

            [JsonProperty("PermissionsManageLeads")]
            public bool PermissionsManageLeads { get; set; }

            [JsonProperty("PermissionsTransferAnyLead")]
            public bool PermissionsTransferAnyLead { get; set; }

            [JsonProperty("PermissionsViewAllData")]
            public bool PermissionsViewAllData { get; set; }

            [JsonProperty("PermissionsEditPublicDocuments")]
            public bool PermissionsEditPublicDocuments { get; set; }

            [JsonProperty("PermissionsViewEncryptedData")]
            public bool PermissionsViewEncryptedData { get; set; }

            [JsonProperty("PermissionsEditBrandTemplates")]
            public bool PermissionsEditBrandTemplates { get; set; }

            [JsonProperty("PermissionsEditHtmlTemplates")]
            public bool PermissionsEditHtmlTemplates { get; set; }

            [JsonProperty("PermissionsChatterInternalUser")]
            public bool PermissionsChatterInternalUser { get; set; }

            [JsonProperty("PermissionsManageEncryptionKeys")]
            public bool PermissionsManageEncryptionKeys { get; set; }

            [JsonProperty("PermissionsDeleteActivatedContract")]
            public bool PermissionsDeleteActivatedContract { get; set; }

            [JsonProperty("PermissionsChatterInviteExternalUsers")]
            public bool PermissionsChatterInviteExternalUsers { get; set; }

            [JsonProperty("PermissionsSendSitRequests")]
            public bool PermissionsSendSitRequests { get; set; }

            [JsonProperty("PermissionsOverrideForecasts")]
            public bool PermissionsOverrideForecasts { get; set; }

            [JsonProperty("PermissionsViewAllForecasts")]
            public bool PermissionsViewAllForecasts { get; set; }

            [JsonProperty("PermissionsApiUserOnly")]
            public bool PermissionsApiUserOnly { get; set; }

            [JsonProperty("PermissionsManageRemoteAccess")]
            public bool PermissionsManageRemoteAccess { get; set; }

            [JsonProperty("PermissionsCanUseNewDashboardBuilder")]
            public bool PermissionsCanUseNewDashboardBuilder { get; set; }

            [JsonProperty("PermissionsManageCategories")]
            public bool PermissionsManageCategories { get; set; }

            [JsonProperty("PermissionsConvertLeads")]
            public bool PermissionsConvertLeads { get; set; }

            [JsonProperty("PermissionsPasswordNeverExpires")]
            public bool PermissionsPasswordNeverExpires { get; set; }

            [JsonProperty("PermissionsUseTeamReassignWizards")]
            public bool PermissionsUseTeamReassignWizards { get; set; }

            [JsonProperty("PermissionsEditActivatedOrders")]
            public bool PermissionsEditActivatedOrders { get; set; }

            [JsonProperty("PermissionsInstallPackaging")]
            public bool PermissionsInstallPackaging { get; set; }

            [JsonProperty("PermissionsPublishPackaging")]
            public bool PermissionsPublishPackaging { get; set; }

            [JsonProperty("PermissionsManagePartners")]
            public bool PermissionsManagePartners { get; set; }

            [JsonProperty("PermissionsChatterOwnGroups")]
            public bool PermissionsChatterOwnGroups { get; set; }

            [JsonProperty("PermissionsEditOppLineItemUnitPrice")]
            public bool PermissionsEditOppLineItemUnitPrice { get; set; }

            [JsonProperty("PermissionsCreatePackaging")]
            public bool PermissionsCreatePackaging { get; set; }

            [JsonProperty("PermissionsBulkApiHardDelete")]
            public bool PermissionsBulkApiHardDelete { get; set; }

            [JsonProperty("PermissionsInboundMigrationToolsUser")]
            public bool PermissionsInboundMigrationToolsUser { get; set; }

            [JsonProperty("PermissionsSolutionImport")]
            public bool PermissionsSolutionImport { get; set; }

            [JsonProperty("PermissionsManageCallCenters")]
            public bool PermissionsManageCallCenters { get; set; }

            [JsonProperty("PermissionsPortalSuperUser")]
            public bool PermissionsPortalSuperUser { get; set; }

            [JsonProperty("PermissionsManageSynonyms")]
            public bool PermissionsManageSynonyms { get; set; }

            [JsonProperty("PermissionsOutboundMigrationToolsUser")]
            public bool PermissionsOutboundMigrationToolsUser { get; set; }

            [JsonProperty("PermissionsDelegatedPortalUserAdmin")]
            public bool PermissionsDelegatedPortalUserAdmin { get; set; }

            [JsonProperty("PermissionsViewContent")]
            public bool PermissionsViewContent { get; set; }

            [JsonProperty("PermissionsManageEmailClientConfig")]
            public bool PermissionsManageEmailClientConfig { get; set; }

            [JsonProperty("PermissionsEnableNotifications")]
            public bool PermissionsEnableNotifications { get; set; }

            [JsonProperty("PermissionsIsSsoEnabled")]
            public bool PermissionsIsSsoEnabled { get; set; }

            [JsonProperty("PermissionsManageDataIntegrations")]
            public bool PermissionsManageDataIntegrations { get; set; }

            [JsonProperty("PermissionsDistributeFromPersWksp")]
            public bool PermissionsDistributeFromPersWksp { get; set; }

            [JsonProperty("PermissionsViewDataCategories")]
            public bool PermissionsViewDataCategories { get; set; }

            [JsonProperty("PermissionsManageDataCategories")]
            public bool PermissionsManageDataCategories { get; set; }

            [JsonProperty("PermissionsAuthorApex")]
            public bool PermissionsAuthorApex { get; set; }

            [JsonProperty("PermissionsManageMobile")]
            public bool PermissionsManageMobile { get; set; }

            [JsonProperty("PermissionsApiEnabled")]
            public bool PermissionsApiEnabled { get; set; }

            [JsonProperty("PermissionsManageCustomReportTypes")]
            public bool PermissionsManageCustomReportTypes { get; set; }

            [JsonProperty("PermissionsManagePartnerNetConn")]
            public bool PermissionsManagePartnerNetConn { get; set; }

            [JsonProperty("PermissionsEditCaseComments")]
            public bool PermissionsEditCaseComments { get; set; }

            [JsonProperty("PermissionsTransferAnyCase")]
            public bool PermissionsTransferAnyCase { get; set; }

            [JsonProperty("PermissionsContentAdministrator")]
            public bool PermissionsContentAdministrator { get; set; }

            [JsonProperty("PermissionsCreateWorkspaces")]
            public bool PermissionsCreateWorkspaces { get; set; }

            [JsonProperty("PermissionsManageContentPermissions")]
            public bool PermissionsManageContentPermissions { get; set; }

            [JsonProperty("PermissionsManageContentProperties")]
            public bool PermissionsManageContentProperties { get; set; }

            [JsonProperty("PermissionsManageContentTypes")]
            public bool PermissionsManageContentTypes { get; set; }

            [JsonProperty("PermissionsScheduleJob")]
            public bool PermissionsScheduleJob { get; set; }

            [JsonProperty("PermissionsManageExchangeConfig")]
            public bool PermissionsManageExchangeConfig { get; set; }

            [JsonProperty("PermissionsManageAnalyticSnapshots")]
            public bool PermissionsManageAnalyticSnapshots { get; set; }

            [JsonProperty("PermissionsScheduleReports")]
            public bool PermissionsScheduleReports { get; set; }

            [JsonProperty("PermissionsManageBusinessHourHolidays")]
            public bool PermissionsManageBusinessHourHolidays { get; set; }

            [JsonProperty("PermissionsManageDynamicDashboards")]
            public bool PermissionsManageDynamicDashboards { get; set; }

            [JsonProperty("PermissionsCustomSidebarOnAllPages")]
            public bool PermissionsCustomSidebarOnAllPages { get; set; }

            [JsonProperty("PermissionsManageInteraction")]
            public bool PermissionsManageInteraction { get; set; }

            [JsonProperty("PermissionsViewMyTeamsDashboards")]
            public bool PermissionsViewMyTeamsDashboards { get; set; }

            [JsonProperty("PermissionsModerateChatter")]
            public bool PermissionsModerateChatter { get; set; }

            [JsonProperty("PermissionsResetPasswords")]
            public bool PermissionsResetPasswords { get; set; }

            [JsonProperty("PermissionsFlowUFLRequired")]
            public bool PermissionsFlowUflRequired { get; set; }

            [JsonProperty("PermissionsCanInsertFeedSystemFields")]
            public bool PermissionsCanInsertFeedSystemFields { get; set; }

            [JsonProperty("PermissionsActivitiesAccess")]
            public bool PermissionsActivitiesAccess { get; set; }

            [JsonProperty("PermissionsManageKnowledgeImportExport")]
            public bool PermissionsManageKnowledgeImportExport { get; set; }

            [JsonProperty("PermissionsEmailTemplateManagement")]
            public bool PermissionsEmailTemplateManagement { get; set; }

            [JsonProperty("PermissionsEmailAdministration")]
            public bool PermissionsEmailAdministration { get; set; }

            [JsonProperty("PermissionsManageChatterMessages")]
            public bool PermissionsManageChatterMessages { get; set; }

            [JsonProperty("PermissionsAllowEmailIC")]
            public bool PermissionsAllowEmailIc { get; set; }

            [JsonProperty("PermissionsChatterFileLink")]
            public bool PermissionsChatterFileLink { get; set; }

            [JsonProperty("PermissionsForceTwoFactor")]
            public bool PermissionsForceTwoFactor { get; set; }

            [JsonProperty("PermissionsViewEventLogFiles")]
            public bool PermissionsViewEventLogFiles { get; set; }

            [JsonProperty("PermissionsManageNetworks")]
            public bool PermissionsManageNetworks { get; set; }

            [JsonProperty("PermissionsViewCaseInteraction")]
            public bool PermissionsViewCaseInteraction { get; set; }

            [JsonProperty("PermissionsManageAuthProviders")]
            public bool PermissionsManageAuthProviders { get; set; }

            [JsonProperty("PermissionsRunFlow")]
            public bool PermissionsRunFlow { get; set; }

            [JsonProperty("PermissionsViewGlobalHeader")]
            public bool PermissionsViewGlobalHeader { get; set; }

            [JsonProperty("PermissionsManageQuotas")]
            public bool PermissionsManageQuotas { get; set; }

            [JsonProperty("PermissionsCreateCustomizeDashboards")]
            public bool PermissionsCreateCustomizeDashboards { get; set; }

            [JsonProperty("PermissionsCreateDashboardFolders")]
            public bool PermissionsCreateDashboardFolders { get; set; }

            [JsonProperty("PermissionsViewPublicDashboards")]
            public bool PermissionsViewPublicDashboards { get; set; }

            [JsonProperty("PermissionsManageDashbdsInPubFolders")]
            public bool PermissionsManageDashbdsInPubFolders { get; set; }

            [JsonProperty("PermissionsCreateCustomizeReports")]
            public bool PermissionsCreateCustomizeReports { get; set; }

            [JsonProperty("PermissionsCreateReportFolders")]
            public bool PermissionsCreateReportFolders { get; set; }

            [JsonProperty("PermissionsViewPublicReports")]
            public bool PermissionsViewPublicReports { get; set; }

            [JsonProperty("PermissionsManageReportsInPubFolders")]
            public bool PermissionsManageReportsInPubFolders { get; set; }

            [JsonProperty("PermissionsEditMyDashboards")]
            public bool PermissionsEditMyDashboards { get; set; }

            [JsonProperty("PermissionsEditMyReports")]
            public bool PermissionsEditMyReports { get; set; }

            [JsonProperty("PermissionsViewAllUsers")]
            public bool PermissionsViewAllUsers { get; set; }

            [JsonProperty("PermissionsBypassEmailApproval")]
            public bool PermissionsBypassEmailApproval { get; set; }

            [JsonProperty("PermissionsAllowUniversalSearch")]
            public bool PermissionsAllowUniversalSearch { get; set; }

            [JsonProperty("PermissionsConnectOrgToEnvironmentHub")]
            public bool PermissionsConnectOrgToEnvironmentHub { get; set; }

            [JsonProperty("PermissionsCreateCustomizeFilters")]
            public bool PermissionsCreateCustomizeFilters { get; set; }

            [JsonProperty("PermissionsContentHubUser")]
            public bool PermissionsContentHubUser { get; set; }

            [JsonProperty("PermissionsModerateNetworkFeeds")]
            public bool PermissionsModerateNetworkFeeds { get; set; }

            [JsonProperty("PermissionsModerateNetworkFiles")]
            public bool PermissionsModerateNetworkFiles { get; set; }

            [JsonProperty("PermissionsGovernNetworks")]
            public bool PermissionsGovernNetworks { get; set; }

            [JsonProperty("PermissionsSalesConsole")]
            public bool PermissionsSalesConsole { get; set; }

            [JsonProperty("PermissionsTwoFactorApi")]
            public bool PermissionsTwoFactorApi { get; set; }

            [JsonProperty("PermissionsDeleteTopics")]
            public bool PermissionsDeleteTopics { get; set; }

            [JsonProperty("PermissionsEditTopics")]
            public bool PermissionsEditTopics { get; set; }

            [JsonProperty("PermissionsCreateTopics")]
            public bool PermissionsCreateTopics { get; set; }

            [JsonProperty("PermissionsAssignTopics")]
            public bool PermissionsAssignTopics { get; set; }

            [JsonProperty("PermissionsIdentityEnabled")]
            public bool PermissionsIdentityEnabled { get; set; }

            [JsonProperty("PermissionsIdentityConnect")]
            public bool PermissionsIdentityConnect { get; set; }

            [JsonProperty("PermissionsAllowViewKnowledge")]
            public bool PermissionsAllowViewKnowledge { get; set; }

            [JsonProperty("PermissionsChatterEnabledForUser")]
            public bool PermissionsChatterEnabledForUser { get; set; }

            [JsonProperty("PermissionsCreateWorkBadgeDefinition")]
            public bool PermissionsCreateWorkBadgeDefinition { get; set; }

            [JsonProperty("PermissionsManageSearchPromotionRules")]
            public bool PermissionsManageSearchPromotionRules { get; set; }

            [JsonProperty("PermissionsCustomMobileAppsAccess")]
            public bool PermissionsCustomMobileAppsAccess { get; set; }

            [JsonProperty("PermissionsViewHelpLink")]
            public bool PermissionsViewHelpLink { get; set; }

            [JsonProperty("PermissionsManageProfilesPermissionsets")]
            public bool PermissionsManageProfilesPermissionsets { get; set; }

            [JsonProperty("PermissionsAssignPermissionSets")]
            public bool PermissionsAssignPermissionSets { get; set; }

            [JsonProperty("PermissionsManageRoles")]
            public bool PermissionsManageRoles { get; set; }

            [JsonProperty("PermissionsManageIpAddresses")]
            public bool PermissionsManageIpAddresses { get; set; }

            [JsonProperty("PermissionsManageSharing")]
            public bool PermissionsManageSharing { get; set; }

            [JsonProperty("PermissionsManageInternalUsers")]
            public bool PermissionsManageInternalUsers { get; set; }

            [JsonProperty("PermissionsManagePasswordPolicies")]
            public bool PermissionsManagePasswordPolicies { get; set; }

            [JsonProperty("PermissionsManageLoginAccessPolicies")]
            public bool PermissionsManageLoginAccessPolicies { get; set; }

            [JsonProperty("PermissionsViewPlatformEvents")]
            public bool PermissionsViewPlatformEvents { get; set; }

            [JsonProperty("PermissionsManageCustomPermissions")]
            public bool PermissionsManageCustomPermissions { get; set; }

            [JsonProperty("PermissionsCanVerifyComment")]
            public bool PermissionsCanVerifyComment { get; set; }

            [JsonProperty("PermissionsManageUnlistedGroups")]
            public bool PermissionsManageUnlistedGroups { get; set; }

            [JsonProperty("PermissionsInsightsAppDashboardEditor")]
            public bool PermissionsInsightsAppDashboardEditor { get; set; }

            [JsonProperty("PermissionsShareFilesWithNetworks")]
            public bool PermissionsShareFilesWithNetworks { get; set; }

            [JsonProperty("PermissionsManageTwoFactor")]
            public bool PermissionsManageTwoFactor { get; set; }

            [JsonProperty("PermissionsInsightsAppUser")]
            public bool PermissionsInsightsAppUser { get; set; }

            [JsonProperty("PermissionsInsightsAppAdmin")]
            public bool PermissionsInsightsAppAdmin { get; set; }

            [JsonProperty("PermissionsInsightsAppEltEditor")]
            public bool PermissionsInsightsAppEltEditor { get; set; }

            [JsonProperty("PermissionsInsightsAppUploadUser")]
            public bool PermissionsInsightsAppUploadUser { get; set; }

            [JsonProperty("PermissionsInsightsCreateApplication")]
            public bool PermissionsInsightsCreateApplication { get; set; }

            [JsonProperty("PermissionsDebugApex")]
            public bool PermissionsDebugApex { get; set; }

            [JsonProperty("PermissionsLightningExperienceUser")]
            public bool PermissionsLightningExperienceUser { get; set; }

            [JsonProperty("PermissionsViewDataLeakageEvents")]
            public bool PermissionsViewDataLeakageEvents { get; set; }

            [JsonProperty("PermissionsConfigCustomRecs")]
            public bool PermissionsConfigCustomRecs { get; set; }

            [JsonProperty("PermissionsSubmitMacrosAllowed")]
            public bool PermissionsSubmitMacrosAllowed { get; set; }

            [JsonProperty("PermissionsBulkMacrosAllowed")]
            public bool PermissionsBulkMacrosAllowed { get; set; }

            [JsonProperty("PermissionsPublicTwitterResponse")]
            public bool PermissionsPublicTwitterResponse { get; set; }

            [JsonProperty("PermissionsShareInternalArticles")]
            public bool PermissionsShareInternalArticles { get; set; }

            [JsonProperty("PermissionsModerateNetworkMessages")]
            public bool PermissionsModerateNetworkMessages { get; set; }

            [JsonProperty("PermissionsManageSessionPermissionSets")]
            public bool PermissionsManageSessionPermissionSets { get; set; }

            [JsonProperty("PermissionsManageTemplatedApp")]
            public bool PermissionsManageTemplatedApp { get; set; }

            [JsonProperty("PermissionsUseTemplatedApp")]
            public bool PermissionsUseTemplatedApp { get; set; }

            [JsonProperty("PermissionsSendAnnouncementEmails")]
            public bool PermissionsSendAnnouncementEmails { get; set; }

            [JsonProperty("PermissionsChatterEditOwnPost")]
            public bool PermissionsChatterEditOwnPost { get; set; }

            [JsonProperty("PermissionsChatterEditOwnRecordPost")]
            public bool PermissionsChatterEditOwnRecordPost { get; set; }

            [JsonProperty("PermissionsSalesAnalyticsUser")]
            public bool PermissionsSalesAnalyticsUser { get; set; }

            [JsonProperty("PermissionsAdminAnalyticsUser")]
            public bool PermissionsAdminAnalyticsUser { get; set; }

            [JsonProperty("PermissionsServiceAnalyticsUser")]
            public bool PermissionsServiceAnalyticsUser { get; set; }

            [JsonProperty("PermissionsCreateAuditFields")]
            public bool PermissionsCreateAuditFields { get; set; }

            [JsonProperty("PermissionsUpdateWithInactiveOwner")]
            public bool PermissionsUpdateWithInactiveOwner { get; set; }

            [JsonProperty("PermissionsWaveTabularDownload")]
            public bool PermissionsWaveTabularDownload { get; set; }

            [JsonProperty("PermissionsWaveCommunityUser")]
            public bool PermissionsWaveCommunityUser { get; set; }

            [JsonProperty("PermissionsManageSandboxes")]
            public bool PermissionsManageSandboxes { get; set; }

            [JsonProperty("PermissionsImportCustomObjects")]
            public bool PermissionsImportCustomObjects { get; set; }

            [JsonProperty("PermissionsDelegatedTwoFactor")]
            public bool PermissionsDelegatedTwoFactor { get; set; }

            [JsonProperty("PermissionsChatterComposeUiCodesnippet")]
            public bool PermissionsChatterComposeUiCodesnippet { get; set; }

            [JsonProperty("PermissionsSelectFilesFromSalesforce")]
            public bool PermissionsSelectFilesFromSalesforce { get; set; }

            [JsonProperty("PermissionsModerateNetworkUsers")]
            public bool PermissionsModerateNetworkUsers { get; set; }

            [JsonProperty("PermissionsMergeTopics")]
            public bool PermissionsMergeTopics { get; set; }

            [JsonProperty("PermissionsSubscribeToLightningReports")]
            public bool PermissionsSubscribeToLightningReports { get; set; }

            [JsonProperty("PermissionsManagePvtRptsAndDashbds")]
            public bool PermissionsManagePvtRptsAndDashbds { get; set; }

            [JsonProperty("PermissionsAllowLightningLogin")]
            public bool PermissionsAllowLightningLogin { get; set; }

            [JsonProperty("PermissionsCampaignInfluence2")]
            public bool PermissionsCampaignInfluence2 { get; set; }

            [JsonProperty("PermissionsViewDataAssessment")]
            public bool PermissionsViewDataAssessment { get; set; }

            [JsonProperty("PermissionsRemoveDirectMessageMembers")]
            public bool PermissionsRemoveDirectMessageMembers { get; set; }

            [JsonProperty("PermissionsCanApproveFeedPost")]
            public bool PermissionsCanApproveFeedPost { get; set; }

            [JsonProperty("PermissionsAddDirectMessageMembers")]
            public bool PermissionsAddDirectMessageMembers { get; set; }

            [JsonProperty("PermissionsAllowViewEditConvertedLeads")]
            public bool PermissionsAllowViewEditConvertedLeads { get; set; }

            [JsonProperty("PermissionsShowCompanyNameAsUserBadge")]
            public bool PermissionsShowCompanyNameAsUserBadge { get; set; }

            [JsonProperty("PermissionsAccessCMC")]
            public bool PermissionsAccessCmc { get; set; }

            [JsonProperty("PermissionsArchiveArticles")]
            public bool PermissionsArchiveArticles { get; set; }

            [JsonProperty("PermissionsPublishArticles")]
            public bool PermissionsPublishArticles { get; set; }

            [JsonProperty("PermissionsViewHealthCheck")]
            public bool PermissionsViewHealthCheck { get; set; }

            [JsonProperty("PermissionsManageHealthCheck")]
            public bool PermissionsManageHealthCheck { get; set; }

            [JsonProperty("PermissionsPackaging2")]
            public bool PermissionsPackaging2 { get; set; }

            [JsonProperty("PermissionsManageCertificates")]
            public bool PermissionsManageCertificates { get; set; }

            [JsonProperty("PermissionsCreateReportInLightning")]
            public bool PermissionsCreateReportInLightning { get; set; }

            [JsonProperty("PermissionsPreventClassicExperience")]
            public bool PermissionsPreventClassicExperience { get; set; }

            [JsonProperty("PermissionsHideReadByList")]
            public bool PermissionsHideReadByList { get; set; }

            [JsonProperty("PermissionsSubmitForTranslation")]
            public bool PermissionsSubmitForTranslation { get; set; }

            [JsonProperty("PermissionsEditTranslation")]
            public bool PermissionsEditTranslation { get; set; }

            [JsonProperty("PermissionsPublishTranslation")]
            public bool PermissionsPublishTranslation { get; set; }

            [JsonProperty("PermissionsListEmailSend")]
            public bool PermissionsListEmailSend { get; set; }

            [JsonProperty("PermissionsFeedPinning")]
            public bool PermissionsFeedPinning { get; set; }

            [JsonProperty("PermissionsChangeDashboardColors")]
            public bool PermissionsChangeDashboardColors { get; set; }

            [JsonProperty("PermissionsManageRecommendationStrategies")]
            public bool PermissionsManageRecommendationStrategies { get; set; }

            [JsonProperty("PermissionsManagePropositions")]
            public bool PermissionsManagePropositions { get; set; }

            [JsonProperty("PermissionsCloseConversations")]
            public bool PermissionsCloseConversations { get; set; }

            [JsonProperty("PermissionsSubscribeReportRolesGrps")]
            public bool PermissionsSubscribeReportRolesGrps { get; set; }

            [JsonProperty("PermissionsSubscribeDashboardRolesGrps")]
            public bool PermissionsSubscribeDashboardRolesGrps { get; set; }

            [JsonProperty("PermissionsUseWebLink")]
            public bool PermissionsUseWebLink { get; set; }

            [JsonProperty("PermissionsHasUnlimitedNBAExecutions")]
            public bool PermissionsHasUnlimitedNbaExecutions { get; set; }

            [JsonProperty("PermissionsViewOnlyEmbeddedAppUser")]
            public bool PermissionsViewOnlyEmbeddedAppUser { get; set; }

            [JsonProperty("PermissionsAdoptionAnalyticsUser")]
            public bool PermissionsAdoptionAnalyticsUser { get; set; }

            [JsonProperty("PermissionsViewAllActivities")]
            public bool PermissionsViewAllActivities { get; set; }

            [JsonProperty("PermissionsSubscribeReportToOtherUsers")]
            public bool PermissionsSubscribeReportToOtherUsers { get; set; }

            [JsonProperty("PermissionsLightningConsoleAllowedForUser")]
            public bool PermissionsLightningConsoleAllowedForUser { get; set; }

            [JsonProperty("PermissionsSubscribeReportsRunAsUser")]
            public bool PermissionsSubscribeReportsRunAsUser { get; set; }

            [JsonProperty("PermissionsSubscribeToLightningDashboards")]
            public bool PermissionsSubscribeToLightningDashboards { get; set; }

            [JsonProperty("PermissionsSubscribeDashboardToOtherUsers")]
            public bool PermissionsSubscribeDashboardToOtherUsers { get; set; }

            [JsonProperty("PermissionsCreateLtngTempInPub")]
            public bool PermissionsCreateLtngTempInPub { get; set; }

            [JsonProperty("PermissionsTransactionalEmailSend")]
            public bool PermissionsTransactionalEmailSend { get; set; }

            [JsonProperty("PermissionsViewPrivateStaticResources")]
            public bool PermissionsViewPrivateStaticResources { get; set; }

            [JsonProperty("PermissionsViewCustomerSentiment")]
            public bool PermissionsViewCustomerSentiment { get; set; }

            [JsonProperty("PermissionsCreateLtngTempFolder")]
            public bool PermissionsCreateLtngTempFolder { get; set; }

            [JsonProperty("PermissionsApexRestServices")]
            public bool PermissionsApexRestServices { get; set; }

            [JsonProperty("PermissionsGiveRecognitionBadge")]
            public bool PermissionsGiveRecognitionBadge { get; set; }

            [JsonProperty("PermissionsAllowObjectDetection")]
            public bool PermissionsAllowObjectDetection { get; set; }

            [JsonProperty("PermissionsUseMySearch")]
            public bool PermissionsUseMySearch { get; set; }

            [JsonProperty("PermissionsLtngPromoReserved01UserPerm")]
            public bool PermissionsLtngPromoReserved01UserPerm { get; set; }

            [JsonProperty("PermissionsManageSubscriptions")]
            public bool PermissionsManageSubscriptions { get; set; }

            [JsonProperty("PermissionsWaveManagePrivateAssetsUser")]
            public bool PermissionsWaveManagePrivateAssetsUser { get; set; }

            [JsonProperty("PermissionsAllowObjectDetectionTraining")]
            public bool PermissionsAllowObjectDetectionTraining { get; set; }

            [JsonProperty("PermissionsCanEditDataPrepRecipe")]
            public bool PermissionsCanEditDataPrepRecipe { get; set; }

            [JsonProperty("PermissionsAddAnalyticsRemoteConnections")]
            public bool PermissionsAddAnalyticsRemoteConnections { get; set; }

            [JsonProperty("PermissionsManageSurveys")]
            public bool PermissionsManageSurveys { get; set; }

            [JsonProperty("PermissionsUseAssistantDialog")]
            public bool PermissionsUseAssistantDialog { get; set; }

            [JsonProperty("PermissionsUseQuerySuggestions")]
            public bool PermissionsUseQuerySuggestions { get; set; }

            [JsonProperty("PermissionsRecordVisibilityAPI")]
            public bool PermissionsRecordVisibilityApi { get; set; }

            [JsonProperty("PermissionsViewRoles")]
            public bool PermissionsViewRoles { get; set; }

            [JsonProperty("PermissionsCanManageMaps")]
            public bool PermissionsCanManageMaps { get; set; }

            [JsonProperty("PermissionsLMOutboundMessagingUserPerm")]
            public bool PermissionsLmOutboundMessagingUserPerm { get; set; }

            [JsonProperty("PermissionsModifyDataClassification")]
            public bool PermissionsModifyDataClassification { get; set; }

            [JsonProperty("PermissionsPrivacyDataAccess")]
            public bool PermissionsPrivacyDataAccess { get; set; }

            [JsonProperty("PermissionsQueryAllFiles")]
            public bool PermissionsQueryAllFiles { get; set; }

            [JsonProperty("PermissionsModifyMetadata")]
            public bool PermissionsModifyMetadata { get; set; }

            [JsonProperty("PermissionsManageCMS")]
            public bool PermissionsManageCms { get; set; }

            [JsonProperty("PermissionsSandboxTestingInCommunityApp")]
            public bool PermissionsSandboxTestingInCommunityApp { get; set; }

            [JsonProperty("PermissionsCanEditPrompts")]
            public bool PermissionsCanEditPrompts { get; set; }

            [JsonProperty("PermissionsViewUserPII")]
            public bool PermissionsViewUserPii { get; set; }

            [JsonProperty("PermissionsMobileAppSecurity")]
            public bool PermissionsMobileAppSecurity { get; set; }

            [JsonProperty("PermissionsViewDraftArticles")]
            public bool PermissionsViewDraftArticles { get; set; }

            [JsonProperty("PermissionsViewArchivedArticles")]
            public bool PermissionsViewArchivedArticles { get; set; }

            [JsonProperty("PermissionsManageHubConnections")]
            public bool PermissionsManageHubConnections { get; set; }

            [JsonProperty("PermissionsB2BMarketingAnalyticsUser")]
            public bool PermissionsB2BMarketingAnalyticsUser { get; set; }

            [JsonProperty("PermissionsTraceXdsQueries")]
            public bool PermissionsTraceXdsQueries { get; set; }

            [JsonProperty("PermissionsViewAllCustomSettings")]
            public bool PermissionsViewAllCustomSettings { get; set; }

            [JsonProperty("PermissionsViewAllForeignKeyNames")]
            public bool PermissionsViewAllForeignKeyNames { get; set; }

            [JsonProperty("PermissionsAddWaveNotificationRecipients")]
            public bool PermissionsAddWaveNotificationRecipients { get; set; }

            [JsonProperty("PermissionsHeadlessCMSAccess")]
            public bool PermissionsHeadlessCmsAccess { get; set; }

            [JsonProperty("PermissionsLMEndMessagingSessionUserPerm")]
            public bool PermissionsLmEndMessagingSessionUserPerm { get; set; }

            [JsonProperty("PermissionsConsentApiUpdate")]
            public bool PermissionsConsentApiUpdate { get; set; }

            [JsonProperty("PermissionsManageMobileAppSecurity")]
            public bool PermissionsManageMobileAppSecurity { get; set; }

            [JsonProperty("PermissionsAccessContentBuilder")]
            public bool PermissionsAccessContentBuilder { get; set; }

            [JsonProperty("PermissionsEmployeeExperience")]
            public bool PermissionsEmployeeExperience { get; set; }

            [JsonProperty("PermissionsAccountSwitcherUser")]
            public bool PermissionsAccountSwitcherUser { get; set; }

            [JsonProperty("PermissionsViewAnomalyEvents")]
            public bool PermissionsViewAnomalyEvents { get; set; }

            [JsonProperty("PermissionsManageC360AConnections")]
            public bool PermissionsManageC360AConnections { get; set; }

            [JsonProperty("PermissionsManageReleaseUpdates")]
            public bool PermissionsManageReleaseUpdates { get; set; }

            [JsonProperty("PermissionsViewAllProfiles")]
            public bool PermissionsViewAllProfiles { get; set; }

            [JsonProperty("PermissionsSkipIdentityConfirmation")]
            public bool PermissionsSkipIdentityConfirmation { get; set; }

            [JsonProperty("PermissionsSendCustomNotifications")]
            public bool PermissionsSendCustomNotifications { get; set; }

            [JsonProperty("PermissionsPackaging2Delete")]
            public bool PermissionsPackaging2Delete { get; set; }

            [JsonProperty("PermissionsViewRestrictionAndScopingRules")]
            public bool PermissionsViewRestrictionAndScopingRules { get; set; }

            [JsonProperty("PermissionsDecisionTableExecUserAccess")]
            public bool PermissionsDecisionTableExecUserAccess { get; set; }

            [JsonProperty("PermissionsFSCComprehensiveUserAccess")]
            public bool PermissionsFscComprehensiveUserAccess { get; set; }

            [JsonProperty("PermissionsBotManageBots")]
            public bool PermissionsBotManageBots { get; set; }

            [JsonProperty("PermissionsBotManageBotsTrainingData")]
            public bool PermissionsBotManageBotsTrainingData { get; set; }

            [JsonProperty("PermissionsManageTrustMeasures")]
            public bool PermissionsManageTrustMeasures { get; set; }

            [JsonProperty("PermissionsViewTrustMeasures")]
            public bool PermissionsViewTrustMeasures { get; set; }

            [JsonProperty("PermissionsIsotopeCToCUser")]
            public bool PermissionsIsotopeCToCUser { get; set; }

            [JsonProperty("PermissionsUseSvcCatalog")]
            public bool PermissionsUseSvcCatalog { get; set; }

            [JsonProperty("PermissionsManageSvcCatalog")]
            public bool PermissionsManageSvcCatalog { get; set; }

            [JsonProperty("PermissionsCanAccessCE")]
            public bool PermissionsCanAccessCe { get; set; }

            [JsonProperty("PermissionsHasUnlimitedErbScoringRequests")]
            public bool PermissionsHasUnlimitedErbScoringRequests { get; set; }

            [JsonProperty("PermissionsFSCAlertFrameworkUserAccess")]
            public bool PermissionsFscAlertFrameworkUserAccess { get; set; }

            [JsonProperty("PermissionsIsotopeAccess")]
            public bool PermissionsIsotopeAccess { get; set; }

            [JsonProperty("PermissionsIsotopeLEX")]
            public bool PermissionsIsotopeLex { get; set; }

            [JsonProperty("PermissionsExplainabilityUserAccess")]
            public bool PermissionsExplainabilityUserAccess { get; set; }

            [JsonProperty("PermissionsExplainabilityCmtyAccess")]
            public bool PermissionsExplainabilityCmtyAccess { get; set; }

            [JsonProperty("PermissionsQuipMetricsAccess")]
            public bool PermissionsQuipMetricsAccess { get; set; }

            [JsonProperty("PermissionsQuipUserEngagementMetrics")]
            public bool PermissionsQuipUserEngagementMetrics { get; set; }

            [JsonProperty("PermissionsIdentityVerificationUserAccess")]
            public bool PermissionsIdentityVerificationUserAccess { get; set; }

            [JsonProperty("PermissionsServiceExcellencePlatformUser")]
            public bool PermissionsServiceExcellencePlatformUser { get; set; }

            [JsonProperty("PermissionsTransactionSecurityExempt")]
            public bool PermissionsTransactionSecurityExempt { get; set; }

            [JsonProperty("PermissionsInteractionCalcUserPerm")]
            public bool PermissionsInteractionCalcUserPerm { get; set; }

            [JsonProperty("PermissionsInteractionCalcAdminPerm")]
            public bool PermissionsInteractionCalcAdminPerm { get; set; }

            [JsonProperty("PermissionsManageExternalConnections")]
            public bool PermissionsManageExternalConnections { get; set; }

            [JsonProperty("PermissionsUseSubscriptionEmails")]
            public bool PermissionsUseSubscriptionEmails { get; set; }

            [JsonProperty("PermissionsAIViewInsightObjects")]
            public bool PermissionsAiViewInsightObjects { get; set; }

            [JsonProperty("PermissionsAICreateInsightObjects")]
            public bool PermissionsAiCreateInsightObjects { get; set; }

            [JsonProperty("PermissionsViewMLModels")]
            public bool PermissionsViewMlModels { get; set; }

            [JsonProperty("PermissionsNativeWebviewScrolling")]
            public bool PermissionsNativeWebviewScrolling { get; set; }

            [JsonProperty("PermissionsViewDeveloperName")]
            public bool PermissionsViewDeveloperName { get; set; }

            [JsonProperty("PermissionsBypassMFAForUiLogins")]
            public bool PermissionsBypassMfaForUiLogins { get; set; }

            [JsonProperty("PermissionsClientSecretRotation")]
            public bool PermissionsClientSecretRotation { get; set; }

            [JsonProperty("PermissionsUpdateReportTypeReferences")]
            public bool PermissionsUpdateReportTypeReferences { get; set; }

            [JsonProperty("PermissionsAccessToServiceProcess")]
            public bool PermissionsAccessToServiceProcess { get; set; }

            [JsonProperty("PermissionsMicrobatching")]
            public bool PermissionsMicrobatching { get; set; }

            [JsonProperty("PermissionsManageOrchInstsAndWorkItems")]
            public bool PermissionsManageOrchInstsAndWorkItems { get; set; }

            [JsonProperty("PermissionsCMSECEAuthoringAccess")]
            public bool PermissionsCmseceAuthoringAccess { get; set; }

            [JsonProperty("PermissionsManageDataspaceScope")]
            public bool PermissionsManageDataspaceScope { get; set; }

            [JsonProperty("PermissionsConfigureDataspaceScope")]
            public bool PermissionsConfigureDataspaceScope { get; set; }

            [JsonProperty("PermissionsOmniSupervisorManageQueue")]
            public bool PermissionsOmniSupervisorManageQueue { get; set; }

            [JsonProperty("PermissionsViewClientSecret")]
            public bool PermissionsViewClientSecret { get; set; }

            [JsonProperty("PermissionsEnableIPFSUpload")]
            public bool PermissionsEnableIpfsUpload { get; set; }

            [JsonProperty("PermissionsEnableBCTransactionPolling")]
            public bool PermissionsEnableBcTransactionPolling { get; set; }

            [JsonProperty("PermissionsFSCArcGraphCommunityUser")]
            public bool PermissionsFscArcGraphCommunityUser { get; set; }
        }

        public partial class Attributes
        {
            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("url")]
            public string Url { get; set; }
        }

        public partial class UserPermissionsDto
    {
            public static UserPermissionsDto FromJson(string json) => JsonConvert.DeserializeObject<UserPermissionsDto>(json, Converter.Settings);
        }

        public static class Serialize
        {
            public static string ToJson(this UserPermissionsDto self) => JsonConvert.SerializeObject(self, Converter.Settings);
        }

        internal static class Converter
        {
            public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
            {
                MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
                DateParseHandling = DateParseHandling.None,
                Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
            };
        }
    }