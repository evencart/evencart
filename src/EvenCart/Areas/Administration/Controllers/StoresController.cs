using DotEntity.Enumerations;
using EvenCart.Areas.Administration.Factories.Stores;
using EvenCart.Areas.Administration.Models.Store;
using EvenCart.Data.Constants;
using EvenCart.Data.Entity.Shop;
using EvenCart.Data.Extensions;
using EvenCart.Infrastructure.Mvc;
using EvenCart.Infrastructure.Mvc.Attributes;
using EvenCart.Infrastructure.Mvc.ModelFactories;
using EvenCart.Infrastructure.Routing;
using EvenCart.Infrastructure.Security.Attributes;
using EvenCart.Services.Products;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace EvenCart.Areas.Administration.Controllers
{
    public class StoresController : FoundationAdminController
    {
        private readonly IStoreService _storeService;
        private readonly IModelMapper _modelMapper;
        private readonly IStoreModelFactory _storeModelFactory;
        public StoresController(IStoreService storeService, IModelMapper modelMapper, IStoreModelFactory storeModelFactory)
        {
            _storeService = storeService;
            _modelMapper = modelMapper;
            _storeModelFactory = storeModelFactory;
        }

        [DualGet("", Name = AdminRouteNames.StoresList)]
        [CapabilityRequired(CapabilitySystemNames.ManageStores)]
        public IActionResult StoresList(StoreSearchModel searchModel)
        {

            Expression<Func<Store, bool>> storeWhere = x => true;
            if (!searchModel.SearchPhrase.IsNullEmptyOrWhiteSpace())
                storeWhere = store => store.Name.StartsWith(searchModel.SearchPhrase);

            var stores = _storeService.Get(out int totalResults, storeWhere, store => store.Id, RowOrder.Ascending, searchModel.Current, searchModel.RowCount);

            var models = stores.Select(_storeModelFactory.Create).ToList();

            return R.Success.With("stores", models)
                .WithGridResponse(totalResults, searchModel.Current, searchModel.RowCount)
                .Result;
        }

        [DualGet("{storeId}", Name = AdminRouteNames.GetStore)]
        [CapabilityRequired(CapabilitySystemNames.ManageStores)]
        public IActionResult StoreEditor(int storeId)
        {
            var store = storeId > 0 ? _storeService.Get(storeId) : new Store();
            if (store == null)
                return NotFound();
            var storeModel = _modelMapper.Map<StoreModel>(store);
            //we need to use storeObject as response because store is already being passed as a global object
            return R.Success.With("storeObject", storeModel).WithAvailableStores().Result;
        }

        [DualPost("", Name = AdminRouteNames.SaveStore, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.ManageStores)]
        [ValidateModelState(ModelType = typeof(StoreModel))]
        public IActionResult SaveStore(StoreModel storeModel)
        {
            var store = storeModel.Id > 0 ? _storeService.Get(storeModel.Id) : new Store();
            if (store == null)
                return NotFound();

            _modelMapper.Map(storeModel, store);
            _storeService.InsertOrUpdate(store);
            return R.Success.Result;
        }

        [DualPost("", Name = AdminRouteNames.CloneStore, OnlyApi = true)]
        [FieldRequired("clone", "true")]
        [CapabilityRequired(CapabilitySystemNames.ManageStores)]
        [ValidateModelState(ModelType = typeof(CreateStoreModel))]
        public IActionResult SaveStore(CreateStoreModel storeModel)
        {
            var sourceStore = storeModel.SourceStoreId > 0 ? _storeService.Get(storeModel.SourceStoreId) : null;
            if (sourceStore == null)
                return R.Fail.With("error", T("The source store was not found")).Result;

            //get all the saved stores
            //is there any saved store with same domain?
            if (!storeModel.Domain.StartsWith("//"))
                storeModel.Domain = $"//{storeModel.Domain}";

            var savedStore = _storeService.GetByDomain(storeModel.Domain);
            if (savedStore != null)
                return R.Fail.With("error", T("Another store with same domain already exist")).Result;

            _storeService.CloneStore(sourceStore, storeModel.Name, storeModel.Domain);
            return R.Success.Result;
        }

        [DualPost("delete", Name = AdminRouteNames.DeleteStore, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.ManageStores)]
        public IActionResult DeleteStore(int storeId)
        {
            var store = storeId > 0 ? _storeService.Get(storeId) : null;
            if (store == null)
                return NotFound();

            _storeService.Delete(store);
            return R.Success.Result;
        }

    }
}