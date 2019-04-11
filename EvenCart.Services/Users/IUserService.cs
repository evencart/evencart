using System.Collections.Generic;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Users;

namespace EvenCart.Services.Users
{
    public interface IUserService : IFoundationEntityService<User>
    {
        IList<User> GetUsers(string searchText, int[] restrictToRoles, int page, int count, out int totalMatches);

        User GetByUserInfo(string email, string guid = null);

        void AnonymizeUser(int userId);
    }
}