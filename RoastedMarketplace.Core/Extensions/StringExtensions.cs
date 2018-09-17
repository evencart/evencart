namespace RoastedMarketplace.Core.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullEmptyOrWhitespace(this string s)
        {
            return string.IsNullOrWhiteSpace(s) || string.IsNullOrWhiteSpace(s);
        }
    }
}