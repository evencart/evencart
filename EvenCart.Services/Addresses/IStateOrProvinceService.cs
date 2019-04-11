using System.Collections.Generic;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Addresses;

namespace EvenCart.Services.Addresses
{
    public interface IStateOrProvinceService : IFoundationEntityService<StateOrProvince>
    {
        IEnumerable<StateOrProvince> GetStateOrProvinces(out int totalMatches, int countryId, string search = null, int page = 1, int count = 15);
    }
}