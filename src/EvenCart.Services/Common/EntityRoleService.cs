using System.Collections.Generic;
using System.Linq;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Common;

namespace EvenCart.Services.Common
{
    public class EntityRoleService : FoundationEntityService<EntityRole>, IEntityRoleService
    {
        public void SetEntityRoles<T>(int id, IList<int> roleIds)
        {
            //get existing roles
            var entityName = typeof(T).Name;
            var savedEntityRoles = Get(x => x.EntityName == entityName && x.EntityId == id).ToList();
            var toDelete = savedEntityRoles.Where(x => !roleIds.Contains(x.RoleId));
            var toInsert = roleIds.Where(x => savedEntityRoles.All(y => y.RoleId != x)).Select(x => new EntityRole()
            {
                EntityId = id,
                EntityName = entityName,
                RoleId = x
            });
            Transaction.Initiate(transaction =>
            {
                foreach (var e in toDelete)
                    Delete(e, transaction);
                foreach (var e in toInsert)
                    Insert(e, transaction);
            });

        }

        public void ClearEntityRoles<T>(int id)
        {
            var entityName = typeof(T).Name;
            Delete(x => x.EntityName == entityName && x.EntityId == id);
        }
    }
}