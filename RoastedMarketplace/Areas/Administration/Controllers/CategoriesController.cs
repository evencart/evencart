using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RoastedMarketplace.Areas.Administration.Models.Shop;
using RoastedMarketplace.Data.Constants;
using RoastedMarketplace.Infrastructure.Mvc;
using RoastedMarketplace.Infrastructure.Routing;
using RoastedMarketplace.Infrastructure.Security.Attributes;
using RoastedMarketplace.Services.Products;

namespace RoastedMarketplace.Areas.Administration.Controllers
{
    public class CategoriesController : FoundationAdminController
    {
        private readonly ICategoryService _categoryService;
        private readonly ICategoryAccountant _categoryAccountant;
        public CategoriesController(ICategoryService categoryService, ICategoryAccountant categoryAccountant)
        {
            _categoryService = categoryService;
            _categoryAccountant = categoryAccountant;
        }

        [DualGet("suggestions", Name = AdminRouteNames.GetCategorySuggestions, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.ViewCategories)]
        public IActionResult Suggestions(string q = null)
        {
            var categories = _categoryService.GetFullCategoryTree();
            var model = new List<AutocompleteModel>();
            foreach (var c in categories)
            {
                model.Add(new AutocompleteModel()
                {
                    Id = c.Id,
                    Text = _categoryAccountant.GetFullBreadcrumb(c)
                });
            }
            return Result(new {success = true, suggestions = model.OrderBy(x => x.Text)});
        }
    }
}