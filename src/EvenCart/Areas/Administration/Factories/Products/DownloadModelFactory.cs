﻿#region License
// Copyright (c) Sojatia Infocrafts Private Limited.
// The following code is part of EvenCart eCommerce Software (https://evencart.co) Dual Licensed under the terms of
// 
// 1. GNU GPLv3 with additional terms (available to read at https://evencart.co/license)
// 2. EvenCart Proprietary License (available to read at https://evencart.co/license/commercial-license).
// 
// You can select one of the above two licenses according to your requirements. The usage of this code is
// subject to the terms of the license chosen by you.
#endregion

using EvenCart.Areas.Administration.Models.Shop;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Data.Entity.Shop;
using Genesis;
using Genesis.Infrastructure;
using Genesis.Infrastructure.Mvc.ModelFactories;
using Genesis.Routing;

namespace EvenCart.Areas.Administration.Factories.Products
{
    public class DownloadModelFactory : IDownloadModelFactory
    {
        private readonly IModelMapper _modelMapper;
        private readonly IGenesisEngine _applicationEngine;
        public DownloadModelFactory(IModelMapper modelMapper, IGenesisEngine applicationEngine)
        {
            _modelMapper = modelMapper;
            _applicationEngine = applicationEngine;
        }

        public DownloadModel Create(Download entity)
        {
            var model = _modelMapper.Map<DownloadModel>(entity);
            model.DownloadUrl = GenesisEngine.Instance.RouteUrl(AdminRouteNames.AdminDownloadFile, new { id = entity.Id });
            return model;
        }

        public OrderDownloadModel Create(Download download, ItemDownload itemDownload)
        {
            var model = new OrderDownloadModel()
            {
                Title = download.Title,
                Description = download.Description,
                Active = itemDownload?.Active ?? download.DownloadActivationType != DownloadActivationType.Manual,
                DownloadCount = itemDownload?.DownloadCount ?? 0,
                DownloadUrl = GenesisEngine.Instance.RouteUrl(AdminRouteNames.AdminDownloadFile, new { id = download.Id }),
                ItemDownloadId = itemDownload?.Id ?? 0,
                DownloadId = download.Id
            };
            return model;
        }
    }
}