using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DotEntity.Enumerations;
using RoastedMarketplace.Core.Services;
using RoastedMarketplace.Data.Entity.Gdpr;
using RoastedMarketplace.Data.Extensions;

namespace RoastedMarketplace.Services.Gdpr
{
    public class ConsentService : FoundationEntityService<Consent>, IConsentService
    {
        public override IEnumerable<Consent> Get(Expression<Func<Consent, bool>> @where, int page = 1, int count = Int32.MaxValue)
        {
            return Repository.Join<ConsentGroup>("ConsentGroupId", "Id", joinType: JoinType.LeftOuter)
                .Relate(RelationTypes.OneToOne<Consent, ConsentGroup>((consent, group) =>
                    {
                        group.Consents = group.Consents ?? new List<Consent>();
                        group.Consents.Add(consent);
                    }))
                .Where(where)
                .OrderBy(x => x.DisplayOrder)
                .SelectNested(page, count);}

        public override Consent Get(int id)
        {
            return Repository.Join<ConsentGroup>("ConsentGroupId", "Id", joinType: JoinType.LeftOuter)
                .Relate(RelationTypes.OneToOne<Consent, ConsentGroup>())
                .Where(x => x.Id == id)
                .SelectNested()
                .FirstOrDefault();

        }

        public IEnumerable<Consent> GetConsents(out int totalResults, int consentGroupId, string searchText = null, int page = 1, int count = Int32.MaxValue)
        {
            var query = Repository.Where(x => x.ConsentGroupId == consentGroupId).Join<ConsentGroup>("ConsentGroupId", "Id", joinType: JoinType.LeftOuter)
                .Relate(RelationTypes.OneToOne<Consent, ConsentGroup>());
            if (searchText.IsNullEmptyOrWhiteSpace())
                query = query.Where(x => x.Title.Contains(searchText));
            query = query.OrderBy(x => x.DisplayOrder);
            return query.SelectWithTotalMatches(out totalResults, page, count);
        }
    }
}