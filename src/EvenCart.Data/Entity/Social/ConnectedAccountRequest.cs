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

namespace EvenCart.Data.Entity.Social
{
    public class ConnectedAccountRequest
    {
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string ProviderName { get; set; }

        public string ProviderUserId { get; set; }

        public string AccessToken { get; set; }

        public bool AutoLogin { get; set; }
    }
}