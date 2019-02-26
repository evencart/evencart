using System.Collections.Generic;
using RoastedMarketplace.Core.Services;
using RoastedMarketplace.Data.Entity.Addresses;

namespace RoastedMarketplace.Services.Addresses
{
    public interface ICountryService : IFoundationEntityService<Country>
    {
        IEnumerable<Country> GetCountries(out int totalResults, string search = null, int page = 1, int count = int.MaxValue);
    }
}