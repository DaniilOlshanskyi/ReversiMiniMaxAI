using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiAI
{
    class Program
    {
        static void Main(string[] args)
        {
            char playerSymbol;
            int maxDepth;
            Board board;
            MiniMaxClass miniMax = new MiniMaxClass();
            Console.WriteLine("Enter \"exit\" to exit\n");

            // Ask a player to chose his symbol (color)
            Console.WriteLine("Select symbol to play, enter O or X:");
            string tempS = Console.ReadLine() + "";
            if (tempS.ToCharArray()[0] == 'X')
            {
                playerSymbol = 'X';
            }
            else if (tempS.ToCharArray()[0] == 'O')
            {
                playerSymbol = 'O';
            }
            else
            {
                Console.WriteLine("Invalid symbol!");
                Console.ReadKey();
                return;
            }

            // Let player select AI depth for the MiniMax algorithm
            Console.WriteLine("Select AI depth:");
            maxDepth = Console.ReadLine().ToCharArray()[0]-48;
            // Ask who makes a move first, if player wants to go second - run one round of AI
            Console.WriteLine("Do you want to go first or second? (input \"first\" or \"second\"):");
            string order = Console.ReadLine();
            // If the player selects to go first - create new board and proceed to routine
            if (order.Equals("first"))
            {
                board = new Board(playerSymbol);
            } else
            {
                // Make move as AI with the color opposite to the player
                if (playerSymbol == 'O')
                {
                    board = new Board('X');
                    board.MakeMove(miniMax.MiniMax(board, 'X', maxDepth, 0, int.MinValue, int.MaxValue).Item2);
                }
                else {
                    board = new Board('O');
                    board.MakeMove(miniMax.MiniMax(board, 'O', maxDepth, 0, int.MinValue, int.MaxValue).Item2);
                }
            }
            Console.WriteLine(board.ToString());

            string input = "";
            // Play untill 'exit' is inputed or untill there are no mroe moves
            while (!board.IsTerminal())
            {
                // Read input
                Console.WriteLine("Enter move(format: x y)");
                input = Console.ReadLine() + "";
                char[] inputArr = input.ToCharArray();
                // Exit check
                if (input.Equals("exit")){
                    return;
                } else if (inputArr.Length < 3) // Too small/ wrong input check 
                {
                    Console.WriteLine("Invalid input!");
                }
                else  if (inputArr[0] - 49 < 0 || inputArr[0] - 49 > 7 || inputArr[2] - 49 < 0 || inputArr[0] - 49 > 7) //Wrong number check
                {
                    Console.WriteLine("Invalid input!");
                } 
                else
                {
                    // Try to make player move, display message if it is wrong 
                    Move temp = new Move(inputArr[0] - 49, inputArr[2] - 49, playerSymbol);
                    if (board.IsMoveValid(temp))
                    {
                        // The move is correct, put it on the board
                        board.MakeMove(new Move(inputArr[0] - 49, inputArr[2] - 49, playerSymbol));
                        Console.WriteLine(board.ToString());
                        //AI makes a move with the symbol opposite to players one
                        Console.WriteLine("AI moves...");
                        if (playerSymbol == 'O')
                        {
                            // Make a move that the MiniMax returns
                            board.MakeMove(miniMax.MiniMax(board, 'X', maxDepth, 0, int.MinValue, int.MaxValue).Item2);
                            Console.WriteLine(board.ToString());
                            Console.WriteLine("Your score: " + board.GetScore(playerSymbol));
                            Console.WriteLine("AI score: " + board.GetScore('X'));
                        }
                        else
                        {
                            // Make a move that the MiniMax returns
                            board.MakeMove(miniMax.MiniMax(board, 'O', maxDepth, 0, int.MinValue, int.MaxValue).Item2);
                            Console.WriteLine(board.ToString());
                            Console.WriteLine("Your score: " + board.GetScore(playerSymbol));
                            Console.WriteLine("AI score: " + board.GetScore('O'));
                        }
                    } else
                    {
                        Console.WriteLine("Invalid move!");
                    }
                }

            }
            // The board is terminal, write out score
            int humanScore = board.GetScore(playerSymbol);
            Console.WriteLine("You got " + humanScore + "points!");
            int AIScore = 0;
            if (playerSymbol == 'O')
            {
                AIScore = board.GetScore('X');
            }
            else
            {
                AIScore = board.GetScore('O');
            }
            if (AIScore > humanScore) Console.WriteLine("Sorry, the AI won!");
            else Console.WriteLine("Congratulations, you won!");
            Console.ReadKey();
        }
    }
}
