using EvenCart.Data.Entity.Addresses;
using EvenCart.Data.Entity.Shop;

namespace EvenCart.Services.Taxes
{
    public interface ITaxAccountant
    {
        decimal GetFinalTaxRate(Product product, Address address, out string taxName);
    }
}