using System;
using EvenCart.Data.Enum;

namespace EvenCart.Data.Extensions
{
    public static class InputTypeExtensions
    {
        public static bool RequireValues(this InputFieldType fieldType)
        {
            switch (fieldType)
            {
                case InputFieldType.Text:
                case InputFieldType.TextArea:
                case InputFieldType.Wyswyg:
                case InputFieldType.Number:
                case InputFieldType.Email:
                case InputFieldType.DateTime:
                case InputFieldType.ImageUpload:
                case InputFieldType.FileUpload:
                case InputFieldType.Hidden:
                    return false;
                case InputFieldType.CheckBox:
                case InputFieldType.RadioButton:
                case InputFieldType.Color:
                case InputFieldType.Dropdown:
                    return true;
                default:
                    return false;
            }
        }
    }
}