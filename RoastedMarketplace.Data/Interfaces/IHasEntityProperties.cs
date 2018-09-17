using RoastedMarketplace.Core.Data;

namespace RoastedMarketplace.Data.Interfaces
{
    public interface IHasEntityProperties<T> : IHasEntityProperties where T: FoundationEntity
    {
       
    }

    public interface IHasEntityProperties
    {
        int Id { get; set; }
    }
}