using RoastedMarketplace.Core.Data;

namespace RoastedMarketplace.Core.Services.Events
{
    public interface IFoundationEntityUpdated<T> where T : FoundationEntity
    {
        void OnUpdated(T entity);
    }
}