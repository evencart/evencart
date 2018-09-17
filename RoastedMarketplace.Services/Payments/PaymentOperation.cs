namespace RoastedMarketplace.Services.Payments
{
    public enum PaymentOperation
    {
        Authorize = 10,
        Capture = 20,
        Refund = 30,
        Void = 40
    }
}