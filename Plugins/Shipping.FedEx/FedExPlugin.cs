using System;
using RoastedMarketplace.Core.Plugins;
using RoastedMarketplace.Data.Entity.Purchases;
using RoastedMarketplace.Infrastructure.Plugins;
using RoastedMarketplace.Services.Plugins;

namespace Shipping.FedEx
{
    public class FedExPlugin : FoundationPlugin, IShipmentHandlerPlugin
    {
        public decimal GetShippingHandlerFee(Cart cart)
        {
            throw new NotImplementedException();
        }
    }
}
