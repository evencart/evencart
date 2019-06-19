namespace EvenCart.Events
{
    public enum NamedEvent
    {
        UserRegistered,
        UserActivated,

        OrderPlaced,
        OrderPaid,
        OrderPaymentInfoSaved,
        OrderAddressSaved,
        OrderShippingInfoSaved,
        OrderCancelled,

        ReturnRequestCreated,

        ShipmentShipped,
        ShipmentDelivered,
        ShipmentDeliveryFailed,

        PasswordResetRequested,
        PasswordReset,

        InvitationRequested,
        Invitation,

        ContactUs
        
    }
}