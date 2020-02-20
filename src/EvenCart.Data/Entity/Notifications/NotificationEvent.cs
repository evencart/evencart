using System.Collections.Generic;
using EvenCart.Core.Data;

namespace EvenCart.Data.Entity.Notifications
{
    public class NotificationEvent : FoundationEntity
    {
        public string EventName { get; set; }

        public bool Enabled { get; set; }

        public virtual IList<Notification> Notifications { get; set; }
    }
}