using EvenCart.Core.Services;
using EvenCart.Data.Entity.Addresses;

namespace EvenCart.Services.Addresses
{
    public interface IAddressService : IFoundationEntityService<Address>
    {
        Address Get<T>(int id);
    }
}