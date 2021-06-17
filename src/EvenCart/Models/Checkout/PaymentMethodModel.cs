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

using FluentValidation;
using Genesis.Infrastructure.Mvc.Models;
using Genesis.Infrastructure.Mvc.Validator;
using Microsoft.AspNetCore.Http;

namespace EvenCart.Models.Checkout
{
    public class PaymentMethodModel : GenesisModel, IRequiresValidations<PaymentMethodModel>
    {
        /// <summary>
        /// The payment method system name
        /// </summary>
        public string SystemName { get; set; }

        /// <summary>
        /// The friendly name for payment method. Ignored for POST requests.
        /// </summary>
        public string FriendlyName { get; set; }

        /// <summary>
        /// The description of payment method. Ignored for POST requests.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The additional fee for payment method. Ignored for POST requests.
        /// </summary>
        public decimal Fee { get; set; }

        /// <summary>
        /// The url to redirect for payment method processing. Ignored for POST requests.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// The payment information in key-value format. 
        /// </summary>
        public IFormCollection FormCollection { get; set; }

        /// <summary>
        /// The order guid for which this payment method is being used. This is used only for retrying a previously failed order.
        /// </summary>
        public string OrderGuid { get; set; }

        /// <summary>
        /// Specifies if store credits should be used if available
        /// </summary>
        public bool UseStoreCredits { get; set; }

        public void SetupValidationRules(ModelValidator<PaymentMethodModel> v)
        {
            v.RuleFor(x => x.SystemName).NotEmpty().When(x => !x.UseStoreCredits);
        }
    }
}