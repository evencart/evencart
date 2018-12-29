namespace RoastedMarketplace.Data.Entity.Purchases
{
    public enum ShipmentStatus
    {
        Preparing = 0,
        Packaged = 10,
        InTransit = 20,
        OutForDelivery = 30,
        DeliveryFailed = 40,
        Delivered = 50,
        Returned = 60
    }
}