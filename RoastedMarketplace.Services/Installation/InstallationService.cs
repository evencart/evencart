using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using RoastedMarketplace.Core.Exception;
using RoastedMarketplace.Core.Infrastructure;
using RoastedMarketplace.Data.Constants;
using RoastedMarketplace.Data.Database;
using RoastedMarketplace.Data.Entity.Notifications;
using RoastedMarketplace.Data.Entity.Settings;
using RoastedMarketplace.Data.Entity.Users;
using RoastedMarketplace.Data.Enum;
using RoastedMarketplace.Services.Notifications;
using RoastedMarketplace.Services.Settings;
using RoastedMarketplace.Services.Users;

namespace RoastedMarketplace.Services.Installation
{
    public class InstallationService : IInstallationService
    {
        private readonly IDatabaseSettings _databaseSettings;
        public InstallationService(IDatabaseSettings databaseSettings)
        {
            _databaseSettings = databaseSettings;
        }

        public void Install()
        {
            DatabaseManager.InitDatabase(_databaseSettings);
            DatabaseManager.ClearVersions();
            DatabaseManager.UpgradeDatabase();
        }


        public void FillRequiredSeedData(string defaultUserEmail, string defaultUserPassword, string installDomain)
        {
            //first the settings
            SeedSettings(installDomain);

            //seed the roles
            SeedRoles();

            //then the user
            SeedDefaultUser(defaultUserEmail, defaultUserPassword);

            //seed email templates
            SeedEmailTemplates(defaultUserEmail, installDomain);

            //seed notification events
            //SeedNotificationEvents();
        }
        /// <summary>
        /// Seed roles
        /// </summary>
        private void SeedRoles()
        {
            var roleService = DependencyResolver.Resolve<IRoleService>();
            var roleCapabilityService = DependencyResolver.Resolve<IRoleCapabilityService>();
            var capabilityService = DependencyResolver.Resolve<ICapabilityService>();
            var capabilityProvider = DependencyResolver.Resolve<ICapabilityProvider>();

            var roles = new Dictionary<string, Role>()
            {
                {
                    SystemRoleNames.Administrator, new Role()
                    {
                        Name = SystemRoleNames.Administrator,
                        IsSystemRole = true,
                        IsActive = true,
                        SystemName = SystemRoleNames.Administrator
                    }
                },
                {
                    SystemRoleNames.Vendor, new Role()
                    {
                        Name = SystemRoleNames.Vendor,
                        IsSystemRole = true,
                        IsActive = true,
                        SystemName = SystemRoleNames.Vendor
                    }
                },
                {
                    SystemRoleNames.Manager, new Role()
                    {
                        Name = SystemRoleNames.Manager,
                        IsSystemRole = true,
                        IsActive = true,
                        SystemName = SystemRoleNames.Manager
                    }
                },
                {
                    SystemRoleNames.Registered, new Role()
                    {
                        Name = SystemRoleNames.Registered,
                        IsSystemRole = true,
                        IsActive = true,
                        SystemName = SystemRoleNames.Registered
                    }
                },
                {
                    SystemRoleNames.Visitor, new Role()
                    {
                        Name = SystemRoleNames.Visitor,
                        IsSystemRole = true,
                        IsActive = true,
                        SystemName = SystemRoleNames.Visitor
                    }
                }
            };


            //insert roles
            foreach (var r in roles)
            {
                roleService.Insert(r.Value);
            }

            //seed capabilities
            //insert all available capabilities using reflection
            var allCapabilityFields = typeof(CapabilitySystemNames).GetFields(BindingFlags.Public | BindingFlags.Static |
                                                    BindingFlags.FlattenHierarchy);

            var savedCapabilities = new List<Capability>();
            foreach (var f in allCapabilityFields)
            {
                var capabilityName = (string)f.GetRawConstantValue();
                var capability = new Capability() {
                    Name = capabilityName,
                    IsActive = true
                };
                capabilityService.Insert(capability);

                //save capability for easy access in a while 
                savedCapabilities.Add(capability);
            }

            //now assign capabilities to roles
            var capabilities = capabilityProvider.GetCapabilities();
            foreach (var c in capabilities)
            {
                if (c.Key == SystemRoleNames.Administrator)
                    continue;
                var roleId = roles.Values.Where(x => x.Name == c.Key).Select(x => x.Id).First();
                var cIds = savedCapabilities.Where(x => c.Value.Contains(x.Name)).Select(x => x.Id);
                roleCapabilityService.SetRoleCapabilities(roleId, cIds.ToArray());
            }

        }

