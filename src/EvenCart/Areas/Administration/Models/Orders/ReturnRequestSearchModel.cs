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
using System.Collections.Generic;
using EvenCart.Data.Entity.Purchases;
using Genesis.Infrastructure.Mvc.Models;

namespace EvenCart.Areas.Administration.Models.Orders
{
    /// <summary>
    /// Represents a return request search object
    /// </summary>
    public class ReturnRequestSearchModel : AdminSearchModel
    {
        /// <summary>
        /// The list of <see cref="ReturnRequestStatus">status</see> values to restrict search results to
        /// </summary>
        public IList<ReturnRequestStatus> ReturnRequestStatus { get; set; }

        /// <summary>
        /// The start date to restrict the search results from
        /// </summary>
        public DateTime? FromDate { get; set; }

        /// <summary>
        /// The end date to restrict the search results from
        /// </summary>
        public DateTime? ToDate { get; set; }
    }
}