using System;

namespace Task2
{
    public class NumberParser : INumberParser
    {
        private const int PositiveSignModifier = 1;
        private const int NegativeSignModifier = -1;

        public int Parse(string stringValue)
        {
            if (stringValue == null)
            {
                throw new ArgumentNullException(nameof(stringValue));
            }

            stringValue = stringValue.Trim();
            if (stringValue.Equals(string.Empty))
            {
                throw new FormatException("String can't be empty or a whitespace.");
            }

            return ParsePreparedString(stringValue);
        }

        private static int ParsePreparedString(string stringValue)
        {
            var startIndex = SkipSign(stringValue, out int sign);
            SkipLeadingZeroes(stringValue, ref startIndex);

            long result = 0;
            for (int i = stringValue.Length - 1, power = 0; i >= startIndex; i--, power++)
            {
                var modifier = (int)Math.Pow(10, power);
                result += GetDigit(stringValue[i]) * modifier;
            }

            result *= sign;
            if (result > int.MaxValue || result < int.MinValue)
            {
                throw new OverflowException("The number is out of Int32 bounds.");
            }

            return (int)result;
        }

        private static int SkipSign(string digits, out int sign)
        {
            sign = PositiveSignModifier;
            if (digits[0] == '-')
            {
                sign = NegativeSignModifier;
            }

            if (digits[0] == '+' || digits[0] == '-')
            {
                return 1;
            }

            return 0;
        }

        private static void SkipLeadingZeroes(string digits, ref int index)
        {
            for (int i = index; i < digits.Length; i++)
            {
                if (digits[i] != '0')
                {
                    break;
                }

                index++;
            }
        }

        private static int GetDigit(char digit)
        {
            return digit switch
            {
                '0' => 0,
                '1' => 1,
                '2' => 2,
                '3' => 3,
                '4' => 4,
                '5' => 5,
                '6' => 6,
                '7' => 7,
                '8' => 8,
                '9' => 9,
                _ => throw new FormatException("String contains non-digit value.")
            };
        }
    }
}