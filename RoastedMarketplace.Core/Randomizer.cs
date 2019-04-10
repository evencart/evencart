using System;
using System.Text;

namespace RoastedMarketplace.Core
{
    public static class Randomizer
    {
        public static string NewString(int size, bool lowerCase = false)
        {
            var builder = new StringBuilder();
            var random = new Random();
            for (var i = 0; i < size; i++)
            {
                var ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }
    }
}