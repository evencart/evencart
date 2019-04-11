using System.Collections.Generic;
using EvenCart.Core.Plugins;
using EvenCart.Data.Entity.Payments;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Services.Payments;

namespace EvenCart.Services.Plugins
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