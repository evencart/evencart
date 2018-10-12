using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RoastedMarketplace.Areas.Administration.Models.Shop;
using RoastedMarketplace.Core.Services;
using RoastedMarketplace.Data.Constants;
using RoastedMarketplace.Data.Entity.Shop;
using RoastedMarketplace.Data.Extensions;
using RoastedMarketplace.Infrastructure.Helpers;
using RoastedMarketplace.Infrastructure.Mvc;
using RoastedMarketplace.Infrastructure.Mvc.Attributes;
using RoastedMarketplace.Infrastructure.Mvc.ModelFactories;
using RoastedMarketplace.Infrastructure.Routing;
using RoastedMarketplace.Infrastructure.Security.Attributes;
using RoastedMarketplace.Services.Extensions;
using RoastedMarketplace.Services.Products;
using RoastedMarketplace.Services.Serializers;

namespace RoastedMarketplace.Areas.Administration.Controllers
{
    public class CategoriesController : FoundationAdminController
    {
        private readonly ICategoryService _categoryService;
        private readonly ICategoryAccountant _categoryAccountant;
        private readonly IModelMapper _modelMapper;
        private readonly IDataSerializer _dataSerializer;
        public CategoriesController(ICategoryService categoryService, ICategoryAccountant categoryAccountant, IModelMapper modelMapper, IDataSerializer dataSerializer)
        {
            _categoryService = categoryService;
            _categoryAccountant = categoryAccountant;
            _modelMapper = modelMapper;
            _dataSerializer = dataSerializer;
        }

        [DualGet("suggestions", Name = AdminRouteNames.GetCategorySuggestions, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.ViewCategories)]
        public IActionResult Suggestions(string q = null)
        {
            var categories = _categoryService.GetFullCategoryTree();
            var model = new List<AutocompleteModel>();
            foreach (var c in categories)
            {
                model.Add(new AutocompleteModel() {
                    Id = c.Id,
                    Text = _categoryAccountant.GetFullBreadcrumb(c)
                });
            }
            return R.Success.With("suggestions", model.OrderBy(x => x.Text)).Result;
        }

        [DualGet("", Name = AdminRouteNames.CategoriesList)]
        [ValidateModelState(ModelType = typeof(CategorySearchModel))]
        [CapabilityRequired(CapabilitySystemNames.ViewCategories)]
        public IActionResult CategoriesList(CategorySearchModel searchModel)
        {
            //retrieve categories
            var allCategories = _categoryService.GetFullCategoryTree();
            var categoryModels = allCategories.Select(x =>
                {
                    var model = _modelMapper.Map<CategoryModel>(x);
                    model.FullCategoryPath =
                        _categoryAccountant.GetFullBreadcrumb(allCategories.First(y => y.Id == x.Id));
                    return model;
                })
                .Where(x => searchModel.SearchPhrase.IsNullEmptyOrWhiteSpace() || x.Name.StartsWith(searchModel.SearchPhrase, StringComparison.InvariantCultureIgnoreCase))
                .OrderBy(x => x.FullCategoryPath)
                .TakeFromPage(searchModel.Current, searchModel.RowCount)
                .ToList();

            return R.Success
                .With("total", allCategories.Count)
                .With("current", searchModel.Current)
                .With("rowCount", searchModel.RowCount)
                .With("categories", () => categoryModels, () => _dataSerializer.Serialize(categoryModels))
                .Result;
        }

