﻿using System;
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
        [InlineData("15", "2", "17")]
        [InlineData("33", "22", "55")]
        [InlineData("115", "1025", "1140")]
        public void Sum_NatualNumbers_SumsThem(string firstNum, string secondNum, string expected)
        {
            var actual = StringSum.Sum(firstNum, secondNum);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("-1", "2", "2")]
        [InlineData("1.24", "3.14", "0")]
        [InlineData("3.14", "5", "5")]
        [InlineData("3.14", "5000", "5000")]
        [InlineData("3167.14", "111.467", "0")]
        public void Sum_NotNatualNumbers_SumsThem(string firstNum, string secondNum, string expected)
        {
            var actual = StringSum.Sum(firstNum, secondNum);

            Assert.Equal(expected, actual);
        }
    }
}
