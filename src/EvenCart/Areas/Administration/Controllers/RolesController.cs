#region License
// Copyright (c) Sojatia Infocrafts Private Limited.
// The following code is part of EvenCart eCommerce Software (https://evencart.co) Dual Licensed under the terms of
// 
// 1. GNU GPLv3 with additional terms (available to read at https://evencart.co/license)
// 2. EvenCart Proprietary License (available to read at https://evencart.co/license/commercial-license).
// 
// You can select one of the above two licenses according to your requirements. The usage of this code is
// subject to the terms of the license chosen by you.
#endregion

using System.Linq;
using EvenCart.Areas.Administration.Factories.Users;
using EvenCart.Areas.Administration.Models.Users;
using EvenCart.Data.Constants;
using EvenCart.Data.Entity.Users;
using EvenCart.Data.Extensions;
using EvenCart.Services.Serializers;
using EvenCart.Services.Users;
using EvenCart.Infrastructure.Helpers;
using EvenCart.Infrastructure.Mvc;
using EvenCart.Infrastructure.Mvc.Attributes;
using EvenCart.Infrastructure.Routing;
using EvenCart.Infrastructure.Security.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Areas.Administration.Controllers
{
    public class RolesController : FoundationAdminController
    {
        private readonly IRoleService _roleService;
        private readonly ICapabilityService _capabilityService;
        private readonly IDataSerializer _dataSerializer;
        private readonly IRoleModelFactory _roleModelFactory;
        public RolesController(IRoleService roleService, ICapabilityService capabilityService, IDataSerializer dataSerializer, IRoleModelFactory roleModelFactory)
        {
            _roleService = roleService;
            _capabilityService = capabilityService;
            _dataSerializer = dataSerializer;
            _roleModelFactory = roleModelFactory;
        }

        [DualGet("", Name = AdminRouteNames.RolesList)]
        [CapabilityRequired(CapabilitySystemNames.ManageRoles)]
        public IActionResult RolesList()
        {
            var roles = _roleService.Get(x => true);
            var roleModels = roles.Select(_roleModelFactory.Create).ToList();
            return R.Success.With("roles",roleModels)
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
            var roleModel = _roleModelFactory.Create(role);
            roleModel.Capabilities = role.Capabilities?.Select(x => x.Id.ToString()).ToList();
            var availableCapabilities = _capabilityService.Get(x => true).OrderBy(x => x.Name).ToList();
            var availableCapabilitiesModel =
                SelectListHelper.GetSelectItemList(availableCapabilities, x => x.Id, x => x.Name);

            return R.Success.With("role", roleModel).With("availableCapabilities", availableCapabilitiesModel).Result;
        }
    }
}