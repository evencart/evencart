using System.Collections.Generic;
using EvenCart.Data.Entity.Payments;
using EvenCart.Data.Entity.Purchases;

namespace EvenCart.Services.Payments
{
    public interface IPaymentProcessor
    {
        TransactionResult ProcessPayment(Order order, Dictionary<string, object> paymentMethodData = null);

        TransactionResult ProcessRefund(Order order, decimal amount);

        TransactionResult ProcessVoid(Order order);

        TransactionResult ProcessCreateSubscription(Order order, Dictionary<string, object> paymentMethodData = null);

        TransactionResult ProcessSubscription(Order order, Dictionary<string, object> paymentMethodData = null);

        TransactionResult ProcessCancelSubscription(Order order, Dictionary<string, object> paymentMethodData = null);
    }
}