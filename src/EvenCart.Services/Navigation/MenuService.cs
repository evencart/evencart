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
using EvenCart.Data.Entity.Navigation;
using EvenCart.Data.Entity.Pages;
using EvenCart.Data.Extensions;

namespace EvenCart.Services.Navigation
{
    public class MenuService : FoundationEntityService<Menu>, IMenuService
    {
        private readonly IEventPublisherService _eventPublisherService;

        public MenuService(IEventPublisherService eventPublisherService)
        {
            _eventPublisherService = eventPublisherService;
        }

        public override Menu Get(int id)
        {
            var menu = GetByWhere(x => x.Id == id).SelectNested().FirstOrDefault();
            if (menu?.MenuItems == null)
                return menu;
            foreach (var menuItem in menu.MenuItems)
            {
                menuItem.Children = menu.MenuItems.Where(x => x.ParentId == menuItem.Id).ToList();
            }

            return menu;
        }

        public override IEnumerable<Menu> Get(Expression<Func<Menu, bool>> @where, int page = 1, int count = Int32.MaxValue)
        {
            return GetByWhere(where).SelectNested(page, count);
        }

        public override IEnumerable<Menu> Get(out int totalResults, Expression<Func<Menu, bool>> @where, Expression<Func<Menu, object>> orderBy = null,
            RowOrder rowOrder = RowOrder.Ascending, int page = 1, int count = Int32.MaxValue)
        {
            return GetByWhere(where).OrderBy(orderBy, rowOrder)
                .SelectNestedWithTotalMatches(out totalResults, page, count);
        }

        private IEntitySet<Menu> GetByWhere(Expression<Func<Menu, bool>> @where)
        {
            Expression<Func<MenuItem, object>> orderBy = x => x.DisplayOrder;
            return _eventPublisherService.Filter(Repository
                .Join<MenuItem>("Id", "MenuId", joinType: JoinType.LeftOuter)
                .Join<SeoMeta>("SeoMetaId", "Id", joinType: JoinType.LeftOuter)
                .Relate(RelationTypes.OneToMany<Menu, MenuItem>())
                .Relate<SeoMeta>((menu, meta) =>
                {
                    var menuItems = menu.MenuItems.Where(x => x.SeoMetaId == meta.Id);
                    foreach (var menuItem in menuItems)
                    {
                        menuItem.SeoMeta = meta;
                    }
                })
                .Where(where)
                .OrderBy(orderBy, RowOrder.Ascending));

        }
    }
}