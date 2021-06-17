﻿#region License
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
using Genesis.Extensions;
using EvenCart.Data.Entity.Shop;
using Genesis;

namespace EvenCart.Services.Products
{
    [AutoResolvable]
    public class CategoryAccountant : ICategoryAccountant
    {
        private readonly ICategoryService _categoryService;
        public CategoryAccountant(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public string GetFullBreadcrumb(Category category, string separator = " > ")
        {
            return category.GetNameBreadCrumb(separator);
        }

        public Category CreateCategoryTree(string categoryTree, IList<Category> allCategories, string separator = ">")
        {
            if (string.IsNullOrEmpty(categoryTree))
                return null;
            //we are getting categories in the format a > b > c, so let's split them up and create such a tree
            var categoryParts = categoryTree.Split(separator, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToArray();
            if (!categoryParts.Any())
                return null;

            //search for all matching categories
            var previousId = 0;
            Category resultantCategory = null;
            foreach (var categoryName in categoryParts) {
                var expectedParentId = previousId;
                var category = allCategories.FirstOrDefault(x => x.ParentId == expectedParentId && x.Name.Equals(categoryName, StringComparison.InvariantCultureIgnoreCase));

                // we didn't find this category, so it's a new category that needs to be created
                if (category == null)
                {
                    //insert this category
                    category = new Category()
                    {
                        Name = categoryName,
                        ParentId = expectedParentId
                    };
                    _categoryService.Insert(category);
                    //add to all categories
                    allCategories.Add(category);
                }
                //now we are good, we can use this as next's parent
                previousId = category.Id;
                resultantCategory = category;
            }
            return resultantCategory;
        }
    }
}