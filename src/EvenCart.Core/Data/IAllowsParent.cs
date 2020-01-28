using System.Collections.Generic;

namespace EvenCart.Core.Data
{
    public interface IAllowsParent<T> where T : FoundationEntity
    {
        int Id { get; set; }

        int ParentId { get; set; }

        T Parent { get; set; }

        IList<T> Children { get; set; }
    }
}