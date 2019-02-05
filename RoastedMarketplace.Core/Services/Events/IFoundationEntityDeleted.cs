using RoastedMarketplace.Core.Data;

namespace RoastedMarketplace.Core.Services.Events
{
    public interface IFoundationEntityDeleted<T> : IFoundationEvent where T : FoundationEntity
    {
        void OnDeleted(T entity);
    }
}