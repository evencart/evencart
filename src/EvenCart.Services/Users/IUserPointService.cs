using EvenCart.Core.Services;
using EvenCart.Data.Entity.Users;

namespace EvenCart.Services.Users
{
    public interface IUserPointService : IFoundationEntityService<UserPoint>
    {
        void AddPoints(User user, int points, string reason, int activatorUserId);

        void AddPoints(int userId, int points, string reason, int activatorUserId);

        int GetPoints(int userId);
    }
}