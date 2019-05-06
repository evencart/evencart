#region Author Information
// Notification.cs
// 
// (c) 2016 Apexol Technologies. All Rights Reserved.
// 
#endregion

using System;
using EvenCart.Core.Data;
using EvenCart.Data.Entity.Users;
using EvenCart.Data.Enum;

namespace EvenCart.Data.Entity.Notifications
{
    public class Notification : FoundationEntity
    {
        public int UserId { get; set; }

        public virtual User User { get; set; }

        public NotificationType NotificationType { get; set; }

        public bool IsRead { get; set; }

        public DateTime PublishDateTime { get; set; }

        public DateTime? ReadDateTime { get; set; }

        public int EntityId { get; set; }

        public string EntityName { get; set; }

        public int? NotificationEventId { get; set; }

        public virtual NotificationEvent NotificationEvent { get; set; }

        public int InitiatorId { get; set; }

        public string InitiatorName { get; set; }
    }

}