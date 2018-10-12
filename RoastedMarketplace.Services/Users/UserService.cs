using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DotEntity.Enumerations;
using RoastedMarketplace.Core.Services;
using RoastedMarketplace.Data.Entity.Users;
using RoastedMarketplace.Data.Extensions;

namespace RoastedMarketplace.Services.Users
{
    public class UserService : FoundationEntityService<User>, IUserService
    {
        public IList<User> GetUsers(string searchText, int[] restrictToRoles, int page, int count, out int totalMatches)
        {
            var query = Repository.Join<UserRole>("Id", "UserId", joinType: JoinType.LeftOuter)
                .Join<Role>("RoleId", "Id", joinType: JoinType.LeftOuter)
                .Relate(RelationTypes.OneToMany<User, Role>());

            if (!searchText.IsNullEmptyOrWhiteSpace())
            {
                searchText = searchText.Trim();
                query = query.Where(x => x.FirstName.Contains(searchText) ||
                                         x.LastName.Contains(searchText) ||
                                         x.LastLoginIpAddress.Contains(searchText) ||
                                         x.Email.Contains(searchText));
            }

            if (restrictToRoles != null && restrictToRoles.Any())
            {
                var roleIds = restrictToRoles.ToList();
                Expression<Func<Role, bool>> roleWhere = role => roleIds.Contains(role.Id);
                query = query.Where(roleWhere);
            }
            query = query.OrderBy(x => x.Name);
            return query.SelectNestedWithTotalMatches(out totalMatches, page, count).ToList();
        }

        public User GetByUserInfo(string email, string guid = null)
        {
            var userObject = GetByWhere(x => x.Email == email);
            return userObject?.Guid.ToString() != guid ? null : userObject;
        }

        public override User Get(int id)
        {
            return GetByWhere(x => x.Id == id);
        }

        private User GetByWhere(Expression<Func<User, bool>> where)
        {
            var query = Repository.Where(where)
                .Join<UserRole>("Id", "UserId", joinType: JoinType.LeftOuter)
                .Join<Role>("RoleId", "Id", joinType: JoinType.LeftOuter)
                .Relate(RelationTypes.OneToMany<User, Role>());

            var userObject = query.SelectNested().FirstOrDefault();
            return userObject;
        }
    }
}