using System;

namespace RoastedMarketplace.Data.Attributes
{
    /// <summary>
    /// Specifies a field that can be used for token replacements processing
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class NonTokenFieldAttribute : Attribute
    {
    }
}
