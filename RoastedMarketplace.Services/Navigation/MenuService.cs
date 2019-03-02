using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DotEntity;
using DotEntity.Enumerations;
using RoastedMarketplace.Core.Services;
using RoastedMarketplace.Data.Entity.Navigation;
using RoastedMarketplace.Data.Entity.Pages;
using RoastedMarketplace.Data.Extensions;

namespace RoastedMarketplace.Services.Navigation
{
    public class MenuService : FoundationEntityService<Menu>, IMenuService
    {
        public override Menu Get(int id)
        {
            return GetByWhere(x => x.Id == id).SelectNested().FirstOrDefault();
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
            return Repository
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
                .OrderBy(orderBy, RowOrder.Ascending);

        }
    }
}