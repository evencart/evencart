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

namespace EvenCart.Areas.Administration.Models.System
{
    /// <summary>
    /// Represents a system and software information object
    /// </summary>
    public class AboutModel : FoundationModel
    {
        public string OperatingSystemName { get; set; }

        public string AspNetVersion { get; set; }

        public string FrameworkVersion { get; set; }

        public string TimeZone { get; set; }

        public string UtcTime { get; set; }

        public IList<string> LoadedAssemblies { get; set; }
    }
}