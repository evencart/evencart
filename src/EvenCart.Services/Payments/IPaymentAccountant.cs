using EvenCart.Data.Entity.Payments;

namespace EvenCart.Services.Payments
{
    public interface IPaymentAccountant
    {
        void ProcessTransactionResult(TransactionResult result, bool clearCart = false);
    }
}