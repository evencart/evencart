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
using EvenCart.Infrastructure.Extensions;
using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Infrastructure.ViewEngines.GlobalObjects.Implementations
{
    public class CartImplementation : FoundationModel
    {
        public int TotalItems { get; set; }

        public decimal SubTotal { get; set; }

        public string SubTotalFormatted => SubTotal.ToCurrency();

        public decimal Tax { get; set; }

        public string TaxFormatted => Tax.ToCurrency();

        public string ShippingMethodName { get; set; }

        public decimal ShippingMethodFee { get; set; }

        public string ShippingMethodFeeFormatted => ShippingMethodFee.ToCurrency();

        public string ShippingOptionName { get; set; }

        public decimal PaymentMethodName { get; set; }

        public decimal PaymentMethodFee { get; set; }

        public string PaymentMethodFeeFormatted => PaymentMethodFee.ToCurrency();

        public decimal FinalAmount { get; set; }

        public string FinalAmountFormatted => FinalAmount.ToCurrency();

        public decimal CompareFinalAmount { get; set; }

        public string CompareFinalAmountFormatted => CompareFinalAmount.ToCurrency();

        public IList<CartItemImplementation> Items { get; set; }

        public decimal Discount { get; set; }

        public string DiscountFormatted => Discount.ToCurrency();

        public string DiscountCoupon { get; set; }

        public bool ConflictingProducts { get; set; }
    }
}