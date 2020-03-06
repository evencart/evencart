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

namespace EvenCart.Infrastructure.Mvc.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class FormatAsCurrenciesAttribute : Attribute
    {
        public string[] PropertyNames { get; set; }

        public string CurrencyCodeProperty { get; set; }

        public FormatAsCurrenciesAttribute(params string[] propertyNames)
        {
            if (PropertyNames?.Length == 0)
                throw new ArgumentException("At least one property must be passed for formatting");
            PropertyNames = propertyNames;
        }

    }
}