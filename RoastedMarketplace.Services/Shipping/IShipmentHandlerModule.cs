using RoastedMarketplace.Core.Modules;
using RoastedMarketplace.Data.Entity.Purchases;

namespace RoastedMarketplace.Services.Shipping
{
    public interface IShipmentHandlerModule : IModule
    {
        decimal GetShippingHandlerFee(Cart cart);
    }
}