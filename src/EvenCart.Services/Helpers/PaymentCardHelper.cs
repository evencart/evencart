using System;
using System.Text.RegularExpressions;

namespace EvenCart.Services.Helpers
{

    public class PaymentCardHelper
    {
        public enum CreditCardType
        {
            Visa,
            MasterCard,
            Maestro,
            Discover,
            Amex,
            Switch,
            Solo
        }

        public static bool IsCardNumberValid(string cardNumber)
        {
            //Luhn's test
            try
            {
                //Clean the card number- remove dashes and spaces
                cardNumber = cardNumber.Replace("-", "").Replace(" ", "");

                //Convert card number into digits array
                var digits = new int[cardNumber.Length];
                for (var len = 0; len < cardNumber.Length; len++)
                {
                    digits[len] = Int32.Parse(cardNumber.Substring(len, 1));
                }

                var sum = 0;
                var isEven = false;
                for (var i = digits.Length - 1; i >= 0; i -= 1)
                {
                    var curDigit = digits[i];
                    if (isEven)
                    {
                        curDigit *= 2;

                    }
                    if (curDigit > 9)
                    {
                        curDigit -= 9;
                    }
                    isEven = !isEven;
                    sum += curDigit;
                }

                //If Mod 10 equals 0, the number is good and this will return true
                return sum % 10 == 0;
            }
            catch (Exception)
            {
                return false;
            }
           
        }

        private const string CardRegex = @"^(?:(?<Visa>4\\d{3})|(?<MasterCard>5[1-5]\\d{2})|(?<Maestro>(5018|5020|5038|6304|6759|6761|6763)[0-9]{8,15})|(?<Discover>6011)|(?<DinersClub>(?:3[68]\\d{2})|(?:30[0-5]\\d))|(?<Amex>3[47]\\d{2}))([ -]?)(?(DinersClub)(?:\\d{6}\\1\\d{4})|(?(Amex)(?:\\d{6}\\1\\d{5})|(?:\\d{4}\\1\\d{4}\\1\\d{4})))$";

        public static CreditCardType? GetCardTypeFromNumber(string cardNum)
        {
            //Create new instance of Regex comparer with our
            //credit card regex patter
            var cardTest = new Regex(CardRegex);

            //Compare the supplied card number with the regex
            //pattern and get reference regex named groups
            var gc = cardTest.Match(cardNum).Groups;

            //Compare each card type to the named groups to 
            //determine which card type the number matches
            if (gc[CreditCardType.Amex.ToString()].Success)
            {
                return CreditCardType.Amex;
            }
            else if (gc[CreditCardType.MasterCard.ToString()].Success)
            {
                return CreditCardType.MasterCard;
            }
            else if (gc[CreditCardType.Maestro.ToString()].Success)
            {
                return CreditCardType.Maestro;
            }
            else if (gc[CreditCardType.Visa.ToString()].Success)
            {
                return CreditCardType.Visa;
            }
            else if (gc[CreditCardType.Discover.ToString()].Success)
            {
                return CreditCardType.Discover;
            }
            else
            {
                //Card type is not supported by our system, return null
                //(You can modify this code to support more (or less)
                // card types as it pertains to your application)
                return null;
            }
        }

        public static string StripDashes(string cardNumber)
        {
            var strippedCardNumber =  cardNumber.Trim().Replace("-", "");
            return strippedCardNumber;
        }
        public static string MaskCardNumber(string cardNumber)
        {
            //find last 4 digits
            string lastFour = cardNumber.Substring(cardNumber.Length - 4);
            return string.Concat("xxxx-xxxx-xxxx-", lastFour);
        }
    }
}
