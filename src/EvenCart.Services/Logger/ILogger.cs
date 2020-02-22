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