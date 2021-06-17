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

using Genesis.Infrastructure.Mvc.Models;
using Genesis.Modules.Data;

namespace EvenCart.Areas.Administration.Models.Users
{
    public class CartItemModel : GenesisModel
    {
        public string ProductName { get; set; }

        public int ProductId { get; set; }

        public string AttributeText { get; set; }

        public decimal Price { get; set; } //= 80

        public string PriceFormatted => Price.ToCurrency();

        public int Quantity { get; set; }

        public decimal Tax { get; set; }

        public string TaxFormatted => Tax.ToCurrency();

        public decimal TaxPercent { get; set; }

        public decimal Discount { get; set; } //=10

        public string DiscountFormatted => Discount.ToCurrency();

        public decimal FinalPrice { get; set; } //=70

        public string FinalPriceFormatted => FinalPrice.ToCurrency();

        public string ImageUrl { get; set; }

        public string Slug { get; set; }

        public decimal SubTotal { get; set; }

        public string SubTotalFormatted => SubTotal.ToCurrency();

        public int Id { get; set; }
    }
}