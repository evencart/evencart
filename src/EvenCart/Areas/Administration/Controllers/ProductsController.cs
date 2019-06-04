using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using EvenCart.Areas.Administration.Extensions;
using EvenCart.Areas.Administration.Models.Media;
using EvenCart.Areas.Administration.Models.Pages;
using EvenCart.Areas.Administration.Models.Shop;
using EvenCart.Core.Services;
using EvenCart.Data.Constants;
using EvenCart.Data.Entity.Shop;
using EvenCart.Data.Enum;
using EvenCart.Data.Extensions;
using EvenCart.Services.MediaServices;
using EvenCart.Services.Pages;
using EvenCart.Services.Products;
using EvenCart.Services.Serializers;
using EvenCart.Infrastructure;
using EvenCart.Infrastructure.MediaServices;
using EvenCart.Infrastructure.Mvc;
using EvenCart.Infrastructure.Mvc.Attributes;
using EvenCart.Infrastructure.Mvc.ModelFactories;
using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Routing;
using EvenCart.Infrastructure.Security.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Areas.Administration.Controllers
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
        private readonly IManufacturerService _manufacturerService;
        private readonly IProductSpecificationService _productSpecificationService;
        private readonly IProductSpecificationValueService _productSpecificationValueService;
        private readonly IProductSpecificationGroupService _productSpecificationGroupService;
        private readonly IProductRelationService _productRelationService;
        private readonly ISeoMetaService _seoMetaService;
        public ProductsController(IProductService productService, IModelMapper modelMapper, IMediaService mediaService, IMediaAccountant mediaAccountant, ICategoryAccountant categoryAccountant, ICategoryService categoryService, IProductAttributeService productAttributeService, IProductVariantService productVariantService, IAvailableAttributeValueService availableAttributeValueService, IAvailableAttributeService availableAttributeService, IProductAttributeValueService productAttributeValueService, IDataSerializer dataSerializer, IManufacturerService manufacturerService, IProductSpecificationService productSpecificationService, IProductSpecificationValueService productSpecificationValueService, IProductSpecificationGroupService productSpecificationGroupService, IProductRelationService productRelationService, ISeoMetaService seoMetaService)
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
            _manufacturerService = manufacturerService;
            _productSpecificationService = productSpecificationService;
            _productSpecificationValueService = productSpecificationValueService;
            _productSpecificationGroupService = productSpecificationGroupService;
            _productRelationService = productRelationService;
            _seoMetaService = seoMetaService;
        }


        #region Products
        [DualGet("", Name = AdminRouteNames.ProductsList)]
        [ValidateModelState(ModelType = typeof(AdminSearchModel))]
        [CapabilityRequired(CapabilitySystemNames.ViewProducts)]
        public IActionResult ProductsList([FromQuery] ProductSearchModel parameters)
        {
            parameters = parameters ?? new ProductSearchModel();
            //create order by expression
            Expression<Func<Product, object>> orderByExpression = null;
            if (!parameters.SortColumn.IsNullEmptyOrWhiteSpace())
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

            var products = _productService.GetProducts(out int totalResults, out decimal availableFromPrice,
                out decimal availableToPrice, out Dictionary<int, string> availableManufacturers,
                out Dictionary<int, string> availableVendors,
                out Dictionary<string, List<string>> availableFilters, parameters.SearchPhrase, null, parameters.Published,
                parameters.ManufacturerIds, parameters.VendorIds, parameters.CategoryIds, null, null, orderByExpression,
                parameters.SortOrder, parameters.Current,
                parameters.RowCount);
            var productsModel = products.Select(x =>
            {
                var model = _modelMapper.Map<ProductModel>(x);
                model.Media = x.MediaItems?.Select(y =>
                    {
                        var mediaModel = _modelMapper.Map<MediaModel>(y);
                        mediaModel.ThumbnailUrl = _mediaAccountant.GetPictureUrl(y, 100, 100);
                        return mediaModel;
                    })
                    .ToList();
                return model;
            });
            return R.Success.WithGridResponse(totalResults, parameters.Current, parameters.RowCount)
                .WithAvailableInputTypes()
                .With("products", () => productsModel, () => _dataSerializer.Serialize(productsModel))
                .With("availableFromPrice", availableFromPrice)
                .With("availableToPrice", availableToPrice)
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
            productModel.SeoMeta = _modelMapper.Map<SeoMetaModel>(product.SeoMeta);
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

            productModel.ManufacturerId = product.Manufacturer?.Id;
            productModel.ManufacturerName = product.Manufacturer?.Name;
            return R.Success.With("product", productModel).With("productId", productId).WithWeightUnits()
                .WithLengthUnits().Result;
        }

        [DualPost("", Name = AdminRouteNames.SaveProduct, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.EditProduct)]
        [ValidateModelState(ModelType = typeof(ProductModel))]
        public IActionResult SaveProduct(ProductModel model)
        {
            var product = model.Id > 0 ? _productService.Get(model.Id) : new Product();
            //is that a non-existent product?
            if (product == null)
                return NotFound();
            if (model.Published && (model.SeoMeta?.Slug.IsNullEmptyOrWhiteSpace() ?? true))
            {
                if (model.Id > 0)
                    return R.Fail.With("error", "Can't publish product without slug").Result;
            }
            //do we have manufacturer?
            if (model.ManufacturerId == null && !model.ManufacturerName.IsNullEmptyOrWhiteSpace())
            {
                //find the manufacturer
                var manufacturer = _manufacturerService.FirstOrDefault(x => x.Name == model.ManufacturerName);
                model.ManufacturerId = manufacturer?.Id;
            }
            else if (model.ManufacturerId.HasValue && model.ManufacturerId.Value > 0)
            {
                var manufacturer = _manufacturerService.Get(model.ManufacturerId.Value);
                model.ManufacturerId = manufacturer?.Id;
            }
            else
                model.ManufacturerId = null;

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

            //update seo
            _seoMetaService.UpdateSeoMetaForEntity(product, model.SeoMeta);
            return R.Success.Result;
        }

        [DualPost("{productId}/delete", Name = AdminRouteNames.DeleteProduct, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.DeleteProduct)]
        public IActionResult DeleteProduct(int productId)
        {
            var product = productId > 0 ? _productService.Get(productId) : null;
            if (product == null)
                return NotFound();
            _productService.Delete(product);
            return R.Success.Result;
        }
        #endregion

        #region Product Attributes
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
                .With("productId", productId)
                .With("attributes", () => amodel, () => _dataSerializer.Serialize(amodel))
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
            return R.Success.With("productAttribute", model).With("productId", productId).WithAvailableInputTypes().Result;
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

        #endregion

        #region Product Variants
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
                .With("productId", productId)
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

        [DualPost("variants/delete", Name = AdminRouteNames.DeleteProductVariant, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.EditProduct)]
        public IActionResult DeleteProductVariant(int productVariantId)
        {
            if (productVariantId <= 0)
                return NotFound();
            var variant = _productVariantService.Get(productVariantId);
            var productId = variant.ProductId;
            _productVariantService.Delete(x => x.Id == productVariantId);
            //update variant's product if there are no more any variants
            if (_productVariantService.Count(x => x.ProductId == productId) == 0)
            {
                _productVariantService.Update(new { HasVariants = false }, x => x.ProductId == productId, null);
            }

            return R.Success.Result;
        }
        #endregion

        #region Categories
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
        #endregion

        #region Product Specs
        [DualGet("{productId}/specifications", Name = AdminRouteNames.ProductSpecificationsList)]
        [CapabilityRequired(CapabilitySystemNames.EditProduct)]
        public IActionResult ProductSpecsList(int productId)
        {
            var product = _productService.FirstOrDefault(x => x.Id == productId);
            if (product == null)
                return NotFound();
            var model = _modelMapper.Map<ProductModel>(product);
            var productSpecs = _productSpecificationService.GetByProductId(productId);
            var aModel = productSpecs.Select(MapProductSpecificationModel).ToList();
            var specsList = aModel.GroupBy(x => x.ProductSpecificationGroup)
                .Select(x =>
                {
                    return new ProductSpecificationListModel() {
                        ProductSpecificationGroup = x.Key,
                        ProductSpecifications = x.ToList(),
                        ProductSpecificationsSerialized = RequestHelper.IsApiCall()
                            ? null
                            : _dataSerializer.Serialize(x.ToList())
                    };
                });

            //also find out groups with no specifications so far
            var groupIds = aModel.Select(x => x.ProductSpecificationGroupId).Distinct().ToList();

            var otherGroups = groupIds.Any() ?
                _productSpecificationGroupService.Get(x => x.ProductId == productId && !groupIds.Contains(x.Id)) :
                _productSpecificationGroupService.Get(x => x.ProductId == productId);

            specsList = specsList.Concat(otherGroups.Select(x => new ProductSpecificationListModel()
            {
                ProductSpecificationGroup = new ProductSpecificationGroupModel()
                {
                    Name = x.Name,
                    ProductId = x.ProductId,
                    DisplayOrder = x.DisplayOrder,
                    Id = x.Id
                },
                ProductSpecifications = null,
                ProductSpecificationsSerialized = "[]"
            }));

           
            var nullProductSpecGroup = new ProductSpecificationGroupModel();

            var productSpecificationListModels = specsList as IList<ProductSpecificationListModel> ?? specsList.ToList();
            foreach (var specListItem in productSpecificationListModels)
            {
                if (specListItem.ProductSpecificationGroup == null)
                    specListItem.ProductSpecificationGroup = nullProductSpecGroup;
            }
            specsList = productSpecificationListModels.OrderBy(x => x.ProductSpecificationGroup?.DisplayOrder);
            var hasAGroup = specsList.Any(x => x.ProductSpecificationGroup != null);
            return R.Success.With("product", model)
                .With("productId", productId)
                .WithGridResponse(productSpecs.Count, 1, productSpecs.Count)
                .With("productSpecificationsList", specsList)
                .With("hasGroup", hasAGroup)
                .Result;
        }

        [DualGet("{productId}/specifications/{groupId}", Name = AdminRouteNames.ProductSpecificationsListByGroup, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.EditProduct)]
        public IActionResult ProductSpecsList(int productId, int groupId)
        {
            var product = _productService.FirstOrDefault(x => x.Id == productId);
            if (product == null)
                return NotFound();
            var model = _modelMapper.Map<ProductModel>(product);
            var productSpecs = _productSpecificationService.GetByProductId(productId, groupId);
            var aModel = productSpecs.Select(MapProductSpecificationModel).ToList();
            var specsList = aModel.GroupBy(x => x.ProductSpecificationGroup)
                .Select(x =>
                {
                    return new ProductSpecificationListModel() {
                        ProductSpecificationGroup = groupId == 0 ? new ProductSpecificationGroupModel() : x.Key,
                        ProductSpecifications = x.ToList(),
                        ProductSpecificationsSerialized = RequestHelper.IsApiCall()
                            ? null
                            : _dataSerializer.Serialize(x.ToList())
                    };
                });

            specsList = specsList.OrderBy(x => x.ProductSpecificationGroup?.DisplayOrder);
            var hasAGroup = specsList.Any(x => x.ProductSpecificationGroup != null);
            return R.Success.With("product", model)
                .WithGridResponse(productSpecs.Count, 1, productSpecs.Count)
                .With("productSpecificationsList", specsList)
                .With("hasGroup", hasAGroup)
                .Result;
        }

        [DualGet("{productId}/specifications/{productSpecificationId}/{productSpecificationGroupId}", Name = AdminRouteNames.EditProductSpecification)]
        [CapabilityRequired(CapabilitySystemNames.EditProduct)]
        public IActionResult ProductSpecEditor(int productId, int productSpecificationId, int productSpecificationGroupId = 0)
        {
            if (productId <= 0 || _productService.Count(x => x.Id == productId) == 0)
                return NotFound();

            var productSpec = productSpecificationId <= 0
                ? null
                : _productSpecificationService.Get(productSpecificationId);

            var model = productSpec == null
                ? new ProductSpecificationModel() {
                    IsVisible = true,
                    IsFilterable = true,
                    ProductSpecificationGroupId = productSpecificationGroupId
                }
                : MapProductSpecificationModel(productSpec);
            return R.Success.With("productSpecification", model).With("productId", productId).Result;
        }

        [DualGet("{productId}/specifications-group/{productSpecificationId}", Name = AdminRouteNames.EditProductSpecificationGroup)]
        [CapabilityRequired(CapabilitySystemNames.EditProduct)]
        public IActionResult ProductSpecGroupEditor(int productId, int productSpecificationGroupId)
        {
            if (productId <= 0 || _productService.Count(x => x.Id == productId) == 0)
                return NotFound();

            var productSpec = productSpecificationGroupId <= 0
                ? null
                : _productSpecificationGroupService.Get(productSpecificationGroupId);

            var model = _modelMapper.Map<ProductSpecificationGroupModel>(productSpec) ??
                        new ProductSpecificationGroupModel() { Name = T("Specifications") };
            return R.Success.With("productSpecificationGroup", model).With("productId", productId).Result;
        }

        [DualPost("specifications", Name = AdminRouteNames.SaveProductSpecification, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.EditProduct)]
        [ValidateModelState(ModelType = typeof(ProductSpecificationModel))]
        public IActionResult SaveProductSpec(int productId, ProductSpecificationModel productSpecification)
        {
            if (productId <= 0 || _productService.Count(x => x.Id == productId) == 0)
                return NotFound();
            if (productSpecification.ProductSpecificationGroupId > 0)
            {
                //check if correct group id is specified
                if (_productSpecificationGroupService.Count(
                    x => x.ProductId == productId && x.Id == productSpecification.ProductSpecificationGroupId) == 0)
                {
                    return R.Fail.With("error", T("The specification group doesn't exist")).Result;
                }
            }
            SaveProductSpecsImpl(productId, new List<ProductSpecificationModel>() { productSpecification });
            return R.Success.Result;
        }

        [DualPost("specifications-group", Name = AdminRouteNames.SaveProductSpecificationGroup, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.EditProduct)]
        [ValidateModelState(ModelType = typeof(ProductSpecificationGroupModel))]
        public IActionResult SaveProductSpecGroup(ProductSpecificationGroupModel productSpecificationGroup)
        {
            if (productSpecificationGroup.ProductId <= 0 || _productService.Count(x => x.Id == productSpecificationGroup.ProductId) == 0)
                return NotFound();

            var specGroup = _productSpecificationGroupService.Get(productSpecificationGroup.Id);
            if (specGroup != null && specGroup.Id > 0 && specGroup.ProductId != productSpecificationGroup.ProductId)
                return R.Fail.With("error", T("The specification group doesn't exist")).Result;

            specGroup = specGroup ?? new ProductSpecificationGroup();
            specGroup.Name = productSpecificationGroup.Name;
            specGroup.ProductId = productSpecificationGroup.ProductId;
            _productSpecificationGroupService.InsertOrUpdate(specGroup);

            productSpecificationGroup.Id = specGroup.Id;
            return R.Success.With("productSpecificationGroup", productSpecificationGroup).Result;
        }

        [DualPost("specifications/delete", Name = AdminRouteNames.DeleteProductSpecification, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.EditProduct)]
        public IActionResult DeleteProductSpec(int productSpecificationId)
        {
            _productSpecificationService.Delete(x => x.Id == productSpecificationId);
            return R.Success.Result;
        }

        [DualPost("specifications/values/delete", Name = AdminRouteNames.DeleteProductSpecificationValue, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.EditProduct)]
        public IActionResult DeleteProductSpecValue(int productSpecificationValueId)
        {
            //find product spec id
            var productSpecValue = _productSpecificationValueService.Get(productSpecificationValueId);
            if (productSpecValue == null)
                return NotFound();
            var productSpec = _productSpecificationService.Get(productSpecValue.ProductSpecificationId);
            //delete variants
            Transaction.Initiate(transaction =>
            {
                _productSpecificationValueService.Delete(x => x.Id == productSpecificationValueId, transaction);
                if (productSpec.ProductSpecificationValues.Count == 1)
                {
                    //this was last value we just deleted, delete the product spec as well
                    _productSpecificationService.Delete(productSpec, transaction);
                }
            });

            return R.Success.Result;
        }

        [DualGet("suggestions", Name = AdminRouteNames.GetProductSpecificationsGroupSuggestions, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.EditProduct)]
        public IActionResult ProductSpecSuggestions(string q = null)
        {
            q = q ?? "";
            var groups = _productSpecificationGroupService.Get(x => x.Name.StartsWith(q));
            var model = new List<AutocompleteModel>();
            foreach (var c in groups)
            {
                model.Add(new AutocompleteModel() {
                    Id = c.Id,
                    Text = c.Name
                });
            }
            return R.Success.With("suggestions", model.OrderBy(x => x.Text)).Result;
        }

        #endregion

        #region Product Relations
        [DualGet("{productId}/relations/{relationType}", Name = AdminRouteNames.ProductRelationsList)]
        [CapabilityRequired(CapabilitySystemNames.EditProduct)]
        public IActionResult ProductRelationsList(int productId, ProductRelationType relationType)
        {
            if (productId < 1 || _productService.Count(x => x.Id == productId) == 0)
                return NotFound();
            var productRelations = _productRelationService.GetByProductId(productId, relationType, 1, int.MaxValue);
            var productRelationModels = productRelations.Select(x =>
            {
                var relationModel = _modelMapper.Map<ProductRelationModel>(x);
                relationModel.DestinationProduct = _modelMapper.Map<ProductModel>(x.DestinationProduct);
                return relationModel;
            }).ToList();
            return R.Success.With("relations", () => productRelationModels, () => _dataSerializer.Serialize(productRelationModels))
                .With("relationType", relationType)
                .With("productId", productId)
                .WithGridResponse(productRelationModels.Count, 1, productRelationModels.Count)
                .Result;
        }

        [DualPost("relations", Name = AdminRouteNames.SaveProductRelation, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.EditProduct)]
        public IActionResult SaveProductRelations(ProductRelationsModel relation)
        {
            //make sure all product ids are valid
            var sourceProductId = relation.ProductId;
            var destinationIds = relation.DestinationProductIds;
            if (!destinationIds.Contains(sourceProductId))
                destinationIds.Add(sourceProductId);
            if (_productService.Count(x => destinationIds.Contains(x.Id)) < destinationIds.Count)
                return NotFound();
            destinationIds = destinationIds.Where(x => x != sourceProductId).ToList();
            if (!destinationIds.Any())
                return R.Success.Result;//do nothing and return success
            _productRelationService.RelateProducts(sourceProductId, destinationIds, relation.RelationType, relation.IsReciprocal);
            return R.Success.Result;
        }

        [DualPost("relations/delete", Name = AdminRouteNames.DeleteProductRelation, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.EditProduct)]
        public IActionResult DeleteProductRelation(int productRelationId)
        {
            _productRelationService.Delete(x => x.Id == productRelationId);
            return R.Success.Result;
        }

        #endregion


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
            attributeModels = attributeModels.Where(x => !x.Name.IsNullEmptyOrWhiteSpace() || x.Id != 0)
                .Select(x =>
                {
                    x.Values = x.Values.Where(y => !y.AttributeValue.IsNullEmptyOrWhiteSpace() || y.Id != 0).ToList();
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

        private ProductSpecificationModel MapProductSpecificationModel(ProductSpecification pa)
        {
            var specModel = _modelMapper.Map<ProductSpecificationModel>(pa);
            specModel.Name = pa.AvailableAttribute.Name;
            foreach (var pav in pa.ProductSpecificationValues)
            {
                var valueModel = _modelMapper.Map<ProductSpecificationValueModel>(pav);
                valueModel.AttributeValue = pav.AvailableAttributeValue.Value;
                specModel.Values.Add(valueModel);
            }
            specModel.ProductSpecificationGroup =
                _modelMapper.Map<ProductSpecificationGroupModel>(pa.ProductSpecificationGroup);
            specModel.ProductSpecificationGroupId = pa.ProductSpecificationGroupId;
            return specModel;
        }

        private void SaveProductSpecsImpl(int productId, IList<ProductSpecificationModel> specModels)
        {
            //exclude invalid attributes
            specModels = specModels.Where(x => !x.Name.IsNullEmptyOrWhiteSpace() || x.Id != 0)
                .Select(x =>
                {
                    x.Values = x.Values.Where(y => !y.AttributeValue.IsNullEmptyOrWhiteSpace() || y.Id != 0).ToList();
                    return x;
                })
                .ToList();

            //first get all the available attributes
            var availableAttributes = _availableAttributeService.Get(x => true).ToList();
            var availableAttributeValues = _availableAttributeValueService.Get(x => true).ToList();

            //get all the existing linked specs
            var productSpecs = _productSpecificationService.GetByProductId(productId);
            //perform everything in a transaction, we don't want to make db inconsistent
            Transaction.Initiate(transaction =>
            {
                transaction = null;
                foreach (var model in specModels)
                {
                    ProductSpecification savedProductSpecification = null;
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
                        savedProductSpecification = new ProductSpecification() {
                            ProductId = productId,
                            AvailableAttributeId = attribute.Id,
                            AvailableAttribute = attribute,
                            ProductSpecificationGroupId = model.ProductSpecificationGroupId
                        };
                    }
                    else
                    {
                        savedProductSpecification =
                            productSpecs.FirstOrDefault(x => x.Id == model.Id && x.ProductId == productId);
                    }

                    if (savedProductSpecification == null)
                    {
                        //probably a wrong product attribute id has been passed, do nothing
                        return;
                    }
                    savedProductSpecification.Label = model.Label;
                    savedProductSpecification.IsFilterable = model.IsFilterable;
                    savedProductSpecification.IsVisible = model.IsVisible;

                    if (savedProductSpecification.Id == 0)
                    {
                        _productSpecificationService.Insert(savedProductSpecification, transaction);
                        //add to our saved list
                        productSpecs.Add(savedProductSpecification);
                    }
                    else
                        _productSpecificationService.Update(savedProductSpecification, transaction);

                    //go for values
                    foreach (var valueModel in model.Values)
                    {
                        AvailableAttributeValue attributeValue = null;
                        if (valueModel.Id == 0)
                        {
                            attributeValue =
                                availableAttributeValues.FirstOrDefault(
                                    x => x.AvailableAttributeId == savedProductSpecification.AvailableAttributeId &&
                                         x.Value.Equals(valueModel.AttributeValue,
                                             StringComparison.InvariantCultureIgnoreCase));

                            if (attributeValue == null)
                            {
                                attributeValue = new AvailableAttributeValue() {
                                    AvailableAttributeId = savedProductSpecification.AvailableAttributeId,
                                    Value = valueModel.AttributeValue,
                                    AvailableAttribute = savedProductSpecification.AvailableAttribute
                                };
                                _availableAttributeValueService.Insert(attributeValue, transaction);
                                //add to available attribute values
                                availableAttributeValues.Add(attributeValue);
                            }
                            savedProductSpecification.ProductSpecificationValues =
                                savedProductSpecification.ProductSpecificationValues ?? new List<ProductSpecificationValue>();

                            if (!savedProductSpecification.ProductSpecificationValues?.Any(
                                    x => x.AvailableAttributeValueId == attributeValue.Id) ?? false)
                            {
                                var savedAttributeValue = new ProductSpecificationValue() {
                                    AvailableAttributeValueId = attributeValue.Id,
                                    ProductSpecificationId = savedProductSpecification.Id,
                                    AvailableAttributeValue = attributeValue
                                };
                                _productSpecificationValueService.Insert(savedAttributeValue, transaction);

                                //and add this to our attribute
                                savedProductSpecification.ProductSpecificationValues.Add(savedAttributeValue);
                            }

                        }
                    }
                }
            });
        }

        #endregion
    }
}