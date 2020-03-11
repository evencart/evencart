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
        Delayed = 100, //manual
        PendingCancellation = 110,
        SubscriptionCancelled = 120
    }
}