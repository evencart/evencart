#region License
// Copyright (c) Sojatia Infocrafts Private Limited.
// The following code is part of EvenCart eCommerce Software (https://evencart.co) Dual Licensed under the terms of
// 
// 1. GNU GPLv3 with additional terms (available to read at https://evencart.co/license)
// 2. EvenCart Proprietary License (available to read at https://evencart.co/license/commercial-license).
// 
// You can select one of the above two licenses according to your requirements. The usage of this code is
// subject to the terms of the license chosen by you.
#endregion

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
        OrderSubTotal = 11,
        [Description("Shipping Fee")]
        ShippingFee = 12,
    }
}