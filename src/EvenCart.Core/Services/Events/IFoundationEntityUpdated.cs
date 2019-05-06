using EvenCart.Core.Data;

namespace EvenCart.Core.Services.Events
{
    public interface IFoundationEntityUpdated<T> : IFoundationEvent where T : FoundationEntity
    {
        void OnUpdated(T entity);
    }
}