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

namespace EvenCart.Areas.Administration.Models.Dialog
{
    public class DialogResultModel : GenesisModel
    {
        public string ApiUrl { get; set; }

        public string UiUrl { get; set; }

        public string ResponseObjectName { get; set; }

        public bool MultiSelect { get; set; }

        public string DialogTitle { get; set; }

        public string DisplayField { get; set; }

        public string RequestExtraData { get; set; }

    }
}