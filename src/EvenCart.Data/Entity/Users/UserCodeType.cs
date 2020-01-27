namespace EvenCart.Data.Entity.Users
{
    public enum UserCodeType
    {
        /// <summary>
        /// Specifies a code for resetting password
        /// </summary>
        PasswordReset,
        /// <summary>
        /// Specifies a code for email verification
        /// </summary>
        EmailVerification,
        /// <summary>
        /// Specifies a code for registration by invitation
        /// </summary>
        RegistrationInvitation,
        /// <summary>
        /// Specifies a 6 digit otp for email verification
        /// </summary>
        EmailOtp
    }
}