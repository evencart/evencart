using System.Collections.Generic;
using EvenCart.Data.Entity.Gdpr;

namespace EvenCart.Services.Gdpr
{
    public interface IGdprService
    {
        void SetUserConsents(int userId, Dictionary<int, ConsentStatus> consentPairs);

        IList<UserConsent> GetUserConsents(int userId);

        IList<Consent> GetPendingConsents(int userId);

        bool IsConsentAccepted(int userId, int consentId);
    }
}