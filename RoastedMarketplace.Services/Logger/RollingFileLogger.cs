#region Author Information
// FileLogger.cs
// 
// (c) 2016 Apexol Technologies. All Rights Reserved.
// 
#endregion

using System;
using System.IO;
using RoastedMarketplace.Core;
using RoastedMarketplace.Data.Database;
using RoastedMarketplace.Data.Entity.Users;
using RoastedMarketplace.Data.Enum;

namespace RoastedMarketplace.Services.Logger
{
    public class RollingFileLogger : Logger
    {
        const string LogFileNameFormat = "~/Logs/log-{0}.txt";
        const string LogEntryFormat = "[{0}] {1} {2} from IP {3} and user {4} message: {5} exception: {6}";

        public override void Log<T>(LogLevel logLevel, string message, Exception exception = null, User user = null)
        {
            using (new OfflineDatabaseContextProvider())
            {
                if (!ShouldLog(logLevel))
                    return;
                var fileName = string.Format(LogFileNameFormat, DateTime.Today.ToString("yyyy-MM-dd"));
                using (var sr = new StreamWriter(fileName))
                {
                    var entry = string.Format(LogEntryFormat,
                        DateTime.Now.ToString("hh:mm:ss tt"),
                        logLevel.ToString().ToUpper(),
                        typeof(T).Name,
                        WebHelper.GetClientIpAddress(),
                        user?.Name + "#" + user?.Id,
                        message,
                        exception);
                    sr.Write(entry);
                    sr.WriteLine();
                }
            }
        }

    }
}
