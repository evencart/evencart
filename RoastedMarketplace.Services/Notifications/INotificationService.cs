#region Author Information
// INotificationService.cs
// 
// (c) 2016 Apexol Technologies. All Rights Reserved.
// 
#endregion

using RoastedMarketplace.Core.Services;
using RoastedMarketplace.Data.Entity.Notifications;

namespace RoastedMarketplace.Services.Notifications
{
    public interface INotificationService : IFoundationEntityService<Notification>
    {
    }
}