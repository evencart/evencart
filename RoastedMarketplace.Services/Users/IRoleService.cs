using System.Collections.Generic;
using RoastedMarketplace.Core.Services;
using RoastedMarketplace.Data.Entity.Users;

namespace RoastedMarketplace.Services.Users
{
    public interface IRoleService : IFoundationEntityService<Role>
    {
        IList<Role> GetUserRoles(int userId);

        void SetUserRoles(int userId, int[] roleIds);
    }
}