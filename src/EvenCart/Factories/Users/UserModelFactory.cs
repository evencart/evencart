using EvenCart.Data.Entity.Users;
using EvenCart.Infrastructure.Mvc.ModelFactories;
using EvenCart.Models.Users;

namespace EvenCart.Factories.Users
{
    public class UserModelFactory : IUserModelFactory
    {
        private readonly IModelMapper _modelMapper;
        public UserModelFactory(IModelMapper modelMapper)
        {
            _modelMapper = modelMapper;
        }

        public UserModel Create(User user)
        {
            return _modelMapper.Map<UserModel>(user);
        }

        public UserMiniModel CreateMini(User user)
        {
            return new UserMiniModel()
            {
                Name = user.Name,
                Id = user.Id
            };
        }
    }
}