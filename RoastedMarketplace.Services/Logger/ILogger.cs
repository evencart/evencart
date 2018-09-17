#region Author Information
// ILogger.cs
// 
// (c) 2016 Apexol Technologies. All Rights Reserved.
// 
#endregion

using System;
using RoastedMarketplace.Data.Entity.Users;
using RoastedMarketplace.Data.Enum;

namespace RoastedMarketplace.Services.Logger
{
    public interface ILogger
    {
        void Log<T>(LogLevel logLevel, string message, Exception exception = null, User user = null);
    }
}