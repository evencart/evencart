using System;
using DotEntity;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Users;

namespace EvenCart.Services.Users
{
    public class StoreCreditService : FoundationEntityService<StoreCredit>, IStoreCreditService
    {
        public decimal GetBalance(int userId)
        {
            var tableName = DotEntityDb.GetTableNameForType<StoreCredit>();
            var creditColumnName = DotEntityDb.Provider.SafeEnclose(nameof(StoreCredit.Credit));
            var userIdColumnName = DotEntityDb.Provider.SafeEnclose(nameof(StoreCredit.UserId));
            var availableOnColumnName = DotEntityDb.Provider.SafeEnclose(nameof(StoreCredit.AvailableOn));
            var expiresOnColumnName = DotEntityDb.Provider.SafeEnclose(nameof(StoreCredit.ExpiresOn));
            var lockedColumnName = DotEntityDb.Provider.SafeEnclose(nameof(StoreCredit.Locked));
            var query = $"SELECT SUM({creditColumnName}) FROM {tableName} WHERE {userIdColumnName}=@UserId AND {lockedColumnName}=@Locked AND {availableOnColumnName}<=@AvailableOn AND ({expiresOnColumnName} IS NULL OR {expiresOnColumnName}>@ExpiresOn)";
            using (var result = EntitySet.Query(query, new { UserId = userId, AvailableOn = DateTime.UtcNow, ExpiresOn = DateTime.UtcNow, Locked = false }))
            {
                return result.SelectScalerAs<decimal>();
            }
        }

        public void LockCredits(decimal balance, int userId, Transaction transaction = null)
        {
            Insert(new StoreCredit()
            {
                AvailableOn = DateTime.UtcNow,
                CreatedOn = DateTime.UtcNow,
                Credit = balance,
                Description = "Locked",
                Locked = true,
                UserId = userId
            }, transaction);
        }

        public void UnlockCredits(decimal balance, int userId, Transaction transaction = null)
        {
            Delete(x => x.Locked && x.UserId == userId && x.Credit == balance, transaction);
        }
    }
}