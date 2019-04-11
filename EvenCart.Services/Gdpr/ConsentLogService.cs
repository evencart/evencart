using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DotEntity;
using DotEntity.Enumerations;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Gdpr;
using EvenCart.Data.Extensions;

namespace EvenCart.Services.Gdpr
{
    public class ConsentLogService : FoundationEntityService<ConsentLog>, IConsentLogService
    {
        public override IEnumerable<ConsentLog> Get(Expression<Func<ConsentLog, bool>> @where, int page = 1, int count = Int32.MaxValue)
        {
            var query = GetByWhere(where);
            query = query.OrderBy(x => x.CreatedOn, RowOrder.Descending);
            return query.SelectNested(page, count);
        }

        public override IEnumerable<ConsentLog> Get(out int totalResults, Expression<Func<ConsentLog, bool>> @where, Expression<Func<ConsentLog, object>> orderBy = null,
            RowOrder rowOrder = RowOrder.Ascending, int page = 1, int count = Int32.MaxValue)
        {
            var query = GetByWhere(where);
            if (orderBy == null)
            {
                orderBy = log => log.Id;
                rowOrder = RowOrder.Descending;
            }

            query = query.OrderBy(orderBy, rowOrder);
            return query.SelectNestedWithTotalMatches(out totalResults, page, count);
        }

        public override ConsentLog FirstOrDefault(Expression<Func<ConsentLog, bool>> @where)
        {
            var query = GetByWhere(where);
            return query.SelectNested().FirstOrDefault();
        }

        #region Helpers
        private IEntitySet<ConsentLog> GetByWhere(Expression<Func<ConsentLog, bool>> where)
        {
            return Repository.Where(where).Join<Consent>("ConsentId", "Id", joinType: JoinType.LeftOuter)
                .Relate(RelationTypes.OneToOne<ConsentLog, Consent>());
        }
        #endregion
    }
}