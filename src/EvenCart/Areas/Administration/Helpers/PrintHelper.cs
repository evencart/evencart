using System.Collections.Generic;
using System.Linq;
using EvenCart.Areas.Administration.Factories.Orders;
using EvenCart.Areas.Administration.Factories.Warehouses;
using EvenCart.Core.Infrastructure;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Infrastructure.ViewEngines;
using IOrderModelFactory = EvenCart.Factories.Orders.IOrderModelFactory;

namespace EvenCart.Areas.Administration.Helpers
{
    public static class PrintHelper
    {
        public static string GetInvoice(Order order)
        {
            var viewAccountant = DependencyResolver.Resolve<IViewAccountant>();
            var orderModelFactory = DependencyResolver.Resolve<IOrderModelFactory>();
            var invoiceViewPath = viewAccountant.GetThemeViewPath("Orders/Invoice", true);

            var model = orderModelFactory.Create(order);
            var htmlView = viewAccountant.RenderView(invoiceViewPath, invoiceViewPath, null,
                new Dictionary<string, object> {{"order", model}});
            return htmlView;
        }

        public static string GetPackingSlip(Shipment shipment)
        {
            var viewAccountant = DependencyResolver.Resolve<IViewAccountant>();
            var shipmentModelFactory = DependencyResolver.Resolve<IShipmentModelFactory>();
            var orderModelFactory = DependencyResolver.Resolve<IOrderModelFactory>();
            var invoiceViewPath = viewAccountant.GetThemeViewPath("Orders/PackingSlip", true);

            var warehouseFactory = DependencyResolver.Resolve<IWarehouseModelFactory>();
            var warehouseModel = warehouseFactory.Create(shipment.Warehouse);

            var shipmentModel = shipmentModelFactory.Create(shipment);
            var orders = shipment.ShipmentItems.Select(x => x.OrderItem.Order).ToList();
            var orderModels = orders.Select(orderModelFactory.Create).ToList();

            var htmlView = viewAccountant.RenderView(invoiceViewPath, invoiceViewPath, null,
                new Dictionary<string, object>
                    {{"shipment", shipmentModel}, {"warehouse", warehouseModel}, {"orders", orderModels}});
            return htmlView;
        }
    }
}