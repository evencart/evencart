using System;
using DotLiquid.NamingConventions;

namespace RoastedMarketplace.Infrastructure.ViewEngines.NamingConventions
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