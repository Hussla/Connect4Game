using System;

namespace Connect4Game
{
    class Program
    {
        static char[,] board = new char[6, 7];
        static char currentPlayer = 'X'; // Players are 'X' and 'O'

        static void Main(string[] args)
        {
            do
            {
                InitializeBoard();
                PlayGame();
                Console.WriteLine("Play again? (Y/N)");
            } while (Console.ReadLine().Trim().ToUpper() == "Y");
        }

        static void PlayGame()
        {
            bool gameRunning = true;

            while (gameRunning)
            {
                PrintBoard();
                if (TakeTurn())
                {
                    if (CheckForWin())
                    {
                        PrintBoard(); // Print the board one last time to show the winning move
                        Console.WriteLine($"Player {currentPlayer} wins!");
                        gameRunning = false;
                    }
                    else if (IsBoardFull())
                    {
                        PrintBoard();
                        Console.WriteLine("The game is a draw!");
                        gameRunning = false;
                    }
                    else
                    {
                        SwitchPlayer();
                    }
                }
                else
                {
                    Console.WriteLine("Invalid move, try again.");
                }
            }
        }

        static void InitializeBoard()
        {
            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int col = 0; col < board.GetLength(1); col++)
                {
                    board[row, col] = '.';
                }
            }
        }

        static void PrintBoard()
        {
            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int col = 0; col < board.GetLength(1); col++)
                {
                    Console.Write(board[row, col] + " ");
                }
                Console.WriteLine();
            }
        }

        static bool TakeTurn()
        {
            int column;
            do
            {
                Console.WriteLine($"Player {currentPlayer}, choose a column (1-7):");
                if (!int.TryParse(Console.ReadLine(), out column) || column < 1 || column > 7)
                {
                    Console.WriteLine("Please enter a number between 1 and 7.");
                    continue;
                }
                column -= 1; // Adjust for zero-based indexing
            } while (!IsValidMove(column));

            for (int row = board.GetLength(0) - 1; row >= 0; row--)
            {
                if (board[row, column] == '.')
                {
                    board[row, column] = currentPlayer;
                    return true;
                }
            }
            return false; // Should never reach here due to IsValidMove check
        }

        static bool IsValidMove(int column)
        {
            return board[0, column] == '.'; // Check if the top of the column is empty
        }

        static void SwitchPlayer()
        {
            currentPlayer = currentPlayer == 'X' ? 'O' : 'X';
        }

        static bool CheckForWin()
        {
            // Check horizontal, vertical, and diagonal wins
            return CheckHorizontalWin() || CheckVerticalWin() || CheckDiagonalWin();
        }

        static bool CheckHorizontalWin()
        {
            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int col = 0; col < board.GetLength(1) - 3; col++)
                {
                    if (board[row, col] == currentPlayer &&
                        board[row, col + 1] == currentPlayer &&
                        board[row, col + 2] == currentPlayer &&
                        board[row, col + 3] == currentPlayer)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        static bool CheckVerticalWin()
        {
            for (int row = 0; row < board.GetLength(0) - 3; row++)
            {
                for (int col = 0; col < board.GetLength(1); col++)
                {
                    if (board[row, col] == currentPlayer &&
                        board[row + 1, col] == currentPlayer &&
                        board[row + 2, col] == currentPlayer &&
                        board[row + 3, col] == currentPlayer)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        static bool CheckDiagonalWin()
        {
            // Check descending diagonal
            for (int row = 0; row < board.GetLength(0) - 3; row++)
            {
                for (int col = 0; col < board.GetLength(1) - 3; col++)
                {
                    if (board[row, col] == currentPlayer &&
                        board[row + 1, col + 1] == currentPlayer &&
                        board[row + 2, col + 2] == currentPlayer &&
                        board[row + 3, col + 3] == currentPlayer)
                    {
                        return true;
                    }
                }
            }

            // Check ascending diagonal
            for (int row = 3; row < board.GetLength(0); row++)
            {
                for (int col = 0; col < board.GetLength(1) - 3; col++)
                {
                    if (board[row, col] == currentPlayer &&
                        board[row - 1, col + 1] == currentPlayer &&
                        board[row - 2, col + 2] == currentPlayer &&
                        board[row - 3, col + 3] == currentPlayer)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        static bool IsBoardFull()
        {
            for (int col = 0; col < board.GetLength(1); col++)
            {
                if (board[0, col] == '.') // If any column's top is empty
                {
                    return false;
                }
            }
            return true;
        }
    }
}
