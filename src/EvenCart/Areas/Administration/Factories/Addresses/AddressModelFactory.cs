using EvenCart.Areas.Administration.Models.Addresses;
using EvenCart.Data.Entity.Addresses;
using EvenCart.Infrastructure.Mvc.ModelFactories;

namespace EvenCart.Areas.Administration.Factories.Addresses
{
    public class AddressModelFactory : IAddressModelFactory
    {
        private readonly IModelMapper _modelMapper;

        public AddressModelFactory(IModelMapper modelMapper)
        {
            _modelMapper = modelMapper;
        }

        public AddressModel Create(Address address)
        {
            var addressModel = _modelMapper.Map<AddressModel>(address);
            addressModel.CountryName = address.Country?.Name;
            return addressModel;
        }
    }
}