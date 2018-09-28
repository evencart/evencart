using System.Collections.Generic;
using RoastedMarketplace.Data.Enum;

namespace RoastedMarketplace.Services.Helpers
{
    public static class VariantHelper
    {
        public static IList<InputFieldType> GetVariantSpecificFieldTypes()
        {
            return new List<InputFieldType>()
            {
                InputFieldType.CheckBox,
                InputFieldType.Color,
                InputFieldType.Dropdown,
                InputFieldType.RadioButton
            };
        }
    }
}