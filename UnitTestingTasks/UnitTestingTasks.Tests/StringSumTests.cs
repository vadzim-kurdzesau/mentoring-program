using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTestingTasks.Tests
{
    public class StringSumTests
    {
        [Fact]
        public void Sum_EmptyStrings_CountThemAsZeros()
        {
            const string expected = "0";

            var actual = StringSum.Sum(string.Empty, string.Empty);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("1", "2", "3")]
        [InlineData("2", "2", "4")]
        public void Sum_NatualNumbers_SumsThem(string firstNum, string secondNum, string expected)
        {
            var actual = StringSum.Sum(firstNum, secondNum);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("-1", "2", "2")]
        [InlineData("1.24", "3.14", "0")]
        [InlineData("3.14", "5", "5")]
        public void Sum_NotNatualNumbers_SumsThem(string firstNum, string secondNum, string expected)
        {
            var actual = StringSum.Sum(firstNum, secondNum);

            Assert.Equal(expected, actual);
        }
    }
}
