using System.Linq;
using RoastedMarketplace.Core.Infrastructure;
using RoastedMarketplace.Data.Entity.Users;
using RoastedMarketplace.Data.Interfaces;
using RoastedMarketplace.Services.Users;

namespace RoastedMarketplace.Services.Extensions
{
    public static class UserCapabilityExtensions
    {
        /// <summary>
        /// Checks if the user has, listed capabilities.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="capabilityName"></param>
        /// <returns>True if all the provided capabilities are possesed by user. False otherwise.</returns>
        public static bool Can(this User user, params string[] capabilityName)
        {
            if (user == null)
                return false;

            //if the user is administrator, he or she should have all capabilities by default
            if (user.IsAdministrator())
                return true;

            var roleCapabilityService = DependencyResolver.Resolve<IRoleCapabilityService>();
            
            //get user's role ids (only active roles)
            var roleIds = user.Roles.Where(x=>  x.IsActive).Select(x => x.Id);

            //and now the capabilities
            var capabilities = roleCapabilityService.GetConsolidatedCapabilities(roleIds.ToArray());

            return capabilities.Select(x => x.Name).Intersect(capabilityName).Count() == capabilityName.Length;
        }

        /// <summary>
        /// Checks if the user has either of listed capabilities
        /// </summary>
        /// <param name="user"></param>
        /// <param name="capabilityName"></param>
        /// <returns>True if at least one capability is possessed, false otherwise</returns>
        public static bool CanEither(this User user, params string[] capabilityName)
        {
            if (user == null)
                return false;

            //if the user is administrator, he or she should have all capabilities by default
            if (user.IsAdministrator())
                return true;

            var roleCapabilityService = DependencyResolver.Resolve<IRoleCapabilityService>();

            //get user's role ids (only active roles)
            var roleIds = user.Roles.Where(x => x.IsActive).Select(x => x.Id);

            //and now the capabilities
            var capabilities = roleCapabilityService.GetConsolidatedCapabilities(roleIds.ToArray());

            return capabilities.Select(x => x.Name).Intersect(capabilityName).Any();
        }

        public static bool IsAdministrator(this User user)
        {
            return user.Is(SystemRoleNames.Administrator);
        }

        public static bool IsVisitor(this User user)
        {
            return user == null || user.Is(SystemRoleNames.Visitor);
        }

        public static bool IsRegistered(this User user)
        {
            return user.Is(SystemRoleNames.Registered);
        }

        public static bool IsAgent(this User user)
        {
            return user.Is(SystemRoleNames.Vendor);
        }

        public static bool IsManager(this User user)
        {
            return user.Is(SystemRoleNames.Manager);
        }

        public static bool CanEditUser(this User user, User resource)
        {
            return user.IsAdministrator() || user.Id == resource.Id;
        }

        public static bool CanEditResource<T>(this User user, T resource) where T: IUserResource
        {
            var resourceName = typeof(T).Name;
            var insertCapabilityName = resourceName + "Insert";
            var updateCapabilityName = resourceName + "Update";

            return (user.IsAdministrator() 
                || resource.UserId == user.Id/*owner?*/ 
                || (resource.Id == 0 && user.CanEither(insertCapabilityName)) /*new resource?*/); /*agent and has capability to update?*/
        }
        /// <summary>
        /// Checks if the user holds the given role
        /// </summary>
        /// <param name="user"></param>
        /// <param name="roleSystemName"></param>
        /// <returns></returns>
        public static bool Is(this User user, string roleSystemName)
        {
            return user?.Roles != null && user.Roles.Any(x => x.SystemName == roleSystemName);
        }

        /// <summary>
        /// Checks if the user is one of the passed roles
        /// </summary>
        /// <param name="user"></param>
        /// <param name="roleSystemNames"></param>
        /// <returns></returns>
        public static bool IsOneOf(this User user, params string[] roleSystemNames)
        {
            return user?.Roles != null && user.Roles.Any(x => roleSystemNames.Contains(x.SystemName));
        }
    }
}