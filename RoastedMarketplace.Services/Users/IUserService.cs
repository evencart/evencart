using System.Collections.Generic;
using RoastedMarketplace.Core.Services;
using RoastedMarketplace.Data.Entity.Users;

namespace RoastedMarketplace.Services.Users
{
    public interface IUserService : IFoundationEntityService<User>
    {
        IList<User> SearchUsers(string searchText, bool excludeLoggedInUser, int page, int count);

        IList<User> SearchUsers(string searchText, bool excludeLoggedInUser, string[] restrictToRoles, int page, int count);

        IList<User> SearchUsers(string searchText, bool excludeLoggedInUser, int[] restrictToRoles, int page, int count);

        User GetByUserInfo(string email, string guid = null);
    }
}