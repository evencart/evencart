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
using System.Collections.Generic;
using System.Linq;
using EvenCart.Areas.Administration.Models.Media;
using EvenCart.Areas.Administration.Models.Shop;
using EvenCart.Data.Extensions;
using EvenCart.Services.Products;
using EvenCart.Factories.Products;
using Genesis.Infrastructure.Mvc;
using Genesis.Infrastructure.Mvc.ModelFactories;
using Genesis.Infrastructure.Mvc.Models;
using Genesis.MediaServices;
using Genesis.Modules.Pluggable;
using Genesis.Plugins;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Areas.Administration.Components.Widgets
{
    [ViewComponent(Name = WidgetSystemName)]
    public class ProductCarouselWidget : GenesisComponent, IWidget
    {
        private const string WidgetSystemName = "ProductCarousel";
        private readonly IWidgetService _widgetService;
        private readonly IProductService _productService;
        private readonly IModelMapper _modelMapper;
        private readonly IMediaAccountant _mediaAccountant;
        private readonly IProductModelFactory _productModelFactory;
        public ProductCarouselWidget(IWidgetService widgetService, IProductService productService, IModelMapper modelMapper, IMediaAccountant mediaAccountant, IProductModelFactory productModelFactory)
        {
            _widgetService = widgetService;
            _productService = productService;
            _modelMapper = modelMapper;
            _mediaAccountant = mediaAccountant;
            _productModelFactory = productModelFactory;
        }

        public override IViewComponentResult Invoke(object data = null)
        {
            var dataAsDict = data as Dictionary<string, object>;
            if (dataAsDict == null)
                return R.Success.ComponentResult;
            var widgetId = dataAsDict["id"].ToString();
            var widgetSettings = _widgetService.LoadWidgetSettings<ProductCarouselWidgetSettings>(widgetId);
            if (widgetSettings.ProductIds == null || !widgetSettings.ProductIds.Any())
                return R.Success.ComponentResult;
            var products = _productService.GetProducts(widgetSettings.ProductIds, true).Where(x => x.IsPublic(CurrentStore.Id));
            var productsModel = products.Select(_productModelFactory.Create)
                .OrderBy(x => widgetSettings.ProductIds.IndexOf(x.Id))
                .ToList();

            return R.Success.With("title", widgetSettings.Title)
                .With("products", productsModel)
                .With("widgetId", widgetId)
                .ComponentResult;
        }

        public string DisplayName => "Product Carousel";

        public string SystemName => WidgetSystemName;

        public IList<string> WidgetZones { get; } = null;

        public bool HasConfiguration { get; } = true;

        public bool SkipDragging { get; } = false;

        public string ConfigurationUrl { get; } = null;

        public Type SettingsType { get; } = typeof(ProductCarouselWidgetSettings);

        public object GetViewObject(object settings)
        {
            var widgetSettings = settings as ProductCarouselWidgetSettings;
            if (widgetSettings?.ProductIds != null)
            {
                var productIds = widgetSettings.ProductIds.Distinct().ToList();
                var products = _productService.GetProducts(productIds);
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
                    })
                    .OrderBy(x => widgetSettings.ProductIds.IndexOf(x.Id))
                    .ToList();
                return new
                {
                    title = widgetSettings.Title,
                    products = productsModel
                };
            }
            return new
            {
                title = widgetSettings?.Title,
            };
        }

        public class ProductCarouselWidgetSettings : WidgetSettingsModel
        {
            public string Title { get; set; }

            public IList<int> ProductIds { get; set; }
        }
    }
}