        /// <summary>
        /// Seed default user
        /// </summary>
        private void SeedDefaultUser(string email, string password)
        {
            var userRegistrationService = DependencyResolver.Resolve<IUserRegistrationService>();
            var securitySettings = DependencyResolver.Resolve<SecuritySettings>();

            var registrationResult = userRegistrationService.Register(email, password, securitySettings.DefaultPasswordStorageFormat);
            if (registrationResult == UserRegistrationStatus.Success)
            {
                //add roles
                var roleService = DependencyResolver.Resolve<IRoleService>();
                var userService = DependencyResolver.Resolve<IUserService>();

                //first get user entity and assign administrator role
                var user = userService.FirstOrDefault(x => x.Email == email);
                if (user != null)
                {
                    roleService.SetUserRoles(user.Id, new[] {1});
                    user.FirstName = "RoastedMarketplace";
                    user.LastName = "Administrator";
                    user.Name = $"{user.FirstName} {user.LastName}";
                }

            }
            else
            {
                throw new RoastedMarketplaceException("Installation failed");
            }
        }

        /// <summary>
        /// Seed settings
        /// </summary>
        private void SeedSettings(string installDomain)
        {
            var settingService = DependencyResolver.Resolve<ISettingService>();

            //general settings
            settingService.Save(new GeneralSettings() {
                StoreDomain = installDomain,
                ApplicationCookieDomain = installDomain
            });

            //media settings
            settingService.Save(new MediaSettings() {

            });

            //system settings
            settingService.Save(new SystemSettings() {
                IsInstalled = true,
                MinimumLogLevel = LogLevel.Debug
            });

            //security settings
            settingService.Save(new SecuritySettings() {
                DefaultPasswordStorageFormat = PasswordFormat.Sha1Hashed
            });

            //user settings
            settingService.Save(new UserSettings() {
                UserRegistrationDefaultMode = RegistrationMode.WithActivationEmail,
                MaximumNumberOfVisibleNotifications = 10,
                AreUserNamesEnabled = true
            });

            //email sender settings
            settingService.Save(new EmailSenderSettings() {
                PasswordChangedEmailEnabled = true,
                SlaModifiedEmailToAdminEnabled = true,
                SlaViolatedEmailToAdminEnabled = true,
                TicketCreatedByAgentEmailEnabled = true,
                TicketCreatedByAgentEmailToAdminEnabled = false,
                TicketResolvedEmailToAdminEnabled = false,
                TicketResolvedEmailEnabled = false,
                TicketUpdatedEmailEnabled = true,
                TicketUpdatedEmailToAdminEnabled = false,
                TicketClosedEmailEnabled = true,
                TicketClosedEmailToAdminEnabled = false,
                TicketDeletedEmailEnabled = false,
                TicketDeletedEmailToAdminEnabled = true,
                TicketCreatedByUserEmailEnabled = true,
                TicketCreatedByUserEmailToAdminEnabled = true,
                UserRegisteredEmailToAdminEnabled = true,
                UserRegisteredEmailEnabled = true,
                UserActivationEmailEnabled = true,
                UserDeactivationEmailEnabled = true,
                UserDeletedEmailEnabled = true,
                UserDeletedEmailToAdminEnabled = false,
                UserDeactivationEmailToAdminEnabled = false,
            });

        }

