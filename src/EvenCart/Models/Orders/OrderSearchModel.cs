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
using Genesis.Infrastructure.Mvc.Models;

namespace EvenCart.Models.Orders
{
    public class OrderSearchModel : GenesisModel
    {
        /// <summary>
        /// The start date to search the orders from. Defaults to 6 months old
        /// </summary>
        public DateTime? FromDate { get; set; } = DateTime.UtcNow.AddMonths(-6); //last 6 months by default
        /// <summary>
        /// The end date to search the orders to. Defaults to today.
        /// </summary>
        public DateTime? ToDate { get; set; } = DateTime.UtcNow;
        /// <summary>
        /// The status of order. Can be one of all, open, closed, returned or cancelled
        /// </summary>
        public string OrderStatus { get; set; } = "all";

        /// <summary>
        /// The page being requested in a paginated request. Default is 1.
        /// </summary>
        public int Current { get; set; } = 1;

        /// <summary>
        /// The total number of result rows to be returned
        /// </summary>
        public int RowCount { get; set; } = 15;
    }
}