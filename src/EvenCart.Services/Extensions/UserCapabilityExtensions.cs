using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EvenCart.Core.Infrastructure;
using EvenCart.Data.Entity.Users;
using EvenCart.Data.Interfaces;
using EvenCart.Services.Users;

namespace EvenCart.Services.Extensions
{
    public static class UserCapabilityExtensions
    {
        /// <summary>
        /// Checks if the user has, listed capabilities.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="capabilityName"></param>
        /// <returns>True if all the provided capabilities are possessed by user. False otherwise.</returns>
        public static bool Can(this User user, params string[] capabilityName)
        {
            if (user?.Roles == null || !user.Roles.Any())
                return false;

            //if the user is administrator, he or she should have all capabilities by default
            if (user.IsAdministrator())
                return true;

            var capabilities = user.Capabilities;
            if (capabilities == null)
            {
                var capabilityService = DependencyResolver.Resolve<ICapabilityService>();
                //get user's role ids (only active roles)
                var roleIds = user.Roles.Where(x => x.IsActive).Select(x => x.Id);

                //and now the capabilities
                capabilities = capabilityService.GetByRolesConsolidated(roleIds.ToArray()).ToList();

                var result = capabilities.Select(x => x.Name).Intersect(capabilityName).Count() == capabilityName.Length;

                if (!result)
                {
                    //search for user specific caps
                    var userCapabilities = capabilityService.GetByUser(user.Id);
                    if (userCapabilities != null)
                        capabilities = capabilities.Concat(userCapabilities).ToList();
                }
            }
           
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

            var capabilityService = DependencyResolver.Resolve<ICapabilityService>();

            //get user's role ids (only active roles)
            var roleIds = user.Roles.Where(x => x.IsActive).Select(x => x.Id);

            //and now the capabilities
            var capabilities = capabilityService.GetByRolesConsolidated(roleIds.ToArray()).ToList();

            var result = capabilities.Select(x => x.Name).Intersect(capabilityName).Any();
            if (!result)
            {
                //search for user specific caps
                var userCapabilities = capabilityService.GetByUser(user.Id);
                capabilities = capabilities.Concat(userCapabilities).ToList();
            }
            return capabilities.Select(x => x.Name).Intersect(capabilityName).Any();
        }

        public static bool IsAdministrator(this User user)
        {
            return user.Is(SystemRoleNames.Administrator);
        }

        public static bool IsVisitor(this User user)
        {
            return user?.Roles == null || !user.Roles.Any();
        }

        public static bool IsRegistered(this User user)
        {
            return user.Is(SystemRoleNames.Registered);
        }

        public static bool IsVendor(this User user)
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