        [DualGet("{categoryId}", Name = AdminRouteNames.EditCategory)]
        [CapabilityRequired(CapabilitySystemNames.EditCategory)]
        public IActionResult CategoryEditor(int categoryId)
        {
            //get the category tree
            var categoryTree = _categoryService.GetFullCategoryTree();
            var category = categoryId > 0 ? categoryTree.FirstOrDefault(x => x.Id == categoryId) : new Category();
            if (category == null)
                return NotFound();
            var categoryModel = _modelMapper.Map<CategoryModel>(category);
            var availableParentCategoryModels = categoryTree.Where(x => x.Id != categoryId && !category.IsAnyChild(x.Id))
                .Select(x =>
                {
                    var model = _modelMapper.Map<CategoryModel>(x);
                    model.FullCategoryPath =
                        _categoryAccountant.GetFullBreadcrumb(categoryTree.First(y => y.Id == x.Id));
                    return model;
                })
                .OrderBy(x => x.FullCategoryPath)
                .ToList();
            var apcAsSelectList =
                SelectListHelper.GetSelectItemList(availableParentCategoryModels, x => x.Id, x => x.FullCategoryPath);

            return R.Success.With("category", categoryModel).With("availableParents", apcAsSelectList).Result;

        }

        [DualPost("", Name = AdminRouteNames.SaveCategory, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.EditCategory)]
        [ValidateModelState(ModelType = typeof(CategoryModel))]
        public IActionResult SaveCategory(CategoryModel categoryModel)
        {
            if (categoryModel.Id == 0)
            {
                var category = new Category() {
                    Name = categoryModel.Name,
                    ParentCategoryId = categoryModel.ParentCategoryId,
                    Description = categoryModel.Description
                };
                _categoryService.Insert(category);
            }
            else
            {
                //get the category
                var category = _categoryService.GetFullCategoryTree().FirstOrDefault(x => x.Id == categoryModel.Id);
                if (category == null)
                    return NotFound();
                if(category.Id == categoryModel.ParentCategoryId)
                    return R.Fail.With("error", T("Can't make a category a parent of self.")).Result;
                //check if parent category is valid
                if (category.IsAnyChild(categoryModel.ParentCategoryId))
                    return R.Fail.With("error", T("Can't make a child category as new parent")).Result;
                category.Name = categoryModel.Name;
                category.Description = categoryModel.Description;
                category.ParentCategoryId = categoryModel.ParentCategoryId;
                _categoryService.Update(category);
            }
            return R.Success.Result;
        }

        [DualPost("tree", Name = AdminRouteNames.SaveCategoryTrees, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.EditCategory)]
        public IActionResult SaveCategoryTrees(string categories)
        {
            if (categories.IsNullEmptyOrWhiteSpace())
                return R.Fail.With("error", "Can't add empty category trees").Result;
            //split the trees by new lines
            var categoryTrees = categories.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            if(!categoryTrees.Any())
                return R.Fail.With("error", "Can't add empty category trees").Result;

            //get all the categories
            var allCategories = _categoryService.GetFullCategoryTree();
            Transaction.Initiate(transaction =>
            {
                foreach (var c in categoryTrees)
                    _categoryAccountant.CreateCategoryTree(c, allCategories);
            });

            return R.Success.Result;
        }

        [DualPost("displayorder", Name = AdminRouteNames.UpdateCategoryDisplayOrder, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.UploadMedia)]
        public IActionResult UpdateDisplayOrders(CategoryModel[] categoryModels)
        {
            if (categoryModels == null)
                return BadRequest();

            //get category models with no-zero ids
            var validCategoryModels = categoryModels.Where(x => x.Id != 0);
            Transaction.Initiate(transaction =>
            {
                foreach (var model in validCategoryModels)
                {
                    _categoryService.Update(new { DisplayOrder = model.DisplayOrder }, m => m.Id == model.Id,
                        transaction);
                }
            });
            return R.Success.Result;
        }

        [DualPost("delete", Name = AdminRouteNames.DeleteCategory, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.DeleteCategory)]
        [ValidateModelState(ModelType = typeof(CategoryModel))]
        public IActionResult DeleteCategory(int categoryId)
        {
            //get the category
            var category = _categoryService.Get(categoryId);
            if (category == null)
                return NotFound();
            Transaction.Initiate(transaction =>
            {
                _categoryService.Delete(category);
            });

            return R.Success.Result;
        }
    }
}