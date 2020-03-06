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
using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Infrastructure.ViewEngines.GlobalObjects.Implementations
{
    public class NavigationImplementation : FoundationModel
    {
        public string Title { get; set; }

        public string Url { get; set; }

        public string Css { get; set; }

        public bool IsGroup { get; set; }

        public bool OpenInNewWindow { get; set; }

        public string Description { get; set; }

        public string ExtraData { get; set; }

        public IList<NavigationImplementation> Children { get; set; }

        public int Id { get; set; }
    }
}