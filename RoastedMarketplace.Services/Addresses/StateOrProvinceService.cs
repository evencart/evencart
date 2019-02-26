using System.Collections.Generic;
using RoastedMarketplace.Core.Extensions;
using RoastedMarketplace.Core.Services;
using RoastedMarketplace.Data.Entity.Addresses;

namespace RoastedMarketplace.Services.Addresses
{
    public class StateOrProvinceService : FoundationEntityService<StateOrProvince>, IStateOrProvinceService
    {
        public IEnumerable<StateOrProvince> GetStateOrProvinces(out int totalMatches, int countryId, string search = null, int page = 1, int count = 15)
        {
            var query = Repository.Where(x => x.CountryId == countryId);
            if (!search.IsNullEmptyOrWhitespace())
                query = query.Where(x => x.Name.StartsWith(search));
            return query.OrderBy(x => x.Name)
                .SelectWithTotalMatches(out totalMatches, page, count);

        }
    }
}