#region Author Information
// UserExtensions.cs
// 
// (c) 2016 Apexol Technologies. All Rights Reserved.
// 
#endregion

using System.Collections.Generic;
using System.Linq;
using RoastedMarketplace.Data.Entity.Emails;
using RoastedMarketplace.Data.Entity.Users;
using RoastedMarketplace.Services.Users;

namespace RoastedMarketplace.Services.Extensions
{
    public static class UserExtensions
    {
        public static IList<User> GetUsersByRoles(this IUserService userService, string[] roles)
        {
            //general get query
           // var uQuery = userService.Get(x => x.UserRoles.Select(y => y.Role.RoleName).Intersect(roles).Any());
            //return uQuery.ToList();
            return null;
        }


        public static EmailMessage.UserInfo ToUserInfo(this User user)
        {
            return new EmailMessage.UserInfo(user.Name, user.Email);
        }
    }
}