using System;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;

namespace EvenCart.Data.Extensions
{
    public static class TypeConversionExtensions
    {
        public static bool GetBoolean(this object value, bool throwExceptionOnError = true)
        {
            var result = false;
            if (bool.TryParse(value.ToString(), out result))
                return result;

            if (throwExceptionOnError)
                throw new FormatException();

            return false;
        }

        public static int GetInteger(this object value, bool throwExceptionOnError = true)
        {
            var result = 0;
            if (int.TryParse(value.ToString(), out result))
                return result;
            if (throwExceptionOnError)
                throw new FormatException();
            return 0;
        }

        public static decimal GetDecimal(this object value, bool throwExceptionOnError = true)
        {

            decimal result = 0;
            if (decimal.TryParse(value.ToString(), out result))
                return result;
            if (throwExceptionOnError)
                throw new FormatException();
            return 0;
        }

        public static DateTime GetDateTime(this object value, bool throwExceptionOnError = true)
        {

            DateTime result ;
            if (DateTime.TryParse(value.ToString(), out result))
                return result;
            if (throwExceptionOnError)
                throw new FormatException();
            return default(DateTime);
        }

        public static bool IsNumeric(this string value)
        {
            decimal result;
            return decimal.TryParse(value, out result);
        }

        public static bool IsInteger(this string value)
        {
            int result;
            return int.TryParse(value, out result);
        }

        public static bool IsDateTime(this string value)
        {
            DateTime result;
            return DateTime.TryParse(value, out result);
        }

        public static bool IsColor(this string value)
        {
            //first check if it's a system color
            var color = Color.FromName(value);
            if (color.IsKnownColor)
                return true;

            //let's check if it's a hexadecimal code
            int result;
            return int.TryParse(value, NumberStyles.HexNumber, null, out result);
        }

        //https://msdn.microsoft.com/en-us/library/01escwtf(v=vs.110).aspx
        public static bool IsValidEmail(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return false;

            // Use IdnMapping class to convert Unicode domain names.
            try
            {
                value = Regex.Replace(value, @"(@)(.+)$", DomainMapper, RegexOptions.None, TimeSpan.FromMilliseconds(200));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }

            if (string.IsNullOrEmpty(value))
                return false;

            // Return true if strIn is in valid e-mail format.
            try
            {
                return Regex.IsMatch(value,
                    @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        private static string DomainMapper(Match match)
        {
            // IdnMapping class with default property values.
            var idn = new IdnMapping();

            var domainName = match.Groups[2].Value;
            try
            {
                domainName = idn.GetAscii(domainName);
            }
            catch (ArgumentException)
            {
                return string.Empty;
            }
            return match.Groups[1].Value + domainName;
        }
    }
}