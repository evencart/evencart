using EvenCart.Data.Entity.Users;
using EvenCart.Infrastructure.Mvc.ModelFactories;
using EvenCart.Models.Users;

namespace EvenCart.Factories.Users
{
    public interface IUserModelFactory : IModelFactory<User, UserModel>
    {
        
    }
}