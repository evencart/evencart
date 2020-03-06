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
using EvenCart.Models.Addresses;
using System.Collections.Generic;

namespace EvenCart.Models.Checkout
{
    /// <summary>
    /// Represents shipping options grouped by warehouse
    /// </summary>
    public class WarehouseShippingOptionModel : FoundationModel
    {
        /// <summary>
        /// The id of the warehouse
        /// </summary>
        public int WarehouseId { get; set; }

        /// <summary>
        /// The <see cref="AddressInfoModel">address</see> of the warehouse
        /// </summary>
        public AddressInfoModel WarehouseAddress { get; set; }

        /// <summary>
        /// The list of available shipping options for this warehouse
        /// </summary>
        public IList<ShippingOptionModel> ShippingOptions { get; set; }
    }
}