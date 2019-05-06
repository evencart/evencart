namespace EvenCart.Core.Services.Events
{
    public interface IFoundationFilter<T> : IFoundationEvent
    {
        T Filter(T entity);
    }
}