// #region Author Information
// // ExtraFieldExtensions.cs
// // 
// // (c) Apexol Technologies. All Rights Reserved.
// // 
// #endregion

using System.Collections.Generic;
using System.Linq;
using RoastedMarketplace.Core.Data;
using RoastedMarketplace.Core.DataStructures;
using RoastedMarketplace.Core.Infrastructure;
using RoastedMarketplace.Data.Entity.ExtraFields;
using RoastedMarketplace.Data.Entity.Users;
using RoastedMarketplace.Data.Enum;
using RoastedMarketplace.Data.Extensions;
using RoastedMarketplace.Data.Interfaces;
using RoastedMarketplace.Services.Enum;
using RoastedMarketplace.Services.ExtraFields;

namespace RoastedMarketplace.Services.Extensions
{
    public static class ExtraFieldExtensions
    {
        public const string ExtraFieldsRequestKey = "e";

        public static string ExtraFieldNameFormat = $"{ExtraFieldsRequestKey}_{0}";

        public static ExtraFieldValidationResult ValidateValueForUser(this ExtraField extraField, string fieldValue, User user)
        {
            var required = (user.IsRegistered() && extraField.RequiredForUsers) ||
                           (user.IsAgent() && extraField.RequiredForAgents);

            if (required && fieldValue.IsNullEmptyOrWhiteSpace())
                return ExtraFieldValidationResult.EmptyValueForRequiredField;

            var visible = (user.IsRegistered() && extraField.VisibleToUsers) ||
                           (user.IsAgent() && extraField.VisibleToAgents);

            if (user.IsRegistered() && (!extraField.IsUserEditable || !visible))
                return ExtraFieldValidationResult.NonEditableField;

            switch (extraField.FieldType)
            {
                case InputFieldType.Number:
                    if(!fieldValue.IsNumeric())
                        return ExtraFieldValidationResult.InvalidValueForFieldType;

                    if (extraField.MinimumValue.IsNumeric())
                    {
                        if (extraField.MinimumValue.GetInteger(false) > fieldValue.GetInteger(false))
                        {
                            return ExtraFieldValidationResult.OutOfRangeValue;
                        }
                    }
                    if (extraField.MaximumValue.IsNumeric())
                    {
                        if (extraField.MaximumValue.GetInteger(false) < fieldValue.GetInteger(false))
                        {
                            return ExtraFieldValidationResult.OutOfRangeValue;
                        }
                    }
                    break;
                case InputFieldType.Email:
                    if(!fieldValue.IsValidEmail())
                        return ExtraFieldValidationResult.InvalidValueForFieldType;
                    break;
                case InputFieldType.DateTime:
                    if (!fieldValue.IsDateTime())
                        return ExtraFieldValidationResult.InvalidValueForFieldType;

                    if (extraField.MinimumValue.IsDateTime())
                    {
                        if (extraField.MinimumValue.GetDateTime(false) > fieldValue.GetDateTime(false))
                        {
                            return ExtraFieldValidationResult.OutOfRangeValue;
                        }
                    }
                    if (extraField.MaximumValue.IsDateTime())
                    {
                        if (extraField.MaximumValue.GetDateTime(false) < fieldValue.GetDateTime(false))
                        {
                            return ExtraFieldValidationResult.OutOfRangeValue;
                        }
                    }
                    break;
                case InputFieldType.Color:
                    if(!fieldValue.IsColor())
                        return ExtraFieldValidationResult.InvalidValueForFieldType;
                    break;
                case InputFieldType.Captcha:
                    break;
                case InputFieldType.Dropdown:
                    break;
                case InputFieldType.NestedDropdown:
                    break;
                case InputFieldType.ImageUpload:
                    break;
                case InputFieldType.FileUpload:
                    break;
                case InputFieldType.Duration:
                    break;
                default:
                    return ExtraFieldValidationResult.UnknownError;
            }

            return ExtraFieldValidationResult.ValidField;
        }

        public static string GetDbFieldName(this ExtraField extraField)
        {
            return string.Format(ExtraFieldNameFormat, extraField.Id);
        }

        public static IList<Pair<ExtraField, string>> GetExtraFields<T>(this IHasEntityProperties<T> entity) where T : FoundationEntity
        {
            var entityProperties = entity.GetProperties();
            var extraFieldService = DependencyResolver.Resolve<IExtraFieldService>();
            var entityName = typeof(T).Name;
            var typeExtraFields = extraFieldService.Get(x => x.EntityName == entityName).ToList();

            var extraFieldList = new List<Pair<ExtraField, string>>();

            foreach (var ef in typeExtraFields) {
                var fieldName = ef.GetDbFieldName();
                var ep = entityProperties.FirstOrDefault(x => x.EntityName == entityName && x.PropertyName == fieldName);
                var fieldValue = ef.DefaultValue;
                if (ep == null)
                {
                    fieldValue = ep.Value;
                }
                extraFieldList.Add(new Pair<ExtraField, string>()
                {
                    First = ef,
                    Second = fieldValue
                });
            }
            return extraFieldList;
        }
    }
}