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