using RoastedMarketplace.Core.Services;
using RoastedMarketplace.Data.Entity.Taxes;

namespace RoastedMarketplace.Services.Taxes
{
    public interface ITaxService : IFoundationEntityService<Tax>
    {
        Tax GetWithTaxRate(int taxId, int countryId, int stateProvinceId, string zipOrPostalCode);
    }
}