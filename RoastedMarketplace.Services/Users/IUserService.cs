using System.Collections.Generic;
using RoastedMarketplace.Core.Services;
using RoastedMarketplace.Data.Entity.Users;

namespace RoastedMarketplace.Services.Users
{
    public interface IUserService : IFoundationEntityService<User>
    {
        IList<User> GetUsers(string searchText, int[] restrictToRoles, int page, int count, out int totalMatches);

        User GetByUserInfo(string email, string guid = null);
    }
}