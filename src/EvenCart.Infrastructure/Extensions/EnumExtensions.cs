using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EvenCart.Infrastructure.Extensions
{
    public static class EnumExtensions
    {
        public static IList<SelectListItem> GetSelectList(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            var selectList = new List<SelectListItem>();

            var checkedType = Nullable.GetUnderlyingType(type) ?? type;
            if (checkedType != type)
            {
                // Underlying type was non-null so handle Nullable<T>; ensure returned list has a spot for null
                selectList.Add(new SelectListItem { Text = string.Empty, Value = string.Empty, });
            }

            foreach (var field in checkedType.GetFields(BindingFlags.Public | System.Reflection.BindingFlags.Static))
            {
                // fieldValue will be an numeric type (byte, ...)
                var fieldValue = field.GetRawConstantValue();

                selectList.Add(new SelectListItem { Text = GetDisplayName(field), Value = fieldValue.ToString(), });
            }

            return selectList;
        }

        // Return non-empty name specified in a [Display] attribute for the given field, if any; field's name otherwise
        private static string GetDisplayName(MemberInfo field)
        {
            var display = field.GetCustomAttribute<DisplayAttribute>(inherit: false);
            var name = display?.GetName();
            return !string.IsNullOrEmpty(name) ? name : field.Name;
        }
    }
}