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

using EvenCart.Data.Entity.Purchases;
using EvenCart.Models.Products;
using Genesis.Infrastructure.Mvc.Models;

namespace EvenCart.Models.Reviews
{
    public class PendingReviewModel : GenesisModel
    {
        public string OrderNumber { get; set; }

        public string OrderGuid { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public ProductModel Product { get; set; }
    }
}