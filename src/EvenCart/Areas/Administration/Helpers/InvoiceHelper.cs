using System.Collections.Generic;
using EvenCart.Core.Infrastructure;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Factories.Orders;
using EvenCart.Infrastructure.ViewEngines;

namespace EvenCart.Areas.Administration.Helpers
{
    public static class InvoiceHelper
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
    }
}