using EvenCart.Core.Services;
using EvenCart.Data.Entity.Users;

namespace EvenCart.Services.Users
{
    public interface IStoreCreditService : IFoundationEntityService<StoreCredit>
    {
        decimal GetBalance(int userId);

        void LockCredits(decimal balance, int userId, Transaction transaction = null);

        void UnlockCredits(decimal balance, int userId, Transaction transaction = null);
    }
}