using EvenCart.Areas.Administration.Models.Users;
using EvenCart.Data.Entity.Users;
using EvenCart.Infrastructure.Mvc.ModelFactories;

namespace EvenCart.Factories.Users
{
    public interface IRoleModelFactory : IModelFactory<Role, RoleModel>, IModelFactory<Capability, CapabilityModel>
    {
        
    }
}