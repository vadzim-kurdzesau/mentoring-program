using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestingTasks
{
    public static class StringSum
    {
        public static string Sum(string num1, string num2)
        {
            return (num1.ParseToNaturalNumber() + num2.ParseToNaturalNumber()).ToString();
        }

        private static int ParseToNaturalNumber(this string number)
        {
            if (!int.TryParse(number, out var result) || result < 0)
            {
                return 0;
            }

            return result;
        }
    }
}
