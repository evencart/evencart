using System.Collections.Generic;
using RoastedMarketplace.Core.Plugins;
using RoastedMarketplace.Data.Entity.Payments;
using RoastedMarketplace.Data.Entity.Purchases;
using RoastedMarketplace.Services.Payments;

namespace RoastedMarketplace.Services.Plugins
{
    public interface IPaymentHandlerPlugin : IPlugin
    {
        PaymentMethodType PaymentMethodType { get; }

        string PaymentHandlerComponentRouteName { get; }

        PaymentOperation[] SupportedOperations { get; }

        TransactionResult ProcessTransaction(TransactionRequest request);

        decimal GetPaymentHandlerFee(Cart cart);

        decimal GetPaymentHandlerFee(Order order);

        bool ValidatePaymentInfo(Dictionary<string, string> parameters, out string error);
    }
}