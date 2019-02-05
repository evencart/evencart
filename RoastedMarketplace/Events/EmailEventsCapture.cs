using RoastedMarketplace.Core.Services.Events;
using RoastedMarketplace.Data.Entity.Purchases;
using RoastedMarketplace.Data.Entity.Users;
using RoastedMarketplace.Services.Emails;

namespace RoastedMarketplace.Events
{
    public class EmailEventsCapture : IEventCapture
    {
        private readonly IEmailSender _emailSender;
        public EmailEventsCapture(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public void Capture(string eventName, object[] eventData = null)
        {
            if (eventData == null)
                return;
            switch (eventName)
            {
                case nameof(NamedEvent.UserRegistered):
                    _emailSender.SendUserRegistered((User) eventData[0]);
                    break;
                case nameof(NamedEvent.UserActivated):
                    _emailSender.SendUserActivated((User) eventData[0]);
                    break;
                case nameof(NamedEvent.OrderPlaced):
                    _emailSender.SendOrderPlaced((User) eventData[0], (Order) eventData[1]);
                    break;
            }
        }

        public string[] EventNames { get; } =
        {
            NamedEvent.UserRegistered.ToString(),
            NamedEvent.OrderPaid.ToString(),
            NamedEvent.OrderPlaced.ToString(),
            NamedEvent.ShipmentShipped.ToString(),
            NamedEvent.ShipmentDelivered.ToString(),
        };
    }
}