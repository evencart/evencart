using System.Collections.Generic;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Gdpr;

namespace EvenCart.Services.Gdpr
{
    public interface IConsentService : IFoundationEntityService<Consent>
    {
        IEnumerable<Consent> GetConsents(out int totalResults, int consentGroupId, string searchText = null, int page = 1, int count = int.MaxValue);
    }
}