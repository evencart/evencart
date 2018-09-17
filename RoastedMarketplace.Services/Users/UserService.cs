using System.Collections.Generic;
using System.Linq;
using RoastedMarketplace.Core.Services;
using RoastedMarketplace.Data.Entity.Users;
using RoastedMarketplace.Data.Extensions;

namespace RoastedMarketplace.Services.Users
{
    public class UserService : FoundationEntityService<User>, IUserService
    {
        public IList<User> SearchUsers(string searchText, bool excludeLoggedInUser, int page, int count)
        {
            throw new System.NotImplementedException();
        }

        public IList<User> SearchUsers(string searchText, bool excludeLoggedInUser, string[] restrictToRoles, int page, int count)
        {
            throw new System.NotImplementedException();
        }

        public IList<User> SearchUsers(string searchText, bool excludeLoggedInUser, int[] restrictToRoles, int page, int count)
        {
            throw new System.NotImplementedException();
        }

        public User GetByUserInfo(string email, string guid = null)
        {
            var query = Repository.Where(x => x.Email == email)
                .Join<UserRole>("Id", "UserId")
                .Join<Role>("RoleId", "Id")
                .Relate(RelationTypes.OneToMany<User, Role>());

            var userObject = query.SelectNested().FirstOrDefault();
          
            return userObject?.Guid.ToString() != guid ? null : userObject;
        }
    }
}