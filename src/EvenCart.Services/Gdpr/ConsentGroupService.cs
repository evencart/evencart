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