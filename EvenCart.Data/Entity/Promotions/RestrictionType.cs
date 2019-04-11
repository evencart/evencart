using System.ComponentModel;

namespace EvenCart.Data.Entity.Promotions
{
    public enum RestrictionType
    {
        Products = 1,
        Categories = 2,
        Users = 3,
        [Description("User Groups")]
        UserGroups = 4,
        Roles = 5,
        Vendors = 6,
        Manufacturers = 7,
        [Description("Payment Methods")]
        PaymentMethods = 8,
        [Description("Shipping Methods")]
        ShippingMethods = 9,
        [Description("Order Total")]
        OrderTotal = 10,
        [Description("Order SubTotal")]
        OrderSubTotal = 11
    }
}