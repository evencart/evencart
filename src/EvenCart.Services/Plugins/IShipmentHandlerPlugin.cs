using System.Collections.Generic;
using EvenCart.Core.Plugins;
using EvenCart.Data.Entity.Addresses;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Data.Entity.Shop;

namespace EvenCart.Services.Plugins
{
    public interface IShipmentHandlerPlugin : IPlugin
    {
        decimal GetShippingHandlerFee(Cart cart);

        bool IsMethodAvailable(Cart cart);

        IList<ShippingOption> GetAvailableOptions(IList<Product> products, Address shipperInfo, Address receiverInfo);
    }
}