using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiAI
{
    /// <summary>
    /// Wrapper class for minimax function
    /// </summary>
    class MiniMaxClass
    {
        public Tuple<int,Move> MiniMax(Board board,char player, int maxDepth, int currentDepth, int alpha, int beta)
        {
            int bestScore;
            Move bestMove = new Move();
            // Check if the bottom of the recursion is reached
            if (board.IsTerminal() || currentDepth == maxDepth)
            {
                return new Tuple<int, Move>(board.GetScore(player), null);
            }
            // Check if the algorithm "plays" for player or for AI
            if (board.currentPlayer == player)
            {
                bestScore = int.MinValue;
            } else
            {
                bestScore = int.MaxValue;
            }
            // Test all moves available at a given board
            foreach (Move move in board.GetMoves()) {
                Board newBoard = new Board(board);
                newBoard.MakeMove(move);
                // Run recursively
                Tuple<int, Move> temp = MiniMax(newBoard,player, maxDepth, currentDepth + 1, alpha, beta);
                if (board.currentPlayer == player)
                {
                    // If this move gives better score - save it ("our" turn, maximizing)
                    if (temp.Item1 > bestScore)
                    {
                        bestScore = temp.Item1;
                        bestMove = move;
                    }
                    alpha = Math.Max(alpha, bestScore);
                    if (beta <= alpha) break;
                } else
                {
                    // If this move gives worse score - save it ("enemy" turn, minimizing)
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
