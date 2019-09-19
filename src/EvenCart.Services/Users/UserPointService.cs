using System;
using DotEntity;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Users;

namespace EvenCart.Services.Users
{
    public class UserPointService : FoundationEntityService<UserPoint>, IUserPointService
    {
        private readonly IUserService _userService;
        public UserPointService(IUserService userService)
        {
            _userService = userService;
        }
        public void AddPoints(User user, int points, string reason, int activatorUserId)
        {
            Transaction.Initiate(transaction =>
            {
                Insert(new UserPoint()
                {
                    UserId = user.Id,
                    Points = points,
                    CreatedOn = DateTime.UtcNow,
                    Reason = reason,
                    ActivatorUserId = activatorUserId
                }, transaction);
                //update the user
                user.Points += points;
                _userService.Update(new { Points = user.Points }, x => x.Id == user.Id, transaction);
            });
        }

        public void AddPoints(int userId, int points, string reason, int activatorUserId)
        {
            var user = _userService.FirstOrDefault(x => x.Id == userId);
            if (user != null)
                AddPoints(user, points, reason, activatorUserId);
        }

        public int GetPoints(int userId)
        {
            var tableName = DotEntityDb.Provider.SafeEnclose(DotEntityDb.GetTableNameForType<UserPoint>());

            var query = $"SELECT SUM({nameof(UserPoint.Points)}) FROM {tableName} WHERE UserId=@UserId";
            using (var resulting = EntitySet.Query(query, new { UserId = userId }))
            {
                return resulting.SelectScalerAs<int>();
            }
        }
    }
}