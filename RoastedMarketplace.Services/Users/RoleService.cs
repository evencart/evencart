using System.Collections.Generic;
using System.Linq;
using DotEntity;
using RoastedMarketplace.Core.Exception;
using RoastedMarketplace.Core.Services;
using RoastedMarketplace.Data.Entity.Users;

namespace RoastedMarketplace.Services.Users
{
    public class RoleService : FoundationEntityService<Role>, IRoleService
    {
        public void AssignRoleToUser(Role role, User user)
        {
            var isAlreadyAssigned = GetUserRoles(user).Any(x => x.Id == role.Id);
            if (isAlreadyAssigned)
                return;

            EntitySet<UserRole>.Insert(new UserRole()
            {
                RoleId = role.Id,
                UserId = user.Id
            });
        }

        public void AssignRoleToUser(string roleName, User user)
        {
            var role =
                Repository.Where(
                    x => x.SystemName == roleName)
                    .SelectSingle();

            if(role == null)
                throw new RoastedMarketplaceException(string.Format("The role with name '{0}' can't be found", roleName));

            AssignRoleToUser(role, user);
        }

        public void UnassignRoleToUser(Role role, User user)
        {
            var userRole = GetUserRoles(user).FirstOrDefault(x => x.Id == role.Id);
            if (userRole == null)
                return;

            EntitySet<UserRole>.Delete(x => x.UserId == user.Id && x.RoleId == role.Id);

        }

        public void UnassignRoleToUser(string roleName, User user)
        {
            var userRole = GetUserRoles(user).FirstOrDefault(x => x.SystemName == roleName);
            if (userRole == null)
                return;

            EntitySet<UserRole>.Delete(x => x.UserId == user.Id && x.Role.SystemName == roleName);
        }

        public IList<Role> GetUserRoles(int userId)
        {
            var userRoles = EntitySet<UserRole>.Where(x => x.UserId == userId)
                .Join<Role>("RoleId", "Id")
                .Relate<Role>((userRole, role) =>
                {
                    userRole.Role = role;
                })
                .SelectNested()
                .Select(x => x.Role)
                .ToList();
            return userRoles;
        }

        public IList<Role> GetUserRoles(User user)
        {
            return GetUserRoles(user.Id);
        }
    }
}