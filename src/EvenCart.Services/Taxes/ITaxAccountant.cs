using System.Collections.Generic;
using EvenCart.Data.Entity.Addresses;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Data.Entity.Shop;
using EvenCart.Data.Entity.Taxes;

namespace EvenCart.Services.Taxes
{
    public interface ITaxAccountant
    {
        decimal GetFinalTaxRate(Product product, Address address, out string taxName);
    }
}