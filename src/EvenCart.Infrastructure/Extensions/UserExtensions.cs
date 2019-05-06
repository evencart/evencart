using EvenCart.Core.Extensions;
using EvenCart.Data.Entity.Users;
using EvenCart.Data.Extensions;

namespace EvenCart.Infrastructure.Extensions
{
    public static class UserExtensions
    {
        public static bool IsImitator(this User user, out string imitator)
        {
            imitator = user.GetMeta<string>(ApplicationConfig.ImitatorKey);
            return !imitator.IsNullEmptyOrWhiteSpace();
        }
    }
}