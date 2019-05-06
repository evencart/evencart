using EvenCart.Core.Plugins;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Services.Plugins;

namespace Shipping.FedEx
{
    public class FedExPlugin : FoundationPlugin, IShipmentHandlerPlugin
    {
        public decimal GetShippingHandlerFee(Cart cart)
        {
            return 0;
        }
    }
}
