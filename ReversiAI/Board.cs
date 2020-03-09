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

        public Board(Board oldBoard) : this()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    this.board[i, j] = oldBoard.GetBoardAsCharArray()[i, j];
                }
            }
        }

        public char[,] GetBoardAsCharArray()
        {
            return board;
        }

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

        public void MakeMove(Move move)
        {
            if (IsMoveValid(move))
            {
                board[move.x, move.y] = move.symbol;
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

        public bool IsMoveValid(Move move)
        {
            //TODO

            int x = move.x;
            int y = move.y;
            bool enemyInLine = false;
            bool allyInLine = false;
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
                if (enemyInLine && board[x, y] == move.symbol)
                {
                    allyInLine = true;
                }
            }

            if (enemyInLine && allyInLine) return true;
            enemyInLine = false; x = move.x; y = move.y;
            // Check top left diagonal from the desired move
            while (y > 0 && x > 0)
            {
                y--;
                x--;
                if (board[x, y] == '.') break;
                if (board[x,y] == enemy)
                {
                    enemyInLine = true;
                }
                if (enemyInLine && board[x,y] == move.symbol)
                {
                    allyInLine = true;
                }
            }
            if (enemyInLine && allyInLine) return true;
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
                if (enemyInLine && board[x, y] == move.symbol)
                {
                    allyInLine = true;
                }
            }
            if (enemyInLine && allyInLine) return true;
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
                if (enemyInLine && board[x, y] == move.symbol)
                {
                    allyInLine = true;
                }
            }
            if (enemyInLine && allyInLine) return true;
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
                if (enemyInLine && board[x, y] == move.symbol)
                {
                    allyInLine = true;
                }
            }
            if (enemyInLine && allyInLine) return true;
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
                if (enemyInLine && board[x, y] == move.symbol)
                {
                    allyInLine = true;
                }
            }
            if (enemyInLine && allyInLine) return true;
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
                if (enemyInLine && board[x, y] == move.symbol)
                {
                    allyInLine = true;
                }
            }
            if (enemyInLine && allyInLine) return true;
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
                if (enemyInLine && board[x, y] == move.symbol)
                {
                    allyInLine = true;
                }
            }
            if (enemyInLine && allyInLine) return true;
            enemyInLine = false; x = move.x; y = move.y;

            return false;
        }

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
