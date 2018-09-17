using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RoastedMarketplace.Areas.Administration.Models.Shop;
using RoastedMarketplace.Data.Entity.Shop;
using RoastedMarketplace.Infrastructure.Mvc;
using RoastedMarketplace.Infrastructure.Mvc.Attributes;
using RoastedMarketplace.Infrastructure.Mvc.ModelFactories;
using RoastedMarketplace.Infrastructure.Routing;
using RoastedMarketplace.Services.Products;

namespace RoastedMarketplace.Areas.Administration.Controllers
{
    public class ProductController : FoundationAdminController
    {
        private readonly IProductService _productService;
        private readonly IModelMapper _modelMapper;
        public ProductController(IProductService productService, IModelMapper modelMapper)
        {
            _productService = productService;
            _modelMapper = modelMapper;
        }

        [DualGet("get", Name = AdminRouteNames.GetProducts)]
        public IActionResult AllProducts()
        {
            var products = _productService.GetProducts();
            var productsModel = products.Select(x => _modelMapper.Map<ProductModel>(x));
            return Result("Catalog/ProductList", new { product = productsModel });
        }

        [HttpGet("get/{productId}", Name = AdminRouteNames.GetProduct)]
     //   [CapabilityRequired(CapabilitySystemNames.EditProduct)]
        public IActionResult GetProduct(int productId)
        {
            var product = productId > 0 ? _productService.Get(productId) : new Product();
            if (productId > 0 && product == null)
                return NotFound();

            var productModel = _modelMapper.Map<ProductModel>(product);
            return Result("Catalog/ProductEditor", new {product = productModel});
        }

        [DualPost("post", Name = AdminRouteNames.SaveProduct)]
       // [CapabilityRequired(CapabilitySystemNames.EditProduct)]
        [ValidateModelState(ModelType = typeof(ProductModel))]
        public IActionResult SaveProduct(ProductModel model)
        {
            var product = _modelMapper.Map<Product>(model);
            if (product.Id == 0)
                product.CreatedOn = DateTime.UtcNow;

            product.UpdatedOn = DateTime.UtcNow;
            if (product.Id == 0)
                _productService.Insert(product);
            else
                _productService.Update(product);

            return Result(new {success = true});
        }
    }
}