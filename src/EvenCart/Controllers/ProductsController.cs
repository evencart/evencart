using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using EvenCart.Areas.Administration.Extensions;
using EvenCart.Core.Infrastructure.Providers;
using EvenCart.Data.Constants;
using EvenCart.Data.Entity.Settings;
using EvenCart.Data.Entity.Shop;
using EvenCart.Data.Enum;
using EvenCart.Data.Extensions;
using EvenCart.Services.Extensions;
using EvenCart.Services.Products;
using EvenCart.Services.Reviews;
using EvenCart.Services.Serializers;
using EvenCart.Factories.Products;
using EvenCart.Infrastructure;
using EvenCart.Infrastructure.Extensions;
using EvenCart.Infrastructure.Helpers;
using EvenCart.Infrastructure.Mvc;
using EvenCart.Infrastructure.Mvc.ModelFactories;
using EvenCart.Infrastructure.Routing;
using EvenCart.Models.Products;
using EvenCart.Models.Reviews;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Controllers
{
    /// <summary>
    /// Allows user to get catalog data such as products
    /// </summary>
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
        private readonly IDownloadService _downloadService;
        private readonly IUploadService _uploadService;
        private readonly ILocalFileProvider _localFileProvider;
        public ProductsController(IProductService productService, ICategoryService categoryService, CatalogSettings catalogSettings, IModelMapper modelMapper, IProductRelationService productRelationService, IProductVariantService productVariantService, IDataSerializer dataSerializer, IPriceAccountant priceAccountant, TaxSettings taxSettings, IReviewService reviewService, GeneralSettings generalSettings, IProductModelFactory productModelFactory, IDownloadService downloadService, IUploadService uploadService, ILocalFileProvider localFileProvider)
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
            _downloadService = downloadService;
            _uploadService = uploadService;
            _localFileProvider = localFileProvider;
        }

        [HttpGet("product-preview/{id}", Name = RouteNames.PreviewProduct)]
        public IActionResult Preview(int id)
        {
            var url = ApplicationEngine.RouteUrl(RouteNames.SingleProduct, new {id = id});
            return Redirect(url);
        }

        [DynamicRoute(Name = RouteNames.SingleProduct, SeoEntityName = nameof(Product), SettingName = nameof(UrlSettings.ProductUrlTemplate))]
        public IActionResult Index(int id)
        {
            if (id < 1)
                return NotFound();
            var product = _productService.Get(id);
            if (!product.IsPublic() && !CurrentUser.Can(CapabilitySystemNames.EditProduct))
                return NotFound();
            //is the product restricted to roles
            if (product.RestrictedToRoles)
            {
                var userRoleIds = CurrentUser?.Roles?.Select(x => x.Id).ToList();
                if(userRoleIds == null || product.EntityRoles.All(x => !userRoleIds.Contains(x.RoleId)))
                {
                    return NotFound();
                }
            }
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
                    _priceAccountant.GetProductPriceDetails(product, null, variant.Price, out decimal priceWithoutTax, out decimal tax, out decimal taxRate, out _);
                    var variantObject = new
                    {
                        attributes = new Dictionary<string, string>(),
                        price = _priceAccountant
                            .ConvertCurrency(
                                (_taxSettings.DisplayProductPricesWithoutTax ? priceWithoutTax : priceWithoutTax + tax),
                                ApplicationEngine.CurrentCurrency).ToCurrency(),
                        isAvailable = !variant.TrackInventory ||
                                      (variant.TrackInventory && variant.IsAvailableInStock(product)),
                        sku = !variant.Sku.IsNullEmptyOrWhiteSpace() ? variant.Sku : product.Sku,
                        gtin = !variant.Gtin.IsNullEmptyOrWhiteSpace() ? variant.Gtin : product.Gtin,
                        mpn = !variant.Mpn.IsNullEmptyOrWhiteSpace() ? variant.Mpn : product.Mpn
                    };
                    foreach (var pva in variant.ProductVariantAttributes)
                    {
                        variantObject.attributes.Add(pva.ProductAttribute.Label, pva.ProductAttributeValue.AvailableAttributeValue.Value);
                    }
                    variantModels.Add(variantObject);
                }

                if (variantModels.Any())
                    response.With("variants", variantModels);
                productModel.IsAvailable = variantModels.Any(x => (bool) ((dynamic) x).isAvailable);
            }

            //downloads
            if (product.IsDownloadable)
            {
                var downloads = _downloadService.GetWithoutBytes(x => x.ProductId == product.Id && !x.RequirePurchase).ToList();
                if (CurrentUser.IsVisitor())
                {
                    downloads = downloads.Where(x => !x.RequireLogin).ToList();
                }

                var downloadModels = downloads.Select(_productModelFactory.Create).ToList();
                response.With("downloads", downloadModels);
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
                var category = product.Categories?.FirstOrDefault();
                var currentCategoryFull = categoryTree.FirstOrDefault(x => x.Id == category?.Id);
                BreadcrumbHelper.SetCategoryBreadcrumb(currentCategoryFull, categoryTree);
                SetBreadcrumbToRoute(product.Name, RouteNames.SingleProduct, new {seName = product.SeoMeta.Slug}, localize: false);
            }
            //seo data
            SeoMetaHelper.SetSeoData(product.Name, product.Description, product.Description);
            return response.With("preview", !product.Published).Result;
        }

        [DynamicRoute(Name = RouteNames.ProductsPage, SeoEntityName = nameof(Category),
            SettingName = nameof(UrlSettings.CategoryUrlTemplate),
            ParameterName = nameof(ProductSearchModel.CategoryId))]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult ProductsList(ProductSearchModel searchModel)
        {
            return ProductsListApi(searchModel);
        }
        /// <summary>
        /// Searches for specific products in the catalog
        /// </summary>
        /// <param name="searchModel"></param>
        /// <response code="200">A list of <see cref="ProductModel">products</see> objects</response>
        [DualGet("~/s", Name = RouteNames.ProductsSearchPage)]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult ProductSearch(ProductSearchModel searchModel)
        {
            if (searchModel.Search.IsNullEmptyOrWhiteSpace())
                return RedirectToRoute(RouteNames.Home);
            return ProductsListApi(searchModel);
        }
        /// <summary>
        /// Gets products from catalog.
        /// </summary>
        /// <param name="searchModel"></param>
        /// <param name="viewName"></param>
        /// <response code="200">A list of <see cref="ProductModel">products</see> objects</response>
        [DualGet("", Name = RouteNames.ProductsPage, OnlyApi = true)]
        public IActionResult ProductsListApi(ProductSearchModel searchModel, string viewName = null)
        {
            searchModel.Count = _catalogSettings.NumberOfProductsPerPage;

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
            switch (searchModel.SortColumn?.ToLower())
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

            IList<int> roleIds = CurrentUser?.Roles?.Select(x => x.Id).ToList();
            var products = _productService.GetProducts(out int totalResults,
                out decimal availableFromPrice,
                out decimal availableToPrice,
                out Dictionary<int, string> availableManufacturers,
                out Dictionary<int, string> availableVendors,
                out Dictionary<string, List<string>> availableFilters,
                searchText: searchModel.Search,
                filterExpression: searchModel.Filters,
                published: true,
                tags: searchModel.Tags,
                vendorIds: searchModel.VendorIds,
                manufacturerIds: searchModel.ManufacturerIds,
                categoryids: categoryIds,
                roleIds:roleIds,
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

        /// <summary>
        /// Uploads a file for a product. The product must have at least one <see cref="InputFieldType.FileUpload"/> attribute to accept uploaded file.
        /// </summary>
        /// <param name="uploadFileModel"></param>
        /// <response code="200">The url of the file as 'url' and the unique id of the file as 'guid'</response>
        [DualPost("upload", Name = RouteNames.UploadFile, OnlyApi = true)]
        public IActionResult UploadFile(UploadFileModel uploadFileModel)
        {
            if (uploadFileModel.ProductId < 1)
                return NotFound();
            var product = _productService.Get(uploadFileModel.ProductId);
            if (!product.IsPublic() && !CurrentUser.Can(CapabilitySystemNames.EditProduct))
                return NotFound();

            if (product.ProductAttributes.All(x => x.InputFieldType != InputFieldType.FileUpload))
                return R.Fail.With("error", T("The product doesn't accept any uploads")).Result;

            //guest signin if user is not signed in
            ApplicationEngine.GuestSignIn();
            var fileBytes = uploadFileModel.MediaFile.GetBytesAsync().Result;
            var upload = new Upload()
            {
                UserId = CurrentUser.Id,
                FileBytes = fileBytes,
                FileType = uploadFileModel.MediaFile.ContentType,
                FileExtension = _localFileProvider.GetExtension(uploadFileModel.MediaFile.FileName),
                Guid = Guid.NewGuid().ToString()
            };
            _uploadService.Insert(upload);
            var downloadUrl = ApplicationEngine.RouteUrl(RouteNames.DownloadUploadFile, new {guid = upload.Guid});
            return R.Success.With("guid", upload.Guid).With("url", downloadUrl).Result;
        }

        /// <summary>
        /// Downloads an uploaded file
        /// </summary>
        /// <param name="guid">The guid of the file</param>
        [HttpGet("upload/download/{guid}", Name = RouteNames.DownloadUploadFile)]
        public IActionResult DownloadUploadFile(string guid)
        {
            var upload = _uploadService.FirstOrDefault(x => x.Guid == guid);
            if (upload == null || upload.UserId != CurrentUser.Id)
                return NotFound();
            return File(upload.FileBytes, upload.FileType, $"{upload.UserId}.{upload.Guid}.{upload.FileExtension}");
        }
    }
}