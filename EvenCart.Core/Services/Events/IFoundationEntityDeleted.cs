using EvenCart.Core.Data;

namespace EvenCart.Core.Services.Events
{
    public interface IFoundationEntityDeleted<T> : IFoundationEvent where T : FoundationEntity
    {
        void OnDeleted(T entity);
    }
}