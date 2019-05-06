namespace EvenCart.Core.Services.Events
{
    public interface IEventCapture : IFoundationEvent
    {
        void Capture(string eventName, object[] eventData = null);

        string[] EventNames { get; }
    }
}