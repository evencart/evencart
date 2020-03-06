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
using System.Linq;
using System.Linq.Expressions;
using EvenCart.Data.Enum;
using EvenCart.Services.Helpers;

namespace EvenCart.Services.Extensions
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