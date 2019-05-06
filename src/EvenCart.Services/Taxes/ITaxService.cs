using EvenCart.Core.Services;
using EvenCart.Data.Entity.Taxes;

namespace EvenCart.Services.Taxes
{
    public interface ITaxService : IFoundationEntityService<Tax>
    {
        Tax GetWithTaxRate(int taxId, int countryId, int stateProvinceId, string zipOrPostalCode);
    }
}