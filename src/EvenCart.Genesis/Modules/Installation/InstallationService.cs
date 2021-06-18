﻿#region License
// Copyright (c) Sojatia Infocrafts Private Limited.
// The following code is part of EvenCart eCommerce Software (https://evencart.co) Dual Licensed under the terms of
// 
// 1. GNU GPLv3 with additional terms (available to read at https://evencart.co/license)
// 2. EvenCart Proprietary License (available to read at https://evencart.co/license/commercial-license).
// 
// You can select one of the above two licenses according to your requirements. The usage of this code is
// subject to the terms of the license chosen by you.
#endregion

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using DotEntity;
using Genesis;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Data.Entity.Settings;
using EvenCart.Data.Entity.Shop;
using EvenCart.Data.Enum;
using EvenCart.Genesis.Exceptions;
using Genesis.Modules.Emails;
using EvenCart.Services.Products;
using Genesis.Database;
using Genesis.Exceptions;
using Genesis.Infrastructure.IO;
using Genesis.Modules.Addresses;
using Genesis.Modules.Installation;
using Genesis.Modules.Localization;
using Genesis.Modules.Logging;
using Genesis.Modules.Notifications;
using Genesis.Modules.Security;
using Genesis.Modules.Settings;
using Genesis.Modules.Stores;
using Genesis.Modules.Users;
using UrlSettings = EvenCart.Data.Entity.Settings.UrlSettings;

namespace EvenCart.Services.Installation
{
    [AutoResolvable]
    public class InstallationService : IInstallationService
    {
        private readonly IDatabaseSettings _databaseSettings;
        public readonly ILocalFileProvider _localFileProvider;
        public readonly ILogger _logger;
        public InstallationService(IDatabaseSettings databaseSettings, ILocalFileProvider localFileProvider)
        {
            _databaseSettings = databaseSettings;
            _localFileProvider = localFileProvider;
            _logger = D.Resolve<ILogger>();
        }

        public void Install()
        {
            DatabaseManager.InitDatabase(_databaseSettings);
            DatabaseManager.ClearVersions();
            DatabaseManager.UpgradeDatabase(true);
        }

        private Store primaryStore;
        public void FillRequiredSeedData(string defaultUserEmail, string defaultUserPassword, string installDomain, string storeName)
        {
            //first create the store
            primaryStore = CreateStore(storeName);

            //first the settings
            SeedSettings(installDomain, storeName);

            //seed the roles
            SeedRoles();

            //then the user
            SeedDefaultUser(defaultUserEmail, defaultUserPassword);

            //seed email templates
            SeedEmailTemplates(defaultUserEmail, installDomain);

            //seed currency
            SeedCurrency();

            //seed catalog
            SeedCatalog();

            //seed language
            SeedLanguages();
            //seed notification events
            //SeedNotificationEvents();
            SeedCountries();
        }

