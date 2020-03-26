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

using System.Collections.Generic;
using System.Linq;
using EvenCart.Core.Data;
using EvenCart.Data.Entity.Promotions;
using EvenCart.Data.Entity.Settings;
using EvenCart.Data.Entity.Shop;
using EvenCart.Data.Enum;
using EvenCart.Data.Extensions;
using EvenCart.Services.Formatter;
using EvenCart.Services.Products;
using EvenCart.Infrastructure;
using EvenCart.Infrastructure.MediaServices;
using EvenCart.Infrastructure.Mvc.ModelFactories;
using EvenCart.Infrastructure.Routing;
using EvenCart.Models.Media;
using EvenCart.Models.Reviews;
using EvenCart.Services.Extensions;
using DownloadModel = EvenCart.Models.Products.DownloadModel;
using ProductAttributeModel = EvenCart.Models.Products.ProductAttributeModel;
using ProductAttributeValueModel = EvenCart.Models.Products.ProductAttributeValueModel;
using ProductModel = EvenCart.Models.Products.ProductModel;
using ProductSpecificationGroupModel = EvenCart.Models.Products.ProductSpecificationGroupModel;
using ProductSpecificationModel = EvenCart.Models.Products.ProductSpecificationModel;

namespace EvenCart.Factories.Products
{
    public class ProductModelFactory : IProductModelFactory
    {
        private readonly IModelMapper _modelMapper;
        private readonly IMediaAccountant _mediaAccountant;
        private readonly CatalogSettings _catalogSettings;
        private readonly IPriceAccountant _priceAccountant;
        private readonly TaxSettings _taxSettings;
        private readonly IRoundingService _roundingService;
        private readonly IDataSerializer _dataSerializer;
        public ProductModelFactory(IModelMapper modelMapper, IMediaAccountant mediaAccountant, CatalogSettings catalogSettings, IPriceAccountant priceAccountant, TaxSettings taxSettings, IRoundingService roundingService, IDataSerializer dataSerializer)
        {
            _modelMapper = modelMapper;
            _mediaAccountant = mediaAccountant;
            _catalogSettings = catalogSettings;
            _priceAccountant = priceAccountant;
            _taxSettings = taxSettings;
            _roundingService = roundingService;
            _dataSerializer = dataSerializer;
        }

        public ProductModel Create(Product product)
        {
            var productModel = _modelMapper.Map<ProductModel>(product);
            productModel.IsAvailable = !product.TrackInventory || (product.TrackInventory && product.IsAvailableInStock());
            productModel.SeName = product.SeoMeta?.Slug;
           
            var mediaModels = product.MediaItems?.OrderBy(x => x.DisplayOrder).Select(y =>
            {
                var mediaModel = _modelMapper.Map<MediaModel>(y);
                if (mediaModel.MediaType != MediaType.Url)
                {
                    mediaModel.ThumbnailUrl =
                        _mediaAccountant.GetPictureUrl(y, ApplicationEngine.ActiveTheme.ProductBoxImageSize, true);
                    mediaModel.Url = _mediaAccountant.GetPictureUrl(y, 0, 0, true);
                }
                else
                {
                    mediaModel.MetaData = _dataSerializer.DeserializeAs<EmbeddedMediaModel>(y.MetaData);
                }
                return mediaModel;
            }).ToList() ?? new List<MediaModel>()
            {
                new MediaModel()
                {
                    ThumbnailUrl = _mediaAccountant.GetPictureUrl(null,
                        ApplicationEngine.ActiveTheme.ProductBoxImageSize, true),
                    Url = _mediaAccountant.GetPictureUrl(null, 0, 0, true)
                }
            };

            productModel.Media = mediaModels;
            if (product.ProductAttributes != null)
            {
                productModel.ProductAttributes = product.ProductAttributes.Select(x =>
                {
                    var paModel = new ProductAttributeModel
                    {
                        Name = x.Label,
                        Id = x.Id,
                        InputFieldType = x.InputFieldType,
                        IsRequired = x.IsRequired,
                      
                    };
                    if (x.AvailableAttribute.AvailableAttributeValues != null)
                    {
                        paModel.AvailableValues = x.AvailableAttribute.AvailableAttributeValues.Select(y =>
                            {
                                var avModel = new ProductAttributeValueModel()
                                {
                                    Name = y.Value
                                };
                                return avModel;
                            })
                            .ToList();
                    }
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
                    var specs = grp.Select(x => new ProductSpecificationModel()
                    {
                        Name = x.Label,
                        Values = x.ProductSpecificationValues.Select(y => y.Label).ToList()
                    }).ToList();
                    productModel.ProductSpecificationGroups.Add(new ProductSpecificationGroupModel()
                    {
                        Name = groupName,
                        ProductSpecifications = specs
                    });
                }
            }

            if (_catalogSettings.EnableReviews)
            {
                //reviews
                if (product.ReviewSummary != null)
                    productModel.ReviewSummary = _modelMapper.Map<ReviewSummaryModel>(product.ReviewSummary);
            }
            else
            {
                product.ReviewSummary = null;
            }
            if (productModel.RequireLoginToViewPrice && ApplicationEngine.CurrentUser.IsVisitor())
            {
                productModel.Price = 0;
                productModel.ComparePrice = null;
            }
            else
            {
                IList<DiscountCoupon> coupons = null;
                //any autodiscounted price
                var price = _priceAccountant.GetAutoDiscountedPriceForUser(product, null, ApplicationEngine.CurrentUser, 1, ref coupons, out decimal discount);
                if (price < product.Price)
                {
                    productModel.ComparePrice = product.Price;
                    if (productModel.ComparePrice < product.ComparePrice)
                    {
                        productModel.ComparePrice = product.ComparePrice;
                    }
                }
                _priceAccountant.GetProductPriceDetails(product, null, price, out decimal priceWithoutTax,
                    out decimal tax, out decimal taxRate, out _);
                if (_taxSettings.DisplayProductPricesWithoutTax)
                    productModel.Price = priceWithoutTax;
                else
                    productModel.Price = priceWithoutTax + tax;

                //change display prices to current currency
                var targetCurrency = ApplicationEngine.CurrentCurrency;
                productModel.Price = _priceAccountant.ConvertCurrency(productModel.Price, targetCurrency);
            }
          
            return productModel;
        }

        public DownloadModel Create(Download download)
        {
            var model = new DownloadModel()
            {
                Description = download.Description,
                Title = download.Title,
                DownloadUrl = ApplicationEngine.RouteUrl(RouteNames.DownloadFile, new {guid = download.Guid}),
                FileType = download.FileType,
                Published = download.Published,
                DisplayOrder = download.DisplayOrder
            };
            return model;
        }
    }
}