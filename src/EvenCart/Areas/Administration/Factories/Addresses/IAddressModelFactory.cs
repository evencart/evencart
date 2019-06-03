using EvenCart.Areas.Administration.Models.Addresses;
using EvenCart.Areas.Administration.Models.Users;
using EvenCart.Data.Entity.Addresses;
using EvenCart.Infrastructure.Mvc.ModelFactories;

namespace EvenCart.Areas.Administration.Factories.Addresses
{
    public interface IAddressModelFactory : IModelFactory<Address, AddressModel>
    {
        
    }
}