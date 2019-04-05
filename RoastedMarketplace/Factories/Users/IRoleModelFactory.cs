using RoastedMarketplace.Areas.Administration.Models.Users;
using RoastedMarketplace.Data.Entity.Users;
using RoastedMarketplace.Infrastructure.Mvc.ModelFactories;

namespace RoastedMarketplace.Factories.Users
{
    public interface IRoleModelFactory : IModelFactory<Role, RoleModel>, IModelFactory<Capability, CapabilityModel>
    {
        
    }
}