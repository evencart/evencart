namespace EvenCart.Data.Entity.Purchases
{
    public enum OrderStatus
    {
        OnHold = 1, //manual
        New = 10, //auto
        Processing = 20, //auto
        PartiallyShipped = 30, //auto
        Shipped = 40, //auto
        Complete = 50, //auto
        Cancelled = 60, //manual
        Closed = 70, //manual
        Returned = 80, //auto
        PartiallyReturned = 90, //manual
        Delayed = 100 //manual
    }
}