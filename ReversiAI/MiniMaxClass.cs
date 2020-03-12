using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiAI
{
    class MiniMaxClass
    {
        //TOCHECK
        public Tuple<int,Move> MiniMax(Board board,char player, int maxDepth, int currentDepth, int alpha, int beta)
        {
            int bestScore;
            Move bestMove = new Move();
            if (board.IsTerminal() || currentDepth == maxDepth)
            {
                // ???
                return new Tuple<int, Move>(board.GetScore(player), null);
            }
            if (board.currentPlayer == player)
            {
                bestScore = int.MinValue;
            } else
            {
                bestScore = int.MaxValue;
            }

            foreach (Move move in board.GetMoves()) {
                Board newBoard = new Board(board);
                newBoard.MakeMove(move);
                Tuple<int, Move> temp = MiniMax(newBoard,player, maxDepth, currentDepth + 1, alpha, beta);
                if (board.currentPlayer == player)
                {
                    if (temp.Item1 > bestScore)
                    {
                        bestScore = temp.Item1;
                        bestMove = move;
                    }
                    alpha = Math.Max(alpha, bestScore);
                    if (beta <= alpha) break;
                } else
                {
                    if (temp.Item1 < bestScore)
                    {
                        bestScore = temp.Item1;
                        bestMove = move;
                    }
                    beta = Math.Min(beta, bestScore);
                    if (beta <= alpha) break;
                }
            }
            return new Tuple<int, Move>(bestScore, bestMove);
        }

    }
}
