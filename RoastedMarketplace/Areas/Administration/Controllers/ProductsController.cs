using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using RoastedMarketplace.Areas.Administration.Models.Media;
using RoastedMarketplace.Areas.Administration.Models.Shop;
using RoastedMarketplace.Core.Extensions;
using RoastedMarketplace.Core.Services;
using RoastedMarketplace.Data.Constants;
using RoastedMarketplace.Data.Entity.Shop;
using RoastedMarketplace.Data.Enum;
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
using RoastedMarketplace.Services.Serializers;

namespace RoastedMarketplace.Areas.Administration.Controllers
{
    public class ProductsController : FoundationAdminController
    {
        private readonly IProductService _productService;
        private readonly IModelMapper _modelMapper;
        private readonly IMediaService _mediaService;
        private readonly IMediaAccountant _mediaAccountant;
        private readonly ICategoryAccountant _categoryAccountant;
        private readonly ICategoryService _categoryService;
        private readonly IProductAttributeService _productAttributeService;
        private readonly IProductAttributeValueService _productAttributeValueService;
        private readonly IProductVariantService _productVariantService;
        private readonly IAvailableAttributeService _availableAttributeService;
        private readonly IAvailableAttributeValueService _availableAttributeValueService;
        private readonly IDataSerializer _dataSerializer;

        public ProductsController(IProductService productService, IModelMapper modelMapper, IMediaService mediaService, IMediaAccountant mediaAccountant, ICategoryAccountant categoryAccountant, ICategoryService categoryService, IProductAttributeService productAttributeService, IProductVariantService productVariantService, IAvailableAttributeValueService availableAttributeValueService, IAvailableAttributeService availableAttributeService, IProductAttributeValueService productAttributeValueService, IDataSerializer dataSerializer)
        {
            _productService = productService;
            _modelMapper = modelMapper;
            _mediaService = mediaService;
            _mediaAccountant = mediaAccountant;
            _categoryAccountant = categoryAccountant;
            _categoryService = categoryService;
            _productAttributeService = productAttributeService;
            _productVariantService = productVariantService;
            _availableAttributeValueService = availableAttributeValueService;
            _availableAttributeService = availableAttributeService;
            _productAttributeValueService = productAttributeValueService;
            _dataSerializer = dataSerializer;
        }

        [HttpGet("", Name = AdminRouteNames.ProductsList)]
        [CapabilityRequired(CapabilitySystemNames.ViewProducts)]
        public IActionResult ProductsList()
        {
            return R.Success.Result;
        }

        [DualGet("{productId}/attributes", Name = AdminRouteNames.ProductAttributesList)]
        [CapabilityRequired(CapabilitySystemNames.EditProduct)]
        public IActionResult ProductAttributesList(int productId)
        {
            var product = _productService.FirstOrDefault(x => x.Id == productId);
            if (product == null)
                return NotFound();
            var model = _modelMapper.Map<ProductModel>(product);
            var productAttributes = _productAttributeService.GetByProductId(productId);
            var amodel = productAttributes.Select(MapProductAttributeModel).ToList();

            return R.Success.With("product", model)
                .With("attributes", () => amodel , () => _dataSerializer.Serialize(amodel))
                .Result;
        }

        [DualGet("{productId}/attributes/{productAttributeId}", Name = AdminRouteNames.EditProductAttribute)]
        [CapabilityRequired(CapabilitySystemNames.EditProduct)]
        public IActionResult ProductAttributeEditor(int productId, int productAttributeId)
        {
            if (productId <= 0 || _productService.Count(x => x.Id == productId) == 0)
                return NotFound();

            var productAttribute = productAttributeId <= 0
                ? null
                : _productAttributeService.Get(productAttributeId);

            var model = productAttribute == null
                ? new ProductAttributeModel() {
                    InputFieldType = InputFieldType.Dropdown
                }
                : MapProductAttributeModel(productAttribute);
            return R.Success.With("productAttribute", model).With("productId", productId).Result;
        }

