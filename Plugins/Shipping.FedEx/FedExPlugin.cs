using RoastedMarketplace.Core.Plugins;
using RoastedMarketplace.Data.Entity.Purchases;
using RoastedMarketplace.Services.Plugins;

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
