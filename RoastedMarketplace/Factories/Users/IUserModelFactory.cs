using RoastedMarketplace.Data.Entity.Users;
using RoastedMarketplace.Infrastructure.Mvc.ModelFactories;
using RoastedMarketplace.Models.Users;

namespace RoastedMarketplace.Factories.Users
{
    public interface IUserModelFactory : IModelFactory<User, UserModel>
    {
        
    }
}