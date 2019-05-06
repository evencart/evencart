using EvenCart.Data.Entity.Gdpr;
using EvenCart.Infrastructure.Mvc.ModelFactories;
using EvenCart.Models.Gdpr;

namespace EvenCart.Factories.Gdpr
{
    public interface IGdprModelFactory : IModelFactory<Consent, ConsentModel>, IModelFactory<ConsentGroup, ConsentGroupModel>
    {
        ConsentModel Create(Consent entity, ConsentStatus consentStatus);

        ConsentGroupModel Create(ConsentGroup consentGroup, int[] acceptedConsentIds, int[] deniedConsentIds);
    }
}