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
        RefundPending = 80,
        RefundedPartially = 90,
        Failed = 100
    }
}