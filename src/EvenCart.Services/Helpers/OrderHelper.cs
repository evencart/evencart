using System.Linq;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Data.Entity.Shop;

namespace EvenCart.Services.Helpers
{
    public static class OrderHelper
    {
        public static bool IsSubscription(Order order)
        {
            return order.OrderItems.All(x => x.Product.ProductSaleType != ProductSaleType.OneTime);
        }
        public static bool IsDownloadOnly(Order order)
        {
            return order.OrderItems.All(x => x.IsDownloadable && !x.Product.IsShippable);
        }
    }
}