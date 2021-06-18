﻿#region License
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
using Genesis.Infrastructure.Mvc.Models;

namespace EvenCart.Models.Gdpr
{
    public class ConsentGroupModel : GenesisEntityModel
    {
        /// <summary>
        /// The name of consent group
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The description of the group
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// List of <see cref="ConsentModel">consents</see> that belong to this group
        /// </summary>
        public IList<ConsentModel> Consents { get; set; }
    }
}