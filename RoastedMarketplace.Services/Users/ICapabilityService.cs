using System.Collections.Generic;
using RoastedMarketplace.Core.Services;
using RoastedMarketplace.Data.Entity.Users;

namespace RoastedMarketplace.Services.Users
{
    public interface ICapabilityService : IFoundationEntityService<Capability>
    {
        IList<Capability> GetByRole(int roleId);

        IList<Capability> GetByRolesConsolidated(int[] roleIds);
    }
}