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

using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Models.Countries
{
    public class StateOrProvinceModel : FoundationEntityModel
    {
        /// <summary>
        /// The country id of the state
        /// </summary>
        public int CountryId { get; set; }

        /// <summary>
        /// The name of the state
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The publish status of state
        /// </summary>
        public bool Published { get; set; }

        /// <summary>
        /// If shipping is available for the state or not
        /// </summary>
        public bool ShippingEnabled { get; set; }

        /// <summary>
        /// The display order of the state in the list
        /// </summary>
        public int DisplayOrder { get; set; }

    }
}