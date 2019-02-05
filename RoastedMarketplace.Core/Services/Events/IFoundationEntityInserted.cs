using RoastedMarketplace.Core.Data;

namespace RoastedMarketplace.Core.Services.Events
{
    public interface IFoundationEntityInserted<T> : IFoundationEvent where T : FoundationEntity
    {
        void OnInserted(T entity);
    }
}