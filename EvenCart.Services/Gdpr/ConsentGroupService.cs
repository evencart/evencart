using System;
using System.Linq;
using System.Linq.Expressions;
using DotEntity.Enumerations;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Gdpr;
using EvenCart.Data.Extensions;

namespace EvenCart.Services.Gdpr
{
    public class ConsentGroupService : FoundationEntityService<ConsentGroup>, IConsentGroupService
    {
        public ConsentGroup GetWithConsents(int consentGroupId)
        {
            Expression<Func<Consent, object>> orderBy = consent => consent.DisplayOrder;
            var consentGroup = Repository.Join<Consent>("Id", "ConsentGroupId")
                .Relate(RelationTypes.OneToMany<ConsentGroup, Consent>())
                .Where(x => x.Id == consentGroupId)
                .OrderBy(orderBy, RowOrder.Ascending)
                .SelectNested()
                .FirstOrDefault();
            return consentGroup;
        }
    }
}