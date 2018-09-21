using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using RoastedMarketplace.Areas.Administration.Models.Media;
using RoastedMarketplace.Areas.Administration.Models.Shop;
using RoastedMarketplace.Core.Extensions;
using RoastedMarketplace.Data.Constants;
using RoastedMarketplace.Data.Entity.Shop;
using RoastedMarketplace.Infrastructure;
using RoastedMarketplace.Infrastructure.MediaServices;
using RoastedMarketplace.Infrastructure.Mvc;
using RoastedMarketplace.Infrastructure.Mvc.Attributes;
using RoastedMarketplace.Infrastructure.Mvc.ModelFactories;
using RoastedMarketplace.Infrastructure.Mvc.Models;
using RoastedMarketplace.Infrastructure.Routing;
using RoastedMarketplace.Infrastructure.Security.Attributes;
using RoastedMarketplace.Services.MediaServices;
using RoastedMarketplace.Services.Products;

namespace RoastedMarketplace.Areas.Administration.Controllers
{
    public class ProductsController : FoundationAdminController
    {
        private readonly IProductService _productService;
        private readonly IModelMapper _modelMapper;
        private readonly IMediaService _mediaService;
        private readonly IMediaAccountant _mediaAccountant;
        private readonly ICategoryAccountant _categoryAccountant;

        public ProductsController(IProductService productService, IModelMapper modelMapper, IMediaService mediaService, IMediaAccountant mediaAccountant, ICategoryAccountant categoryAccountant)
        {
            _productService = productService;
            _modelMapper = modelMapper;
            _mediaService = mediaService;
            _mediaAccountant = mediaAccountant;
            _categoryAccountant = categoryAccountant;
        }

        [HttpGet("", Name = AdminRouteNames.ProductsList)]
        [CapabilityRequired(CapabilitySystemNames.ViewProducts)]
        public IActionResult Products()
        {
            return View("Catalog/ProductList");
        }

        [DualGet("get", Name = AdminRouteNames.GetProducts, OnlyApi = true)]
        [ValidateModelState(ModelType = typeof(SearchModel))]
        [CapabilityRequired(CapabilitySystemNames.ViewProducts)]
        public IActionResult GetProducts([FromQuery] ProductSearchModel parameters)
        {
            //create order by expression
            Expression<Func<Product, object>> orderByExpression = null;
            if (!parameters.SortColumn.IsNullEmptyOrWhitespace())
            {
                switch (parameters.SortColumn.ToLower())
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
                    default:
                        orderByExpression = product => product.Id;
                        break;

                }
            }

            var products = _productService.GetProducts(out int totalResults, parameters.Search, parameters.Published,
                parameters.ManufacturerIds, parameters.VendorIds, parameters.CategoryIds, orderByExpression, parameters.SortOrder, parameters.Page,
                parameters.Count);
            var productsModel = products.Select(x => _modelMapper.Map<ProductModel>(x));
            return Result(new { success = true, totalResults = totalResults, page = parameters.Page, count = parameters.Count, products = productsModel });
        }

        [DualGet("get/{productId}", Name = AdminRouteNames.GetProduct)]
        [CapabilityRequired(CapabilitySystemNames.EditProduct)]
        public IActionResult GetProduct(int productId)
        {
            var product = productId > 0 ? _productService.Get(productId) : new Product();
            if (productId > 0 && product == null)
                return NotFound();

            var productModel = _modelMapper.Map<ProductModel>(product);
            productModel.Media = product.MediaItems?.Select(x =>
                {
                    var model = _modelMapper.Map<MediaModel>(x);
                    model.ThumbnailUrl = _mediaAccountant.GetPictureUrl(x, ApplicationConfig.AdminThumbnailWidth,
                        ApplicationConfig.AdminThumbnailHeight);
                    return model;
                })
                .OrderBy(x => x.DisplayOrder)
                .ToList();

            //categories
            productModel.Categories = product.Categories?.Select(x =>
                {
                    var model = _modelMapper.Map<CategoryModel>(x);
                    model.FullCategoryPath = _categoryAccountant.GetFullBreadcrumb(x);
                    return model;
                })
                .OrderBy(x => x.DisplayOrder)
                .ToList();
            return Result("Catalog/ProductEditor", new { product = productModel });
        }

        [DualPost("post", Name = AdminRouteNames.SaveProduct)]
        [CapabilityRequired(CapabilitySystemNames.EditProduct)]
        [ValidateModelState(ModelType = typeof(ProductModel))]
        public IActionResult SaveProduct(ProductModel model)
        {
            var product = model.Id > 0 ? _productService.Get(model.Id) : new Product();
            //is that a non-existent product?
            if (product == null)
                return BadRequest();
            //update the product properties
            _modelMapper.Map(model, product);
            product.UpdatedOn = DateTime.UtcNow;
            //perform the requisites
            _productService.InsertOrUpdate(product);
            //attach media for new products only
            if (model.Id == 0 && model.Media != null && model.Media.Any(x => x.Id > 0))
            {
                foreach (var mediaModel in model.Media)
                {
                    _productService.LinkMediaWithProduct(mediaModel.Id, product.Id);
                }
            }

            //attach category
            if (model.Categories != null)
            {
                foreach (var categoryModel in model.Categories)
                {
                    _productService.LinkCategoryWithProduct(categoryModel.Id, product.Id, categoryModel.DisplayOrder);
                }
            }
            return Result(new {success = true});
        }

    }
}