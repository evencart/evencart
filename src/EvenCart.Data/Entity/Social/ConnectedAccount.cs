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

namespace EvenCart.Data.Entity.Social
{
    public class ConnectedAccount : FoundationEntity
    {
        public int UserId { get; set; }

        public string ProviderName { get; set; }

        public string ProviderUserId { get; set; }

        public string AccessToken { get; set; }

        #region Virtual Properties
        public virtual User User { get; set; }
        #endregion
    }
}