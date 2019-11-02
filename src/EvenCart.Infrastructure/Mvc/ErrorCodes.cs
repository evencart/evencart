namespace EvenCart.Infrastructure.Mvc
{
    public static class ErrorCodes
    {
        public const string ParentEntityMustBeNonZero = "PARENT_ID_MUST_NONZERO";

        public const string CaptchaValidationRequired = "CAPTCHA_VALIDATION_REQUIRED";

        public const string AntiForgeryValidationFailed = "ANTIFORGERY_VALIDATION_FAILED";
    }
}