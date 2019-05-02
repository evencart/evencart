using System;
using System.Linq;
using DotEntity.Enumerations;
using EvenCart.Areas.Administration.Models.Emails;
using EvenCart.Data.Constants;
using EvenCart.Data.Entity.Emails;
using EvenCart.Data.Entity.Settings;
using EvenCart.Data.Extensions;
using EvenCart.Services.Emails;
using EvenCart.Services.Security;
using EvenCart.Services.Serializers;
using EvenCart.Services.Settings;
using EvenCart.Infrastructure.Helpers;
using EvenCart.Infrastructure.Mvc;
using EvenCart.Infrastructure.Mvc.Attributes;
using EvenCart.Infrastructure.Mvc.ModelFactories;
using EvenCart.Infrastructure.Routing;
using EvenCart.Infrastructure.Security.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Areas.Administration.Controllers
{
    public class EmailsController : FoundationAdminController
    {
        private readonly IEmailTemplateService _emailTemplateService;
        private readonly IEmailAccountService _emailAccountService;
        private readonly IModelMapper _modelMapper;
        private readonly EmailSenderSettings _emailSenderSettings;
        private readonly ISettingService _settingService;
        private readonly IEmailSender _emailSender;
        private readonly IDataSerializer _dataSerializer;
        private readonly ICryptographyService _cryptographyService;
        private readonly IEmailService _emailService;
        public EmailsController(IEmailTemplateService emailTemplateService, IEmailAccountService emailAccountService, IModelMapper modelMapper, EmailSenderSettings emailSenderSettings, ISettingService settingService, IEmailSender emailSender, IDataSerializer dataSerializer, ICryptographyService cryptographyService, IEmailService emailService)
        {
            _emailTemplateService = emailTemplateService;
            _emailAccountService = emailAccountService;
            _modelMapper = modelMapper;
            _emailSenderSettings = emailSenderSettings;
            _settingService = settingService;
            _emailSender = emailSender;
            _dataSerializer = dataSerializer;
            _cryptographyService = cryptographyService;
            _emailService = emailService;
        }

        #region EmailTemplates
        [DualGet("emailtemplates", Name = AdminRouteNames.EmailTemplatesList)]
        [CapabilityRequired(CapabilitySystemNames.ManageEmailTemplates)]
        public IActionResult EmailTemplatesList(EmailTemplateSearchModel searchModel)
        {
            var emailTemplates = _emailTemplateService.Get(out int totalResults, x => true, page: searchModel.Current, count: searchModel.RowCount)
                .ToList();
            var models = emailTemplates.Select(x => _modelMapper.Map<EmailTemplateModel>(x)).ToList();
            return R.Success.With("emailTemplates", () => models, () => _dataSerializer.Serialize(models))
                .WithParams(searchModel)
                .WithGridResponse(totalResults, searchModel.Current, searchModel.RowCount)
                .Result;
        }

        [DualGet("emailtemplates/{emailTemplateId}", Name = AdminRouteNames.GetEmailTemplate)]
        [CapabilityRequired(CapabilitySystemNames.ManageEmailTemplates)]
        public IActionResult EmailTemplateEditor(int emailTemplateId)
        {
            var emailTemplate = emailTemplateId > 0 ? _emailTemplateService.Get(emailTemplateId) : null;
            if (emailTemplate == null)
                return NotFound();
            var model = _modelMapper.Map<EmailTemplateModel>(emailTemplate);
            if (emailTemplate.ParentEmailTemplate != null)
                model.ParentEmailTemplate = _modelMapper.Map<EmailTemplateModel>(emailTemplate.ParentEmailTemplate);

            model.EmailAccount = _modelMapper.Map<EmailAccountModel>(emailTemplate.EmailAccount);
            //available email accounts
            var emailAccounts = _emailAccountService.Get(x => true).OrderBy(x => x.Email).ToList();
            var availableEmailAccounts = SelectListHelper.GetSelectItemList(emailAccounts, x => x.Id, x => x.Email);

            //available master templates
            var masterTemplates = _emailTemplateService.Get(x => x.IsMaster).OrderBy(x => x.TemplateName).ToList();
            var availableMasterTemplates =
                SelectListHelper.GetSelectItemList(masterTemplates, x => x.Id, x => x.TemplateName);

            //available tokens
            var tokens = _emailTemplateService.GetTemplateTokens(emailTemplate.TemplateSystemName).OrderBy(x => x);
            return R.Success.With("emailTemplate", model)
                .With("availableEmailAccounts", availableEmailAccounts)
                .With("availableMasterTemplates", availableMasterTemplates)
                .With("availableTokens", tokens)
                .Result;
        }

        [DualPost("emailtemplates", Name = AdminRouteNames.SaveEmailTemplate, OnlyApi = true)]
        [ValidateModelState(ModelType = typeof(EmailTemplateModel))]
        [CapabilityRequired(CapabilitySystemNames.ManageEmailTemplates)]
        public IActionResult SaveEmailTemplate(EmailTemplateModel model)
        {
            var emailTemplate = model.Id > 0 ? _emailTemplateService.Get(model.Id) : new EmailTemplate();
            if (emailTemplate == null)
                return NotFound();

            if (emailTemplate.Id == 0 && _emailTemplateService.Count(x => x.TemplateSystemName == model.TemplateSystemName) > 0)
            {
                return R.Fail.With("error", T("A template with this system name already exists")).Result;
            }
            _modelMapper.Map(model, emailTemplate, nameof(EmailTemplate.Id), nameof(EmailTemplate.IsSystem));
            if (emailTemplate.Id == 0)
                emailTemplate.IsSystem = false;

            _emailTemplateService.InsertOrUpdate(emailTemplate);
            return R.Success.Result;
        }

        [DualPost("emailtemplates/delete", Name = AdminRouteNames.DeleteEmailTemplate, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.ManageEmailTemplates)]
        public IActionResult DeleteEmailTemplate(int id)
        {
            var emailTemplate = id > 0 ? _emailTemplateService.Get(id) : null;
            if (emailTemplate == null)
                return NotFound();

            if (emailTemplate.IsSystem)
            {
                return R.Fail.With("error", T("A system template can't be deleted")).Result;
            }
            _emailTemplateService.Delete(emailTemplate);
            return R.Success.Result;
        }
        #endregion

        #region Email Accounts
        [DualGet("emailaccounts", Name = AdminRouteNames.EmailAccountsList)]
        [CapabilityRequired(CapabilitySystemNames.ManageEmailAccounts)]
        public IActionResult EmailAccountsList()
        {
            var emailAccounts = _emailAccountService.Get(x => true)
                .ToList();
            var models = emailAccounts.Select(x =>
            {
                var model = _modelMapper.Map<EmailAccountModel>(x);
                model.IsDefault = _emailSenderSettings.DefaultEmailAccountId == x.Id;
                return model;
            }).ToList();
            return R.Success.With("emailAccounts", () => models, () => _dataSerializer.Serialize(models))
                .WithGridResponse(models.Count, 1, models.Count)
                .Result;
        }

        [DualGet("emailaccounts/{emailAccountId}", Name = AdminRouteNames.GetEmailAccount)]
        [CapabilityRequired(CapabilitySystemNames.ManageEmailAccounts)]
        public IActionResult EmailAccountEditor(int emailAccountId)
        {
            var emailAccount = emailAccountId > 0 ? _emailAccountService.Get(emailAccountId) : new EmailAccount();
            if (emailAccount == null)
                return NotFound();
            var model = _modelMapper.Map<EmailAccountModel>(emailAccount);
            model.IsDefault = _emailSenderSettings.DefaultEmailAccountId == emailAccount.Id;
            return R.Success.With("emailAccount", model).With("emailAccountId", emailAccountId).Result;
        }

        [DualPost("emailaccounts", Name = AdminRouteNames.SaveEmailAccount, OnlyApi = true)]
        [ValidateModelState(ModelType = typeof(EmailAccountModel))]
        [CapabilityRequired(CapabilitySystemNames.ManageEmailAccounts)]
        public IActionResult SaveEmailAccount(EmailAccountModel model)
        {
            var emailAccount = model.Id > 0 ? _emailAccountService.Get(model.Id) : new EmailAccount();
            if (emailAccount == null)
                return NotFound();

            _modelMapper.Map(model, emailAccount, nameof(EmailAccount.Id), nameof(EmailAccount.Password));
            if (!model.Password.IsNullEmptyOrWhiteSpace())
            {
                emailAccount.Password = _cryptographyService.Encrypt(model.Password);
            }
            _emailAccountService.InsertOrUpdate(emailAccount);
            if (model.IsDefault)
            {
                //mark all the others as non default
                _emailSenderSettings.DefaultEmailAccountId = emailAccount.Id;
                _settingService.Save(_emailSenderSettings);
            }
            return R.Success.Result;
        }

        [DualPost("emailaccounts/delete", Name = AdminRouteNames.DeleteEmailAccount, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.ManageEmailAccounts)]
        public IActionResult DeleteEmailAccount(int id)
        {
            var emailAccount = id > 0 ? _emailAccountService.Get(id) : null;
            if (emailAccount == null)
                return NotFound();

            _emailAccountService.Delete(emailAccount);
            return R.Success.Result;
        }

        [DualPost("emailaccounts/test", Name = AdminRouteNames.TestEmailAccount, OnlyApi = true)]
        [ValidateModelState(ModelType = typeof(EmailAccountModel))]
        [CapabilityRequired(CapabilitySystemNames.ManageEmailAccounts)]
        public IActionResult SendTestEmail(EmailAccountModel emailAccountModel)
        {
            var emailAccount = _modelMapper.Map<EmailAccount>(emailAccountModel);
            emailAccount.Id = emailAccountModel.Id;
            if (emailAccount.Password.IsNullEmptyOrWhiteSpace())
            {
                //if password is empty, we'll need to find saved password
                var savedEmailAccount = _emailAccountService.Get(emailAccount.Id);
                if (savedEmailAccount == null)
                    return R.Fail.Result;
                emailAccount.Password = savedEmailAccount.Password;
            }
            else
            {
                emailAccount.Password = _cryptographyService.Encrypt(emailAccount.Password);
            }
            var success = _emailSender.SendTestEmail(emailAccountModel.TestEmail, emailAccount, out Exception ex);
            if (success)
                return R.Success.Result;
            return R.Fail.With("error", ex.Message).Result;
        }
        #endregion

        #region Email Queue

        [DualGet("emailmessages", Name = AdminRouteNames.EmailMessagesList)]
        [CapabilityRequired(CapabilitySystemNames.ManageEmailMessage)]
        public IActionResult EmailMessagesList(EmailMessageSearchModel searchModel)
        {
            var emailMessages = _emailService.Get(out int totalResults, x => true, x => x.SendingDate,
                    RowOrder.Descending, searchModel.Current, searchModel.RowCount)
                .ToList();
            var models = emailMessages.Select(x =>
            {
                var model = _modelMapper.Map<EmailMessageModel>(x);
                model.Tos = x.Tos?.Select(y => $"{y.Name}({y.Email})").ToList();
                model.Ccs = x.Ccs?.Select(y => $"{y.Name}({y.Email})").ToList();
                model.Bccs = x.Bccs?.Select(y => $"{y.Name}({y.Email})").ToList();
                model.ReplyTos = x.ReplyTos?.Select(y => $"{y.Name}({y.Email})").ToList();
                return model;
            }).ToList();
            return R.Success.With("emailMessages", () => models, () => _dataSerializer.Serialize(models))
                .WithGridResponse(totalResults, searchModel.Current, searchModel.RowCount)
                .Result;
        }

        [DualGet("emailmessages/{emailMessageId}", Name = AdminRouteNames.GetEmailMessage)]
        [CapabilityRequired(CapabilitySystemNames.ManageEmailMessage)]
        public IActionResult EmailMessageEditor(int emailMessageId)
        {
            var emailMessage = emailMessageId > 0 ? _emailService.Get(emailMessageId) : null;
            if (emailMessage == null)
                return NotFound();
            var model = _modelMapper.Map<EmailMessageModel>(emailMessage);
            return R.Success.With("emailMessage", model).With("emailMessageId", emailMessageId).Result;
        }

        [DualPost("emailmessages", Name = AdminRouteNames.SaveEmailMessage, OnlyApi = true)]
        [ValidateModelState(ModelType = typeof(EmailMessageModel))]
        [CapabilityRequired(CapabilitySystemNames.ManageEmailMessage)]
        public IActionResult SaveEmailMessage(EmailMessageModel model)
        {
            var emailMessage = model.Id > 0 ? _emailService.Get(model.Id) : new EmailMessage();
            if (emailMessage == null)
                return NotFound();

            _modelMapper.Map(model, emailMessage, nameof(EmailMessage.Id));
            return R.Success.Result;
        }

        [DualPost("emailmessages/delete", Name = AdminRouteNames.DeleteEmailMessage, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.ManageEmailMessage)]
        public IActionResult DeleteEmailMessage(int emailMessageId)
        {
            var emailMessage = emailMessageId > 0 ? _emailService.Get(emailMessageId) : null;
            if (emailMessage == null)
                return NotFound();

            _emailService.Delete(emailMessage);
            return R.Success.Result;
        }
        #endregion
    }
}