using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using RoastedMarketplace.Core.Services;
using RoastedMarketplace.Data.Entity.Users;

namespace RoastedMarketplace.Services.Users
{
    public class CapabilityService : FoundationEntityService<Capability>, ICapabilityService
    {
        private readonly IRoleCapabilityService _roleCapabilityService;
        public CapabilityService(IRoleCapabilityService roleCapabilityService)
        {
            _roleCapabilityService = roleCapabilityService;
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
            throw new System.NotImplementedException();
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
    }
}