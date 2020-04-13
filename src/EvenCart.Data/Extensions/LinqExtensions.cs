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

namespace EvenCart.Data.Extensions
{
    public static class LinqExtensions
    {
        /// <summary>
        /// Returns last n elements from a sequence
        /// </summary>
        public static IEnumerable<T> TakeLast<T>(this IEnumerable<T> source, int n)
        {
            var enumerable = source as IList<T> ?? source.ToList();
            return enumerable.Skip(Math.Max(0, enumerable.Count() - n));
        }

        /// <summary>
        /// Returns last n elements from a sequence
        /// </summary>
        public static IEnumerable<T> TakeLast<T>(this IEnumerable<T> source, int n, int skip)
        {
            return source.TakeLast(n + skip).Take(n);
        }

        /// <summary>
        /// Returns specified number of elements after skipping previous pages
        /// </summary>
        public static IEnumerable<T> TakeFromPage<T>(this IEnumerable<T> source, int page, int count)
        {
            var enumerable = source as IList<T> ?? source.ToList();
            return enumerable.Skip((page - 1) * count).Take(count);
        }

        /// <summary>
        /// Returns elements from the last page considering specified count per page
        /// </summary>
        public static IEnumerable<T> TakeFromLastPage<T>(this IEnumerable<T> source, int countPerPage)
        {
            var enumerable = source as IList<T> ?? source.ToList();
            var totalPages = (int)Math.Ceiling((decimal)enumerable.Count() / countPerPage);
            return enumerable.TakeFromPage<T>(totalPages, countPerPage);

        }
        /// <summary>
        /// Selects many elements recursively
        /// </summary>
        public static IEnumerable<T> SelectManyRecursive<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>> selector)
        {
            var result = source.SelectMany(selector);
            if (!result.Any())
            {
                return source;
            }
            return source.Concat(result.SelectManyRecursive(selector));
        }

        public static void ForEach<T>(this ICollection<T> source, Action<T> action)
        {
            if (source == null || action == null)
                return;

            foreach (var t in source)
                action(t);

        }
    }
}