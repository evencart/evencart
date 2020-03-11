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

using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Models.Orders
{
    /// <summary>
    /// A return request object for single order item
    /// </summary>
    public class ReturnRequestModel : FoundationModel
    {
        /// <summary>
        /// The id of the order item
        /// </summary>
        public int OrderItemId { get; set; }

        /// <summary>
        /// The quantity of the item to return
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// The reason id for return request
        /// </summary>
        public int ReturnReasonId { get; set; }

        /// <summary>
        /// The action id for return request
        /// </summary>
        public int ReturnActionId { get; set; }

        /// <summary>
        /// The comments as provided by customer
        /// </summary>
        public string CustomerComments { get; set; }
    }
}