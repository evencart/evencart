#region Author Information
// NotificationExtensions.cs
// 
// (c) 2016 Apexol Technologies. All Rights Reserved.
// 
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