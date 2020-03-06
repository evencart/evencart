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
using System.Collections.Generic;
using System.Linq.Expressions;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Users;
using EvenCart.Data.Enum;

namespace EvenCart.Services.Users
{
    public interface IUserService : IFoundationEntityService<User>
    {
        IList<User> GetUsers(string searchText, int[] restrictToRoles, Expression<Func<User, object>> orderBy, SortOrder sortOrder, int page, int count, out int totalMatches, bool negateRoleRestriction = false, Expression<Func<User, bool>> where = null);

        User GetByUserInfo(string email, string guid = null);

        void AnonymizeUser(int userId);
    }
}