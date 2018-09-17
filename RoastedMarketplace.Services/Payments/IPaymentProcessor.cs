using System.Collections.Generic;
using RoastedMarketplace.Data.Entity.Payments;
using RoastedMarketplace.Data.Entity.Purchases;

namespace RoastedMarketplace.Services.Payments
{
    public interface IPaymentProcessor
    {
        TransactionResult ProcessPayment(Order order, Dictionary<string, object> paymentMethodData = null);
    }
}