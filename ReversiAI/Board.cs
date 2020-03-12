using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiAI
{
    class Board
    {
        char[,] board;
        public char currentPlayer;

        /// <summary>
        /// Constructor to generate basic board according to conventions
        /// </summary>
        public Board()
        {
            currentPlayer = 'O';
            board = new char[8,8];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    this.board[i, j] = '.';
                }
            }
            board[3, 3] = 'X';
            board[4, 4] = 'X';
            board[3, 4] = 'O';
            board[4, 3] = 'O';
        }

        /// <summary>
        /// Constructor to generate basic board with a custom first one to move
        /// </summary>
        /// <param name="playerSymbol"> Symbol (color) that moves first </param>
        public Board(char playerSymbol) : this()
        {
            currentPlayer = playerSymbol;
        }

        /// <summary>
        /// Constructor with a custom board layout
        /// </summary>
        /// <param name="board"> Starting board </param>
        public Board(char[,] board) : this()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    this.board[i, j] = board[i, j];
                }
            }
        }

        /// <summary>
        /// Copy constructor (from board object)
        /// </summary>
        /// <param name="oldBoard"> Board to copy </param>
        public Board(Board oldBoard) : this()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    this.board[i, j] = oldBoard.GetBoardAsCharArray()[i, j];
                }
            }
            this.currentPlayer = oldBoard.currentPlayer;
        }

        /// <summary>
        /// Get the game board as char array
        /// </summary>
        /// <returns> Current playing board with char array</returns>
        public char[,] GetBoardAsCharArray()
        {
            return board;
        }

        /// <summary>
        /// Override of the ToString method to print out the class (print the board)
        /// </summary>
        /// <returns> Board string representation</returns>
        public override string ToString()
        {
            string s = "x\\y1 2 3 4 5 6 7 8 \n";
            for (int i = 0; i < 7; i++)
            {
                s += (i+1) + " |";
                for (int j = 0; j < 8; j++)
                {
                    s += board[i, j];
                    s += "|";
                }
                s += "\n";
            }
            s += "8 |";
            for (int j = 0; j < 8; j++)
            {
                s += board[7, j];
                s += "|";
            }

            return s;
        }

        /// <summary>
        /// Method to make a move on the current board
        /// </summary>
        /// <param name="move"> Move to be made </param>
        public void MakeMove(Move move)
        {
            if (IsMoveValid(move)) // Test if this is a valid move first
            {
                board[move.x, move.y] = move.symbol;
                int x = move.x;
                int y = move.y;

                /* Following logic is present in all further loops, just different direction:
                 * Start from the point in the move
                 * Move while you encouter an empty space or a friendly symbol
                 * Empty space means there is no your second symbol capturing enemies so no flip, break
                 * Once you encouter the friendly color - go back to start flipping everything (only enemy colors will be there)
                 */

                // Check top vertical from the desired move
                while (x > 0)
                {
                    x--;
                    if (board[x, y] == '.') break;
                    if (board[x, y] == move.symbol)
                    {
                        while (x != move.x)
                        {
                            board[x, y] = move.symbol;
                            x++;
                        }
                        break;
                    }
                }
                x = move.x;  y = move.y;
                // Check top left diagonal from the desired move
                while (y > 0 && x > 0)
                {
                    y--;
                    x--;
                    if (board[x, y] == '.') break;
                    if (board[x, y] == move.symbol)
                    {
                        while (x != move.x && y != move.y)
                        {
                            board[x, y] = move.symbol;
                            x++;
                            y++;
                        }
                        break;
                    }
                }
                x = move.x; y = move.y;
                // Check top right diagonal from the desired move
                while (y < 7 && x > 0)
                {
                    x--;
                    y++;
                    if (board[x, y] == '.') break;
                    if (board[x, y] == move.symbol)
                    {
                        while (x != move.x && y != move.y)
                        {
                            board[x, y] = move.symbol;
                            x++;
                            y--;
                        }
                        break;
                    }
                }
                x = move.x; y = move.y;
                // Check right horizontal from the desired move
                while (y < 7)
                {
                    y++;
                    if (board[x, y] == '.') break;
                    if (board[x, y] == move.symbol)
                    {
                        while (y != move.y)
                        {
                            board[x, y] = move.symbol;
                            y--;
                        }
                        break;
                    }
                }
                x = move.x; y = move.y;
                // Check bottom right diagonal from the desired move
                while (y < 7 && x < 7)
                {
                    y++;
                    x++;
                    if (board[x, y] == '.') break;
                    if (board[x, y] == move.symbol)
                    {
                        while (x != move.x && y != move.y)
                        {
                            board[x, y] = move.symbol;
                            x--;
                            y--;
                        }
                        break;
                    }
                }
                x = move.x; y = move.y;
                // Check bottom vertical from the desired move
                while (x < 7)
                {
                    x++;
                    if (board[x, y] == '.') break;
                    if (board[x, y] == move.symbol)
                    {
                        while (x != move.x)
                        {
                            board[x, y] = move.symbol;
                            x--;
                        }
                        break;
                    }
                }
                x = move.x; y = move.y;
                // Check bottom left diagonal from the desired move
                while (x < 7 && y > 0)
                {
                    x++;
                    y--;
                    if (board[x, y] == '.') break;
                    if (board[x, y] == move.symbol)
                    {
                        while (x != move.x && y != move.y)
                        {
                            board[x, y] = move.symbol;
                            x--;
                            y++;
                        }
                        break;
                    }
                }
                x = move.x; y = move.y;
                // Check left horizontal from the desired move
                while (y > 0)
                {
                    y--;
                    if (board[x, y] == '.') break;
                    if (board[x, y] == move.symbol)
                    {
                        while (y != move.y)
                        {
                            board[x, y] = move.symbol;
                            y++;
                        }
                        break;
                    }
                }
                if (currentPlayer == 'O')
                {
                    currentPlayer = 'X';
                }
                else currentPlayer = 'O';
            } else
            {
                System.Console.WriteLine("Invalid move!");
            }
        }

        /// <summary>
        /// Method to check if the move is valid
        /// </summary>
        /// <param name="move"> Move to check </param>
        /// <returns> True or false </returns>
        public bool IsMoveValid(Move move)
        {
            int x = move.x;
            int y = move.y;
            bool enemyInLine = false;
            // Check if space is already ocupied
            if (board[x, y] != '.')
            {
                return false;
            } 
            // Identify enemy symbol (color)
            char enemy = 'X';
            if (move.symbol == 'X')
            {
                enemy = 'O';
            }

            // Check top vertical from the desired move
            while (x > 0)
            {
                x--;
                if (board[x, y] == '.') break;
                if (board[x, y] == enemy)
                {
                    enemyInLine = true;
                }
                else if (enemyInLine && board[x, y] == move.symbol)
                {
                    return true;
                }
                else break;
            }
            //if (enemyInLine && allyInLine) return true;
            enemyInLine = false; x = move.x; y = move.y;
            // Check top left diagonal from the desired move
            while (y > 0 && x > 0)
            {
                y--;
                x--;
                if (board[x, y] == '.') break;
                if (board[x, y] == enemy)
                {
                    enemyInLine = true;
                }
                else if (enemyInLine && board[x, y] == move.symbol)
                {
                    return true;
                }
                else break;
            }
            //if (enemyInLine && allyInLine) return true;
            enemyInLine = false; x = move.x; y = move.y;
            // Check top right diagonal from the desired move
            while (y < 7 && x > 0)
            {
                y++;
                x--;
                if (board[x, y] == '.') break;
                if (board[x, y] == enemy)
                {
                    enemyInLine = true;
                }
                else if (enemyInLine && board[x, y] == move.symbol)
                {
                    return true;
                }
                else break;
            }
            //if (enemyInLine && allyInLine) return true;
            enemyInLine = false; x = move.x; y = move.y;
            // Check right horizontal from the desired move
            while (y<7)
            {
                y++;
                if (board[x, y] == '.') break;
                if (board[x, y] == enemy)
                {
                    enemyInLine = true;
                }
                else if (enemyInLine && board[x, y] == move.symbol)
                {
                    return true;
                }
                else break;
            }
            //if (enemyInLine && allyInLine) return true;
            enemyInLine = false; x = move.x; y = move.y;
            // Check bottom right diagonal from the desired move
            while (y < 7 && x < 7)
            {
                y++;
                x++;
                if (board[x, y] == '.') break;
                if (board[x, y] == enemy)
                {
                    enemyInLine = true;
                }
                else if (enemyInLine && board[x, y] == move.symbol)
                {
                    return true;
                }
                else break;
            }
            //if (enemyInLine && allyInLine) return true;
            enemyInLine = false; x = move.x; y = move.y;
            // Check bottom vertical from the desired move
            while (x < 7)
            {
                x++;
                if (board[x, y] == '.') break;
                if (board[x, y] == enemy)
                {
                    enemyInLine = true;
                }
                else if (enemyInLine && board[x, y] == move.symbol)
                {
                    return true;
                }
                else break;
            }
            //if (enemyInLine && allyInLine) return true;
            enemyInLine = false; x = move.x; y = move.y;
            // Check bottom left diagonal from the desired move
            while (x < 7 && y > 0)
            {
                x++;
                y--;
                if (board[x, y] == '.') break;
                if (board[x, y] == enemy)
                {
                    enemyInLine = true;
                }
                else if (enemyInLine && board[x, y] == move.symbol)
                {
                    return true;
                }
                else break;
            }
            //if (enemyInLine && allyInLine) return true;
            enemyInLine = false; x = move.x; y = move.y;
            // Check left horizontal from the desired move
            while (y > 0)
            {
                y--;
                if (board[x, y] == '.') break;
                if (board[x, y] == enemy)
                {
                    enemyInLine = true;
                }
                else if (enemyInLine && board[x, y] == move.symbol)
                {
                    return true;
                }
                else break;
            }
            //if (enemyInLine && allyInLine) return true;

            return false;
        }

        /// <summary>
        /// Check if the board is terminal, i.e. if there are at least one move to make
        /// </summary>
        /// <returns> True or false </returns>
        public bool IsTerminal()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (board[i,j] == '.')
                    {
                        if (IsMoveValid(new Move(i, j, 'X')) || IsMoveValid(new Move(i, j, 'O')))
                        {
                            return false;
                        }

                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Get the score for the designated player on the current board
        /// </summary>
        /// <param name="playerSymbol"> Symbol of player for whom to count</param>
        /// <returns> Score value </returns>
        public int GetScore(char playerSymbol)
        {
            int score = 0;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (board[i, j] == playerSymbol)
                    {
                        score++;
                    }
                }
            }
            return score;
        }

        /// <summary>
        /// Get all possible moves on the current board
        /// </summary>
        /// <returns> List of possible moves </returns>
        public List<Move> GetMoves()
        {
            List<Move> moves = new List<Move>();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Move move = new Move(i, j, currentPlayer);
                    if (IsMoveValid(move))
                    {
                        moves.Add(move);
                    }
                }
            }
            return moves;
        }


    }
}
