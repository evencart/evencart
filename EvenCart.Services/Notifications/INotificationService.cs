#region Author Information
// INotificationService.cs
// 
// (c) 2016 Apexol Technologies. All Rights Reserved.
// 
#endregion

using EvenCart.Core.Services;
using EvenCart.Data.Entity.Notifications;

namespace EvenCart.Services.Notifications
{
    public interface INotificationService : IFoundationEntityService<Notification>
    {
    }
}