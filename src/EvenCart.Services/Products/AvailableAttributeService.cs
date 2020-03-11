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
using DotEntity.Enumerations;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Shop;
using EvenCart.Data.Extensions;

namespace EvenCart.Services.Products
{
    public class AvailableAttributeService : FoundationEntityService<AvailableAttribute>, IAvailableAttributeService
    {
        public IEnumerable<AvailableAttribute> GetAvailableAttributes(out int totalResults, string searchText = null, int page = 1, int count = Int32.MaxValue)
        {
            var query = Repository;
            if (!searchText.IsNullEmptyOrWhiteSpace())
                query = query.Where(x => x.Name.Contains(searchText));

            return query.Join<AvailableAttributeValue>("Id", "AvailableAttributeId", joinType: JoinType.LeftOuter)
                .Relate(RelationTypes.OneToMany<AvailableAttribute, AvailableAttributeValue>())
                .OrderBy(x => x.Name)
                .SelectNestedWithTotalMatches(out totalResults, page, count);
        }

        public override AvailableAttribute Get(int id)
        {
            return Repository.Join<AvailableAttributeValue>("Id", "AvailableAttributeId", joinType: JoinType.LeftOuter)
                .Relate(RelationTypes.OneToMany<AvailableAttribute, AvailableAttributeValue>())
                .Where(x => x.Id == id)
                .SelectNested()
                .FirstOrDefault();
        }
    }
}