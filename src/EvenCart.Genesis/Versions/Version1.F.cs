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
using DotEntity;
using DotEntity.Versioning;
using Genesis.Data;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Data.Entity.Shop;
using Genesis.Modules.Meta;
using Genesis.Modules.Navigation;
using Genesis.Modules.Settings;
using Genesis.Modules.Stores;
using Genesis.Modules.Web;
using Newtonsoft.Json;
using Db = DotEntity.DotEntity.Database;
namespace EvenCart.Data.Versions
{
    public class Version1F : IDatabaseVersion
    {
        public void Upgrade(IDotEntityTransaction transaction)
        {
            Db.AddColumn<OrderFulfillment, bool>(nameof(OrderFulfillment.Locked), true, transaction);

            #region MultiStore and Catalog

            Db.CreateTable<Store>(transaction);
            Db.CreateTable<Catalog>(transaction);
            Db.CreateTable<EntityStore>(transaction);
            Db.CreateTable<ProductCatalog>(transaction);
            Db.CreateTable<CatalogCountry>(transaction);

            Db.CreateConstraint(Relation.Create<Store, EntityStore>("Id", "StoreId"), transaction, true);
            Db.CreateConstraint(Relation.Create<Product, ProductCatalog>("Id", "ProductId"), transaction, true);
            Db.CreateConstraint(Relation.Create<Catalog, ProductCatalog>("Id", "CatalogId"), transaction, true);

            //run following only if it's not a fresh install
            if (transaction.CurrentlyRanVersions.All(x => x.GetType() != typeof(Version1)))
            {
                //add a default store
                var store = new Store()
                {
                    Name = "Primary Store",
                    Live = true
                };
                EntitySet<Store>.Insert(store, transaction);
                var storeId = 1; //this will be the store id for new store
                //next create a default catalog
                var catalog = new Catalog()
                {
                    Name = "Primary Catalog",
                    IsCountrySpecific = false
                };
                EntitySet<Catalog>.Insert(catalog, transaction);

                EntitySet<EntityStore>.Insert(new EntityStore()
                {
                    EntityId = 1,
                    StoreId = 1,
                    EntityName = nameof(Catalog)
                }, transaction);

                Db.AddColumn<Setting, int>(nameof(Setting.StoreId), storeId, transaction);
                Db.AddColumn<Order, int>(nameof(Order.StoreId), storeId, transaction);

                //assign all products to this catalog
                var productTableName = DotEntityDb.Provider.SafeEnclose(DotEntityDb.GetTableNameForType<Product>());
                var productCatalogTableName = DotEntityDb.Provider.SafeEnclose(DotEntityDb.GetTableNameForType<ProductCatalog>());
                var productIdCol = DotEntityDb.Provider.SafeEnclose(nameof(ProductCatalog.ProductId));
                var catalogIdCol = DotEntityDb.Provider.SafeEnclose(nameof(ProductCatalog.CatalogId));
                var idCol = DotEntityDb.Provider.SafeEnclose(nameof(ProductCatalog.Id));

                var query =
                    $"INSERT INTO {productCatalogTableName}({productIdCol}, {catalogIdCol}) SELECT {idCol}, 1 FROM {productTableName}";
                Db.Query(query, null, transaction);

                //update active status of plugins
                var pluginSetting = EntitySet<Setting>.Where(x =>
                    x.GroupName == nameof(PluginSettings) && x.Key == nameof(PluginSettings.SitePlugins)).Select().FirstOrDefault();
                if (pluginSetting != null)
                {
                    var pluginStatuses = JsonConvert.DeserializeObject<IList<PluginStatus>>(pluginSetting.Value);
                    foreach (var pluginStatus in pluginStatuses)
                    {
                        pluginStatus.ActiveStoreIds = pluginStatus.Active ? new List<int> { store.Id } : new List<int>();
                    }

                    pluginSetting.Value = JsonConvert.SerializeObject(pluginStatuses);
                    EntitySet<Setting>.Update(pluginSetting, transaction);
                }

                //update widgets
                pluginSetting = EntitySet<Setting>.Where(x =>
                    x.GroupName == nameof(PluginSettings) && x.Key == nameof(PluginSettings.SiteWidgets)).Select().FirstOrDefault();
                if (pluginSetting != null)
                {
                    var widgetStatuses = JsonConvert.DeserializeObject<IList<WidgetStatus>>(pluginSetting.Value);
                    foreach (var ws in widgetStatuses)
                    {
                        ws.StoreId = storeId;
                    }
                    pluginSetting.Value = JsonConvert.SerializeObject(widgetStatuses);
                    EntitySet<Setting>.Update(pluginSetting, transaction);
                }

                //update store ids to 0 for plugin settings as it is now global from this version
                EntitySet<Setting>.Update(new { storeId = 0 }, x => x.GroupName == nameof(PluginSettings), transaction);

                //update menus
                MoveEntityToStore<Menu>(storeId, transaction);
                //update content pages
                MoveEntityToStore<ContentPage>(storeId, transaction);
            }


            #endregion
        }

        public void Downgrade(IDotEntityTransaction transaction)
        {
            Db.DropColumn<OrderFulfillment>(nameof(OrderFulfillment.Locked), transaction);

            Db.DropConstraint(Relation.Create<Store, EntityStore>("Id", "StoreId"), transaction);
            Db.DropConstraint(Relation.Create<Product, ProductCatalog>("Id", "ProductId"), transaction);
            Db.DropConstraint(Relation.Create<Catalog, ProductCatalog>("Id", "CatalogId"), transaction);

            Db.DropTable<Store>(transaction);
            Db.DropTable<Catalog>(transaction);
            Db.DropTable<EntityStore>(transaction);
            Db.DropTable<ProductCatalog>(transaction);
            Db.DropTable<CatalogCountry>(transaction);
        }

        public string VersionKey => "EvenCart.Version.1F";

        #region Helpers

        private void MoveEntityToStore<T>(int storeId, IDotEntityTransaction transaction) where T : GenesisEntity
        {
            var entities = EntitySet<T>.Select();
            var entityName = typeof(T).Name;
            foreach (var entity in entities)
            {
                EntitySet<EntityStore>.Insert(new EntityStore()
                {
                    EntityId = entity.Id,
                    EntityName = entityName,
                    StoreId = storeId
                }, transaction);
            }
        }
        #endregion
    }
}