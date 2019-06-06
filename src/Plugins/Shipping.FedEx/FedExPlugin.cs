using System.Collections.Generic;
using EvenCart.Core.Plugins;
using EvenCart.Data.Entity.Addresses;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Data.Entity.Shop;
using EvenCart.Services.Plugins;

namespace Shipping.FedEx
{
    public class FedExPlugin : FoundationPlugin, IShipmentHandlerPlugin
    {
        public decimal GetShippingHandlerFee(Cart cart)
        {
            return 0;
        }

        public bool IsMethodAvailable(Cart cart)
        {
            return true;
        }

        public IList<ShippingOption> GetAvailableOptions(IList<Product> products, Address shipperInfo, Address receiverInfo)
        {
            throw new System.NotImplementedException();
        }
    }
}
