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

using System;

namespace EvenCart.Data.Enum
{
    [Flags]
    public enum CreditTransactionType
    {
        /// <summary>
        /// Credits are issued to user
        /// </summary>
        Issued = 1,

        /// <summary>
        /// Some issued credits have been taken back
        /// </summary>
        IssuedRevert = 2,

        /// <summary>
        /// Credits are spent by user
        /// </summary>
        Spent = 4,

        /// <summary>
        /// Credits are refunded to user's account
        /// </summary>
        Refund = 8
    }
}