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
using EvenCart.Core.Config;
using EvenCart.Data.Enum;

namespace EvenCart.Data.Entity.Settings
{
    public class SystemSettings : ISettingGroup
    {
        /// <summary>
        /// Is the application installed?
        /// </summary>
        public bool IsInstalled { get; set; }

        /// <summary>
        /// Specifies minimum log level that should be used for logging
        /// </summary>
        public LogLevel MinimumLogLevel { get; set; }

        /// <summary>
        /// Specifies the update fetch interval in hours.
        /// </summary>
        public int UpdateFetchIntervalInHours { get; set; }

        /// <summary>
        /// The latest updates fetched
        /// </summary>
        public string LatestUpdatesFetched { get; set; }

        /// <summary>
        /// The latest date when updates were fetched
        /// </summary>
        public DateTime LatestFetchedOn { get; set; }
    }
}