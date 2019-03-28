using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using RoastedMarketplace.Data.Entity.Promotions;
using RoastedMarketplace.Data.Entity.Settings;
using RoastedMarketplace.Data.Entity.Shop;
using RoastedMarketplace.Data.Enum;
using RoastedMarketplace.Data.Extensions;
using RoastedMarketplace.Factories.Products;
using RoastedMarketplace.Infrastructure;
using RoastedMarketplace.Infrastructure.Helpers;
using RoastedMarketplace.Infrastructure.MediaServices;
using RoastedMarketplace.Infrastructure.Mvc;
using RoastedMarketplace.Infrastructure.Mvc.ModelFactories;
using RoastedMarketplace.Infrastructure.Routing;
using RoastedMarketplace.Models.Media;
using RoastedMarketplace.Models.Pages;
using RoastedMarketplace.Models.Products;
using RoastedMarketplace.Models.Reviews;
using RoastedMarketplace.Services.Extensions;
using RoastedMarketplace.Services.Products;
using RoastedMarketplace.Services.Reviews;
using RoastedMarketplace.Services.Search;
using RoastedMarketplace.Services.Serializers;

namespace RoastedMarketplace.Controllers
{
    public class ProductsController : FoundationController
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly CatalogSettings _catalogSettings;
        private readonly IModelMapper _modelMapper;
        private readonly IProductRelationService _productRelationService;
        private readonly IProductVariantService _productVariantService;
        private readonly IDataSerializer _dataSerializer;
        private readonly IPriceAccountant _priceAccountant;
        private readonly TaxSettings _taxSettings;
        private readonly IReviewService _reviewService;
        private readonly GeneralSettings _generalSettings;
        private readonly IProductModelFactory _productModelFactory;

        public ProductsController(IProductService productService, ICategoryService categoryService, CatalogSettings catalogSettings, IModelMapper modelMapper, IProductRelationService productRelationService, IProductVariantService productVariantService, IDataSerializer dataSerializer, IPriceAccountant priceAccountant, TaxSettings taxSettings, IReviewService reviewService, GeneralSettings generalSettings, IProductModelFactory productModelFactory)
        {
            _productService = productService;
            _categoryService = categoryService;
            _catalogSettings = catalogSettings;
            _modelMapper = modelMapper;
            _productRelationService = productRelationService;
            _productVariantService = productVariantService;
            _dataSerializer = dataSerializer;
            _priceAccountant = priceAccountant;
            _taxSettings = taxSettings;
            _reviewService = reviewService;
            _generalSettings = generalSettings;
            _productModelFactory = productModelFactory;
        }

        [DynamicRoute(Name = RouteNames.SingleProduct, SeoEntityName = nameof(Product), SettingName = nameof(UrlSettings.ProductUrlTemplate))]
        public IActionResult Index(int id)
        {
            if (id < 1)
                return NotFound();
            var product = _productService.Get(id);
            if (!product.IsPublic())
                return NotFound();

            var productModel = _productModelFactory.Create(product);

            var response = R.Success;
            response.With("product", productModel);

            if (_catalogSettings.EnableRelatedProducts)
            {
                var productRelations = _productRelationService.GetByProductId(id, ProductRelationType.RelatedProduct, 1,
                    _catalogSettings.NumberOfRelatedProducts);
                if (productRelations.Any())
                {
                    var relatedProductsModel = productRelations.Select(x => x.DestinationProduct).Select(_productModelFactory.Create).ToList();
                    response.With("relatedProducts", relatedProductsModel);
                }
            }

            if (product.HasVariants)
            {
                //any variants
                var variants = _productVariantService.GetByProductId(product.Id);
                var variantModels = new List<object>();
                foreach (var variant in variants)
                {
                    _priceAccountant.GetProductPriceDetails(product, null, variant.Price, out decimal priceWithoutTax, out decimal tax, out decimal taxRate);
                    var variantObject = new
                    {
                        attributes = new Dictionary<string, string>(),
                        price = (_taxSettings.DisplayProductPricesWithoutTax ? priceWithoutTax : priceWithoutTax + tax).ToCurrency(),
                        isAvailable = variant.TrackInventory && (variant.StockQuantity > 0 || variant.CanOrderWhenOutOfStock)
                    };
                    foreach (var pva in variant.ProductVariantAttributes)
                    {
                        variantObject.attributes.Add(pva.ProductAttribute.Label, pva.ProductAttributeValue.AvailableAttributeValue.Value);
                    }
                    variantModels.Add(variantObject);
                }

                if (variantModels.Any())
                    response.With("variants", () => variantModels, () => _dataSerializer.Serialize(variantModels));
            }

            //reviews
            if (_catalogSettings.EnableReviews)
            {
                var reviews = _reviewService.Get(x => x.ProductId == product.Id, 1,
                    _catalogSettings.NumberOfReviewsToDisplayOnProductPage).ToList();

                if (reviews.Any())
                {
                    var reviewModels = reviews.Select(x =>
                    {
                        var model = _modelMapper.Map<ReviewModel>(x);
                        model.DisplayName = x.Private ? _catalogSettings.DisplayNameForPrivateReviews : x.User.Name;
                        if (model.DisplayName.IsNullEmptyOrWhiteSpace())
                        {
                            model.DisplayName = T("Store Customer");
                        }
                        return model;
                    }).ToList();
                    response.With("reviews", reviewModels);
                }
            }

            //breadcrumbs
            if (_generalSettings.EnableBreadcrumbs)
            {
                var categoryTree = _categoryService.GetFullCategoryTree();
                var category = product.Categories.FirstOrDefault();
                var currentCategoryFull = categoryTree.FirstOrDefault(x => x.Id == category?.Id);
                BreadcrumbHelper.SetCategoryBreadcrumb(currentCategoryFull, categoryTree);
                SetBreadcrumbToRoute(product.Name, RouteNames.SingleProduct, new {seName = product.SeoMeta.Slug}, localize: false);
            }
            //seo data
            SeoMetaHelper.SetSeoData(product.Name, product.Description, product.Description);
            return response.Result;
        }

