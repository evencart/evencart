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
using System.Linq.Expressions;
using DotEntity.Enumerations;
using EvenCart.Areas.Administration.Factories.Catalogs;
using EvenCart.Areas.Administration.Models.Catalog;
using EvenCart.Data.Constants;
using EvenCart.Data.Entity.Shop;
using EvenCart.Data.Extensions;
using EvenCart.Infrastructure.Helpers;
using EvenCart.Infrastructure.Mvc;
using EvenCart.Infrastructure.Mvc.Attributes;
using EvenCart.Infrastructure.Mvc.ModelFactories;
using EvenCart.Infrastructure.Routing;
using EvenCart.Infrastructure.Security.Attributes;
using EvenCart.Services.Products;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Areas.Administration.Controllers
{
    public class CatalogsController : FoundationAdminController
    {
        private readonly ICatalogService _catalogService;
        private readonly IModelMapper _modelMapper;
        private readonly IStoreService _storeService;
        private readonly ICatalogModelFactory _catalogModelFactory;
        public CatalogsController(ICatalogService catalogService, IModelMapper modelMapper, IStoreService storeService, ICatalogModelFactory catalogModelFactory)
        {
            _catalogService = catalogService;
            _modelMapper = modelMapper;
            _storeService = storeService;
            _catalogModelFactory = catalogModelFactory;
        }

        [DualGet("", Name = AdminRouteNames.CatalogsList)]
        [CapabilityRequired(CapabilitySystemNames.ManageCatalog)]
        public IActionResult CatalogsList(CatalogSearchModel searchModel)
        {
            Expression<Func<Catalog, bool>> catalogWhere = x => true;
            if (!searchModel.SearchPhrase.IsNullEmptyOrWhiteSpace())
                catalogWhere = store => store.Name.StartsWith(searchModel.SearchPhrase);

            var catalogs = _catalogService.Get(out var totalResults, catalogWhere, catalog => catalog.Id, RowOrder.Ascending, searchModel.Current, searchModel.RowCount);

            var models = catalogs.Select(x => _modelMapper.Map<CatalogModel>(x)).ToList();

            return R.Success.With("catalogs", models)
                .WithGridResponse(totalResults, searchModel.Current, searchModel.RowCount)
                .Result;
        }

        [DualGet("{catalogId}", Name = AdminRouteNames.GetCatalog)]
        [CapabilityRequired(CapabilitySystemNames.ManageCatalog)]
        public IActionResult CatalogEditor(int catalogId)
        {
            var catalog = catalogId > 0 ? _catalogService.Get(catalogId) : new Catalog();
            if (catalog == null)
                return NotFound();
            var catalogModel = _catalogModelFactory.Create(catalog);
            var stores = _storeService.Get(x => true).ToList();
            var catalogStoreIds = catalog.Stores?.Select(x => x.Id).ToList() ?? new List<int>();
            var availableStores = SelectListHelper.GetSelectItemList(stores, x => x.Id, x => x.Name)
                .Select(x =>
                {
                    if (catalogStoreIds.Any(y => y.ToString() == x.Value))
                        x.Selected = true;
                    return x;
                }).ToList();

            return R.Success.With("catalog", catalogModel).With("availableStores", availableStores).Result;
        }

        [DualPost("", Name = AdminRouteNames.SaveCatalog, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.ManageCatalog)]
        [ValidateModelState(ModelType = typeof(CatalogModel))]
        public IActionResult SaveCatalog(CatalogModel catalogModel)
        {
            var catalog = catalogModel.Id > 0 ? _catalogService.Get(catalogModel.Id) : new Catalog();
            if (catalog == null)
                return NotFound();

            _modelMapper.Map(catalogModel, catalog);
            catalog.StoreIds = catalogModel.StoreIds;
            //save the catalog
            _catalogService.InsertOrUpdate(catalog);
            return R.Success.Result;
        }

        [DualPost("delete", Name = AdminRouteNames.DeleteCatalog, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.ManageCatalog)]
        public IActionResult DeleteCatalog(int catalogId)
        {
            var catalog = catalogId > 0 ? _catalogService.Get(catalogId) : null;
            if (catalog == null)
                return NotFound();

            _catalogService.Delete(catalog);
            return R.Success.Result;
        }
    }
}