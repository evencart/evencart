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