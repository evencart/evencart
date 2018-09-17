using System;
using RoastedMarketplace.Core.Infrastructure;
using RoastedMarketplace.Data.Constants;
using RoastedMarketplace.Data.Entity.Settings;
using RoastedMarketplace.Data.Entity.Users;
using RoastedMarketplace.Data.Enum;
using RoastedMarketplace.Services.Extensions;

namespace RoastedMarketplace.Services.Helpers
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
    }
}