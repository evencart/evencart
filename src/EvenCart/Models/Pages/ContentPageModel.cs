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
using EvenCart.Models.Users;
using Genesis.Extensions;
using Genesis.Infrastructure.Mvc.Models;

namespace EvenCart.Models.Pages
{
    public class ContentPageModel : GenesisModel
    {
        public string Name { get; set; }

        public string Content { get; set; }

        public bool Published { get; set; }

        public bool Private { get; set; }

        public string Password { get; set; }

        public string SystemName { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime CreatedOnLocal => CreatedOn.ToUserDateTime();

        public DateTime UpdatedOn { get; set; }

        public DateTime UpdatedOnLocal => UpdatedOn.ToUserDateTime();

        public DateTime PublishedOn { get; set; }

        public UserMiniModel User { get; set; }
    }
}