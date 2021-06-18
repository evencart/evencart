#region License
// Copyright (c) Sojatia Infocrafts Private Limited.
// The following code is part of EvenCart eCommerce Software (https://evencart.co) Dual Licensed under the terms of
// 
// 1. GNU GPLv3 with additional terms (available to read at https://evencart.co/license)
// 2. EvenCart Proprietary License (available to read at https://evencart.co/license/commercial-license).
// 
// You can select one of the above two licenses according to your requirements. The usage of this code is
// subject to the terms of the license chosen by you.
#endregion

using System;
using System.Data.SqlClient;
using System.Linq;
using Genesis.Data;
using EvenCart.Data.Entity.Shop;
using EvenCart.Services.Products;
using Genesis.Database;
using Genesis.Extensions;
using Genesis.Infrastructure.Mvc.ModelFactories;
using Genesis.Modules.Logging;
using Genesis.Modules.MediaServices;
using Genesis.Modules.Users;
using Genesis.Modules.Vendors;

namespace Genesis.Modules.DataTransfer
{
    [AutoResolvable]
    public class DataTransferManager : IDataTransferManager
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IModelMapper _modelMapper;
        private readonly IMediaService _mediaService;
        private readonly IProductSpecificationGroupService _productSpecificationGroupService;
        private readonly IProductSpecificationService _productSpecificationService;
        private readonly IProductSpecificationValueService _productSpecificationValueService;
        private readonly IProductAttributeService _productAttributeService;
        private readonly IProductAttributeValueService _productAttributeValueService;
        private readonly IProductVariantService _productVariantService;
        private readonly IAvailableAttributeService _availableAttributeService;
        private readonly IAvailableAttributeValueService _attributeValueService;
        private readonly IManufacturerService _manufacturerService;
        private readonly IVendorService _vendorService;
        private readonly IUserService _userService;
        private readonly ILogger _logger;
        private readonly IRoleService _roleService;
        public DataTransferManager(IProductService productService, ICategoryService categoryService, IModelMapper modelMapper, IMediaService mediaService, IProductSpecificationGroupService productSpecificationGroupService, IProductSpecificationService productSpecificationService, IProductSpecificationValueService productSpecificationValueService, IProductAttributeService productAttributeService, IProductAttributeValueService productAttributeValueService, IProductVariantService productVariantService, IAvailableAttributeService availableAttributeService, IAvailableAttributeValueService attributeValueService, IManufacturerService manufacturerService, IVendorService vendorService, IUserService userService, ILogger logger, IRoleService roleService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _modelMapper = modelMapper;
            _mediaService = mediaService;
            _productSpecificationGroupService = productSpecificationGroupService;
            _productSpecificationService = productSpecificationService;
            _productSpecificationValueService = productSpecificationValueService;
            _productAttributeService = productAttributeService;
            _productAttributeValueService = productAttributeValueService;
            _productVariantService = productVariantService;
            _availableAttributeService = availableAttributeService;
            _attributeValueService = attributeValueService;
            _manufacturerService = manufacturerService;
            _vendorService = vendorService;
            _userService = userService;
            _logger = logger;
            _roleService = roleService;
        }

