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

using System.Collections.Generic;
using System.Linq;
using DotEntity;
using DotEntity.Enumerations;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Users;
using EvenCart.Data.Extensions;

namespace EvenCart.Services.Users
{
    public class RoleService : FoundationEntityService<Role>, IRoleService
    {
        private readonly IUserRoleService _userRoleService;
        public RoleService(IUserRoleService userRoleService)
        {
            _userRoleService = userRoleService;
        }

        public IList<Role> GetUserRoles(int userId)
        {
            var userRoles = EntitySet<UserRole>.Where(x => x.UserId == userId)
                .Join<Role>("RoleId", "Id")
                .Relate<Role>((userRole, role) =>
                {
                    userRole.Role = role;
                })
                .SelectNested()
                .Select(x => x.Role)
                .ToList();
            return userRoles;
        }

        public void SetUserRoles(int userId, int[] roleIds, bool deletePreviousRoles = false)
        {
            //if there are no roles, it means all roles have been removed
            if (roleIds == null || !roleIds.Any())
            {
                if (deletePreviousRoles)
                {
                    _userRoleService.Delete(x => x.UserId == userId);
                    return;
                }
            }
            //get all the roles of current user
            var userRoles = _userRoleService.Get(x => x.UserId == userId).ToList();
            var newRoles = new List<UserRole>();
            foreach (var roleId in roleIds)
            {
                //if the role is already there, no need to proceed
                if (userRoles.Any(x => x.RoleId == roleId))
                    continue;
                //add this new role
                newRoles.Add(new UserRole()
                {
                    RoleId = roleId,
                    UserId = userId
                });
            }
            //insert new roles
            _userRoleService.Insert(newRoles.ToArray());
            if (deletePreviousRoles)
            {
                //delete other roles
                foreach (var roleToRemove in userRoles.Where(x => !roleIds.Contains(x.RoleId)))
                    _userRoleService.Delete(roleToRemove);
            }
        }

        public override Role Get(int id)
        {
            return Repository.Where(x => x.Id == id)
                .Join<RoleCapability>("Id", "RoleId", joinType: JoinType.LeftOuter)
                .Join<Capability>("CapabilityId", "Id", joinType: JoinType.LeftOuter)
                .Relate(RelationTypes.OneToMany<Role, Capability>())
                .SelectNested()
                .FirstOrDefault();
        }
    }
}