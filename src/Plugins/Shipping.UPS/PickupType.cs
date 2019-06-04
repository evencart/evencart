using System.ComponentModel;

namespace Shipping.UPS
{
    public enum PickupType
    {
        [Description("Daily Pickup")]
        DailyPickup,
        [Description("Customer Center")]
        CustomerCounter,
        [Description("One Time Pickup")]
        OneTimePickup,
        [Description("Letter Center")]
        LetterCenter,
        [Description("Air Service Center")]
        AirServiceCenter

    }
}