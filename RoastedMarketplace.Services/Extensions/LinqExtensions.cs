#region Author Information
// LinqExtensions.cs
// 
// (c) 2016 Apexol Technologies. All Rights Reserved.
// 
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using RoastedMarketplace.Data.Enum;
using RoastedMarketplace.Services.Helpers;

namespace RoastedMarketplace.Services.Extensions
{
    public static class LinqExtensions
    {
        public static IEnumerable<T> OrderByTimeUnit<T>(this IEnumerable<T> source, Expression<Func<T, int>> timeValueExpression, Expression<Func<T, TimeUnit>> timeUnitExpression, bool descending = false)
        {
            var timeValuePropertyExpression = timeValueExpression.Body as MemberExpression;
            if (timeValuePropertyExpression == null)
                throw new ArgumentException($"Expression {timeValueExpression} refers a method, not a property");

            var timeUnitPropertyExpression = timeUnitExpression.Body as MemberExpression;
            if (timeUnitPropertyExpression == null)
                throw new ArgumentException($"Expression {timeUnitExpression} refers a method, not a property");

            var tType = typeof(T);
            var timeValueProperty = tType.GetProperty(timeValuePropertyExpression.Member.Name);
            var timeUnitProperty = tType.GetProperty(timeUnitPropertyExpression.Member.Name);

            return descending ?
                source.OrderByDescending(x => DateTimeHelper.GetTimeInSeconds((TimeUnit)timeUnitProperty.GetValue(x), (int)timeValueProperty.GetValue(x)))
                : source.OrderBy(x => DateTimeHelper.GetTimeInSeconds((TimeUnit) timeUnitProperty.GetValue(x), (int) timeValueProperty.GetValue(x)));
        }
    }
}