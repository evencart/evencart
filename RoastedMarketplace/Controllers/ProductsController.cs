using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RoastedMarketplace.Data.Entity.Settings;
using RoastedMarketplace.Data.Entity.Shop;
using RoastedMarketplace.Data.Enum;
using RoastedMarketplace.Data.Extensions;
using RoastedMarketplace.Infrastructure;
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
using RoastedMarketplace.Services.Search;

namespace RoastedMarketplace.Controllers
{
    public class ProductsController : FoundationController
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly CatalogSettings _catalogSettings;
        private readonly IModelMapper _modelMapper;
        private readonly IMediaAccountant _mediaAccountant;
        private readonly ISearchQueryParserService _searchQueryParserService;
        private readonly IProductRelationService _productRelationService;
        public ProductsController(IProductService productService, ICategoryService categoryService, CatalogSettings catalogSettings, IModelMapper modelMapper, IMediaAccountant mediaAccountant, ISearchQueryParserService searchQueryParserService, IProductRelationService productRelationService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _catalogSettings = catalogSettings;
            _modelMapper = modelMapper;
            _mediaAccountant = mediaAccountant;
            _searchQueryParserService = searchQueryParserService;
            _productRelationService = productRelationService;
        }

        [DynamicRoute(Name = RouteNames.SingleProduct, SeoEntityName = nameof(Product), SettingName = nameof(UrlSettings.ProductUrlTemplate))]
        public IActionResult Index(int id)
        {
            if (id < 1)
                return NotFound();
            var product = _productService.Get(id);
            if (!product.IsPublic())
                return NotFound();

            var productModel = MapProductModel(product);

            var response = R.Success;
            response.With("product", productModel);

            if (_catalogSettings.EnableRelatedProducts)
            {
                var productRelations = _productRelationService.GetByProductId(id, ProductRelationType.RelatedProduct, 1,
                    _catalogSettings.NumberOfRelatedProducts);
                if (productRelations.Any())
                {
                    var relatedProductsModel = productRelations.Select(x => x.DestinationProduct).Select(MapProductModel).ToList();
                    response.With("relatedProducts", relatedProductsModel);
                }

            }
            SeoMetaModel seoMetaModel = null;
            if (product.SeoMeta != null)
            {
                seoMetaModel = _modelMapper.Map<SeoMetaModel>(product.SeoMeta);
            }
            seoMetaModel = seoMetaModel ?? new SeoMetaModel();
            seoMetaModel.Description = product.Description;
            seoMetaModel.PageTitle = seoMetaModel.PageTitle ?? product.Name;
            seoMetaModel.MetaDescription = seoMetaModel.MetaDescription ?? product.Description;

            response.WithSeoMeta(seoMetaModel);
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
                if (_catalogSettings.DisplayProductsFromChildCategories)
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
                vendorIds: searchModel.VendorIds,
                manufacturerIds: searchModel.ManufacturerIds,
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

            if (product.ProductSpecifications != null)
            {
                productModel.ProductSpecificationGroups = new List<ProductSpecificationGroupModel>();

                foreach (var grp in product.ProductSpecifications.GroupBy(x => x.ProductSpecificationGroup))
                {
                    var groupName = grp?.Key?.Name ?? "";
                    var specs = grp.Select(x => new ProductSpecificationModel() {
                        Name = x.Label,
                        Values = x.ProductSpecificationValues.Select(y => y.Label).ToList()
                    }).ToList();
                    productModel.ProductSpecificationGroups.Add(new ProductSpecificationGroupModel() {
                        Name = groupName,
                        ProductSpecifications = specs
                    });
                }
            }

            //reviews
            if (product.ReviewSummary != null)
                productModel.ReviewSummary = _modelMapper.Map<ReviewSummaryModel>(product.ReviewSummary);
            return productModel;
        }
        #endregion

    }
}