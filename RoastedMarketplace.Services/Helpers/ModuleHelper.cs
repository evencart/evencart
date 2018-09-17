using RoastedMarketplace.Core.Modules;
using RoastedMarketplace.Services.Modules;
using RoastedMarketplace.Services.Shipping;

namespace RoastedMarketplace.Services.Helpers
{
    public static class ModuleHelper
    {
        public static IPaymentHandlerModule GetPaymentHandler(string paymentHandlerName)
        {
            return ModuleFinder.FindModule(paymentHandlerName).LoadModuleInstance<IPaymentHandlerModule>();
        }

        public static IShipmentHandlerModule GetShipmentHandler(string shipmentHandlerName)
        {
            return ModuleFinder.FindModule(shipmentHandlerName).LoadModuleInstance<IShipmentHandlerModule>();
        }
    }
}