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
using EvenCart.Data.Entity.Users;

namespace EvenCart.Data.Entity.Gdpr
{
    public class UserConsent : FoundationEntity
    {
        public int UserId { get; set; }

        public int ConsentId { get; set; }

        public ConsentStatus ConsentStatus { get; set; }

        #region Virtual Properties
        public virtual User User { get; set; }

        public virtual Consent Consent { get; set; }
        #endregion
    }
}