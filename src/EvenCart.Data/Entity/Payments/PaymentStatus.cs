namespace EvenCart.Data.Entity.Payments
{
    public enum PaymentStatus
    {
        OnHold = 1,
        Pending = 10,
        Processing = 20,
        Complete = 30,
        Refunded = 40,
        Voided = 50,
        Authorized = 60,
        Captured = 70,
        RefundPending = 80
    }
}