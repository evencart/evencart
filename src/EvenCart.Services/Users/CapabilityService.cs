using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Users;

namespace EvenCart.Services.Users
{
    public class CapabilityService : FoundationEntityService<Capability>, ICapabilityService
    {
        private readonly IRoleCapabilityService _roleCapabilityService;
        private readonly IUserCapabilityService _userCapabilityService;
        public CapabilityService(IRoleCapabilityService roleCapabilityService, IUserCapabilityService userCapabilityService)
        {
            _roleCapabilityService = roleCapabilityService;
            _userCapabilityService = userCapabilityService;
        }

        public IEnumerable<Capability> GetByRole(int roleId)
        {
            Expression<Func<RoleCapability, bool>> rcWhere = rc => rc.RoleId == roleId;
            return Repository.Join<RoleCapability>("Id", "CapabilityId")
                .Where(rcWhere)
                .SelectNested();
        }

        public IEnumerable<Capability> GetByRolesConsolidated(int[] roleIds)
        {
            if (!roleIds.Any())
                return null;
            var roleIdsAsList = roleIds.ToList();
            Expression<Func<RoleCapability, bool>> rcWhere = rc => roleIdsAsList.Contains(rc.RoleId);
            return Repository.Join<RoleCapability>("Id", "CapabilityId")
                .Where(rcWhere)
                .SelectNested()
                .Distinct();
        }

        public IEnumerable<Capability> GetByUser(int userId)
        {
            var capabilityIds = _userCapabilityService.Get(x => x.UserId == userId).Select(x => x.CapabilityId)
                .ToList();
            if (!capabilityIds.Any())
                return null;
            return Get(x => capabilityIds.Contains(x.Id));

        }

        public void SetRoleCapabilities(int roleId, int[] capabilityIds)
        {
            //if there are no caps, it means all caps have been removed
            if (capabilityIds == null || !capabilityIds.Any())
            {
                _roleCapabilityService.Delete(x => x.RoleId == roleId);
                return;
            }
            //get all the caps of current role
            var roleCapabilities = _roleCapabilityService.Get(x => x.RoleId == roleId).ToList();
            var newCaps = new List<RoleCapability>();
            foreach (var cid in capabilityIds)
            {
                //if the cap is already there, no need to proceed
                if (roleCapabilities.Any(x => x.CapabilityId == cid))
                    continue;
                //add this new role
                newCaps.Add(new RoleCapability()
                {
                    RoleId = roleId,
                    CapabilityId = cid
                });
            }
            //insert new roles
            _roleCapabilityService.Insert(newCaps.ToArray());
            //delete other roles
            foreach (var capToRemove in roleCapabilities.Where(x => !capabilityIds.Contains(x.CapabilityId)))
                _roleCapabilityService.Delete(capToRemove);
        }

        public void SetUserCapabilities(int userId, int[] capabilityIds)
        {
            //if there are no caps, it means all caps have been removed
            if (capabilityIds == null || !capabilityIds.Any())
            {
                _userCapabilityService.Delete(x => x.UserId == userId);
                return;
            }
            //get all capabilities of current user
            var userCapabilities = _userCapabilityService.Get(x => x.UserId == userId).ToList();
            foreach (var userCapability in userCapabilities)
            {
                if(!capabilityIds.Contains(userCapability.CapabilityId))
                    _userCapabilityService.Delete(userCapability);
            }

            foreach (var id in capabilityIds)
            {
                if (userCapabilities.All(x => x.CapabilityId != id))
                {
                    _userCapabilityService.Insert(new UserCapability() { UserId = userId, CapabilityId = id});
                }
            }
        }
    }
}