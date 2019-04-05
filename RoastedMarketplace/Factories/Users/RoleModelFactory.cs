using RoastedMarketplace.Areas.Administration.Models.Users;
using RoastedMarketplace.Data.Entity.Users;
using RoastedMarketplace.Infrastructure.Mvc.ModelFactories;

namespace RoastedMarketplace.Factories.Users
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