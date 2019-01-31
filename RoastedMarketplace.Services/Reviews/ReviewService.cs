using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DotEntity.Enumerations;
using RoastedMarketplace.Core.Services;
using RoastedMarketplace.Data.Entity.Reviews;
using RoastedMarketplace.Data.Entity.Users;
using RoastedMarketplace.Data.Extensions;

namespace RoastedMarketplace.Services.Reviews
{
    public class ReviewService : FoundationEntityService<Review>, IReviewService
    {
        public override IEnumerable<Review> Get(Expression<Func<Review, bool>> @where, int page = 1, int count = Int32.MaxValue)
        {
            return Repository.Join<User>("UserId", "Id", joinType: JoinType.LeftOuter)
                .Relate(RelationTypes.OneToOne<Review, User>())
                .OrderBy(x => x.CreatedOn, RowOrder.Descending)
                .SelectNested(page, count);
        }
    }
}