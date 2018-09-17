using System.Collections.Generic;
using RoastedMarketplace.Core.Services;
using RoastedMarketplace.Data.Entity.Users;

namespace RoastedMarketplace.Services.Users
{
    public class CapabilityService : FoundationEntityService<Capability>, ICapabilityService
    {
        public IList<Capability> GetByRole(int roleId)
        {
            throw new System.NotImplementedException();
        }

        public IList<Capability> GetByRolesConsolidated(int[] roleIds)
        {
            throw new System.NotImplementedException();
        }
    }
}