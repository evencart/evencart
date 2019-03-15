using RoastedMarketplace.Core.Services;
using RoastedMarketplace.Data.Entity.Users;

namespace RoastedMarketplace.Services.Users
{
    public interface IUserCodeService : IFoundationEntityService<UserCode>
    {
        UserCode GetUserCode(int userId, UserCodeType userCodeType);

        UserCode GetUserCode(string code, UserCodeType userCodeType);

    }
}