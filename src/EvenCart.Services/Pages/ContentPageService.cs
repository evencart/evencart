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