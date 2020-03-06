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
using DotLiquid.NamingConventions;

namespace EvenCart.Infrastructure.ViewEngines.NamingConventions
{
    public class CamelCaseNamingConvention : INamingConvention
    {
        public string GetMemberName(string name)
        {
            return char.ToLowerInvariant(name[0]) + (name.Length > 1 ? name.Substring(1) : "");
        }

        public bool OperatorEquals(string testedOperator, string referenceOperator)
        {
            return GetMemberName(testedOperator).Equals(GetMemberName(referenceOperator));
        }

        public StringComparer StringComparer => StringComparer.OrdinalIgnoreCase;
    }
}