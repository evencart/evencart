// #region Author Information
// // ExtraFieldValidationResult.cs
// // 
// // (c) Apexol Technologies. All Rights Reserved.
// // 
// #endregion
namespace RoastedMarketplace.Services.Enum
{
    public enum ExtraFieldValidationResult
    {
        EmptyValueForRequiredField,
        InvalidValueForFieldType,
        OutOfRangeValue,
        NonEditableField,
        UnknownError,
        ValidField
    }
}