        [DynamicRoute(Name = RouteNames.ProductsPage, SeoEntityName = nameof(Category),
            SettingName = nameof(UrlSettings.CategoryUrlTemplate),
            ParameterName = nameof(ProductSearchModel.CategoryId))]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult ProductsList(ProductSearchModel searchModel)
        {
            return ProductsListApi(searchModel);
        }
        [DualGet("~/s", Name = RouteNames.ProductsSearchPage)]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult ProductSearch(ProductSearchModel searchModel)
        {
            if (searchModel.Search.IsNullEmptyOrWhiteSpace())
                return RedirectToRoute(RouteNames.Home);
            return ProductsListApi(searchModel);
        }

        [DualGet("", Name = RouteNames.ProductsPage, OnlyApi = true)]
        public IActionResult ProductsListApi(ProductSearchModel searchModel, string viewName = null)
        {
            IList<int> categoryIds = null;
            if (searchModel.CategoryId.HasValue && searchModel.CategoryId.Value > 0)
            {
                categoryIds = new List<int>()
                {
                    searchModel.CategoryId.Value
                };
                var fullCategoryTree = _categoryService.GetFullCategoryTree();
                var currentCategory = fullCategoryTree.FirstOrDefault(x => x.Id == searchModel.CategoryId.Value);
                //set breadcrumb
                BreadcrumbHelper.SetCategoryBreadcrumb(currentCategory, fullCategoryTree);

                if (_catalogSettings.DisplayProductsFromChildCategories)
                {
                    if (currentCategory != null)
                    {
                        var childIds = currentCategory.ChildCategories.SelectManyRecursive(x => x.ChildCategories).Select(x => x.Id);
                        categoryIds = categoryIds.Concat(childIds).ToList();
                    }
                }
            }

            //create order by expression
            Expression<Func<Product, object>> orderByExpression = null;
            if (!searchModel.SortColumn.IsNullEmptyOrWhiteSpace())
            {
                switch (searchModel.SortColumn.ToLower())
                {
                    case "name":
                        orderByExpression = product => product.Name;
                        break;
                    case "createdon":
                        orderByExpression = product => product.CreatedOn;
                        break;
                    case "price":
                        orderByExpression = product => product.Price;
                        break;
                    case "popularity":
                    default:
                        orderByExpression = product => product.PopularityIndex;
                        searchModel.SortOrder = SortOrder.Descending;
                        searchModel.SortColumn = "popularity";
                        break;
                }
            }

            var products = _productService.GetProducts(out int totalResults,
                out decimal availableFromPrice,
                out decimal availableToPrice,
                out Dictionary<int, string> availableManufacturers,
                out Dictionary<int, string> availableVendors,
                out Dictionary<string, List<string>> availableFilters,
                searchText: searchModel.Search,
                filterExpression: searchModel.Filters,
                published: true,
                vendorIds: searchModel.VendorIds,
                manufacturerIds: searchModel.ManufacturerIds,
                categoryids: categoryIds,
                fromPrice: searchModel.FromPrice,
                toPrice: searchModel.ToPrice,
                sortOrder: searchModel.SortOrder,
                orderByExpression: orderByExpression,
                page: searchModel.Page,
                count: searchModel.Count);

            var productModels = products.Select(_productModelFactory.Create).ToList();
            searchModel.AvailableFromPrice = availableFromPrice;
            searchModel.AvailableToPrice = availableToPrice;

            searchModel.AvailableFilters = availableFilters;
            searchModel.AvailableManufacturers = availableManufacturers;
            searchModel.AvailableVendors = availableVendors;

            //get the category
            if (searchModel.CategoryId > 0)
            {
                var category = _categoryService.Get(searchModel.CategoryId.Value);
                if (category != null)
                    SeoMetaHelper.SetSeoData(category.Name, category.Description, category.Description);
            }

            if (viewName.IsNullEmptyOrWhiteSpace())
                viewName = "ProductsList";

            return R.Success.With("products", productModels)
                .WithParams(searchModel)
                .WithGridResponse(totalResults, searchModel.Page, searchModel.Count, searchModel.SortColumn, searchModel.SortOrder)
                .WithView(viewName)
                .Result;
        }

    }
}