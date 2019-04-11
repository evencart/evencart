using EvenCart.Core.Services;
using EvenCart.Data.Entity.Users;

namespace EvenCart.Services.Users
{
    public interface IUserCodeService : IFoundationEntityService<UserCode>
    {
        UserCode GetUserCode(int userId, UserCodeType userCodeType);

        UserCode GetUserCode(string code, UserCodeType userCodeType);

    }
}