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

using System.Collections.Generic;
using System.Linq;
using EvenCart.Data.Entity.Shop;
using EvenCart.Services.Extensions;
using EvenCart.Services.Products;
using EvenCart.Infrastructure;
using EvenCart.Infrastructure.Mvc;
using EvenCart.Infrastructure.Routing;
using EvenCart.Models.Components;
using EvenCart.Models.Products;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Components
{
    [ViewComponent(Name = "CategoryFilter")]
    public class CategoryFilterComponent : FoundationComponent
    {
        private readonly ICategoryService _categoryService;
        public CategoryFilterComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public override IViewComponentResult Invoke(object data = null)
        {
            var dataAsDict = data as Dictionary<string, object>;
            if (dataAsDict == null)
                return R.Success.ComponentResult;
            var searchModel = dataAsDict["model"] as ProductSearchModel;
            var currentCategoryId = searchModel?.CategoryId ?? 0;
            if (currentCategoryId == 0)
                return R.Success.ComponentResult;
            var allCategories = _categoryService.GetFullCategoryTree();

            var filterModels = new List<CategoryFilterModel>();
            //parent categories to current category
            if (currentCategoryId > 0)
            {
                var category = allCategories.FirstOrDefault(x => x.Id == currentCategoryId);
                while (category != null)
                {
                    var categoryFilterModel = PrepareModel(category, currentCategoryId, allCategories);
                    filterModels.Add(categoryFilterModel);
                    //add child categories to current category
                    if (categoryFilterModel.Selected)
                    {
                        foreach (var child in category.Children)
                        {
                            var childModel = PrepareModel(child, currentCategoryId, allCategories);
                            categoryFilterModel.ChildCategories.Add(childModel);
                        }
                    }
                    category = category.Parent;
                }
                //reverse the list to show correct order
                filterModels.Reverse();
            }
            else
            {
                var rootCategories = allCategories.Where(x => x.ParentId == 0);
                foreach (var category in rootCategories)
                {
                    var categoryFilterModel = PrepareModel(category, currentCategoryId, allCategories);
                    filterModels.Add(categoryFilterModel);
                }
            }
            
            return R.With("categories", filterModels).ComponentResult;
        }

        private CategoryFilterModel PrepareModel(Category category, int currentCategoryId, IList<Category> allCategories)
        {
            var categoryFilterModel = new CategoryFilterModel() {
                Name = category.Name,
                Url = ApplicationEngine.RouteUrl(RouteNames.ProductsPage,
                    new {
                        seName = category.SeoMeta.Slug,
                        categoryPath = category.GetCategoryPath()
                    }),
                Selected = category.Id == currentCategoryId,
                Id = category.Id,
                ParentId = category.ParentId
            };
            return categoryFilterModel;
        }
    }
}