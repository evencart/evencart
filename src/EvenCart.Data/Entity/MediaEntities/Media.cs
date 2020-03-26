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

using System;
using EvenCart.Core.Data;
using EvenCart.Data.Enum;
using EvenCart.Data.Interfaces;

namespace EvenCart.Data.Entity.MediaEntities
{
    public class Media : FoundationEntity, IUserResource
    {
        public string Name { get; set; }

        public string SystemName { get; set; }

        public string Description { get; set; }

        public string AlternativeText { get; set; }

        public string LocalPath { get; set; }

        public string ThumbnailPath { get; set; }

        public string MimeType { get; set; }

        public MediaType MediaType { get; set; }

        public DateTime CreatedOn { get; set; }

        public int UserId { get; set; }

        public int DisplayOrder { get; set; }

        public string MetaData { get; set; }
    }
}