using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RoastedMarketplace.Data.Entity.Shop;
using RoastedMarketplace.Infrastructure;
using RoastedMarketplace.Infrastructure.Mvc;
using RoastedMarketplace.Infrastructure.Routing;
using RoastedMarketplace.Models.Components;
using RoastedMarketplace.Models.Products;
using RoastedMarketplace.Services.Extensions;
using RoastedMarketplace.Services.Products;

namespace RoastedMarketplace.Components
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
            var searchModel = data as ProductSearchModel;
            var currentCategoryId = searchModel?.CategoryId ?? 0;
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
                        foreach (var child in category.ChildCategories)
                        {
                            var childModel = PrepareModel(child, currentCategoryId, allCategories);
                            categoryFilterModel.ChildCategories.Add(childModel);
                        }
                    }
                    category = category.ParentCategory;
                }
                //reverse the list to show correct order
                filterModels.Reverse();
            }
            else
            {
                var rootCategories = allCategories.Where(x => x.ParentCategoryId == 0);
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
                        categoryPath = category.GetCategoryPath(allCategories)
                    }),
                Selected = category.Id == currentCategoryId,
                Id = category.Id,
                ParentCategoryId = category.ParentCategoryId
            };
            return categoryFilterModel;
        }
    }
}