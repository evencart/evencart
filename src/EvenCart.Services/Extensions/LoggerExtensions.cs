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