using System;
using Xunit;

namespace UnitTestingTasks.Tests
{
    public class RecentlyUsedListTests
    {
        [Fact]
        public void Initialize_ListIsEmpty()
        {
            const int emptyLength = 0;
            var list = new RecentlyUsedList();

            Assert.Equal(emptyLength, list.Length);
        }

        [Fact]
        public void Add_ValidString_AddsOnToFirstPosition()
        {
            const string firstValue = "FirstValue", lastValue = "LastValue";
            var list = CreateListWithData(lastValue);

            list.Add(firstValue);

            var actual = list[0];
            Assert.Equal(firstValue, actual);
        }

        [Fact]
        public void Add_ValidString_MovesExistingToNextPositions()
        {
            const string firstValue = "FirstValue", secondValue = "SecondValue", lastValue = "LastValue";
            var list = CreateListWithData(lastValue, secondValue);

            list.Add(firstValue);

            var actualSecond = list[1];
            var actualLast = list[2];
            Assert.Equal(secondValue, actualSecond);
            Assert.Equal(lastValue, actualLast);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Add_InvalidString_ThrowsArgumentNullException(string invalidString)
        {
            var list = new RecentlyUsedList();

            Assert.Throws<ArgumentNullException>(() => list.Add(invalidString));
        }

        [Fact]
        public void Add_StringAlreadyExists_MovesItAtFirstPosition()
        {
            const string existingString = "ExistingString", anotherString = "AnotherString";
            var list = CreateListWithData(existingString, anotherString);

            list.Add(existingString);

            var actualFirst = list[0];
            Assert.Equal(existingString, anotherString);
        }

        [Fact]
        public void Add_ExceedsCapacity_AddsNewAndRemovesTheLast()
        {
            const int capacity = 2;
            const string oldestString = "OldestString", anotherString = "AnotherString", newString = "NewString";
            var list = CreateListWithData(capacity, oldestString, anotherString);

            list.Add(newString);

            var actualFirst = list[0];
            var actualLast = list[1];
            Assert.Equal(newString, actualFirst);
            Assert.Equal(anotherString, actualLast);
        }

        [Fact]
        public void Get_NegativeIndex_ThrowsIndexOutOfRangeException()
        {
            const int negativeIndex = -1;
            var list = new RecentlyUsedList();

            Assert.Throws<IndexOutOfRangeException>(() => list[negativeIndex]);
        }

        [Fact]
        public void Get_IndexExceeedsCapacity_ThrowsIndexOutOfRangeException()
        {
            const int capacity = 4, index = capacity + 1;
            var list = new RecentlyUsedList(capacity);

            Assert.Throws<IndexOutOfRangeException>(() => list[index]);
        }

        private static RecentlyUsedList CreateListWithData(params string[] data)
        {
            return CreateListWithData(int.MaxValue, data);
        }

        private static RecentlyUsedList CreateListWithData(int capacity = int.MaxValue, params string[] data)
        {
            var list = new RecentlyUsedList(capacity);
            foreach (var str in data)
            {
                list.Add(str);
            }

            return list;
        }
    }
}
