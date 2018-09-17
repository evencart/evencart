using RoastedMarketplace.Core.Data;

namespace RoastedMarketplace.Data.Interfaces
{
    public interface IPicturesSupported<T> where T: FoundationEntity
    {
        int Id { get; set; }
    }
}