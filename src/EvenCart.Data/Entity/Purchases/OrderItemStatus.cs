namespace EvenCart.Data.Entity.Purchases
{
    public enum OrderItemStatus
    {
        Pending = 1,
        Shipped = 10,
        Delivered = 20,
        Returned = 30,
        Cancelled = 40
    }
}