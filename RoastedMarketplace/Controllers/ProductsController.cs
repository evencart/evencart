using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using RoastedMarketplace.Data.Entity.Settings;
using RoastedMarketplace.Data.Entity.Shop;
using RoastedMarketplace.Data.Extensions;
using RoastedMarketplace.Infrastructure;
using RoastedMarketplace.Infrastructure.MediaServices;
using RoastedMarketplace.Infrastructure.Mvc;
using RoastedMarketplace.Infrastructure.Mvc.ModelFactories;
using RoastedMarketplace.Infrastructure.Routing;
using RoastedMarketplace.Models.Media;
using RoastedMarketplace.Models.Products;
using RoastedMarketplace.Models.Site;
using RoastedMarketplace.Services.Products;
using RoastedMarketplace.Services.Search;

namespace RoastedMarketplace.Controllers
{
    public class ProductsController : FoundationController
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly ProductsSettings _productsSettings;
        private readonly IModelMapper _modelMapper;
        private readonly IMediaAccountant _mediaAccountant;
        private readonly ISearchQueryParserService _searchQueryParserService;

        public ProductsController(IProductService productService, ICategoryService categoryService, ProductsSettings productsSettings, IModelMapper modelMapper, IMediaAccountant mediaAccountant, ISearchQueryParserService searchQueryParserService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _productsSettings = productsSettings;
            _modelMapper = modelMapper;
            _mediaAccountant = mediaAccountant;
            _searchQueryParserService = searchQueryParserService;
        }

        [DynamicRoute("{id}", Name = RouteNames.SingleProduct)]
        public IActionResult Index(int id)
        {
            if (id < 1)
                return NotFound();
            var product = _productService.Get(id);
            if (product == null)
                return NotFound();

            var productModel = MapProductModel(product);
            return R.Success.With("product", productModel).Result;
        }

        [DynamicRoute("", Name = RouteNames.ProductsPage)]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult ProductsList(ProductSearchModel searchModel)
        {
            return ProductsListApi(searchModel);
        }

        [DualGet("", Name = RouteNames.ProductsPage, OnlyApi = true)]
        public IActionResult ProductsListApi(ProductSearchModel searchModel)
        {
            IList<int> categoryIds = null;
            if (searchModel.CategoryId.HasValue && searchModel.CategoryId.Value > 0)
            {
                categoryIds = new List<int>()
                {
                    searchModel.CategoryId.Value
                };
                if (_productsSettings.DisplayProductsFromChildCategories)
                {
                    var fullCategoryTree = _categoryService.GetFullCategoryTree();
                    var currentCategory = fullCategoryTree.FirstOrDefault(x => x.Id == searchModel.CategoryId.Value);
                    if (currentCategory != null)
                    {
                        var childIds = currentCategory.ChildCategories.SelectManyRecursive(x => x.ChildCategories).Select(x => x.Id);
                        categoryIds = categoryIds.Concat(childIds).ToList();
                    }
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
                vendorIds:searchModel.VendorIds,
                manufacturerIds:searchModel.ManufacturerIds,
                categoryids: categoryIds, 
                fromPrice: searchModel.FromPrice,
                toPrice: searchModel.ToPrice,
                sortOrder: searchModel.SortOrder, 
                page: searchModel.Page,
                count: searchModel.Count);

            var productModels = products.Select(MapProductModel).ToList();
            searchModel.AvailableFromPrice = availableFromPrice;
            searchModel.AvailableToPrice = availableToPrice;

            searchModel.AvailableFilters = availableFilters;
            searchModel.AvailableManufacturers = availableManufacturers;
            searchModel.AvailableVendors = availableVendors;

            SeoMetaModel seoMetaModel = null;
            //get the category
            if (searchModel.CategoryId > 0)
            {
                var category = _categoryService.Get(searchModel.CategoryId.Value);
                var seoMeta = category?.SeoMeta;

                if (seoMeta != null)
                {
                    seoMetaModel = _modelMapper.Map<SeoMetaModel>(seoMeta);
                }
                seoMetaModel = seoMetaModel ?? new SeoMetaModel();
                seoMetaModel.Description = category?.Description;
                seoMetaModel.PageTitle = seoMeta?.PageTitle ?? category?.Name;
            }
            return R.Success.With("products", productModels)
                .WithSeoMeta(seoMetaModel)
                .WithParams(searchModel)
                .WithGridResponse(totalResults, searchModel.Page, searchModel.Count)
                .Result;
        }
        #region Helpers

        private ProductModel MapProductModel(Product product)
        {
            var productModel = _modelMapper.Map<ProductModel>(product);
            productModel.SeName = product.SeoMeta.Slug;
            var mediaModels = product.MediaItems?.Select(y =>
            {
                var mediaModel = _modelMapper.Map<MediaModel>(y);
                mediaModel.ThumbnailUrl =
                    _mediaAccountant.GetPictureUrl(y, ApplicationEngine.ActiveTheme.ProductBoxImageSize, true);
                mediaModel.Url = _mediaAccountant.GetPictureUrl(y, ApplicationEngine.ActiveTheme.ProductBoxImageSize, true);
                return mediaModel;
            }).ToList();
            productModel.Media = mediaModels;
            if (product.ProductAttributes != null)
            {
                productModel.ProductAttributes = product.ProductAttributes.Select(x =>
                    {
                        var paModel = new ProductAttributeModel {
                            Name = x.Label,
                            Id = x.Id,
                            InputFieldType = x.InputFieldType,
                            IsRequired = x.IsRequired,
                            AvailableValues = x.AvailableAttribute.AvailableAttributeValues.Select(y =>
                                {
                                    var avModel = new ProductAttributeValueModel() {
                                        Name = y.Value
                                    };
                                    return avModel;
                                })
                                .ToList()
                        };
                        if (paModel.Name.IsNullEmptyOrWhiteSpace())
                        {
                            paModel.Name = x.AvailableAttribute.Name;
                        }
                        return paModel;
                    })
                    .ToList();
            }

            return productModel;
        }
        #endregion

    }
}