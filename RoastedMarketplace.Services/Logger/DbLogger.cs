#region Author Information
// DbLogger.cs
// 
// (c) 2016 Apexol Technologies. All Rights Reserved.
// 
#endregion

using System;
using RoastedMarketplace.Core;
using RoastedMarketplace.Core.Infrastructure;
using RoastedMarketplace.Data.Entity.Logs;
using RoastedMarketplace.Data.Entity.Users;
using RoastedMarketplace.Data.Enum;

namespace RoastedMarketplace.Services.Logger
{
    public class DbLogger : Logger
    {
        public override void Log<T>(LogLevel logLevel, string message, Exception exception = null, User user = null)
        {

            if (!ShouldLog(logLevel)) //can we log this message
                return;

            var logEntry = new Log() {
                DateCreated = DateTime.UtcNow,
                IpAddress = WebHelper.GetClientIpAddress(),
                LogLevel = logLevel,
                ShortMessage = $"{typeof(T).Name} {message}",
                Details = exception?.ToString(),
                Url = WebHelper.GetCurrentUrl(),
                ReferralUrl = WebHelper.GetReferrerUrl()
            };
            var loggerEntityService = DependencyResolver.Resolve<ILoggerEntityService>();
            loggerEntityService.Insert(logEntry);
        }
    }
}