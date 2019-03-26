using RoastedMarketplace.Data.Entity.Payments;

namespace RoastedMarketplace.Services.Payments
{
    public interface IPaymentAccountant
    {
        void ProcessTransactionResult(TransactionResult result, bool clearCart = false);
    }
}