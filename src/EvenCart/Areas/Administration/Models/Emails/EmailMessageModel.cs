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
using System.Collections.Generic;
using Genesis.Infrastructure.Mvc.Models;

namespace EvenCart.Areas.Administration.Models.Emails
{
    public class EmailMessageModel : GenesisEntityModel
    {
        public IList<string> Tos { get; set; }

        public IList<string> Ccs { get; set; }

        public IList<string> Bccs { get; set; }

        public IList<string> ReplyTos { get; set; }

        public string Subject { get; set; }

        public string EmailBody { get; set; }

        public bool IsEmailBodyHtml { get; set; }

        public DateTime SendingDate { get; set; }

        public bool IsSent { get; set; }

        public bool DeleteExisting { get; set; }
    }
}