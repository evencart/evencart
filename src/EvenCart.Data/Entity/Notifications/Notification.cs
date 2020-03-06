#region License
// Copyright (c) Sojatia Infocrafts Private Limited.
// The following code is part of EvenCart eCommerce Software (https://evencart.co) Dual Licensed under the terms of
// 
// 1. GNU GPLv3 with additional terms (available to read at https://evencart.co/license)
// 2. EvenCart Proprietary License (available to read at https://evencart.co/license/commercial-license).
// 
// You can select one of the above two licenses according to your requirements. The usage of this code is
// subject to the terms of the license chosen by you.
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