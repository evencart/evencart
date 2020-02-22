namespace EvenCart.Services.Enum
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