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

using EvenCart.Core.Data;
using EvenCart.Data.Entity.Shop;

namespace EvenCart.Data.Entity.Purchases
{
    public class OrderItem : FoundationEntity
    {
        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public int ProductVariantId { get; set; }

        public string AttributeJson { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public decimal Tax { get; set; }

        public decimal TaxPercent { get; set; }

        public string TaxName { get; set; }

        public bool IsDownloadable { get; set; }

        public ProductSaleType ProductSaleType { get; set; }

        public TimeCycle SubscriptionCycle { get; set; }

        public int CycleCount { get; set; }

        public int? TrialDays { get; set; }

        #region Virtual Properties
        public virtual Order Order { get; set; }

        public virtual Shipment Shipment { get; set; }

        public virtual Product Product { get; set; }

        public virtual ProductVariant ProductVariant { get; set; }
        #endregion
    }
}