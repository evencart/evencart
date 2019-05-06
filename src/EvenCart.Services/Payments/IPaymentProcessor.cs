using System.Collections.Generic;
using EvenCart.Data.Entity.Payments;
using EvenCart.Data.Entity.Purchases;

namespace EvenCart.Services.Payments
{
    public interface IPaymentProcessor
    {
        TransactionResult ProcessPayment(Order order, Dictionary<string, object> paymentMethodData = null);
    }
}