        private int ImportProducts(DataTransferChunk chunk, IDataTransferProvider<Product> dataTransferProvider)
        {
            var done = 0;
            try
            {
                var products = dataTransferProvider.GetDataList(chunk);
                var productAttributes = products.Where(x => x.ProductAttributes != null)
                    .SelectMany(x => x.ProductAttributes).ToList();
                var availableAttributes = productAttributes.Select(x => x.AvailableAttribute).Distinct().ToList();
                var manufacturers = _manufacturerService.Get(x => true).ToList();
                var vendors = products.Where(x => x.Vendors != null).SelectMany(x => x.Vendors).ToList();
                var categories = products.Where(x => x.Categories != null).SelectMany(x => x.Categories).ToList();
                availableAttributes.ForEach(attribute =>
                {
                    attribute.Id = 0;
                    _availableAttributeService.Insert(attribute);

                    attribute.AvailableAttributeValues.ForEach(value =>
                    {
                        value.AvailableAttributeId = attribute.Id;
                        value.Id = 0;
                        _attributeValueService.Insert(value);
                    });
                });

                vendors.ForEach(vendor =>
                {
                    vendor.Id = 0;
                    _vendorService.Insert(vendor);
                });

                categories.ForEach(category =>
                {
                    category.Id = 0;
                    var parent = category.Parent;
                    while (parent != null)
                    {
                        parent.Id = 0;
                        parent = parent.Parent;
                    }

                    _categoryService.InsertTree(category);
                });
                products.ForEach(product =>
                {
                    if (!product.Sku.IsNullEmptyOrWhiteSpace())
                    {
                        var savedProduct = _productService.FirstOrDefault(x => x.Sku == product.Sku);
                        if (savedProduct != null)
                        {
                            _modelMapper.Map(product, savedProduct, nameof(Product.Id), nameof(Product.CreatedOn),
                                nameof(Product.UpdatedOn));
                            _productService.Update(savedProduct);
                            done++;
                            return;
                        }
                    }

                    product.Id = 0;
                    if (product.SeoMeta != null)
                    {
                        product.SeoMeta.Id = 0;
                    }
                    if (product.Manufacturer != null)
                    {
                        var manufacturer =
                            manufacturers.FirstOrDefault(x => x.Name == product.Manufacturer.Name);
                        if (manufacturer == null)
                        {
                            manufacturer = product.Manufacturer;
                            manufacturer.Id = 0;
                            if (manufacturer.SeoMeta != null)
                                manufacturer.SeoMeta.Id = 0;
                            _manufacturerService.Insert(manufacturer);
                            manufacturers.Add(manufacturer);
                        }

                        product.ManufacturerId = manufacturer.Id;
                    }

                    _productService.Insert(product);

                    //now insert everything else
                    product.Categories.ForEach(category =>
                    {
                        _productService.LinkCategoryWithProduct(category.Id, product.Id, 0);
                    });

                    product.MediaItems.ForEach(media =>
                    {
                        media.Id = 0;
                        _mediaService.Insert(media);
                        _productService.LinkMediaWithProduct(media.Id, product.Id);
                    });

                    product.ProductAttributes.ForEach(productAttribute =>
                    {
                        productAttribute.Id = 0;
                        productAttribute.ProductId = product.Id;
                        productAttribute.AvailableAttributeId = productAttribute.AvailableAttribute.Id;
                        productAttribute.ProductAttributeValues.ForEach(productAttributeValue =>
                        {
                            productAttributeValue.Id = 0;
                            productAttributeValue.AvailableAttributeValueId = productAttributeValue.AvailableAttributeValue.Id;
                        });
                        _productAttributeService.Insert(productAttribute);

                    });
                    if (product.ProductSpecifications != null)
                    {
                        var specificationGroups = product.ProductSpecifications
                            .Select(x => x.ProductSpecificationGroup).ToList();
                        specificationGroups.ForEach(group =>
                        {
                            group.Id = 0;
                            group.ProductId = product.Id;
                            _productSpecificationGroupService.Insert(group);
                        });
                    }
                    product.ProductSpecifications.ForEach(specification =>
                    {
                        specification.ProductSpecificationGroupId = specification.ProductSpecificationGroup?.Id ?? 0;
                        specification.Id = 0;
                        specification.ProductId = product.Id;
                        specification.AvailableAttributeId = specification.AvailableAttribute.Id;
                        _productSpecificationService.Insert(specification);
                        specification.ProductSpecificationValues.ForEach(value =>
                        {
                            value.Id = 0;
                            value.ProductSpecificationId = specification.Id;
                            value.AvailableAttributeValueId = value.AvailableAttributeValue.Id;
                            _productSpecificationValueService.Insert(value);
                        });
                    });
                    product.ProductVariants.ForEach(variant =>
                    {
                        variant.ProductId = product.Id;
                        variant.Id = 0;

                        variant.ProductVariantAttributes.ForEach(attribute =>
                        {
                            attribute.ProductVariantId = variant.Id;
                            attribute.ProductAttributeId = attribute.ProductAttribute.Id;
                            attribute.ProductAttributeValueId = attribute.ProductAttributeValue.Id;
                            attribute.Id = 0;
                        });
                        _productVariantService.AddVariant(product, variant);
                    });
                    product.Vendors.ForEach(vendor => { _vendorService.AddVendorProduct(vendor.Id, product.Id); });
                    done++;
                });
            }
            catch (Exception ex)
            {
                _logger.Log<DatabaseManager>(LogLevel.Error, "Error occurred while importing products", ex);
            }

            return done;

        }

