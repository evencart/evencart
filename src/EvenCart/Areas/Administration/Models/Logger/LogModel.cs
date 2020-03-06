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
using EvenCart.Data.Enum;
using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Areas.Administration.Models.Logger
{
    public class LogModel : FoundationEntityModel
    {
        public LogLevel LogLevel { get; set; }

        public string ShortMessage { get; set; }

        public string Details { get; set; }

        public string IpAddress { get; set; }

        public string Url { get; set; }

        public string ReferralUrl { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}