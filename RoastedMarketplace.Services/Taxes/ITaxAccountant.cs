using RoastedMarketplace.Data.Entity.Addresses;
using RoastedMarketplace.Data.Entity.Shop;

namespace RoastedMarketplace.Services.Taxes
{
    public interface ITaxAccountant
    {
        decimal GetFinalTaxRate(Product product, Address address);
    }
}