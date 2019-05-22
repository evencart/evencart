#region Author Information
// DbLogger.cs
// 
// (c) 2016 Apexol Technologies. All Rights Reserved.
// 
#endregion

using System;
using EvenCart.Core;
using EvenCart.Core.Infrastructure;
using EvenCart.Data.Entity.Logs;
using EvenCart.Data.Entity.Users;
using EvenCart.Data.Enum;

namespace EvenCart.Services.Logger
{
    public class DbLogger : Logger
    {
        public override void Log<T>(LogLevel logLevel, string message, Exception exception = null, User user = null)
        {

            if (!ShouldLog(logLevel)) //can we log this message
                return;

            var logEntry = new Log() {
                CreatedOn = DateTime.UtcNow,
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