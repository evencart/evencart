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
using Genesis.Data;
using Genesis.Modules.Users;

namespace EvenCart.Genesis.Modules.Users
{
    public class UserProfile : GenesisEntity
    {
        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Name { get; set; }

        public string CompanyName { get; set; }

        public string MobileNumber { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string Remarks { get; set; }

        public bool NewslettersEnabled { get; set; }

        public int? ProfilePictureId { get; set; }

        public string FatherHusbandName { get; set; }

        public string Gender { get; set; }

        public bool IsTaxExempt { get; set; }

        #region Virtual Properties
        public virtual User User { get; set; }
        #endregion

    }
}