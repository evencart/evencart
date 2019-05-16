namespace EvenCart.Data.Entity.Gdpr
{
    /// <summary>
    /// Represents status of a GDPR consent
    /// </summary>
    public enum ConsentStatus
    {
        /// <summary>
        /// Specifies that consent has been accepted
        /// </summary>
        Accepted = 1,
        /// <summary>
        /// Specifies that consent has been denied
        /// </summary>
        Denied = 2,
        /// <summary>
        /// Specifies that consent is not yet acted upon
        /// </summary>
        NotSelected = 0
    }
}