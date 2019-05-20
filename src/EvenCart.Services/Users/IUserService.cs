using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Users;
using EvenCart.Data.Enum;

namespace EvenCart.Services.Users
{
    public interface IUserService : IFoundationEntityService<User>
    {
        IList<User> GetUsers(string searchText, int[] restrictToRoles, Expression<Func<User, object>> orderBy, SortOrder sortOrder, int page, int count, out int totalMatches, bool negateRoleRestriction = false);

        User GetByUserInfo(string email, string guid = null);

        void AnonymizeUser(int userId);
    }
}