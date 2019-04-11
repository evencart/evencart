#region Author Information
// UserExtensions.cs
// 
// (c) 2016 Apexol Technologies. All Rights Reserved.
// 
#endregion

using EvenCart.Data.Entity.Emails;
using EvenCart.Data.Entity.Users;

namespace EvenCart.Services.Extensions
{
    public static class UserExtensions
    {
        public static EmailMessage.UserInfo ToUserInfo(this User user)
        {
            return new EmailMessage.UserInfo(user.Name, user.Email);
        }
    }
}