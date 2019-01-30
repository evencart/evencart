using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DotEntity.Enumerations;
using RoastedMarketplace.Core.Services;
using RoastedMarketplace.Data.Entity.Pages;
using RoastedMarketplace.Data.Entity.Shop;
using RoastedMarketplace.Data.Extensions;

namespace RoastedMarketplace.Services.Products
{
    public class CategoryService : FoundationEntityService<Category>, ICategoryService
    {
        public override Category Get(int id)
        {
            Expression<Func<SeoMeta, bool>> nameWhere = (meta) => meta.EntityName == "Category" && meta.EntityId == id;
            return Repository.Join<SeoMeta>("Id", "EntityId", joinType: JoinType.LeftOuter)
                .Relate(RelationTypes.OneToOne<Category, SeoMeta>())
                .Where(x => x.Id == id)
                .Where(nameWhere)
                .SelectNested()
                .FirstOrDefault();
        }

        public IList<Category> GetFullCategoryTree()
        {
            Expression<Func<SeoMeta, bool>> nameWhere = (meta) => meta.EntityName == "Category";
            var allCategories = Repository.Join<SeoMeta>("Id", "EntityId", joinType: JoinType.LeftOuter)
                .Relate(RelationTypes.OneToOne<Category, SeoMeta>())
                .Where(nameWhere)
                .SelectNested()
                .ToList();
            
            foreach(var category in allCategories)
            {
                MakeTree(category, allCategories);
            }
            return allCategories.ToList();
        }

        private void MakeTree(Category parentCategory, IList<Category> categories)
        {
            parentCategory.ChildCategories = categories.Where(x => x.ParentCategoryId == parentCategory.Id).ToList();
            foreach (var c in parentCategory.ChildCategories)
                c.ParentCategory = parentCategory;
        }
    }
}