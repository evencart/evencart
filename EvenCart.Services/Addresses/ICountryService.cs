using System.Collections.Generic;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Addresses;

namespace EvenCart.Services.Addresses
{
    public interface ICountryService : IFoundationEntityService<Country>
    {
        IEnumerable<Country> GetCountries(out int totalResults, string search = null, int page = 1, int count = int.MaxValue);
    }
}