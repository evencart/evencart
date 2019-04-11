#region Author Information
// Logger.cs
// 
// (c) 2016 Apexol Technologies. All Rights Reserved.
// 
#endregion

using System;
using EvenCart.Core.Infrastructure;
using EvenCart.Data.Entity.Settings;
using EvenCart.Data.Entity.Users;
using EvenCart.Data.Enum;

namespace EvenCart.Services.Logger
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