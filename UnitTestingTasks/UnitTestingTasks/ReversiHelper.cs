namespace UnitTestingTasks
{
    public class ReversiHelper
    {
        private const int FieldSize = 8;
        private const char BlackMove = 'B';
        private const char WhiteMove = 'W';
        private const char PossibleMove = '0';
        private const char EmptyCell = '.';

        public void GetPossibleMoves(char[,] board, char turn)
        {
            var oppositeTurn = GetOppositeTurn(turn);
            for (int i = 0; i < FieldSize; i++)
            {
                for (int j = 0; j < FieldSize; j++)
                {
                    if (board[i,j] == turn)
                    {
                        GetPossibleMoves(i, j);
                    }
                }
            }

            void GetPossibleMoves(int i, int j)
            {
                foreach (var (advancedI, advancedJ) in GetAdjacentCells(i, j))
                {
                    if (board[advancedI, advancedJ] == oppositeTurn)
                    {
                        MarkPossiblePerpendicularMove(board, i, j, advancedI, advancedJ, turn);
                    }
                }
            }

            IEnumerable<(int i, int j)> GetAdjacentCells(int i, int j)
            {
                if (i != 0)
                {
                    // Up
                    yield return (i - 1, j);

                    if (j != 0)
                    {
                        // Up and Left 
                        yield return (i - 1, j - 1);
                    }
                }

                if (i != FieldSize - 1)
                {
                    // Down
                    yield return (i + 1, j);

                    if (j != FieldSize - 1)
                    {
                        // Down and Right
                        yield return (i + 1, j + 1);
                    }
                }

                if (j != 0)
                {
                    // Left
                    yield return (i, j - 1);

                    if (i != FieldSize - 1)
                    {
                        // Left and Down
                        yield return (i + 1, j - 1);
                    }
                }

                if (j != FieldSize - 1)
                {
                    // Right
                    yield return (i, j + 1);

                    if (i != 0)
                    {
                        // Right and Up
                        yield return (i - 1, j + 1);
                    }
                }
            }
        }

        private static void MarkPossiblePerpendicularMove(char[,] board, int i, int j, int advancedI, int advancedJ, char turn)
        {
            Action? iIteration = null, jIteration = null;
            if (i < advancedI)
            {
                iIteration = () => i++;
            }
            else if (i > advancedI)
            {
                iIteration = () => i--;
            }

            if (j < advancedJ)
            {
                jIteration = () => j++;
            }
            else if (j > advancedJ)
            {
                jIteration = () => j--;
            }

            SearchPossibleMove(iIteration, jIteration);

            void SearchPossibleMove(Action? iIterationLogic, Action? jIterationLogic)
            {
                iIterationLogic?.Invoke();
                jIterationLogic?.Invoke();

                while (AreIndexesValid(i, j) && board[i,j] != turn)
                {
                    if (board[i,j] == EmptyCell)
                    {
                        board[i,j] = PossibleMove;
                        return;
                    }

                    iIterationLogic?.Invoke();
                    jIterationLogic?.Invoke();
                }
            }
        }

        private static bool AreIndexesValid(int i, int j)
        {
            return i != 0 && i != FieldSize - 1 && j != 0 && j != FieldSize - 1;
        }

        private static char GetOppositeTurn(char currentTurn)
        {
            return currentTurn == BlackMove ? WhiteMove : BlackMove;
        }
    }
}
