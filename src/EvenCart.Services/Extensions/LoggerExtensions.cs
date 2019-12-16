#region Author Information
// LoggerExtensions.cs
// 
// (c) 2016 Apexol Technologies. All Rights Reserved.
// 
#endregion

using System;
using EvenCart.Data.Entity.Users;
using EvenCart.Data.Enum;
using EvenCart.Services.Logger;

namespace EvenCart.Services.Extensions
{
    public static class LoggerExtensions
    {
        private static void Log<T>(LogLevel level, ILogger logger, Exception ex, string message, User user = null,
            params object[] parameters)
        {
            try
            {
                logger.Log<T>(level, string.Format(message, parameters), ex, user);
            }
            catch (FormatException)
            {
                logger.Log<T>(level, message, ex, user);
            }
        }
        public static void LogTrace<T>(this ILogger logger, Exception ex, string message, User user = null, params object[] parameters)
        {
            Log<T>(LogLevel.Trace, logger, ex, message, user, parameters);
        }

        public static void LogInfo<T>(this ILogger logger, Exception ex, string message, User user = null, params object[] parameters)
        {
            Log<T>(LogLevel.Information, logger, ex, message, user, parameters);
        }

        public static void LogWarning<T>(this ILogger logger, Exception ex, string message, User user = null, params object[] parameters)
        {
            Log<T>(LogLevel.Warning, logger, ex, message, user, parameters);
        }

        public static void LogError<T>(this ILogger logger, Exception ex, string message, User user = null, params object[] parameters)
        {
            Log<T>(LogLevel.Error, logger, ex, message, user, parameters);
        }

        public static void LogFatal<T>(this ILogger logger, Exception ex, string message, User user = null, params object[] parameters)
        {
            Log<T>(LogLevel.Fatal, logger, ex, message, user, parameters);
        }

        public static void LogDebug<T>(this ILogger logger, Exception ex, string message, User user = null, params object[] parameters)
        {
            Log<T>(LogLevel.Debug, logger, ex, message, user, parameters);
        }
    }
}