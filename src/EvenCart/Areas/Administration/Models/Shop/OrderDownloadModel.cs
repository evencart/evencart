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
using EvenCart.Infrastructure.Mvc.Validator;
using FluentValidation;

namespace EvenCart.Areas.Administration.Models.Shop
{
    public class OrderDownloadModel : FoundationModel, IRequiresValidations<OrderDownloadModel>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public int DownloadCount { get; set; }

        public bool Active { get; set; }

        public string DownloadUrl { get; set; }

        public int ItemDownloadId { get; set; }

        public int DownloadId { get; set; }

        public void SetupValidationRules(ModelValidator<OrderDownloadModel> v)
        {
            v.RuleFor(x => x.DownloadId).GreaterThan(0);
        }
    }
}