using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestingTasks
{
    public class ReversiHelper
    {
        private const char possibleMove = '0';
        private const char emptyCell = '.';

        public void GetPossibleMoves(char[,] board, char turn)
        {
            var firstDimentionLength = board.GetLength(0);
            var secondDimentionLength = board.GetLength(1);
            for (int i = 0; i < firstDimentionLength; i++)
            {
                for (int j = 0; j < secondDimentionLength; j++)
                {
                    if (board[i,j] != turn && board[i,j] != emptyCell && board[i, j] != possibleMove)
                    {
                        GetPossibleMoves(i, j);
                    }
                }
            }

            void GetPossibleMoves(int i, int j)
            {
                if (i != 0 && board[i - 1, j] == emptyCell)
                {
                    board[i - 1, j] = possibleMove;
                }

                if (i != firstDimentionLength - 1 && board[i + 1, j] == emptyCell)
                {
                    board[i + 1, j] = possibleMove;
                }

                if (j != 0 && board[i, j - 1] == emptyCell)
                {
                    board[i, j - 1] = possibleMove;
                }

                if (j != secondDimentionLength - 1 && board[i, j + 1] == emptyCell)
                {
                    board[i, j + 1] = possibleMove;
                }
            }
        }
    }
}
