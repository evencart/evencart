#region Author Information
// DateTimeExtensions.cs
// 
// (c) 2016 Apexol Technologies. All Rights Reserved.
// 
#endregion

using System;
using System.Globalization;
using EvenCart.Core.Infrastructure;
using EvenCart.Data.Entity.Users;
using EvenCart.Services.Formatter;

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

        public static string GetMonthName(this DateTime dateTime)
        {
            return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(dateTime.Month);
        }

        public static void GetWeekRangeDates(this DateTime dateTime, out DateTime startDate, out DateTime endDate)
        {
            //we assume week starts on Mondays.
            //todo: make this configurable
            var dayNum = (int) dateTime.DayOfWeek;
            if (dayNum == 0)
            {
                startDate = dateTime.AddDays(-6);
                endDate = dateTime;
            }
            else
            {
                var dayToSubtract = dayNum - 1;
                startDate = dateTime.AddDays(-dayToSubtract);
                endDate = startDate.AddDays(6);
            }
        }

        public static string ToFormattedString(this DateTime dateTime, bool onlyDatePart = true)
        {
            if (onlyDatePart)
                dateTime = dateTime.Date;
            var formatterService = DependencyResolver.Resolve<IFormatterService>();
            return formatterService.FormatDateTime(dateTime, ApplicationEngine.CurrentLanguageCultureCode, onlyDatePart);
        }
    }
}