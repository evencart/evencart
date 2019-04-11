using EvenCart.Core.Data;

namespace EvenCart.Data.Interfaces
{
    public interface IHasEntityProperties<T> : IHasEntityProperties where T: FoundationEntity
    {
       
    }

    public interface IHasEntityProperties
    {
        int Id { get; set; }
    }
}