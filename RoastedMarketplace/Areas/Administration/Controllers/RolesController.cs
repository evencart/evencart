using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RoastedMarketplace.Areas.Administration.Models.Users;
using RoastedMarketplace.Data.Constants;
using RoastedMarketplace.Data.Entity.Users;
using RoastedMarketplace.Data.Extensions;
using RoastedMarketplace.Infrastructure.Helpers;
using RoastedMarketplace.Infrastructure.Mvc;
using RoastedMarketplace.Infrastructure.Mvc.Attributes;
using RoastedMarketplace.Infrastructure.Mvc.ModelFactories;
using RoastedMarketplace.Infrastructure.Routing;
using RoastedMarketplace.Infrastructure.Security.Attributes;
using RoastedMarketplace.Services.Serializers;
using RoastedMarketplace.Services.Users;

namespace RoastedMarketplace.Areas.Administration.Controllers
{
    public class RolesController : FoundationAdminController
    {
        private readonly IRoleService _roleService;
        private readonly ICapabilityService _capabilityService;
        private readonly IModelMapper _modelMapper;
        private readonly IDataSerializer _dataSerializer;
        public RolesController(IRoleService roleService, ICapabilityService capabilityService, IModelMapper modelMapper, IDataSerializer dataSerializer)
        {
            _roleService = roleService;
            _capabilityService = capabilityService;
            _modelMapper = modelMapper;
            _dataSerializer = dataSerializer;
        }

        [DualGet("", Name = AdminRouteNames.RolesList)]
        [CapabilityRequired(CapabilitySystemNames.ManageRoles)]
        public IActionResult RolesList()
        {
            var roles = _roleService.Get(x => true);
            var roleModels = roles.Select(x => _modelMapper.Map<RoleModel>(x)).ToList();
            return R.Success.With("roles", () => roleModels, () => _dataSerializer.Serialize(roleModels))
                .WithGridResponse(roleModels.Count, 1, roleModels.Count)
                .Result;
        }

        [DualPost("", Name = AdminRouteNames.SaveRole, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.ManageRoles)]
        [ValidateModelState(ModelType = typeof(RoleModel))]
        public IActionResult SaveRole(RoleModel roleModel)
        {
            var role = roleModel.Id > 0 ? _roleService.Get(roleModel.Id) : new Role();
            if (role == null)
                return NotFound();
            if (role.Id == 0)
            {
                //check if this system name is available?
                if (_roleService.Count(x => x.SystemName == roleModel.SystemName) > 0)
                {
                    return R.Fail.With("error", T("A role with this system name already exists")).Result;
                }
            }
            role.Name = roleModel.Name;
            role.IsActive = roleModel.IsActive;
            if (!role.IsSystemRole)
                role.SystemName = roleModel.SystemName;

            _roleService.InsertOrUpdate(role);

            //get valid capabilities
            var capabilityIds = roleModel.Capabilities?.Where(x => !x.IsNullEmptyOrWhiteSpace() && x.IsInteger())
                                    .Select(int.Parse)
                                    .ToArray() ?? new int[] { };
            //set the values
            _capabilityService.SetRoleCapabilities(role.Id, capabilityIds);
            return R.Success.With("id", role.Id).Result;
        }

        [DualPost("delete", Name = AdminRouteNames.DeleteRole)]
        [CapabilityRequired(CapabilitySystemNames.ManageRoles)]
        public IActionResult DeleteRole(int roleId)
        {
            Role role;
            if (roleId <= 0 || (role = _roleService.Get(roleId)) == null)
                return NotFound();

            if (role.IsSystemRole)
                return R.Fail.With("error", T("A system role can not be deleted")).Result;

            //delete the role
            _roleService.Delete(role);
            return R.Success.Result;
        }

        [DualGet("{roleId}", Name = AdminRouteNames.GetRole)]
        [CapabilityRequired(CapabilitySystemNames.ManageRoles)]
        public IActionResult RoleEditor(int roleId)
        {
            var role = roleId > 0 ? _roleService.Get(roleId) : new Role();
            if (role == null)
                return NotFound();
            var roleModel = _modelMapper.Map<RoleModel>(role);
            roleModel.Capabilities = role.Capabilities?.Select(x => x.Id.ToString()).ToList();
            var availableCapabilities = _capabilityService.Get(x => true).ToList();
            var availableCapabilitiesModel =
                SelectListHelper.GetSelectItemList(availableCapabilities, x => x.Id, x => x.Name);

            return R.Success.With("role", roleModel).With("availableCapabilities", availableCapabilitiesModel).Result;
        }
    }
}