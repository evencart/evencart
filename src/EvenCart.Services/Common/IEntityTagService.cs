using System.Collections.Generic;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Common;

namespace EvenCart.Services.Common
{
    public interface IEntityTagService : IFoundationEntityService<EntityTag>
    {
        void SetEntityTags<T>(int entityId, params string[] tags);

        IList<string> GetEntityTags<T>(int entityId);

        IList<string> GetDistinctTags(string startsWith = null);
    }
}