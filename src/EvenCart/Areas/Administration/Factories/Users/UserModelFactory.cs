using System.Linq;
using EvenCart.Areas.Administration.Models.Users;
using EvenCart.Data.Entity.Users;
using EvenCart.Infrastructure.Mvc.ModelFactories;

namespace EvenCart.Areas.Administration.Factories.Users
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

            var userModel = _modelMapper.Map<UserModel>(user);

            var roles = user.Roles;
            //set default role to registered
            if (user.Id == 0)
            {
                userModel.Roles = roles?.Where(x => x.SystemName == SystemRoleNames.Registered).Select(x => _modelMapper.Map<RoleModel>(x)).ToList();
            }
            else
            {
                userModel.Roles = user.Roles?.Select(x => _modelMapper.Map<RoleModel>(x)).ToList();
            }
            return userModel;
        }
    }
}