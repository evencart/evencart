using System.Collections.Generic;
using EvenCart.Areas.Administration.Models.Orders;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Data.Entity.Shop;
using EvenCart.Infrastructure.Mvc.ModelFactories;

namespace EvenCart.Areas.Administration.Factories.Orders
{
    public interface IOrderFulfillmentModelFactory : IModelFactory<IList<OrderFulfillment>, IList<OrderFulfillmentListModel>>
    {
        IList<OrderFulfillmentEditorModel> Create(IList<WarehouseInventory> inventories,
            IList<OrderFulfillment> fulfillments);
    }
}