using System.Collections.Generic;
using EvenCart.Data.Enum;

namespace EvenCart.Services.Helpers
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