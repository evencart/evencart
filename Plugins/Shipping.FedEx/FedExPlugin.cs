using System;
using RoastedMarketplace.Core.Infrastructure.Routing;
using RoastedMarketplace.Data.Entity.Purchases;
using RoastedMarketplace.Services.Shipping;

namespace Shipping.FedEx
{
    public class FedExPlugin : IShipmentHandlerPlugin
    {
        public void Install()
        {
            
        }

        public void Uninstall()
        {
            
        }

        public RouteData GetConfigurationPageRouteData()
        {
            throw new NotImplementedException();
        }

        public RouteData GetDisplayPageRouteData()
        {
            throw new NotImplementedException();
        }

        public decimal GetShippingHandlerFee(Cart cart)
        {
            throw new NotImplementedException();
        }
    }
}
