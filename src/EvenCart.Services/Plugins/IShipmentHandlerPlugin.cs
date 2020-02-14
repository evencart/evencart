using System.Collections.Generic;
using EvenCart.Core.Plugins;
using EvenCart.Data.Entity.Addresses;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Data.Entity.Shop;

namespace EvenCart.Services.Plugins
{
    public interface IShipmentHandlerPlugin : IPlugin
    {
        bool IsMethodAvailable(Cart cart);

        IList<ShippingOption> GetAvailableOptions(IList<(Product, int)> products, Address shipperInfo, Address receiverInfo);

        ShipmentInfo GetShipmentInfo(ShippingOption selectedShippingOption, IList<(Product, int)> products);

        bool SupportsLabelPurchase { get; }
    }
}