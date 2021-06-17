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

using EvenCart.Data.Entity.Shop;
using Genesis.Infrastructure.Mvc.Attributes;
using Genesis.Infrastructure.Mvc.Models;
using Genesis.Modules.Data;

namespace EvenCart.Areas.Administration.Models.Orders
{
    [FormatAsCurrencies(nameof(Price), nameof(TotalPrice), nameof(LineTotal), nameof(Tax))]
    public class OrderItemModel : GenesisEntityModel
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public string ImageUrl { get; set; }

        public string AttributeText { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public decimal TotalPrice { get; set; }

        public decimal LineTotal => TotalPrice + Tax;

        public decimal Tax { get; set; }

        public decimal TaxPercent { get; set; }

        public bool Shipped { get; set; }

        public ProductSaleType ProductSaleType { get; set; }

        public TimeCycle SubscriptionCycle { get; set; }

        public int CycleCount { get; set; }

        public int? TrialDays { get; set; }
    }
}