using System.Collections.Generic;
using RoastedMarketplace.Data.Entity.Gdpr;
using RoastedMarketplace.Infrastructure.Mvc.ModelFactories;
using RoastedMarketplace.Models.Gdpr;

namespace RoastedMarketplace.Factories.Gdpr
{
    public interface IGdprModelFactory : IModelFactory<Consent, ConsentModel>, IModelFactory<ConsentGroup, ConsentGroupModel>
    {
        ConsentModel Create(Consent entity, ConsentStatus consentStatus);

        ConsentGroupModel Create(ConsentGroup consentGroup, int[] acceptedConsentIds, int[] deniedConsentIds);
    }
}