        public bool InstallSamplePackage(string packageFilePath)
        {
            if (!File.Exists(packageFilePath))
                return false; // do nothing

            //get temporary directory to extract the package
            var tempDirectory = _localFileProvider.GetTemporaryDirectory();
            try
            {
                //first extract the zip
                _localFileProvider.ExtractArchive(packageFilePath, tempDirectory);

                //now copy the image files to the uploads directory
                var wwwroot = ServerHelper.MapPath("~/", true);
                var uploadsDirectory = _localFileProvider.CombinePaths(wwwroot, "content", "Uploads");
                var sourceFilesDirectory = _localFileProvider.CombinePaths(tempDirectory, "images");
                var files = _localFileProvider.GetFiles(sourceFilesDirectory);
                foreach (var file in files)
                {
                    //copy the file
                    var fileName = _localFileProvider.GetFileName(file);
                    _localFileProvider.CopyFile(file, Path.Combine(uploadsDirectory, fileName));
                }

                //sql path if any
                var sqlFile = _localFileProvider.CombinePaths(tempDirectory, "data.sql");
                if (File.Exists(sqlFile))
                {
                    var sql = _localFileProvider.ReadAllText(sqlFile);
                    //execute the query file
                    using (EntitySet.Query(sql, null))
                    {
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.Log<InstallationService>(LogLevel.Error, ex.Message, ex);
                return false;
            }
            finally
            {
                //delete the temporary directory
                _localFileProvider.DeleteDirectory(tempDirectory, true);
            }
        }

        public bool InstallSamplePackage(byte[] bytes)
        {
            //get a temporary file from provided bytes
            var fileName = _localFileProvider.GetTemporaryFile(bytes);
            try
            {
                return InstallSamplePackage(fileName);
            }
            catch (Exception ex)
            {
                _logger.Log<InstallationService>(LogLevel.Error, ex.Message, ex);
                return false;
            }
        }

        private Store CreateStore(string storeName)
        {
            var storeService = D.Resolve<IStoreService>();
            var store = new Store()
            {
                Name = storeName
            };
            storeService.Insert(store);
            return store;
        }

        private void SeedCountries()
        {
            var countryService = D.Resolve<ICountryService>();
            var warehouseService = D.Resolve<IWarehouseService>();
            var addressService = D.Resolve<IAddressService>();
            var localFileProvider = D.Resolve<ILocalFileProvider>();
            //read sql file and execute it
            var sqlFilePath = ServerHelper.MapPath("~/App_Data/Install/country-state.sql");
            var countryId = 0;
            if (localFileProvider.FileExists(sqlFilePath))
            {
                var sql = localFileProvider.ReadAllText(sqlFilePath);
                using (EntitySet.Query(sql, null)) { }
                //set to India - 101 from data file
                countryId = 101;
            }
            else
            {
                var country = new Country()
                {
                    Name = "India",
                    Code = "IN",
                    Published = true,
                    ShippingEnabled = true,
                    DisplayOrder = 0
                };
                countryService.Insert(country);
                countryId = country.Id;
            }
            var address = new Address()
            {
                EntityName = nameof(Warehouse),
                Name = "Primary Fulfillment Center",
                CountryId = countryId
            };
            addressService.Insert(address);
            //insert warehouse
            var wareHouse = new Warehouse()
            {
                AddressId = address.Id
            };
            warehouseService.Insert(wareHouse);
        }
        /// <summary>
        /// Seed roles
        /// </summary>
        private void SeedRoles()
        {
            var roleService = D.Resolve<IRoleService>();
            var capabilityService = D.Resolve<ICapabilityService>();
            var capabilityProvider = D.Resolve<ICapabilityProvider>();

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
            var allCapabilityFields = capabilityProvider.GetRawCapabilities();

            var savedCapabilities = new List<Capability>();
            foreach (var f in allCapabilityFields)
            {
                var capabilityName = f;
                var capability = new Capability()
                {
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
                capabilityService.SetRoleCapabilities(roleId, cIds.ToArray());
            }

        }

        private void SeedCurrency()
        {
            var currencyService = D.Resolve<ICurrencyService>();
            currencyService.Insert(new Currency()
            {
                Name = "US Dollar",
                CultureCode = "en-US",
                ExchangeRate = 1,
                Flag = "us.png",
                IsoCode = "USD",
                NumberOfDecimalPlaces = 2,
                RoundingType = Rounding.Default,
                Published = true
            });

            currencyService.Insert(new Currency()
            {
                Name = "Indian Rupee",
                CultureCode = "en-IN",
                ExchangeRate = 68,
                Flag = "in.png",
                IsoCode = "INR",
                NumberOfDecimalPlaces = 2,
                RoundingType = Rounding.Default,
                Published = true
            });
        }
        /// <summary>
        /// Seed default user
        /// </summary>
        private void SeedDefaultUser(string email, string password)
        {
            var userRegistrationService = D.Resolve<IUserRegistrationService>();

            var registrationResult = userRegistrationService.Register(email, password, PasswordFormat.Sha1Hashed);
            if (registrationResult == UserRegistrationStatus.Success)
            {
                //add roles
                var roleService = D.Resolve<IRoleService>();
                var userService = D.Resolve<IUserService>();

                //first get user entity and assign administrator role
                var user = userService.FirstOrDefault(x => x.Email == email);
                if (user != null)
                {
                    roleService.SetUserRoles(user.Id, new[] { 1 });
                    user.FirstName = "Administrator";
                    user.LastName = "";
                    user.Name = $"{user.FirstName} {user.LastName}".Trim();
                }

            }
            else
            {
                throw new EvenCartException("Installation failed");
            }
        }

        private void SeedLanguages()
        {
            var languageService = D.Resolve<ILanguageService>();
            languageService.Insert(new Language()
            {
                CultureCode = "en-IN",
                Name = "English",
                PrimaryLanguage = true,
                Published = true,
                Flag = "in.png",
                Rtl = true
            });
        }

        private void SeedCatalog()
        {
            var catalogService = D.Resolve<ICatalogService>();
            catalogService.Insert(new Catalog()
            {
                IsCountrySpecific = false,
                Name = "Primary Catalog",
                StoreIds = new List<int>() { 1 }
            });
        }

        /// <summary>
        /// Seed settings
        /// </summary>
        private void SeedSettings(string installDomain, string storeName)
        {
            var settingService = D.Resolve<ISettingService>();
            var cryptographyService = D.Resolve<ICryptographyService>();

            //general settings
            settingService.Save(new GeneralSettings()
            {
                StoreDomain = installDomain,
                StoreName = storeName,
                DefaultTimeZoneId = "UTC",
                EnableBreadcrumbs = true,
                LogoId = 0,
                PrimaryNavigationId = 0,
                ActiveTheme = "Default"
            }, primaryStore.Id);

            //media settings
            settingService.Save(new MediaSettings()
            {

            }, primaryStore.Id);

            //vendor settings
            settingService.Save(new VendorSettings()
            {
                EnableVendorSignup = true
            }, primaryStore.Id);



            //order settings
            settingService.Save(new OrderSettings()
            {
                AllowGuestCheckout = true,
                OrderNumberTemplate = "{UID}{YYYY:R}{MM:R}{DD:R}-{ID}",
                AllowReorder = true,
                EnableWishlist = true,
                AllowCancellation = true,
                CancellationAllowedFor = new List<string>()
                {
                    OrderStatus.New.ToString(),
                    OrderStatus.Processing.ToString(),
                    OrderStatus.OnHold.ToString(),
                    OrderStatus.Delayed.ToString()
                }
            }, primaryStore.Id);
            //tax settings
            settingService.Save(new TaxSettings()
            {
                DisplayProductPricesWithoutTax = false,
                ChargeTaxOnShipping = false,
                DefaultTaxRate = 0,
                PricesIncludeTax = true,
                ShippingPricesIncludeTax = true,
                ShippingTaxId = null
            }, primaryStore.Id);


            //system settings
            settingService.Save(new SystemSettings()
            {
                IsInstalled = true,
                MinimumLogLevel = LogLevel.Debug,
                LatestFetchedOn = DateTime.UtcNow.AddHours(-24),
                LatestUpdatesFetched = null
            }, primaryStore.Id);

            //security settings
            settingService.Save(new SecuritySettings()
            {
                DefaultPasswordStorageFormat = PasswordFormat.Sha1Hashed,
                HoneypotFieldName = "refcode-" + cryptographyService.GetRandomPassword(),
                EmailVerificationCodeExpirationMinutes = 5,
                EmailVerificationLinkExpirationHours = 24,
                InviteLinkExpirationHours = 48
            }, primaryStore.Id);

            //user settings
            settingService.Save(new UserSettings()
            {
                UserRegistrationDefaultMode = RegistrationMode.WithActivationEmail,
                AreUserNamesEnabled = true,
                AreProfilePicturesEnabled = true,
                ActivateUserForConnectedAccount = true
            }, primaryStore.Id);

            //email sender settings
            settingService.Save(new EmailSenderSettings()
            {
                PasswordChangedEmailEnabled = true,
                UserRegisteredEmailToAdminEnabled = true,
                UserRegisteredEmailEnabled = true,
                UserActivationEmailEnabled = true,
                UserDeactivationEmailEnabled = true,
                UserDeletedEmailEnabled = true,
                UserDeletedEmailToAdminEnabled = false,
                UserDeactivationEmailToAdminEnabled = false,
                OrderPaidEmailToAdminEnabled = true,
                OrderPlacedEmailEnabled = true,
                OrderPlacedEmailToAdminEnabled = true,
                ProductOutOfStockToAdminEnabled = true,
                OrderPaidEmailEnabled = false,
                ShipmentDeliveredEmailEnabled = false,
                ShipmentDeliveredEmailToAdminEnabled = true,
                ShipmentShippedEmailEnabled = true,
                DefaultEmailAccountId = 1
            }, primaryStore.Id);

            //url settings
            settingService.Save(new UrlSettings()
            {
                CategoryUrlTemplate = "/c/{CategoryPath}/{SeName}",
                ProductUrlTemplate = "/product/{SeName}",
                ContentPageUrlTemplate = "/{SeName}"
            }, primaryStore.Id);

            //catalog settings
            settingService.Save(new CatalogSettings()
            {
                EnableReviews = true,
                NumberOfReviewsToDisplayOnProductPage = 5,
                DisplayNameForPrivateReviews = $"{storeName} Customer",
                NumberOfRelatedProducts = 10,
                EnableReviewModeration = false,
                DisplaySortOptions = false,
                EnableRelatedProducts = true,
                AllowOneReviewPerUserPerItem = true,
                AllowReviewsForStorePurchaseOnly = false,
                DisplayProductsFromChildCategories = true,
                CatalogPaginationType = CatalogPaginationType.Numbered,
                NumberOfProductsPerPage = 15
            }, primaryStore.Id);

            //tax
            settingService.Save(new TaxSettings()
            {
                ShippingTaxId = null,
                ChargeTaxOnShipping = false,
                DefaultTaxRate = 0,
                DisplayProductPricesWithoutTax = false,
                PricesIncludeTax = true,
                ShippingPricesIncludeTax = true
            }, primaryStore.Id);

            settingService.Save(new LocalizationSettings()
            {
                AllowUserToSelectCurrency = true,
                AllowUserToSelectLanguage = false,
            }, primaryStore.Id);

            settingService.Save(new EmailSenderSettings()
            {
                DefaultEmailAccountId = 0,
                OrderPaidEmailEnabled = false,
                OrderPaidEmailToAdminEnabled = true,
                OrderPlacedEmailEnabled = true,
                OrderPlacedEmailToAdminEnabled = true,
                PasswordChangedEmailEnabled = true,
                ProductOutOfStockToAdminEnabled = true,
                ShipmentDeliveredEmailEnabled = true,
                ShipmentDeliveredEmailToAdminEnabled = false,
                ShipmentDeliveryFailedEmailEnabled = true,
                ShipmentDeliveryFailedToAdminEmailEnabled = true,
                ShipmentShippedEmailEnabled = true,
                UserActivationEmailEnabled = true,
                UserDeactivationEmailEnabled = false,
                UserDeactivationEmailToAdminEnabled = false,
                UserDeletedEmailEnabled = false,
                UserDeletedEmailToAdminEnabled = true,
                UserRegisteredEmailEnabled = true,
                UserRegisteredEmailToAdminEnabled = true
            }, primaryStore.Id);

            settingService.Save(new PluginSettings()
            {
                SitePlugins = "[]",
                SiteWidgets = "[]"
            }, 0);

            settingService.Save(new AffiliateSettings()
            {
                StoreCreditsExchangeRate = 1,
                MinimumStoreCreditsToAllowPurchases = 0,
                AllowStoreCreditsForPurchases = true,
                EnableAffiliates = false,
                AffiliateCookieExpirationDays = 1,
                AffiliateCookieName = "ref",
                AutoActivateAffiliateAccount = true,
                CommissionValue = 1,
                ExcludeTaxFromCalculation = true,
                SignupCreditToAffiliate = 0,
                SignupCreditToNewUser = 0,
                UseCommissionPercentage = true
            }, primaryStore.Id);
        }

        private void SeedNotificationEvents()
        {
            var notificationEventService = D.Resolve<INotificationEventService>();
            //get all events from notification event class. use reflection for easy insert
            var fieldInfos = typeof(NotificationEventNames).GetFields(BindingFlags.Public | BindingFlags.Static);
            foreach (var fi in fieldInfos)
            {
                if (!fi.IsLiteral || fi.IsInitOnly)
                    continue;
                //it's a constant
                var eventName = fi.GetRawConstantValue().ToString();
                notificationEventService.Insert(new NotificationEvent()
                {
                    EventName = eventName,
                    Enabled = true
                });
            }
        }

        private void SeedEmailTemplates(string adminEmail, string installDomain)
        {
            var emailAccountService = D.Resolve<IEmailAccountService>();
            var emailTemplateService = D.Resolve<IEmailTemplateService>();
            var installEmailTemplatesPath = ServerHelper.MapPath("~/App_Data/Install/EmailTemplates/");
            if (installDomain.StartsWith("//"))
                installDomain = installDomain.Substring(2);
            var emailAccount = new EmailAccount()
            {
                Email = "support@" + installDomain,
                FromName = "EvenCart",
                Host = "",
                Port = 485,
                UseDefaultCredentials = true,
                UseSsl = true,
                UserName = "user"
            };
            emailAccountService.Insert(emailAccount);

            var masterTemplate = new EmailTemplate()
            {
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

            emailTemplateService.Insert(new EmailTemplate()
            {
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

            emailTemplateService.Insert(new EmailTemplate()
            {
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

            emailTemplateService.Insert(new EmailTemplate()
            {
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

            emailTemplateService.Insert(new EmailTemplate()
            {
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

            emailTemplateService.Insert(new EmailTemplate()
            {
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

            emailTemplateService.Insert(new EmailTemplate()
            {
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

            emailTemplateService.Insert(new EmailTemplate()
            {
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

            emailTemplateService.Insert(new EmailTemplate()
            {
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

            emailTemplateService.Insert(new EmailTemplate()
            {
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

            emailTemplateService.Insert(new EmailTemplate()
            {
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

            emailTemplateService.Insert(new EmailTemplate()
            {
                AdministrationEmail = adminEmail,
                IsMaster = false,
                Subject = "Thank for your interest",
                TemplateSystemName = EmailTemplateNames.InvitationRequestedMessage,
                TemplateName = "Invitation Requested",
                IsSystem = true,
                EmailAccountId = emailAccount.Id,
                Template = ReadEmailTemplate(installEmailTemplatesPath, EmailTemplateNames.InvitationRequestedMessage),
                ParentEmailTemplateId = masterTemplate.Id
            });

            emailTemplateService.Insert(new EmailTemplate()
            {
                AdministrationEmail = adminEmail,
                IsMaster = false,
                Subject = "{{email}} has requested for an invitation",
                TemplateSystemName = EmailTemplateNames.InvitationRequestedMessageToAdmin,
                TemplateName = "Invitation Requested Administrator",
                IsSystem = true,
                EmailAccountId = emailAccount.Id,
                Template = ReadEmailTemplate(installEmailTemplatesPath, EmailTemplateNames.InvitationRequestedMessageToAdmin),
                ParentEmailTemplateId = masterTemplate.Id
            });

            emailTemplateService.Insert(new EmailTemplate()
            {
                AdministrationEmail = adminEmail,
                IsMaster = false,
                Subject = "We invite you to join {{store.name}}",
                TemplateSystemName = EmailTemplateNames.InvitationMessage,
                TemplateName = "Invitation",
                IsSystem = true,
                EmailAccountId = emailAccount.Id,
                Template = ReadEmailTemplate(installEmailTemplatesPath, EmailTemplateNames.InvitationMessage),
                ParentEmailTemplateId = masterTemplate.Id
            });

            emailTemplateService.Insert(new EmailTemplate()
            {
                AdministrationEmail = adminEmail,
                IsMaster = false,
                Subject = "Your recent order # {{order.orderNumber}}",
                TemplateSystemName = EmailTemplateNames.OrderPlacedMessage,
                TemplateName = "Order Placed",
                IsSystem = true,
                EmailAccountId = emailAccount.Id,
                Template = ReadEmailTemplate(installEmailTemplatesPath, EmailTemplateNames.OrderPlacedMessage),
                ParentEmailTemplateId = masterTemplate.Id
            });

            emailTemplateService.Insert(new EmailTemplate()
            {
                AdministrationEmail = adminEmail,
                IsMaster = false,
                Subject = "Order # {{order.orderNumber}} placed by {{user.name}}",
                TemplateSystemName = EmailTemplateNames.OrderPlacedMessageToAdmin,
                TemplateName = "Order Placed Administrator",
                IsSystem = true,
                EmailAccountId = emailAccount.Id,
                Template = ReadEmailTemplate(installEmailTemplatesPath, EmailTemplateNames.OrderPlacedMessageToAdmin),
                ParentEmailTemplateId = masterTemplate.Id
            });

            emailTemplateService.Insert(new EmailTemplate()
            {
                AdministrationEmail = adminEmail,
                IsMaster = false,
                Subject = "Your recent order # {{order.orderNumber}} has been paid",
                TemplateSystemName = EmailTemplateNames.OrderPaidMessage,
                TemplateName = "Order Paid",
                IsSystem = true,
                EmailAccountId = emailAccount.Id,
                Template = ReadEmailTemplate(installEmailTemplatesPath, EmailTemplateNames.OrderPaidMessage),
                ParentEmailTemplateId = masterTemplate.Id
            });

            emailTemplateService.Insert(new EmailTemplate()
            {
                AdministrationEmail = adminEmail,
                IsMaster = false,
                Subject = "Order # {{order.orderNumber}} placed by {{user.name}} has been paid",
                TemplateSystemName = EmailTemplateNames.OrderPaidMessageToAdmin,
                TemplateName = "Order Paid Administrator",
                IsSystem = true,
                EmailAccountId = emailAccount.Id,
                Template = ReadEmailTemplate(installEmailTemplatesPath, EmailTemplateNames.OrderPaidMessageToAdmin),
                ParentEmailTemplateId = masterTemplate.Id
            });

            emailTemplateService.Insert(new EmailTemplate()
            {
                AdministrationEmail = adminEmail,
                IsMaster = false,
                Subject = "Your order has been shipped",
                TemplateSystemName = EmailTemplateNames.ShipmentShippedMessage,
                TemplateName = "Order Shipped",
                IsSystem = true,
                EmailAccountId = emailAccount.Id,
                Template = ReadEmailTemplate(installEmailTemplatesPath, EmailTemplateNames.ShipmentShippedMessage),
                ParentEmailTemplateId = masterTemplate.Id
            });

            emailTemplateService.Insert(new EmailTemplate()
            {
                AdministrationEmail = adminEmail,
                IsMaster = false,
                Subject = "Your order has been delivered",
                TemplateSystemName = EmailTemplateNames.ShipmentDeliveredMessage,
                TemplateName = "Order Delivered",
                IsSystem = true,
                EmailAccountId = emailAccount.Id,
                Template = ReadEmailTemplate(installEmailTemplatesPath, EmailTemplateNames.ShipmentDeliveredMessage),
                ParentEmailTemplateId = masterTemplate.Id
            });

            emailTemplateService.Insert(new EmailTemplate()
            {
                AdministrationEmail = adminEmail,
                IsMaster = false,
                Subject = "An order has been delivered",
                TemplateSystemName = EmailTemplateNames.ShipmentDeliveredMessageToAdmin,
                TemplateName = "Order Delivered Administrator",
                IsSystem = true,
                EmailAccountId = emailAccount.Id,
                Template = ReadEmailTemplate(installEmailTemplatesPath, EmailTemplateNames.ShipmentDeliveredMessageToAdmin),
                ParentEmailTemplateId = masterTemplate.Id
            });

            emailTemplateService.Insert(new EmailTemplate()
            {
                AdministrationEmail = adminEmail,
                IsMaster = false,
                Subject = "The delivery of your recent order failed",
                TemplateSystemName = EmailTemplateNames.ShipmentDeliveryFailedMessage,
                TemplateName = "Order Delivery Failed",
                IsSystem = true,
                EmailAccountId = emailAccount.Id,
                Template = ReadEmailTemplate(installEmailTemplatesPath, EmailTemplateNames.ShipmentDeliveryFailedMessage),
                ParentEmailTemplateId = masterTemplate.Id
            });

            emailTemplateService.Insert(new EmailTemplate()
            {
                AdministrationEmail = adminEmail,
                IsMaster = false,
                Subject = "A recent order failed to deliver",
                TemplateSystemName = EmailTemplateNames.ShipmentDeliveryFailedMessageToAdmin,
                TemplateName = "Order Delivery Failed Administrator",
                IsSystem = true,
                EmailAccountId = emailAccount.Id,
                Template = ReadEmailTemplate(installEmailTemplatesPath, EmailTemplateNames.ShipmentDeliveryFailedMessageToAdmin),
                ParentEmailTemplateId = masterTemplate.Id
            });

            emailTemplateService.Insert(new EmailTemplate()
            {
                AdministrationEmail = adminEmail,
                IsMaster = false,
                Subject = "Return request for order # {{order.orderNumber}}",
                TemplateSystemName = EmailTemplateNames.ReturnRequestCreatedMessage,
                TemplateName = "Return Request Created",
                IsSystem = true,
                EmailAccountId = emailAccount.Id,
                Template = ReadEmailTemplate(installEmailTemplatesPath, EmailTemplateNames.ReturnRequestCreatedMessage),
                ParentEmailTemplateId = masterTemplate.Id
            });

            emailTemplateService.Insert(new EmailTemplate()
            {
                AdministrationEmail = adminEmail,
                IsMaster = false,
                Subject = "Return request for order # {{order.orderNumber}}",
                TemplateSystemName = EmailTemplateNames.ReturnRequestCreatedMessageToAdmin,
                TemplateName = "Return Request Created Administrator",
                IsSystem = true,
                EmailAccountId = emailAccount.Id,
                Template = ReadEmailTemplate(installEmailTemplatesPath, EmailTemplateNames.ReturnRequestCreatedMessageToAdmin),
                ParentEmailTemplateId = masterTemplate.Id
            });

            emailTemplateService.Insert(new EmailTemplate()
            {
                AdministrationEmail = adminEmail,
                IsMaster = false,
                Subject = "Contact Form Query - {{contact.subject}}",
                TemplateSystemName = EmailTemplateNames.ContactUsMessageToAdmin,
                TemplateName = "Contact Form Query Administrator",
                IsSystem = true,
                EmailAccountId = emailAccount.Id,
                Template = ReadEmailTemplate(installEmailTemplatesPath, EmailTemplateNames.ContactUsMessageToAdmin),
                ParentEmailTemplateId = masterTemplate.Id
            });

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