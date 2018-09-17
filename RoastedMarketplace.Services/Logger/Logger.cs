#region Author Information
// Logger.cs
// 
// (c) 2016 Apexol Technologies. All Rights Reserved.
// 
#endregion

using System;
using RoastedMarketplace.Core.Infrastructure;
using RoastedMarketplace.Data.Entity.Settings;
using RoastedMarketplace.Data.Entity.Users;
using RoastedMarketplace.Data.Enum;

namespace RoastedMarketplace.Services.Logger
{
    public abstract class Logger : ILogger
    {
        public abstract void Log<T>(LogLevel logLevel, string message, Exception exception = null, User user = null);

        public bool ShouldLog(LogLevel logLevel)
        {
            var systemSettings = DependencyResolver.Resolve<SystemSettings>();
            return systemSettings.MinimumLogLevel < logLevel && systemSettings.MinimumLogLevel != LogLevel.Trace;
        }
    }
}