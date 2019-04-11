﻿#region Author Information
// DateTimeExtensions.cs
// 
// (c) 2016 Apexol Technologies. All Rights Reserved.
// 
#endregion

using System;
using EvenCart.Data.Entity.Users;

namespace EvenCart.Infrastructure.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime ToUserDateTime(this DateTime dateTime)
        {
            return dateTime.ToUserDateTime(null);
        }

        public static DateTime ToUserDateTime(this DateTime dateTime, User user)
        {
            return dateTime;
        }
    }
}