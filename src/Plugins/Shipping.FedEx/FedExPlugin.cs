using System.Collections.Generic;
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

        public bool IsMethodAvailable(Cart cart)
        {
            return true;
        }

        public IList<ShippingOption> GetAvailableOptions(Cart cart, ShipperInfo shipperInfo)
        {
            throw new System.NotImplementedException();
        }

        public IList<ShippingOption> GetAvailableOptions(Cart cart)
        {
            throw new System.NotImplementedException();
        }
    }
}