        private void SeedNotificationEvents()
        {
            var notificationEventService = DependencyResolver.Resolve<INotificationEventService>();
            //get all events from notification event class. use reflection for easy insert
            var fieldInfos = typeof(NotificationEventNames).GetFields(BindingFlags.Public | BindingFlags.Static);
            foreach (var fi in fieldInfos)
            {
                if (!fi.IsLiteral || fi.IsInitOnly)
                    continue;
                //it's a constant
                var eventName = fi.GetRawConstantValue().ToString();
                notificationEventService.Insert(new NotificationEvent() {
                    EventName = eventName,
                    Enabled = true
                });
            }
        }

        private void SeedEmailTemplates(string adminEmail, string installDomain)
        {
            /*
            var emailAccountService = DependencyResolver.Resolve<IEmailAccountService>();
            var emailTemplateService = DependencyResolver.Resolve<IEmailTemplateService>();
            var installEmailTemplatesPath = ServerHelper.GetLocalPathFromRelativePath("~/App_Data/Install/EmailTemplates/");
            //add email account
            var emailAccount = new EmailAccount() {
                Email = "support@" + installDomain,
                FromName = "RoastedMarketplace Helpdesk Software",
                Host = "",
                IsDefault = true,
                Port = 485,
                UseDefaultCredentials = true,
                UseSsl = true,
                UserName = "user"
            };
            emailAccountService.Insert(emailAccount);

            var masterTemplate = new EmailTemplate() {
                AdministrationEmail = adminEmail,
                IsMaster = true,
                Subject = "Master Template",
                TemplateSystemName = EmailTemplateNames.Master,
                TemplateName = "Master",
                IsSystem = true,
                EmailAccountId = emailAccount.Id,
                Template = ReadEmailTemplate(installEmailTemplatesPath, EmailTemplateNames.Master)
            };
            emailTemplateService.Insert(masterTemplate);

            emailTemplateService.Insert(new EmailTemplate() {
                AdministrationEmail = adminEmail,
                IsMaster = false,
                Subject = "Your account has been created",
                TemplateSystemName = EmailTemplateNames.UserRegisteredMessage,
                TemplateName = "User Registered",
                IsSystem = true,
                EmailAccountId = emailAccount.Id,
                Template = ReadEmailTemplate(installEmailTemplatesPath, EmailTemplateNames.UserRegisteredMessage),
                ParentEmailTemplateId = masterTemplate.Id
            });

            emailTemplateService.Insert(new EmailTemplate() {
                AdministrationEmail = adminEmail,
                IsMaster = false,
                Subject = "A new user has registered",
                TemplateSystemName = EmailTemplateNames.UserRegisteredMessageToAdmin,
                TemplateName = "User Registered Administrator",
                IsSystem = true,
                EmailAccountId = emailAccount.Id,
                Template = ReadEmailTemplate(installEmailTemplatesPath, EmailTemplateNames.UserRegisteredMessageToAdmin),
                ParentEmailTemplateId = masterTemplate.Id
            });

            emailTemplateService.Insert(new EmailTemplate() {
                AdministrationEmail = adminEmail,
                IsMaster = false,
                Subject = "Your account has been activated",
                TemplateSystemName = EmailTemplateNames.UserActivatedMessage,
                TemplateName = "User Activated",
                IsSystem = true,
                EmailAccountId = emailAccount.Id,
                Template = ReadEmailTemplate(installEmailTemplatesPath, EmailTemplateNames.UserActivatedMessage),
                ParentEmailTemplateId = masterTemplate.Id
            });

            emailTemplateService.Insert(new EmailTemplate() {
                AdministrationEmail = adminEmail,
                IsMaster = false,
                Subject = "Activate your account",
                TemplateSystemName = EmailTemplateNames.UserActivationLinkMessage,
                TemplateName = "User Activation Link",
                IsSystem = true,
                EmailAccountId = emailAccount.Id,
                Template = ReadEmailTemplate(installEmailTemplatesPath, EmailTemplateNames.UserActivationLinkMessage),
                ParentEmailTemplateId = masterTemplate.Id
            });

            emailTemplateService.Insert(new EmailTemplate() {
                AdministrationEmail = adminEmail,
                IsMaster = false,
                Subject = "We have received a password reset request",
                TemplateSystemName = EmailTemplateNames.PasswordRecoveryLinkMessage,
                TemplateName = "Password Recovery Link",
                IsSystem = true,
                EmailAccountId = emailAccount.Id,
                Template = ReadEmailTemplate(installEmailTemplatesPath, EmailTemplateNames.PasswordRecoveryLinkMessage),
                ParentEmailTemplateId = masterTemplate.Id
            });

            emailTemplateService.Insert(new EmailTemplate() {
                AdministrationEmail = adminEmail,
                IsMaster = false,
                Subject = "Your password has been changed",
                TemplateSystemName = EmailTemplateNames.PasswordChangedMessage,
                TemplateName = "Password Changed",
                IsSystem = true,
                EmailAccountId = emailAccount.Id,
                Template = ReadEmailTemplate(installEmailTemplatesPath, EmailTemplateNames.PasswordChangedMessage),
                ParentEmailTemplateId = masterTemplate.Id
            });

            emailTemplateService.Insert(new EmailTemplate() {
                AdministrationEmail = adminEmail,
                IsMaster = false,
                Subject = "Your account has been deactivated",
                TemplateSystemName = EmailTemplateNames.UserDeactivatedMessage,
                TemplateName = "User Account Deactivated",
                IsSystem = true,
                EmailAccountId = emailAccount.Id,
                Template = ReadEmailTemplate(installEmailTemplatesPath, EmailTemplateNames.UserDeactivatedMessage),
                ParentEmailTemplateId = masterTemplate.Id
            });

            emailTemplateService.Insert(new EmailTemplate() {
                AdministrationEmail = adminEmail,
                IsMaster = false,
                Subject = "Your account has been deactivated",
                TemplateSystemName = EmailTemplateNames.UserDeactivatedMessageToAdmin,
                TemplateName = "User Account Deactivated Administrator",
                IsSystem = true,
                EmailAccountId = emailAccount.Id,
                Template = ReadEmailTemplate(installEmailTemplatesPath, EmailTemplateNames.UserDeactivatedMessageToAdmin),
                ParentEmailTemplateId = masterTemplate.Id
            });

            emailTemplateService.Insert(new EmailTemplate() {
                AdministrationEmail = adminEmail,
                IsMaster = false,
                Subject = "Your account has been deleted",
                TemplateSystemName = EmailTemplateNames.UserAccountDeletedMessage,
                TemplateName = "User Account Deleted",
                IsSystem = true,
                EmailAccountId = emailAccount.Id,
                Template = ReadEmailTemplate(installEmailTemplatesPath, EmailTemplateNames.UserAccountDeletedMessage),
                ParentEmailTemplateId = masterTemplate.Id
            });

            emailTemplateService.Insert(new EmailTemplate() {
                AdministrationEmail = adminEmail,
                IsMaster = false,
                Subject = "A user account has been deleted",
                TemplateSystemName = EmailTemplateNames.UserAccountDeletedMessageToAdmin,
                TemplateName = "User Account Deleted Administrator",
                IsSystem = true,
                EmailAccountId = emailAccount.Id,
                Template = ReadEmailTemplate(installEmailTemplatesPath, EmailTemplateNames.UserAccountDeletedMessageToAdmin),
                ParentEmailTemplateId = masterTemplate.Id
            });

            emailTemplateService.Insert(new EmailTemplate() {
                AdministrationEmail = adminEmail,
                IsMaster = false,
                Subject = "We've received your query",
                TemplateSystemName = EmailTemplateNames.TicketCreatedMessage,
                TemplateName = "Ticket Created",
                IsSystem = true,
                EmailAccountId = emailAccount.Id,
                Template = ReadEmailTemplate(installEmailTemplatesPath, EmailTemplateNames.TicketCreatedMessage),
                ParentEmailTemplateId = masterTemplate.Id
            });

            emailTemplateService.Insert(new EmailTemplate() {
                AdministrationEmail = adminEmail,
                IsMaster = false,
                Subject = "We've received your query",
                TemplateSystemName = EmailTemplateNames.TicketCreatedMessageToAdmin,
                TemplateName = "Ticket Created Administrator",
                IsSystem = true,
                EmailAccountId = emailAccount.Id,
                Template = ReadEmailTemplate(installEmailTemplatesPath, EmailTemplateNames.TicketCreatedMessageToAdmin),
                ParentEmailTemplateId = masterTemplate.Id
            });

            emailTemplateService.Insert(new EmailTemplate() {
                AdministrationEmail = adminEmail,
                IsMaster = false,
                Subject = "Your ticket {{Ticket.Id}} has been updated",
                TemplateSystemName = EmailTemplateNames.TicketUpdatedMessage,
                TemplateName = "Ticket Updated",
                IsSystem = true,
                EmailAccountId = emailAccount.Id,
                Template = ReadEmailTemplate(installEmailTemplatesPath, EmailTemplateNames.TicketUpdatedMessage),
                ParentEmailTemplateId = masterTemplate.Id
            });

            emailTemplateService.Insert(new EmailTemplate() {
                AdministrationEmail = adminEmail,
                IsMaster = false,
                Subject = "Ticket {{Ticket.Id}} has been updated",
                TemplateSystemName = EmailTemplateNames.TicketUpdatedMessageToAdmin,
                TemplateName = "Ticket Updated Administrator",
                IsSystem = true,
                EmailAccountId = emailAccount.Id,
                Template = ReadEmailTemplate(installEmailTemplatesPath, EmailTemplateNames.TicketUpdatedMessageToAdmin),
                ParentEmailTemplateId = masterTemplate.Id
            });

            emailTemplateService.Insert(new EmailTemplate() {
                AdministrationEmail = adminEmail,
                IsMaster = false,
                Subject = "Your ticket {{Ticket.Id}} has been closed",
                TemplateSystemName = EmailTemplateNames.TicketClosedMessage,
                TemplateName = "Ticket Closed",
                IsSystem = true,
                EmailAccountId = emailAccount.Id,
                Template = ReadEmailTemplate(installEmailTemplatesPath, EmailTemplateNames.TicketClosedMessage),
                ParentEmailTemplateId = masterTemplate.Id
            });

            emailTemplateService.Insert(new EmailTemplate() {
                AdministrationEmail = adminEmail,
                IsMaster = false,
                Subject = "Ticket {{Ticket.Id}} has been closed",
                TemplateSystemName = EmailTemplateNames.TicketClosedMessageToAdmin,
                TemplateName = "Ticket Closed Administrator",
                IsSystem = true,
                EmailAccountId = emailAccount.Id,
                Template = ReadEmailTemplate(installEmailTemplatesPath, EmailTemplateNames.TicketClosedMessageToAdmin),
                ParentEmailTemplateId = masterTemplate.Id
            });

            emailTemplateService.Insert(new EmailTemplate() {
                AdministrationEmail = adminEmail,
                IsMaster = false,
                Subject = "Your ticket {{Ticket.Id}} has been resolved",
                TemplateSystemName = EmailTemplateNames.TicketResolvedMessage,
                TemplateName = "Ticket Resolved",
                IsSystem = true,
                EmailAccountId = emailAccount.Id,
                Template = ReadEmailTemplate(installEmailTemplatesPath, EmailTemplateNames.TicketResolvedMessage),
                ParentEmailTemplateId = masterTemplate.Id
            });

            emailTemplateService.Insert(new EmailTemplate() {
                AdministrationEmail = adminEmail,
                IsMaster = false,
                Subject = "Ticket {{Ticket.Id}} has been resolved",
                TemplateSystemName = EmailTemplateNames.TicketResolvedMessageToAdmin,
                TemplateName = "Ticket Resolved Administrator",
                IsSystem = true,
                EmailAccountId = emailAccount.Id,
                Template = ReadEmailTemplate(installEmailTemplatesPath, EmailTemplateNames.TicketResolvedMessageToAdmin),
                ParentEmailTemplateId = masterTemplate.Id
            });

            emailTemplateService.Insert(new EmailTemplate() {
                AdministrationEmail = adminEmail,
                IsMaster = false,
                Subject = "Your ticket {{Ticket.Id}} has been deleted",
                TemplateSystemName = EmailTemplateNames.TicketDeletedMessage,
                TemplateName = "Ticket Deleted",
                IsSystem = true,
                EmailAccountId = emailAccount.Id,
                Template = ReadEmailTemplate(installEmailTemplatesPath, EmailTemplateNames.TicketDeletedMessage),
                ParentEmailTemplateId = masterTemplate.Id
            });

            emailTemplateService.Insert(new EmailTemplate() {
                AdministrationEmail = adminEmail,
                IsMaster = false,
                Subject = "Ticket {{Ticket.Id}} has been deleted",
                TemplateSystemName = EmailTemplateNames.TicketDeletedMessageToAdmin,
                TemplateName = "Ticket Deleted Administrator",
                IsSystem = true,
                EmailAccountId = emailAccount.Id,
                Template = ReadEmailTemplate(installEmailTemplatesPath, EmailTemplateNames.TicketDeletedMessageToAdmin),
                ParentEmailTemplateId = masterTemplate.Id
            });

            emailTemplateService.Insert(new EmailTemplate() {
                AdministrationEmail = adminEmail,
                IsMaster = false,
                Subject = "Ticket {{Ticket.Id}} has violated SLA",
                TemplateSystemName = EmailTemplateNames.SlaViolatedMessage,
                TemplateName = "Sla Violated",
                IsSystem = true,
                EmailAccountId = emailAccount.Id,
                Template = ReadEmailTemplate(installEmailTemplatesPath, EmailTemplateNames.SlaViolatedMessage),
                ParentEmailTemplateId = masterTemplate.Id
            });

            emailTemplateService.Insert(new EmailTemplate() {
                AdministrationEmail = adminEmail,
                IsMaster = false,
                Subject = "Ticket {{Ticket.Id}} has violated SLA",
                TemplateSystemName = EmailTemplateNames.SlaViolatedMessageToAdmin,
                TemplateName = "Sla Violated Administrator",
                IsSystem = true,
                EmailAccountId = emailAccount.Id,
                Template = ReadEmailTemplate(installEmailTemplatesPath, EmailTemplateNames.SlaViolatedMessageToAdmin),
                ParentEmailTemplateId = masterTemplate.Id
            });

            emailTemplateService.Insert(new EmailTemplate() {
                AdministrationEmail = adminEmail,
                IsMaster = false,
                Subject = "URGENT! Ticket {{Ticket.Id}} is about to violate SLA",
                TemplateSystemName = EmailTemplateNames.SlaViolationReminderMessage,
                TemplateName = "Sla Violation Reminder",
                IsSystem = true,
                EmailAccountId = emailAccount.Id,
                Template = ReadEmailTemplate(installEmailTemplatesPath, EmailTemplateNames.SlaViolationReminderMessage),
                ParentEmailTemplateId = masterTemplate.Id
            });

            emailTemplateService.Insert(new EmailTemplate() {
                AdministrationEmail = adminEmail,
                IsMaster = false,
                Subject = "An SLA has been modified",
                TemplateSystemName = EmailTemplateNames.SlaModifiedMessageToAdmin,
                TemplateName = "Sla Modified Administrator",
                IsSystem = true,
                EmailAccountId = emailAccount.Id,
                Template = ReadEmailTemplate(installEmailTemplatesPath, EmailTemplateNames.SlaModifiedMessageToAdmin),
                ParentEmailTemplateId = masterTemplate.Id
            });*/
        }

        #region Helper
        private string ReadEmailTemplate(string path, string templateName)
        {
            var filePath = path + templateName + ".html";
            return File.Exists(filePath) ? File.ReadAllText(filePath) : string.Empty;
        }
        #endregion
    }
}