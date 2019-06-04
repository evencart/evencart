using System.Collections.Generic;
using EvenCart.Core.Plugins;
using EvenCart.Data.Entity.Purchases;

namespace EvenCart.Services.Plugins
{
    public interface IShipmentHandlerPlugin : IPlugin
    {
        decimal GetShippingHandlerFee(Cart cart);

        bool IsMethodAvailable(Cart cart);

        IList<ShippingOption> GetAvailableOptions(Cart cart, ShipperInfo shipperInfo);
    }
}