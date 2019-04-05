using RoastedMarketplace.Data.Entity.Users;
using RoastedMarketplace.Data.Extensions;

namespace RoastedMarketplace.Infrastructure.Extensions
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