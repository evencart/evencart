using System.Collections.Generic;
using RoastedMarketplace.Core.Services;
using RoastedMarketplace.Data.Entity.Users;

namespace RoastedMarketplace.Services.Users
{
    public interface IRoleService : IFoundationEntityService<Role>
    {
        void AssignRoleToUser(Role role, User user);

        void AssignRoleToUser(string roleName, User user);

        void UnassignRoleToUser(Role role, User user);

        void UnassignRoleToUser(string roleName, User user);

        IList<Role> GetUserRoles(int userId);

        IList<Role> GetUserRoles(User user);

    }
}