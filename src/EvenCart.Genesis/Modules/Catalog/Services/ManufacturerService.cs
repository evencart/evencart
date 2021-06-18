﻿#region License
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
using Genesis.Services;
using EvenCart.Data.Entity.Shop;
using Genesis.Extensions;

namespace EvenCart.Services.Products
{
    public class ManufacturerService : GenesisEntityService<Manufacturer>, IManufacturerService
    {
        public IEnumerable<Manufacturer> SearchManufacturers(out int totalResults, string searchText, int page = 1, int count = Int32.MaxValue)
        {
            var query = Repository;
            if (searchText.IsNullEmptyOrWhiteSpace())
                query = query.Where(x => x.Name.Contains(searchText));
            query = query.OrderBy(x => x.Name);
            return query.SelectWithTotalMatches(out totalResults, page, count);
        }
    }
}