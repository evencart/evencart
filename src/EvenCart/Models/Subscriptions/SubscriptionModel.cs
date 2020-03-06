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

using EvenCart.Infrastructure;
using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Mvc.Validator;
using EvenCart.Services.Extensions;
using FluentValidation;

namespace EvenCart.Models.Subscriptions
{
    /// <summary>
    /// Represents a single subscription 
    /// </summary>
    public class SubscriptionModel : FoundationModel, IRequiresValidations<SubscriptionModel>
    {
        public string SubscriptionGuid { get; set; }

        public string Email { get; set; }

        public string Data { get; set; }

        public void SetupValidationRules(ModelValidator<SubscriptionModel> v)
        {
            v.RuleFor(x => x.SubscriptionGuid).NotEmpty();
            v.RuleFor(x => x.Email).NotEmpty().EmailAddress().When(x => ApplicationEngine.CurrentUser.IsVisitor());
        }
    }
}