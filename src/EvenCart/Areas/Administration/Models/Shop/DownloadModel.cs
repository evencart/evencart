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
using FluentValidation;
using Genesis.Infrastructure.Mvc.Models;
using Genesis.Infrastructure.Mvc.Validator;

namespace EvenCart.Areas.Administration.Models.Shop
{
    public class DownloadModel : GenesisEntityModel, IRequiresValidations<DownloadModel>
    {
        public string Guid { get; set; }

        public int ProductId { get; set; }

        public int ProductVariantId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string FileLocation { get; set; }

        public bool IsFileLocationUrl { get; set; }

        public string DownloadUrl { get; set; }

        public bool RequirePurchase { get; set; }

        public bool RequireLogin { get; set; }

        public int MaximumDownloads { get; set; }

        public int DisplayOrder { get; set; }

        public DownloadActivationType DownloadActivationType { get; set; }

        public int DownloadCount { get; set; }

        public bool Published { get; set; }

        public void SetupValidationRules(ModelValidator<DownloadModel> v)
        {
            v.RuleFor(x => x.ProductId).GreaterThan(0);
        }
    }
}