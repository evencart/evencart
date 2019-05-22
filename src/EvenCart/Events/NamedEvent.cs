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

        ShipmentShipped,
        ShipmentDelivered,
        ShipmentDeliveryFailed,

        PasswordResetRequested,
        PasswordReset,

        InvitationRequested,
        Invitation
        
    }
}