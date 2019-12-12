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