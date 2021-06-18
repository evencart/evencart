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
using Genesis.Infrastructure.Mvc.Models;

namespace EvenCart.Areas.Administration.Models.Updates
{
    /// <summary>
    /// Represents a news object
    /// </summary>
    public class UpdateModel : GenesisModel
    {
        /// <summary>
        /// The title of the update
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The summary of the update
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The date when this update was published
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// The target url of the update
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Specifies if update should be highlighted
        /// </summary>
        public bool Highlight { get; set; }
    }
}