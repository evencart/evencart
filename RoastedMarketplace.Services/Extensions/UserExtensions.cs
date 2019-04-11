#region Author Information
// UserExtensions.cs
// 
// (c) 2016 Apexol Technologies. All Rights Reserved.
// 
#endregion

using RoastedMarketplace.Data.Entity.Emails;
using RoastedMarketplace.Data.Entity.Users;

namespace RoastedMarketplace.Services.Extensions
{
    public static class UserExtensions
    {
        public static EmailMessage.UserInfo ToUserInfo(this User user)
        {
            return new EmailMessage.UserInfo(user.Name, user.Email);
        }
    }
}