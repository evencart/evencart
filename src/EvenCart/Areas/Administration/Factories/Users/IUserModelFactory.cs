using EvenCart.Areas.Administration.Models.Users;
using EvenCart.Data.Entity.Users;
using EvenCart.Infrastructure.Mvc.ModelFactories;

namespace EvenCart.Areas.Administration.Factories.Users
{
    public interface IUserModelFactory : IModelFactory<User, UserModel>
    {
        UserMiniModel CreateMini(User user);
    }
}