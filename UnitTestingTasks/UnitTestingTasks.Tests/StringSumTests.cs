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
    }
}