        [DualGet("{productId}/variants", Name = AdminRouteNames.ProductVariantsList)]
        [CapabilityRequired(CapabilitySystemNames.EditProduct)]
        public IActionResult ProductVariantsList(int productId)
        {
            Product product;
            if (productId == 0 || (product = _productService.FirstOrDefault(x => x.Id == productId)) == null)
                return NotFound();
            var productModel = _modelMapper.Map<ProductModel>(product);
            var variants = _productVariantService.GetByProductId(productId);
            var variantModels = variants.Select(MapProductVariantModel);
            return R.Success.With("variants", () => variantModels, () => _dataSerializer.Serialize(variantModels))
                .With("product", productModel)
                .Result;
        }

        [DualGet("{productId}/variants/{productVariantId}", Name = AdminRouteNames.EditProductVariant)]
        [CapabilityRequired(CapabilitySystemNames.EditProduct)]
        public IActionResult ProductVariantEditor(int productId, int productVariantId)
        {
            if (productId <= 0 || _productService.Count(x => x.Id == productId) == 0)
                return NotFound();
            var productVariant = productVariantId <= 0
                ? new ProductVariant() { ProductId = productId }
                : _productVariantService.Get(productVariantId);

            //get allowed product attributes
            var productAttributes = productVariantId > 0 ? null : _productAttributeService.GetByProductId(productId, true);
            var attributeModels = productAttributes?.Select(MapProductAttributeModel).ToList();
            var variantModel = MapProductVariantModel(productVariant);
            return R.Success.With("attributes", attributeModels).With("variant", variantModel).Result;
        }

        [DualGet("", Name = AdminRouteNames.GetProducts, OnlyApi = true)]
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
            return R.Success.With("totalResults", totalResults)
                .With("page", parameters.Page)
                .With("count", parameters.Count)
                .With("products", productsModel)
                .Result;
        }

        [DualGet("{productId}", Name = AdminRouteNames.GetProduct)]
        [CapabilityRequired(CapabilitySystemNames.EditProduct)]
        public IActionResult ProductEditor(int productId)
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

