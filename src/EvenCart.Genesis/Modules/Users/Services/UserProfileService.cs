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

using EvenCart.Genesis.Modules.Users;
using Genesis.Services;

namespace Genesis.Modules.Users
{
    public class UserProfileService : GenesisEntityService<UserProfile>, IUserProfileService
    {
        public UserProfile GetUserProfile(int userId)
        {
            return FirstOrDefault(x => x.UserId == userId);
        }
    }
}