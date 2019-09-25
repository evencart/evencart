using System.Collections.Generic;
using EvenCart.Data.Entity.EntityProperties;

namespace EvenCart.Data.Interfaces
{
    public interface IHasEntityProperties
    {
        int Id { get; set; }

        IList<EntityProperty> EntityProperties { get; set; }
    }
}