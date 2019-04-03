using System.Collections.Generic;
using RoastedMarketplace.Core.Services;
using RoastedMarketplace.Data.Entity.Gdpr;

namespace RoastedMarketplace.Services.Gdpr
{
    public interface IConsentService : IFoundationEntityService<Consent>
    {
        IEnumerable<Consent> GetConsents(out int totalResults, int consentGroupId, string searchText = null, int page = 1, int count = int.MaxValue);
    }
}