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
using DotEntity;
using DotEntity.Enumerations;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Addresses;
using EvenCart.Data.Entity.Taxes;
using EvenCart.Data.Extensions;

namespace EvenCart.Services.Taxes
{
    public class TaxRateService : FoundationEntityService<TaxRate>, ITaxRateService
    {
        public override IEnumerable<TaxRate> Get(Expression<Func<TaxRate, bool>> @where, int page = 1, int count = Int32.MaxValue)
        {
            return Repository.Join<Country>("CountryId", "Id", joinType: JoinType.LeftOuter)
                .Join<StateOrProvince>("StateOrProvinceId", "Id", SourceColumn.Parent, JoinType.LeftOuter)
                .Relate(RelationTypes.OneToOne<TaxRate, Country>())
                .Relate(RelationTypes.OneToOne<TaxRate, StateOrProvince>())
                .Where(where)
                .OrderBy(x => x.Id)
                .SelectNested(page, count);
        }

        public override TaxRate Get(int id)
        {
            return Get(x => x.Id == id).FirstOrDefault();
        }
    }
}