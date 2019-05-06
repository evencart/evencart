using System.Collections.Generic;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Users;

namespace EvenCart.Services.Users
{
    public interface IRoleService : IFoundationEntityService<Role>
    {
        IList<Role> GetUserRoles(int userId);

        void SetUserRoles(int userId, int[] roleIds);
    }
}