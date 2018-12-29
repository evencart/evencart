using RoastedMarketplace.Core.Data;
using RoastedMarketplace.Core.Infrastructure;

namespace RoastedMarketplace.Core.Services.Events
{
    public class EventPublisherService : IEventPublisherService
    {
        public void Publish<T>(T entity, EventType eventType) where T : FoundationEntity
        {
            switch (eventType)
            {
                case EventType.Insert:

                    var iConsumers = DependencyResolver.ResolveMany<IFoundationEntityInserted<T>>();
                    //first find out all the consumers
                    foreach (var ec in iConsumers)
                    {
                        ec.OnInserted(entity);
                    }
                    break;
                case EventType.Update:
                    var uConsumers = DependencyResolver.ResolveMany<IFoundationEntityUpdated<T>>();
                    //first find out all the consumers
                    foreach (var ec in uConsumers)
                    {
                        ec.OnUpdated(entity);
                    }
                    break;
                case EventType.Delete:
                    var dConsumers = DependencyResolver.ResolveMany<IFoundationEntityDeleted<T>>();
                    //first find out all the consumers
                    foreach (var ec in dConsumers)
                    {
                        ec.OnDeleted(entity);
                    }
                    break;
            }
        }

        public T Filter<T>(T input)
        {
            var dFilters = DependencyResolver.ResolveMany<IFoundationFilter<T>>();
            foreach (var ec in dFilters)
            {
                input = ec.Filter(input);
            }
            return input;
        }
    }
}