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

using System.Collections.Generic;
using System.Linq;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Common;
using EvenCart.Data.Extensions;

namespace EvenCart.Services.Common
{
    public class EntityTagService : FoundationEntityService<EntityTag>, IEntityTagService
    {
        public void SetEntityTags<T>(int entityId, params string[] tags)
        {
            var tagsList = tags.Distinct().ToList();
            //get existing roles
            var entityName = typeof(T).Name;
            var savedEntityTags = Get(x => x.EntityName == entityName && x.EntityId == entityId).ToList();
            var toDelete = savedEntityTags.Where(x => !tagsList.Contains(x.Tag));
            var toInsert = tagsList.Where(x => savedEntityTags.All(y => y.Tag != x)).Select(x => new EntityTag()
            {
                EntityId = entityId,
                EntityName = entityName,
                Tag = x
            });
            Transaction.Initiate(transaction =>
            {
                foreach (var e in toDelete)
                    Delete(e, transaction);
                foreach (var e in toInsert)
                    Insert(e, transaction);
            });
        }

        public IList<string> GetEntityTags<T>(int entityId)
        {
            var entityName = typeof(T).Name;
            return Get(x => x.EntityId == entityId && x.EntityName == entityName).Select(x => x.Tag).ToList();
        }

        public IList<string> GetDistinctTags(string startsWith = null)
        {
            var query = Repository;
            if (!startsWith.IsNullEmptyOrWhiteSpace())
                query = query.Where(x => x.Tag.StartsWith(startsWith));
            return query.Select().Select(x => x.Tag).Distinct().ToList();
        }
    }
}