        private int ImportUsers(DataTransferChunk chunk, IDataTransferProvider<User> dataTransferProvider)
        {
            var done = 0;
            try
            {
                var users = dataTransferProvider.GetDataList(chunk).ToList();
                //saved roles
                var savedRoles = _roleService.Get(x => true).ToList();

                //new roles
                var roles = users.SelectMany(x => x.Roles).Where(x => x != null).ToList();

                //new roles to insert
                roles.ForEach(role =>
                {
                    if (savedRoles.All(x => x.SystemName != role.SystemName))
                    {
                        //insert new role
                        role.Id = 0;
                        _roleService.Insert(role);
                        savedRoles.Add(role);
                    }
                });
                users.ForEach(user =>
                {
                    //user with same email should not be inserted
                    if (_userService.Count(x => x.Email == user.Email) > 0)
                        return;
                    user.Id = 0;
                    user.Guid = Guid.NewGuid();
                    _userService.Insert(user);
                    var userRoleNames = user.Roles.Select(x => x.SystemName).ToList();
                    var newUserRoleIds = savedRoles.Where(x => userRoleNames.Contains(x.SystemName)).Select(x => x.Id).ToArray();
                    _roleService.SetUserRoles(user.Id, newUserRoleIds);
                    done++;
                });
            }
            catch (Exception ex)
            {
                _logger.Log<DatabaseManager>(LogLevel.Error, "Error occurred while importing users", ex);
            }
            return done;
        }

        private int ImportCategories(DataTransferChunk chunk, IDataTransferProvider<Category> dataTransferProvider)
        {
            var done = 0;
            try
            {
                var categories = dataTransferProvider.GetDataList(chunk).ToList();
                categories.ForEach(category =>
                {
                    category.Id = 0;
                    var parent = category.Parent;
                    while (parent != null)
                    {
                        parent.Id = 0;
                        parent = parent.Parent;
                    }
                    _categoryService.InsertTree(category);
                });
                done = categories.Count;
            }
            catch (Exception ex)
            {
                _logger.Log<DatabaseManager>(LogLevel.Error, "Error occurred while importing categories", ex);
            }

            return done;
        }

        public int Import<T>(DataTransferChunk chunk, IDataTransferProvider<T> dataTransferProvider) where T : GenesisEntity
        {
            switch (typeof(T).Name)
            {
                case nameof(Product):
                    return ImportProducts(chunk, (IDataTransferProvider<Product>) dataTransferProvider);
                case nameof(Category):
                    return ImportCategories(chunk, (IDataTransferProvider<Category>) dataTransferProvider);
                case nameof(User):
                    return ImportUsers(chunk, (IDataTransferProvider<User>)dataTransferProvider);
            }

            return 0;
        }

        public DataTransferChunk Export<T>(IDataTransferProvider<T> dataTransferProvider) where T : GenesisEntity
        {

            DataTransferChunk chunk = null;
            switch (typeof(T).Name)
            {
                case nameof(Product):
                    var products = _productService.GetProducts(true, true, true, true, true, true);
                    chunk = ((IDataTransferProvider<Product>) dataTransferProvider).GetTransferChunks(products);
                    break;
                case nameof(Category):
                    var categories = _categoryService.Get(x => true).ToList();
                    chunk = ((IDataTransferProvider<Category>)dataTransferProvider).GetTransferChunks(categories);
                    break;
                case nameof(User):
                    var exceptRoleIds = new[]
                        {_roleService.FirstOrDefault(x => x.SystemName == SystemRoleNames.Visitor)?.Id ?? 0};
                    var users = _userService.GetUsers(null, exceptRoleIds, null, SortOrder.Ascending, 1,
                        int.MaxValue, out _, negateRoleRestriction: true);

                    chunk = ((IDataTransferProvider<User>)dataTransferProvider).GetTransferChunks(users);
                    break;
            }

            return chunk;
        }
    }
}