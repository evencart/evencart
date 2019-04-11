#region Author Information
// ILogger.cs
// 
// (c) 2016 Apexol Technologies. All Rights Reserved.
// 
#endregion

using System;
using EvenCart.Data.Entity.Users;
using EvenCart.Data.Enum;

namespace EvenCart.Services.Logger
{
    public interface ILogger
    {
        void Log<T>(LogLevel logLevel, string message, Exception exception = null, User user = null);
    }
}