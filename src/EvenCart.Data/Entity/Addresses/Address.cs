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

using EvenCart.Core.Data;

namespace EvenCart.Data.Entity.Addresses
{
    /// <summary>
    /// Addresses associated with various entities such as User, Page etc.
    /// </summary>
    public class Address : FoundationEntity
    {
        public string EntityName { get; set; }

        public int EntityId { get; set; }

        public string Name { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string Landmark { get; set; }

        public int? StateProvinceId { get; set; }

        public string StateProvinceName { get; set; }

        public string City { get; set; }

        public string ZipPostalCode { get; set; }

        public int CountryId { get; set; }

        public string Phone { get; set; }

        public string Website { get; set; }

        public string Email { get; set; }

        public AddressType AddressType { get; set; }

        #region Virtual Properties
        public virtual Country Country { get; set; }

        public virtual StateOrProvince StateOrProvince { get; set; }
        #endregion
    }
}