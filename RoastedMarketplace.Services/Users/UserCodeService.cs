using System;
using System.Linq;
using DotEntity.Enumerations;
using RoastedMarketplace.Core.Services;
using RoastedMarketplace.Data.Entity.Users;
using RoastedMarketplace.Data.Extensions;

namespace RoastedMarketplace.Services.Users
{
    public class UserCodeService : FoundationEntityService<UserCode>, IUserCodeService
    {
        public UserCode GetUserCode(int userId, UserCodeType userCodeType)
        {
            var userCode = Repository.Where(x => x.UserId == userId && x.CodeType == userCodeType)
                .Join<User>("UserId", "Id", joinType: JoinType.LeftOuter)
                .Relate(RelationTypes.OneToOne<UserCode, User>())
                .SelectNested()
                .FirstOrDefault() ?? new UserCode();

            userCode.UserId = userId;
            userCode.Code = Guid.NewGuid().ToString("D");
            userCode.CodeType = userCodeType;
            userCode.DateCreated = DateTime.UtcNow;
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
    }
}