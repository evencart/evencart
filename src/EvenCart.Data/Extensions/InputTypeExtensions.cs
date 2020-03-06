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