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
using System.Linq;
using DotEntity.Enumerations;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Users;
using EvenCart.Data.Extensions;
using EvenCart.Services.Security;

namespace EvenCart.Services.Users
{
    public class UserCodeService : FoundationEntityService<UserCode>, IUserCodeService
    {
        private readonly ICryptographyService _cryptographyService;

        public UserCodeService(ICryptographyService cryptographyService)
        {
            _cryptographyService = cryptographyService;
        }

        public UserCode GetUserCode(int userId, UserCodeType userCodeType)
        {
            var userCode = Repository.Where(x => x.UserId == userId && x.CodeType == userCodeType)
                .Join<User>("UserId", "Id", joinType: JoinType.LeftOuter)
                .Relate(RelationTypes.OneToOne<UserCode, User>())
                .SelectNested()
                .FirstOrDefault() ?? new UserCode();

            userCode.UserId = userId;
            userCode.Code = userCodeType == UserCodeType.EmailOtp ? _cryptographyService.GetNumericCode(6) : Guid.NewGuid().ToString("D");
         
            userCode.CodeType = userCodeType;
            userCode.CreatedOn = DateTime.UtcNow;
            InsertOrUpdate(userCode);
            return userCode;
        }

        public UserCode GetUserCode(string code, UserCodeType userCodeType)
        {
            return Repository.Where(x => x.Code == code && x.CodeType == userCodeType)
                .Join<User>("UserId", "Id", joinType: JoinType.LeftOuter)
                .Relate(RelationTypes.OneToOne<UserCode, User>())
                .SelectNested()
                .FirstOrDefault();
        }

        public UserCode GetUserCodeByEmail(string email, UserCodeType userCodeType)
        {
            var userCode = Repository.Where(x => x.Email == email && x.CodeType == userCodeType)
                .Select()
                .FirstOrDefault() ?? new UserCode();
            userCode.Code = Guid.NewGuid().ToString("D");
            userCode.CodeType = userCodeType;
            userCode.CreatedOn = DateTime.UtcNow;
            userCode.Email = email;
            InsertOrUpdate(userCode);
            return userCode;
        }
    }
}