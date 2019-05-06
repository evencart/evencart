using System.Collections.Generic;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Users;

namespace EvenCart.Services.Users
{
    public interface ICapabilityService : IFoundationEntityService<Capability>
    {
        IEnumerable<Capability> GetByRole(int roleId);

        IEnumerable<Capability> GetByRolesConsolidated(int[] roleIds);

        IEnumerable<Capability> GetByUser(int userId);

        void SetRoleCapabilities(int roleId, int[] capabilityIds);

        void SetUserCapabilities(int userId, int[] capabilityIds);
    }
}