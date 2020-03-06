#region License
// Copyright (c) Sojatia Infocrafts Private Limited.
// The following code is part of EvenCart eCommerce Software (https://evencart.co) Dual Licensed under the terms of
// 
// 1. GNU GPLv3 with additional terms (available to read at https://evencart.co/license)
// 2. EvenCart Proprietary License (available to read at https://evencart.co/license/commercial-license).
// 
// You can select one of the above two licenses according to your requirements. The usage of this code is
// subject to the terms of the license chosen by you.
#endregion

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DotEntity;
using DotEntity.Enumerations;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Users;
using EvenCart.Data.Extensions;

namespace EvenCart.Services.Users
{
    public class UserPointService : FoundationEntityService<UserPoint>, IUserPointService
    {
        private readonly IUserService _userService;
        public UserPointService(IUserService userService)
        {
            _userService = userService;
        }

        public override void Insert(UserPoint entity, Transaction transaction = null)
        {
            base.Insert(entity, transaction);
            //get the user points now
            var points = GetPoints(entity.UserId);
            _userService.Update(new { Points = points }, x => x.Id == entity.UserId, transaction);
        }

        public override void Update(UserPoint entity, Transaction transaction = null)
        {
            base.Update(entity, transaction);
            //get the user points now
            var points = GetPoints(entity.UserId);
            _userService.Update(new { Points = points }, x => x.Id == entity.UserId, transaction);
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

        public override IEnumerable<UserPoint> Get(out int totalResults, Expression<Func<UserPoint, bool>> @where, Expression<Func<UserPoint, object>> orderBy = null,
            RowOrder rowOrder = RowOrder.Ascending, int page = 1, int count = Int32.MaxValue)
        {
            return Repository.Join<User>("ActivatorUserId", "Id", joinType: JoinType.LeftOuter)
                .Relate(RelationTypes.OneToOne<UserPoint, User>())
                .Where(where)
                .OrderBy(orderBy, rowOrder)
                .SelectNestedWithTotalMatches(out totalResults, page, count);
        }
    }
}