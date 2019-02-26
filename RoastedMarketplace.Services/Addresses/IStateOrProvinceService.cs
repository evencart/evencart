using System.Collections.Generic;
using RoastedMarketplace.Core.Services;
using RoastedMarketplace.Data.Entity.Addresses;

namespace RoastedMarketplace.Services.Addresses
{
    public interface IStateOrProvinceService : IFoundationEntityService<StateOrProvince>
    {
        IEnumerable<StateOrProvince> GetStateOrProvinces(out int totalMatches, int countryId, string search = null, int page = 1, int count = 15);
    }
}