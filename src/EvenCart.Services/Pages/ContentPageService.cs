using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DotEntity;
using DotEntity.Enumerations;
using EvenCart.Core.Services;
using EvenCart.Core.Services.Events;
using EvenCart.Data.Entity.Pages;
using EvenCart.Data.Entity.Users;
using EvenCart.Data.Extensions;

namespace EvenCart.Services.Pages
{
    public class ContentPageService : FoundationEntityService<ContentPage>, IContentPageService
    {
        private readonly IEventPublisherService _eventPublisherService;
        public ContentPageService(IEventPublisherService eventPublisherService)
        {
            _eventPublisherService = eventPublisherService;
        }

        public IList<ContentPage> GetContentPages(out int totalResults, string search = null, int page = 1, int count = Int32.MaxValue)
        {
            var query = Repository;
            if (!search.IsNullEmptyOrWhiteSpace())
                query = query.Where(x => x.Name.Contains(search));
            query = WithRelations(query);
            query = query.OrderBy(x => x.Name, RowOrder.Ascending);
;            return query.SelectNestedWithTotalMatches(out totalResults, page, count)
                 .ToList();
        }

        public override ContentPage Get(int id)
        {
            var query = WithRelations(Repository);
            query = query.Where(x => x.Id == id);
            return query.SelectNested().FirstOrDefault();
        }

        public override IEnumerable<ContentPage> Get(Expression<Func<ContentPage, bool>> @where, int page = 1, int count = Int32.MaxValue)
        {
            var query = WithRelations(Repository);
            query = query.Where(where);
            return query.SelectNested(page, count);
        }

        IEntitySet<ContentPage> WithRelations(IEntitySet<ContentPage> query)
        {
            return _eventPublisherService.Filter(query.Join<SeoMeta>("Id", "EntityId",
                    additionalExpression: (page, meta) => meta.EntityName == "ContentPage")
                .Join<User>("UserId", "Id", SourceColumn.Parent)
                .Relate(RelationTypes.OneToOne<ContentPage, SeoMeta>())
                .Relate(RelationTypes.OneToOne<ContentPage, User>()));
        }
    }
}