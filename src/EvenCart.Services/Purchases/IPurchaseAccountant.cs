using EvenCart.Data.Entity.Purchases;

namespace EvenCart.Services.Purchases
{
    public interface IPurchaseAccountant
    {
        //todo: move these methods to order accountant
        void EvaluateOrderStatus(Order order);

        void EvaluateOrderStatus(string orderGuid);
    }
}