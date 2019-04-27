using EvenCart.Core.Services;
using EvenCart.Data.Entity.Gdpr;

namespace EvenCart.Services.Gdpr
{
    public interface IConsentGroupService : IFoundationEntityService<ConsentGroup>
    {
        ConsentGroup GetWithConsents(int consentGroupId);
    }
}