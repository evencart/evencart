using EvenCart.Core.Plugins;
using EvenCart.Services.Plugins;

namespace EvenCart.Services.Helpers
{
    public static class PluginHelper
    {
        public static IPaymentHandlerPlugin GetPaymentHandler(string paymentHandlerName)
        {
            return PluginFinder.FindPlugin(paymentHandlerName)?.LoadPluginInstance<IPaymentHandlerPlugin>();
        }

        public static IShipmentHandlerPlugin GetShipmentHandler(string shipmentHandlerName)
        {
            return PluginFinder.FindPlugin(shipmentHandlerName)?.LoadPluginInstance<IShipmentHandlerPlugin>();
        }
    }
}