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
using EvenCart.Core.Data;
using EvenCart.Data.Entity.EntityProperties;
using EvenCart.Data.Entity.Pages;
using EvenCart.Data.Entity.Shop;
using EvenCart.Data.Interfaces;

namespace EvenCart.Data.Entity.Users
{
    public class Vendor : FoundationEntity, IHasEntityProperties, ISoftDeletable
    {
        public string Name { get; set; }

        public string GstNumber { get; set; }

        public string Tin { get; set; }

        public string Pan { get; set; }

        public string Address { get; set; }

        public int? StateProvinceId { get; set; }

        public string StateProvinceName { get; set; }

        public string City { get; set; }

        public int CountryId { get; set; }

        public string ZipPostalCode { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public bool Deleted { get; set; }

        public VendorStatus VendorStatus { get; set; }

        #region Virtual Properties
        public virtual IList<User> Users { get; set; }

        public virtual IList<Product> Products { get; set; }

        public virtual SeoMeta SeoMeta { get; set; }

        public virtual IList<EntityProperty> EntityProperties { get; set; }
        #endregion



    }
}