            //to create category breadcrumb, we'll need all categories so tree can be found
            //todo: find a better way of retrieving categories, as getting all categories is just waste of resources
            var allCategories = _categoryService.GetFullCategoryTree();
            productModel.Categories = product.Categories?.Select(x =>
                {
                    var model = _modelMapper.Map<CategoryModel>(x);
                    model.FullCategoryPath = _categoryAccountant.GetFullBreadcrumb(allCategories.First(y => y.Id == x.Id));
                    return model;
                })
                .OrderBy(x => x.DisplayOrder)
                .ToList();
            return R.Success.With("product", productModel).Result;
        }

        [DualPost("", Name = AdminRouteNames.SaveProduct)]
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
                LinkProductWithCategories(product.Id, model.Categories);
            }
            return R.Success.Result;
        }

        [DualPost("categories/displayorder", Name = AdminRouteNames.UpdateProductCategoryDisplayOrder, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.EditProduct)]
        public IActionResult UpdateCategoriesDisplayOrder(int productId, IList<CategoryModel> categories)
        {
            if (productId == 0 || _productService.Count(x => x.Id == productId) == 0)
                return NotFound();

            LinkProductWithCategories(productId, categories);
            return R.Success.Result;
        }

        [DualPost("categories/delete", Name = AdminRouteNames.DeleteProductCategories, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.EditProduct)]
        public IActionResult DeleteProductCategories(int productId, IList<CategoryModel> categories)
        {
            if (!categories.Any())
                return BadRequest();

            if (productId == 0 || _productService.Count(x => x.Id == productId) == 0)
                return NotFound();

            var categoryIds = categories.Select(x => x.Id).ToArray();
            _productService.RemoveProductCategories(productId, categoryIds);
            return R.Success.Result;
        }

        [DualPost("variants", Name = AdminRouteNames.SaveProductVariant, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.EditProduct)]
        [ValidateModelState(ModelType = typeof(ProductVariantModel))]
        public IActionResult SaveProductVariant(int productId, ProductVariantModel variant)
        {
            Product product;
            if (productId == 0 || (product = _productService.FirstOrDefault(x => x.Id == productId)) == null)
                return NotFound();

            #region Product and Available Attributes
            //create grouping for easy attribute additions
            var productAttributes = _productAttributeService.GetByProductId(productId, true);
            #endregion

            #region Variants

            var variantModel = variant;
            //check existing product variants
            var productVariants = _productVariantService.GetByProductId(productId);
            //is this an update?
            ProductVariant currentVariant = null;
            if (variantModel.Id > 0)
            {
                currentVariant = productVariants.FirstOrDefault(x => x.Id == variantModel.Id);
                if (currentVariant == null)
                {
                    return NotFound();
                }
            }
            else
            {
                //validate if such a variant already exist
                var attributeModels = variantModel.Attributes.Where(x => x.Values != null && x.Values[0].Id > 0);
                var productVariantAttributes = attributeModels.Select(x =>
                    {
                        //find by id and then by name if required
                        var productAttribute =
                                productAttributes.FirstOrDefault(pa => pa.Id == x.Id) ??
                                productAttributes.FirstOrDefault(
                                    y => y.AvailableAttribute.Name.Equals(x.Name, StringComparison.InvariantCultureIgnoreCase));

                        if (productAttribute == null)
                            //overly pessimistic check
                            return null;

                        var attributeId = productAttribute.Id;
                        //same with value
                        var productAttributeValue =
                                productAttribute.ProductAttributeValues.FirstOrDefault(
                                    pav => pav.Id == x.Values[0].Id) ??
                                productAttribute.ProductAttributeValues.FirstOrDefault(
                                    y => y.AvailableAttributeValue.Value.Equals(x.Values[0].AttributeValue,
                                        StringComparison.InvariantCultureIgnoreCase));
                        if (productAttributeValue == null)
                            //overly pessimistic check
                            return null;
                        var attributeValueId = productAttributeValue.Id;

                        return new ProductVariantAttribute() {
                            ProductAttributeId = attributeId,
                            ProductAttributeValueId = attributeValueId
                        };
                    })
                    .ToList();

                currentVariant = productVariants.FirstOrDefault(x =>
                 {
                     return x.ProductVariantAttributes.Count == productVariantAttributes.Count &&
                         x.ProductVariantAttributes.Intersect(productVariantAttributes).Count() ==
                         productVariantAttributes.Count;
                 });

                if (currentVariant != null)
                {
                    return R.Fail.With("error", T("A variant with this combination already exists")).Result;
                }
                currentVariant = new ProductVariant() {
                    ProductVariantAttributes = productVariantAttributes,
                    ProductId = productId
                };
            }
            currentVariant.Gtin = variantModel.Gtin;
            currentVariant.Mpn = variantModel.Mpn;
            currentVariant.Price = variantModel.Price;
            currentVariant.Sku = variantModel.Sku;
            currentVariant.CanOrderWhenOutOfStock = variantModel.CanOrderWhenOutOfStock;
            currentVariant.StockQuantity = variantModel.StockQuantity;
            currentVariant.TrackInventory = variantModel.TrackInventory;
            currentVariant.MediaId = variantModel.MediaId;


            if (variant.Id > 0)
            {
                _productVariantService.Update(currentVariant);
            }
            else
            {
                _productVariantService.AddVariant(product, currentVariant);
            }

            #endregion

            return R.Success.Result;
        }

        [DualPost("attributes", Name = AdminRouteNames.SaveProductAttribute, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.EditProduct)]
        [ValidateModelState(ModelType = typeof(ProductAttributeModel))]
        public IActionResult SaveProductAttributes(int productId, ProductAttributeModel attribute)
        {
            if (productId <= 0 || _productService.Count(x => x.Id == productId) == 0)
                return NotFound();

            SaveProductAttributesImpl(productId, new List<ProductAttributeModel>() { attribute });
            return R.Success.Result;
        }

        [DualPost("attributes/delete", Name = AdminRouteNames.DeleteProductAttribute, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.EditProduct)]
        public IActionResult DeleteProductAttribute(int productAttributeId)
        {
            //delete variants
            Transaction.Initiate(transaction =>
            {
                _productVariantService.DeleteVariantsByProductAttributeId(productAttributeId, transaction);
                _productAttributeService.Delete(x => x.Id == productAttributeId, transaction);
            });

            return R.Success.Result;
        }

        [DualPost("variants/delete", Name = AdminRouteNames.DeleteProductVariant, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.EditProduct)]
        public IActionResult DeleteProductVariant(int productVariantId)
        {
            if (productVariantId <= 0)
                return NotFound();
            _productVariantService.Delete(x => x.Id == productVariantId);
            return R.Success.Result;
        }

        [DualPost("attributes/values/delete", Name = AdminRouteNames.DeleteProductAttributeValue, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.EditProduct)]
        public IActionResult DeleteProductAttributeValue(int productAttributeValueId)
        {
            //find product attribute id
            var productAttributeValue = _productAttributeValueService.Get(productAttributeValueId);
            if (productAttributeValue == null)
                return NotFound();
            var productAttribute = _productAttributeService.Get(productAttributeValue.ProductAttributeId);
            //delete variants
            Transaction.Initiate(transaction =>
           {
               _productVariantService.DeleteVariantsByProductAttributeValueId(productAttributeValueId, transaction);
               _productAttributeValueService.Delete(x => x.Id == productAttributeValueId, transaction);
               if (productAttribute.ProductAttributeValues.Count == 1)
               {
                   //this was last value we just deleted, delete the product attribute as well
                   _productAttributeService.Delete(productAttribute, transaction);
               }
           });

            return R.Success.Result;
        }

        #region Helpers

        private ProductAttributeModel MapProductAttributeModel(ProductAttribute pa)
        {
            var attributeModel = _modelMapper.Map<ProductAttributeModel>(pa);
            attributeModel.Name = pa.AvailableAttribute.Name;
            foreach (var pav in pa.ProductAttributeValues)
            {
                var valueModel = _modelMapper.Map<ProductAttributeValueModel>(pav);
                valueModel.AttributeValue = pav.AvailableAttributeValue.Value;
                attributeModel.Values.Add(valueModel);
            }
            return attributeModel;
        }

        private ProductVariantModel MapProductVariantModel(ProductVariant pv)
        {
            var vm = _modelMapper.Map<ProductVariantModel>(pv);
            if (pv.ProductVariantAttributes == null)
                return vm;
            vm.Attributes = pv.ProductVariantAttributes.Select(y =>
            {
                var am = new ProductAttributeModel() {
                    Id = y.ProductAttributeId,
                    Name = y.ProductAttribute.AvailableAttribute.Name,
                    Label = y.ProductAttribute.Label,
                    Values = new List<ProductAttributeValueModel>()
                    {
                        new ProductAttributeValueModel()
                        {
                            Label = y.ProductAttributeValue.Label,
                            AttributeValue = y.ProductAttributeValue.AvailableAttributeValue.Value,
                            Id = y.ProductAttributeValueId
                        }
                    }
                };
                return am;
            }).ToList();
            return vm;
        }

        private void LinkProductWithCategories(int productId, IList<CategoryModel> categories)
        {
            IList<Category> allCategories = null;

            if (categories.Any(x => x.Id == 0))
            {
                allCategories = _categoryService.GetFullCategoryTree();
            }
            foreach (var categoryModel in categories)
            {
                if (categoryModel.Id == 0)
                {
                    //new category to be added
                    var newCategory = _categoryAccountant.CreateCategoryTree(categoryModel.FullCategoryPath, allCategories);
                    categoryModel.Id = newCategory.Id;
                }
                _productService.LinkCategoryWithProduct(categoryModel.Id, productId, categoryModel.DisplayOrder);
            }
        }

        private IList<ProductAttribute> SaveProductAttributesImpl(int productId, IList<ProductAttributeModel> attributeModels)
        {
            //exclude invalid attributes
            attributeModels = attributeModels.Where(x => !x.Name.IsNullEmptyOrWhitespace() || x.Id != 0)
                .Select(x =>
                {
                    x.Values = x.Values.Where(y => !y.AttributeValue.IsNullEmptyOrWhitespace() || y.Id != 0).ToList();
                    return x;
                })
                .ToList();

            //first get all the available attributes
            var availableAttributes = _availableAttributeService.Get(x => true).ToList();
            var availableAttributeValues = _availableAttributeValueService.Get(x => true).ToList();

            //get all the existing linked attributes
            var productAttributes = _productAttributeService.GetByProductId(productId);
            //perform everything in a transaction, we don't want to make db inconsistent
            Transaction.Initiate(transaction =>
            {
                transaction = null;
                foreach (var model in attributeModels)
                {
                    ProductAttribute savedProductAttribute = null;
                    if (model.Id == 0)
                    {
                        var attribute = availableAttributes.FirstOrDefault(x => x.Name.Equals(model.Name, StringComparison.InvariantCultureIgnoreCase));
                        if (attribute == null)
                        {
                            //save this attribute for future usage
                            attribute = new AvailableAttribute() {
                                Name = model.Name
                            };
                            _availableAttributeService.Insert(attribute, transaction);

                            //add to existing attribute list
                            availableAttributes.Add(attribute);
                        }
                        savedProductAttribute = new ProductAttribute() {
                            ProductId = productId,
                            AvailableAttributeId = attribute.Id,
                            AvailableAttribute = attribute,
                        };
                    }
                    else
                    {
                        savedProductAttribute =
                            productAttributes.FirstOrDefault(x => x.Id == model.Id && x.ProductId == productId);
                    }

                    if (savedProductAttribute == null)
                    {
                        //probably a wrong product attribute id has been passed, do nothing
                        return;
                    }
                    savedProductAttribute.InputFieldType = model.InputFieldType;
                    savedProductAttribute.Label = model.Label;
                    savedProductAttribute.IsRequired = model.IsRequired;

                    if (savedProductAttribute.Id == 0)
                    {
                        _productAttributeService.Insert(savedProductAttribute, transaction);
                        //add to our saved list
                        productAttributes.Add(savedProductAttribute);
                    }
                    else
                        _productAttributeService.Update(savedProductAttribute, transaction);

                    //go for values
                    foreach (var valueModel in model.Values)
                    {
                        AvailableAttributeValue attributeValue = null;
                        if (valueModel.Id == 0)
                        {
                            attributeValue =
                                availableAttributeValues.FirstOrDefault(
                                    x => x.AvailableAttributeId == savedProductAttribute.AvailableAttributeId &&
                                         x.Value.Equals(valueModel.AttributeValue,
                                             StringComparison.InvariantCultureIgnoreCase));

                            if (attributeValue == null)
                            {
                                attributeValue = new AvailableAttributeValue() {
                                    AvailableAttributeId = savedProductAttribute.AvailableAttributeId,
                                    Value = valueModel.AttributeValue,
                                    AvailableAttribute = savedProductAttribute.AvailableAttribute
                                };
                                _availableAttributeValueService.Insert(attributeValue, transaction);
                                //add to available attribute values
                                availableAttributeValues.Add(attributeValue);
                            }
                            savedProductAttribute.ProductAttributeValues =
                                savedProductAttribute.ProductAttributeValues ?? new List<ProductAttributeValue>();

                            if (!savedProductAttribute.ProductAttributeValues?.Any(
                                    x => x.AvailableAttributeValueId == attributeValue.Id) ?? false)
                            {
                                var savedAttributeValue = new ProductAttributeValue() {
                                    AvailableAttributeValueId = attributeValue.Id,
                                    ProductAttributeId = savedProductAttribute.Id,
                                    AvailableAttributeValue = attributeValue
                                };
                                _productAttributeService.AddProductAttributeValue(savedAttributeValue, transaction);

                                //and add this to our attribute
                                savedProductAttribute.ProductAttributeValues.Add(savedAttributeValue);
                            }

                        }
                    }
                }
            });
            return productAttributes;
        }
        #endregion
    }
}