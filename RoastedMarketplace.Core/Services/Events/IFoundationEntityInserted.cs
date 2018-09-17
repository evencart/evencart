using RoastedMarketplace.Core.Data;

namespace RoastedMarketplace.Core.Services.Events
{
    public interface IFoundationEntityInserted<T> where T : FoundationEntity
    {
        void OnInserted(T entity);
    }
}