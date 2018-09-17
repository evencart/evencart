using System.Collections.Generic;
using RoastedMarketplace.Core.Services;
using RoastedMarketplace.Data.Entity.Users;

namespace RoastedMarketplace.Services.Users
{
    public interface IRoleCapabilityService : IFoundationEntityService<RoleCapability>
    {
        IList<Capability> GetRoleCapabilities(string roleSystemName);

        IList<Capability> GetRoleCapabilities(int roleId);

        IList<Capability> GetConsolidatedCapabilities(int[] roleIds);

        IList<Capability> GetConsolidatedCapabilities(string[] roleSystemNames);

        void SetRoleCapabilities(int roleId, int[] capabilityIds, bool deleteOtherEntries = true);
    }
}