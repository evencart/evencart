using RoastedMarketplace.Core.Modules;
using RoastedMarketplace.Data.Entity.Payments;
using RoastedMarketplace.Data.Entity.Purchases;
using RoastedMarketplace.Services.Payments;

namespace RoastedMarketplace.Services.Modules
{
    public interface IPaymentHandlerModule : IModule
    {
        PaymentMethodType PaymentMethodType { get; }

        string PaymentHandlerComponentName { get; }

        PaymentOperation[] SupportedOperations { get; }

        TransactionResult ProcessTransaction(TransactionRequest request);

        decimal GetPaymentHandlerFee(Cart cart);
    }
}