using System.Linq;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Gdpr;
using EvenCart.Data.Extensions;

namespace EvenCart.Services.Gdpr
{
    public class ConsentGroupService : FoundationEntityService<ConsentGroup>, IConsentGroupService
    {
        public ConsentGroup GetWithConsents(int consentGroupId)
        {
            return Repository.Join<Consent>("Id", "ConsentGroupId")
                .Relate(RelationTypes.OneToMany<ConsentGroup, Consent>())
                .Where(x => x.Id == consentGroupId)
                .SelectNested()
                .FirstOrDefault();
        }
    }
}