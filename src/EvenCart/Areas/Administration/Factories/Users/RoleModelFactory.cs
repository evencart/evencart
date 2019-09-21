using EvenCart.Areas.Administration.Models.Users;
using EvenCart.Data.Entity.Users;
using EvenCart.Infrastructure.Mvc.ModelFactories;

namespace EvenCart.Areas.Administration.Factories.Users
{
    public class RoleModelFactory : IRoleModelFactory
    {
        private readonly IModelMapper _modelMapper;
        public RoleModelFactory(IModelMapper modelMapper)
        {
            _modelMapper = modelMapper;
        }

        public RoleModel Create(Role entity)
        {
            return _modelMapper.Map<RoleModel>(entity);
        }

        public CapabilityModel Create(Capability entity)
        {
            return _modelMapper.Map<CapabilityModel>(entity);
        }
    }
}