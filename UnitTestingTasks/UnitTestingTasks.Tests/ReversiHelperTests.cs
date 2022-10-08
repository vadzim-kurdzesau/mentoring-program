﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTestingTasks.Tests
{
    public class ReversiHelperTests
    {
        [Theory]
        [MemberData(nameof(TestData))]
        public void GetPossibleMoves(char[,] board, char[,] expectedBoard, char currentTurn)
        {
            var reversiHelper = new ReversiHelper();

            reversiHelper.GetPossibleMoves(board, currentTurn);

            AssertBoardsEqual(expectedBoard, board);
        }

        public static IEnumerable<object[]> TestData => new List<object[]>
        {
            new object[] 
            {
                new char[,]
                {
                    { '.','.','.','.','.','.','.','.' },
                    { '.','.','.','.','.','.','.','.' },
                    { '.','.','.','.','.','.','.','.' },
                    { '.','.','.','B','W','.','.','.' },
                    { '.','.','.','W','B','.','.','.' },
                    { '.','.','.','.','.','.','.','.' },
                    { '.','.','.','.','.','.','.','.' },
                    { '.','.','.','.','.','.','.','.' },
                },
                new char[,]
                {
                    { '.','.','.','.','.','.','.','.' },
                    { '.','.','.','.','.','.','.','.' },
                    { '.','.','.','.','0','.','.','.' },
                    { '.','.','.','B','W','0','.','.' },
                    { '.','.','0','W','B','.','.','.' },
                    { '.','.','.','0','.','.','.','.' },
                    { '.','.','.','.','.','.','.','.' },
                    { '.','.','.','.','.','.','.','.' },
                },
                'B'
            }
        };

        private static void AssertBoardsEqual(char[,] expectedBoard, char[,] board)
        {
            var firstDimentionLength = expectedBoard.GetLength(0);
            Assert.True(board.GetLength(0) == firstDimentionLength, $"Expected 0-dimension length equal '{firstDimentionLength}', but was '{board.GetLength(0)}'.");

            var secondDimentionLength = expectedBoard.GetLength(1);
            Assert.True(board.GetLength(1) == secondDimentionLength, $"Expected 1-dimension length equal '{secondDimentionLength}', but was '{board.GetLength(1)}'.");

            for (int i = 0; i < firstDimentionLength; i++)
            {
                for (int j = 0; j < secondDimentionLength; j++)
                {
                    Assert.True(board[i, j] == expectedBoard[i, j], $"Expected '{expectedBoard[i,j]}' at ({i},{j}), but was '{board[i,j]}'.");
                }
            }
        }
    }
}
