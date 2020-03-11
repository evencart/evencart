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

using System.Collections.Generic;
using EvenCart.Data.Entity.Purchases;

namespace EvenCart.Data.Entity.Payments
{
    /// <summary>
    /// A single request class to implement all types of transaction requests
    /// </summary>
    public class TransactionRequest 
    {
        public Order Order { get; set; }

        public string TransactionGuid { get; set; }

        public Dictionary<string, object> Parameters { get; set; }

        public bool IsPartialRefund { get; set; }

        public TransactionRequestType RequestType { get; set; }

        public decimal? Amount { get; set; }

        public decimal? InitialFee { get; set; }
    }
}