using RoastedMarketplace.Data.Entity.Users;
using RoastedMarketplace.Infrastructure.Mvc.ModelFactories;
using RoastedMarketplace.Models.Users;

namespace RoastedMarketplace.Factories.Users
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
    }
}