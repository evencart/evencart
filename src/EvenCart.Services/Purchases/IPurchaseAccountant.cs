using EvenCart.Data.Entity.Purchases;

namespace EvenCart.Services.Purchases
{
    public interface IPurchaseAccountant
    {
        void EvaluateOrderStatus(Order order);

        void EvaluateOrderStatus(string orderGuid);
    }
}