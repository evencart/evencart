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

using Genesis.Modules.Users;

namespace EvenCart.Genesis.Modules.Users.Extensions
{
    public static class UserCapabilityExtensions
    {
        public static bool IsAdvisor(this User user)
        {
            return user.Is(RoleNames.Advisor);
        }

        public static bool IsManager(this User user)
        {
            return user.Is(RoleNames.Manager);
        }
    }
}