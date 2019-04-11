using System.Linq;
using DotEntity.Enumerations;
using EvenCart.Areas.Administration.Factories.Gdpr;
using EvenCart.Areas.Administration.Models.Gdpr;
using EvenCart.Core.Services;
using EvenCart.Data.Constants;
using EvenCart.Data.Entity.Gdpr;
using EvenCart.Data.Extensions;
using EvenCart.Services.Extensions;
using EvenCart.Services.Gdpr;
using EvenCart.Services.Security;
using EvenCart.Services.Users;
using EvenCart.Infrastructure.Helpers;
using EvenCart.Infrastructure.Mvc;
using EvenCart.Infrastructure.Mvc.Attributes;
using EvenCart.Infrastructure.Mvc.ModelFactories;
using EvenCart.Infrastructure.Routing;
using EvenCart.Infrastructure.Security.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Areas.Administration.Controllers
{
    public class GdprController : FoundationAdminController
    {
        private const string DefaultGroupName = "Default";

        private readonly IConsentService _consentService;
        private readonly IGdprModelFactory _gdprModelFactory;
        private readonly IConsentGroupService _consentGroupService;
        private readonly IModelMapper _modelMapper;
        private readonly IConsentLogService _consentLogService;
        private readonly ICryptographyService _cryptographyService;
        private readonly IUserService _userService;

        public GdprController(IConsentService consentService, IGdprModelFactory gdprModelFactory, IConsentGroupService consentGroupService, IModelMapper modelMapper, IConsentLogService consentLogService, ICryptographyService cryptographyService, IUserService userService)
        {
            _consentService = consentService;
            _gdprModelFactory = gdprModelFactory;
            _consentGroupService = consentGroupService;
            _modelMapper = modelMapper;
            _consentLogService = consentLogService;
            _cryptographyService = cryptographyService;
            _userService = userService;
        }

        #region ConsentGroups
        [DualGet("consent-groups", Name = AdminRouteNames.ConsentGroupsList)]
        [CapabilityRequired(CapabilitySystemNames.ManageGdpr)]
        public IActionResult ConsentGroupsList()
        {
            var consentGroups = _consentGroupService.Get(out int totalResults, x => true, x => x.DisplayOrder);

            var models = consentGroups.Select(x => _modelMapper.Map<ConsentGroupModel>(x)).ToList();
            models.Insert(0, new ConsentGroupModel()
            {
                Name = T(DefaultGroupName),
                IsSystemGroup = true
            });
            return R.Success.With("consentGroups", models)
                .WithGridResponse(totalResults + 1, 1, models.Count)
                .Result;
        }

        [DualGet("consent-groups/{consentGroupId}", Name = AdminRouteNames.GetConsentGroup)]
        [CapabilityRequired(CapabilitySystemNames.ManageGdpr)]
        public IActionResult ConsentGroupEditor(int consentGroupId)
        {
            var consentGroup = consentGroupId > 0 ? _consentGroupService.Get(consentGroupId) : new ConsentGroup();
            if (consentGroup == null)
                return NotFound();
            var model = _modelMapper.Map<ConsentGroupModel>(consentGroup);
            return R.Success.With("consentGroup", model).Result;
        }

        [DualPost("consent-groups", Name = AdminRouteNames.SaveConsentGroup, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.ManageGdpr)]
        [ValidateModelState(ModelType = typeof(ConsentGroupModel))]
        public IActionResult SaveConsentGroup(ConsentGroupModel consentGroupModel)
        {
            var consentGroup = consentGroupModel.Id > 0 ? _consentGroupService.Get(consentGroupModel.Id) : new ConsentGroup();
            if (consentGroup == null)
                return NotFound();

            consentGroup.Name = consentGroupModel.Name;
            consentGroup.Description = consentGroupModel.Description;
            _consentGroupService.InsertOrUpdate(consentGroup);

            return R.Success.Result;
        }

        [DualPost("consent-groups/delete", Name = AdminRouteNames.DeleteConsentGroup, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.ManageGdpr)]
        public IActionResult DeleteConsentGroup(int consentGroupId)
        {
            var consentGroup = consentGroupId > 0 ? _consentGroupService.Get(consentGroupId) : null;
            if (consentGroup == null)
                return NotFound();

            return R.Success.Result;
        }

        [DualPost("consent-groups/display-order", Name = AdminRouteNames.UpdateConsentGroupDisplayOrder, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.ManageGdpr)]
        public IActionResult UpdateConsentGroupDisplayOrder(ConsentGroupModel[] groupModels)
        {
            if (groupModels == null)
                return BadRequest();
            //get category models with no-zero ids
            var validModels = groupModels.Where(x => x.Id != 0);
            Transaction.Initiate(transaction =>
            {
                foreach (var model in validModels)
                {
                    _consentGroupService.Update(new { DisplayOrder = model.DisplayOrder }, m => m.Id == model.Id,
                        transaction);
                }
            });

            return R.Success.Result;
        }

        #endregion

        #region Consents
        [DualGet("{consentGroupId}/consents", Name = AdminRouteNames.ConsentsList)]
        [CapabilityRequired(CapabilitySystemNames.ManageGdpr)]
        public IActionResult ConsentsList(int consentGroupId, ConsentSearchModel searchModel)
        {
            var group = consentGroupId > 0
                ? _consentGroupService.Get(consentGroupId)
                : new ConsentGroup()
                {
                    Name = T(DefaultGroupName)
                };
            if (group == null)
                return NotFound();
            var consents = _consentService.GetConsents(out int totalResults, consentGroupId, searchModel.SearchPhrase, searchModel.Current, searchModel.RowCount);

            var models = consents.Select(_gdprModelFactory.Create).ToList();
            var groupId = group.Id;
            var groupName = group.Name;

            return R.Success.With("consents", models)
                .WithGridResponse(totalResults, searchModel.Current, searchModel.RowCount)
                .With("consentGroupId", groupId)
                .With("consentGroupName", groupName)
                .Result;
        }

        [DualGet("{consentGroupId}/consents/{consentId}", Name = AdminRouteNames.GetConsent)]
        [CapabilityRequired(CapabilitySystemNames.ManageGdpr)]
        public IActionResult ConsentEditor(int consentGroupId, int consentId)
        {
            var consent = consentId > 0
                ? _consentService.Get(consentId)
                : new Consent()
                {
                    ConsentGroup = new ConsentGroup() { Id = consentGroupId },
                    ConsentGroupId = consentGroupId
                };
            if (consent == null || consent.ConsentGroupId != consentGroupId)
                return NotFound();
            var model = _gdprModelFactory.Create(consent);

            //get available groups
            var consentGroups = _consentGroupService.Get(x => true).OrderBy(x => x.Name).ToList();
            var consentGroupsSelectList = SelectListHelper.GetSelectItemList(consentGroups, x => x.Id, x => x.Name);
            return R.Success.With("consent", model).With("consentGroups", consentGroupsSelectList).Result;
        }

        [DualPost("consents", Name = AdminRouteNames.SaveConsent, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.ManageGdpr)]
        [ValidateModelState(ModelType = typeof(ConsentModel))]
        public IActionResult SaveConsent(ConsentModel consentModel)
        {
            var consent = consentModel.Id > 0 ? _consentService.Get(consentModel.Id) : new Consent();
            if (consent == null)
                return NotFound();

            consent.Title = consentModel.Title;
            consent.Description = consentModel.Description;
            consent.DisplayOrder = consentModel.DisplayOrder;
            consent.EnableLogging = consentModel.EnableLogging;
            consent.Published = consentModel.Published;
            consent.IsRequired = consentModel.IsRequired;
            consent.OneTimeSelection = consentModel.OneTimeSelection;
            consent.ConsentGroupId = consentModel.ConsentGroup?.Id ?? 0;
            if (consent.ConsentGroupId < 0)
                consent.ConsentGroupId = 0;
            _consentService.InsertOrUpdate(consent);

            return R.Success.Result;
        }

        [DualPost("consents/delete", Name = AdminRouteNames.DeleteConsent, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.ManageGdpr)]
        public IActionResult DeleteConsent(int consentId)
        {
            var consent = consentId > 0 ? _consentService.Get(consentId) : null;
            if (consent == null)
                return NotFound();
            _consentService.Delete(consent);
            return R.Success.Result;
        }

        [DualPost("consents/display-order", Name = AdminRouteNames.UpdateConsentDisplayOrder, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.ManageGdpr)]
        public IActionResult UpdateConsentDisplayOrder(ConsentModel[] consentModels)
        {
            if (consentModels == null)
                return BadRequest();
            //get category models with no-zero ids
            var validModels = consentModels.Where(x => x.Id != 0);
            Transaction.Initiate(transaction =>
            {
                foreach (var model in validModels)
                {
                    _consentService.Update(new { DisplayOrder = model.DisplayOrder }, m => m.Id == model.Id,
                        transaction);
                }
            });

            return R.Success.Result;
        }
        #endregion

        #region Consent Logs
        [DualGet("consent-logs/{userId}", Name = AdminRouteNames.ConsentLogsList)]
        [CapabilityRequired(CapabilitySystemNames.ManageGdpr)]
        public IActionResult ConsentLogsList(int userId, ConsentLogSearchModel searchModel)
        {
           //is the user valid?
           var user = userId > 0 ? _userService.Get(userId) : null;
           if (user == null)
               return NotFound();
            var consentLogs = _consentLogService.Get(out int totalResults, x => x.UserId == userId, x => x.Id, RowOrder.Ascending,
                searchModel.Current, searchModel.RowCount);
            var models = consentLogs.Select(_gdprModelFactory.Create).ToList();
            //We'll need to make sure that current admin can actually view the email
            var withUserInfo = false;
            if (CurrentUser.Can(CapabilitySystemNames.ManageGdprPrivate))
            {
                withUserInfo = true;
                foreach (var model in models)
                {
                    if (!model.UserInfo.IsNullEmptyOrWhiteSpace())
                        model.UserInfo = _cryptographyService.Decrypt(model.UserInfo);
                }
            }
            return R.Success.With("consentLogs", models)
                .With("withUserInfo", withUserInfo)
                .WithGridResponse(totalResults, searchModel.Current, searchModel.RowCount)
                .Result;
        }

        #endregion
    }
}