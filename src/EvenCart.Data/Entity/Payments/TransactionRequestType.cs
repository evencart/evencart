namespace EvenCart.Data.Entity.Payments
{
    public enum TransactionRequestType
    {
        Payment,
        Refund,
        Void,
        Capture
    }
}