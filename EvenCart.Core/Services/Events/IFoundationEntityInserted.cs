using EvenCart.Core.Data;

namespace EvenCart.Core.Services.Events
{
    public interface IFoundationEntityInserted<T> : IFoundationEvent where T : FoundationEntity
    {
        void OnInserted(T entity);
    }
}