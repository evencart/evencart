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
using System.Collections.Generic;
using EvenCart.Core.Infrastructure;
using EvenCart.Data.Constants;
using EvenCart.Data.Entity.Settings;
using EvenCart.Data.Entity.Users;
using EvenCart.Data.Enum;
using EvenCart.Services.Extensions;

namespace EvenCart.Services.Helpers
{
    public class DateTimeHelper
    {
        /// <summary>
        /// Gets a particular date in the target time zone
        /// </summary>
        /// <param name="sourceDate"></param>
        /// <param name="sourceTimeZoneInfo"></param>
        /// <param name="destinationTimeZoneInfo"></param>
        /// <returns></returns>
        public static DateTime GetDateInTimeZone(DateTime sourceDate, TimeZoneInfo sourceTimeZoneInfo, TimeZoneInfo destinationTimeZoneInfo)
        {
            return TimeZoneInfo.ConvertTime(sourceDate, sourceTimeZoneInfo, destinationTimeZoneInfo);
        }

        /// <summary>
        /// Gets a particular date in target timezone
        /// </summary>
        /// <param name="sourcedate"></param>
        /// <param name="destinationTimeZoneInfo"></param>
        /// <returns></returns>
        public static DateTime GetDateInTimeZone(DateTime sourcedate, TimeZoneInfo destinationTimeZoneInfo)
        {
            return TimeZoneInfo.ConvertTime(sourcedate, destinationTimeZoneInfo);
        }
        /// <summary>
        /// Gets a partiuclar date in UTC
        /// </summary>
        /// <param name="sourceDate"></param>
        /// <returns></returns>
        public static DateTime GetDateInUtc(DateTime sourceDate)
        {
            return sourceDate.ToUniversalTime();
        }

        public static DateTime GetDateInUserTimeZone(DateTime sourceDate, DateTimeKind sourceDateTimeKind, User user)
        {
            sourceDate = DateTime.SpecifyKind(sourceDate, sourceDateTimeKind);
            //get the timezone of mentioned user
            var userTimezoneId = user.GetPropertyValueAs<string>(PropertyNames.DefaultTimeZoneId);
            if (string.IsNullOrEmpty(userTimezoneId))
            {
                //get default timezone
                var generalSettings = DependencyResolver.Resolve<GeneralSettings>();
                userTimezoneId = generalSettings.DefaultTimeZoneId;
            }
            //let's find the timezone
            TimeZoneInfo timeZoneInfo;
            try
            {
                timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(userTimezoneId);
            }
            catch
            {
                //in case of error let's default to local timezone
                timeZoneInfo = TimeZoneInfo.Local;
            }
            //get the timezone
            return GetDateInTimeZone(sourceDate, timeZoneInfo);
        }
        /// <summary>
        /// Evaluates the datetime relative to the provided datetime according to the <see cref="TimeUnit"/> and TimeValue provided
        /// </summary>
        /// <param name="baseDateTime"></param>
        /// <param name="timeValue"></param>
        /// <param name="timeUnit"></param>
        /// <param name="futureDatetime">True if the datetime should be in the future or past from the base datetime</param>
        /// <returns>New datetime. or same if the caculation fails</returns>
        public static DateTime EvaluateDateTime(DateTime baseDateTime, int timeValue, TimeUnit timeUnit, bool futureDatetime = true)
        {
            if (!futureDatetime)
            {
                //negate for reverse calculation
                timeValue = -timeValue;
            }
            switch (timeUnit)
            {
                case TimeUnit.Seconds:
                    return baseDateTime.AddSeconds(timeValue);
                case TimeUnit.Minutes:
                    return baseDateTime.AddMinutes(timeValue);
                case TimeUnit.Hours:
                    return baseDateTime.AddHours(timeValue);
                case TimeUnit.Days:
                    return baseDateTime.AddDays(timeValue);
                case TimeUnit.Months:
                    return baseDateTime.AddMonths(timeValue);
                case TimeUnit.Years:
                    return baseDateTime.AddYears(timeValue);
                default:
                    return baseDateTime;
            }
        }

        /// <summary>
        /// Gets the specified time with units and value in number of seconds
        /// </summary>
        /// <param name="timeUnit"></param>
        /// <param name="timeValue"></param>
        /// <returns></returns>
        public static int GetTimeInSeconds(TimeUnit timeUnit, int timeValue)
        {
            switch (timeUnit)
            {
                case TimeUnit.Seconds:
                    return timeValue;
                case TimeUnit.Minutes:
                    return timeValue * 60;
                case TimeUnit.Hours:
                    return timeValue * 60 * 60;
                case TimeUnit.Days:
                    return timeValue * 60 * 60 * 24;
                case TimeUnit.Months:
                    return timeValue * 60 * 60 * 24 * 30;
                case TimeUnit.Years:
                    return timeValue * 60 * 60 * 24 * 30 * 12;
                default:
                    throw new ArgumentOutOfRangeException(nameof(timeUnit), timeUnit, null);
            }
        }
        /// <summary>
        /// Gets dates between two dates
        /// </summary>
        public static IEnumerable<DateTime> DatesBetween(DateTime start, DateTime end)
        {
            var step = start > end ? -1 : 1;
            for (var day = start.Date; day <= end; day = day.AddDays(step))
                yield return day;
        }

        /// <summary>
        /// Gets the relative date for a date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string GetRelativeDate(DateTime date)
        {
            var s = DateTime.UtcNow.Subtract(date);
            var dayDiff = (int)s.TotalDays;
            var secDiff = (int)s.TotalSeconds;
            if (dayDiff < 0 || dayDiff >= 31)
            {
                return date.ToString("D") + " at " + date.ToString("hh:mm tt");
            }
        
            if (dayDiff == 0)
            {
                // A.
                // Less than one minute ago.
                if (secDiff < 60)
                {
                    return "just now";
                }
                // B.
                // Less than 2 minutes ago.
                if (secDiff < 120)
                {
                    return "1 minute ago";
                }
                // C.
                // Less than one hour ago.
                if (secDiff < 3600)
                {
                    return $"{Math.Floor((double) secDiff / 60)} minutes ago";
                }
                // D.
                // Less than 2 hours ago.
                if (secDiff < 7200)
                {
                    return "1 hour ago";
                }
                // E.
                // Less than one day ago.
                if (secDiff < 86400)
                {
                    return $"{Math.Floor((double) secDiff / 3600)} hours ago";
                }
            }

            // Handle previous days.
            if (dayDiff == 1)
            {
                return "yesterday";
            }
            if (dayDiff < 7)
            {
                return $"{dayDiff} days ago";
            }
            if (dayDiff < 30)
            {
                return $"{Math.Ceiling((double) dayDiff / 7)} weeks ago";
            }
            if (dayDiff < 31)
            {
                return $"1 month ago";
            }
            if (dayDiff < 31)
            {
                return $"1 month ago";
            }

            return date.ToString("D") + " at " + date.ToString("hh:mm tt");
        }
    }
}
