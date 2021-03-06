﻿#region License
// Copyright (c) Sojatia Infocrafts Private Limited.
// The following code is part of EvenCart eCommerce Software (https://evencart.co) Dual Licensed under the terms of
// 
// 1. GNU GPLv3 with additional terms (available to read at https://evencart.co/license)
// 2. EvenCart Proprietary License (available to read at https://evencart.co/license/commercial-license).
// 
// You can select one of the above two licenses according to your requirements. The usage of this code is
// subject to the terms of the license chosen by you.
#endregion

using Genesis.Infrastructure.Mvc.Models;

namespace EvenCart.Models.Orders
{
    /// <summary>
    /// Represents a unique tax for a single order
    /// </summary>
    public class OrderTaxModel : GenesisModel
    {
        public string TaxName { get; set; }

        public decimal TaxPercent { get; set; }

        public decimal Amount { get; set; }
    }
}