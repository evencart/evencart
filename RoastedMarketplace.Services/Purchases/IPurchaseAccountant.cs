using RoastedMarketplace.Data.Entity.Purchases;

namespace RoastedMarketplace.Services.Purchases
{
    public interface IPurchaseAccountant
    {
        void EvaluateOrderStatus(Order order);

        void EvaluateOrderStatus(string orderGuid);
    }
}