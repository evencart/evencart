using System.Collections.Generic;
using RoastedMarketplace.Core.Services;
using RoastedMarketplace.Data.Entity.Users;

namespace RoastedMarketplace.Services.Users
{
    public interface ICapabilityService : IFoundationEntityService<Capability>
    {
        IEnumerable<Capability> GetByRole(int roleId);

        IEnumerable<Capability> GetByRolesConsolidated(int[] roleIds);

        void SetRoleCapabilities(int roleId, int[] capabilityIds);
    }
}