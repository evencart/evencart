#region Author Information
// NotificationEventType.cs
// 
// (c) 2016 Apexol Technologies. All Rights Reserved.
// 
#endregion

using System.Collections.Generic;
using RoastedMarketplace.Core.Data;

namespace RoastedMarketplace.Data.Entity.Notifications
{
    public class NotificationEvent : FoundationEntity
    {
        public string EventName { get; set; }

        public bool Enabled { get; set; }

        public virtual IList<Notification> Notifications { get; set; }
    }
}