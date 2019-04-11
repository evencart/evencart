using EvenCart.Core.Data;

namespace EvenCart.Core.Services.Events
{
    public interface IEventPublisherService
    {
        /// <summary>
        /// Publishes a particular event
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="eventType"></param>
        void Publish<T>(T entity, EventType eventType) where T : FoundationEntity;

        /// <summary>
        /// Used to change state of an object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        T Filter<T>(T input);

        void Publish(string eventName, object[] eventData = null);
    }
}