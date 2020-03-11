#region License
// Copyright (c) Sojatia Infocrafts Private Limited.
// The following code is part of EvenCart eCommerce Software (https://evencart.co) Dual Licensed under the terms of
// 
// 1. GNU GPLv3 with additional terms (available to read at https://evencart.co/license)
// 2. EvenCart Proprietary License (available to read at https://evencart.co/license/commercial-license).
// 
// You can select one of the above two licenses according to your requirements. The usage of this code is
// subject to the terms of the license chosen by you.
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DotEntity.Enumerations;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Gdpr;
using EvenCart.Data.Extensions;

namespace EvenCart.Services.Gdpr
{
    public class ConsentService : FoundationEntityService<Consent>, IConsentService
    {
        public override IEnumerable<Consent> Get(Expression<Func<Consent, bool>> @where, int page = 1, int count = Int32.MaxValue)
        {
            Expression<Func<ConsentGroup, object>> orderByConsentGroup = x => x.DisplayOrder;
            return Repository.Join<ConsentGroup>("ConsentGroupId", "Id", joinType: JoinType.LeftOuter)
                .Relate(RelationTypes.OneToOne<Consent, ConsentGroup>((consent, group) =>
                    {
                        group.Consents = group.Consents ?? new List<Consent>();
                        group.Consents.Add(consent);
                    }))
                .Where(where)
                .OrderBy(x => x.DisplayOrder)
                .OrderBy(orderByConsentGroup)
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