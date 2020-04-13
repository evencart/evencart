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
using DotEntity.Enumerations;
using EvenCart.Core.Caching;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Pages;
using EvenCart.Data.Entity.Shop;
using EvenCart.Data.Extensions;

namespace EvenCart.Services.Products
{
    public class CategoryService : FoundationEntityService<Category>, ICategoryService
    {
        private const string CategoryTreeCacheKey = "CATEGORY_TREE";

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
            return CacheProvider.Get<IList<Category>>(CategoryTreeCacheKey, () =>
            {
                Expression<Func<SeoMeta, bool>> nameWhere = (meta) => meta.EntityName == "Category";
                var allCategories = Repository.Join<SeoMeta>("Id", "EntityId", joinType: JoinType.LeftOuter)
                    .Relate(RelationTypes.OneToOne<Category, SeoMeta>())
                    .Where(nameWhere)
                    .SelectNested()
                    .ToList();

                foreach (var category in allCategories)
                {
                    MakeTree(category, allCategories);
                }

                return allCategories.ToList();
            });
        }

        public void InsertTree(Category category)
        {
            if (category.Parent != null && category.Children != null && category.Children.Any())
            {
                throw new Exception("A category tree to insert should only have parent or children");
            }
            if (category.Parent != null)
            {
                InsertTree(category.Parent);
                category.ParentId = category.Parent.Id;
            }
            Insert(category);
            if (category.Children != null && category.Children.Any())
            {
                foreach (var child in category.Children)
                {
                    child.ParentId = category.Id;
                    InsertTree(child);
                }
            }
        }

        private void MakeTree(Category parentCategory, IList<Category> categories)
        {
            parentCategory.Children = categories.Where(x => x.ParentId == parentCategory.Id).ToList();
            foreach (var c in parentCategory.Children)
                c.Parent = parentCategory;
        }
    }
}