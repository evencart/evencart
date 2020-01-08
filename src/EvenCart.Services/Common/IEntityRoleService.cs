using System.Collections.Generic;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Common;

namespace EvenCart.Services.Common
{
    public interface IEntityRoleService : IFoundationEntityService<EntityRole>
    {
        void SetEntityRoles<T>(int id, IList<int> roleIds);

        void ClearEntityRoles<T>(int id);
    }
}