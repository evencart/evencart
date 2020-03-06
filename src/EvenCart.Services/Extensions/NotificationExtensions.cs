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
using EvenCart.Core.Infrastructure;
using EvenCart.Data.Entity.Notifications;
using EvenCart.Data.Enum;
using EvenCart.Services.Notifications;

namespace EvenCart.Services.Extensions
{
    public static class NotificationExtensions
    {
        public static Notification Notify<T>(this INotificationService notificationService, int userId, NotificationType notificationType, string eventName, T entity, DateTime publishDate) where T : FoundationEntity
        {
            var notificationEventService = DependencyResolver.Resolve<INotificationEventService>();
            var notificationEvent = notificationEventService.FirstOrDefault(x => x.EventName == eventName);
            if (notificationEvent != null && !notificationEvent.Enabled)
                return null;

            var notification = new Notification() {
                EntityId = entity.Id,
                EntityName = typeof(T).Name,
                IsRead = false,
                UserId = userId,
                PublishDateTime = publishDate,
                ReadDateTime = null,
                NotificationType = notificationType,
                NotificationEventId = notificationEvent?.Id
            };
            notificationService.Insert(notification);
            return notification;
        }

        public static Notification NotifyInformationNow<T>(this INotificationService notificationService, int userId, string eventName, T entity) where T : FoundationEntity
        {
            return notificationService.Notify<T>(userId, NotificationType.Information, eventName, entity, DateTime.UtcNow);
        }

        public static Notification NotifyErrorNow<T>(this INotificationService notificationService, int userId, string eventName, T entity) where T : FoundationEntity
        {
            return notificationService.Notify<T>(userId, NotificationType.Error, eventName, entity, DateTime.UtcNow);
        }

        public static Notification NotifyPromotionNow<T>(this INotificationService notificationService, int userId, string eventName, T entity) where T : FoundationEntity
        {
            return notificationService.Notify<T>(userId, NotificationType.Promotion, eventName, entity, DateTime.UtcNow);
        }

        public static void MarkRead(this INotificationService notificationService,  int notificationId)
        {
            var notification = notificationService.Get(notificationId);
            if (notification != null)
            {
                notification.IsRead = true;
                notification.ReadDateTime = DateTime.UtcNow;
                notificationService.Update(notification);
            }
        }
    }
}