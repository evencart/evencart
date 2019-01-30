using RoastedMarketplace.Core.Plugins;
using RoastedMarketplace.Data.Entity.Purchases;

namespace RoastedMarketplace.Services.Plugins
{
    public interface IShipmentHandlerPlugin : IPlugin
    {
        decimal GetShippingHandlerFee(Cart cart);
    }
}