namespace RoastedMarketplace.Core.Services.Events
{
    public interface IFoundationFilter<T> : IFoundationEntityEvent
    {
        T Filter(T entity);
    }
}