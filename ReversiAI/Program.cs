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


            Console.WriteLine("Select AI depth:");
            maxDepth = Console.ReadLine().ToCharArray()[0]-48;

            Console.WriteLine("Do you want to go first or second? (input \"first\" or \"second\"):");
            string order = Console.ReadLine();
            if (order.Equals("first"))
            {
                board = new Board(playerSymbol);
            } else
            {
                if (playerSymbol == 'O')
                {
                    board = new Board('X');
                    board.MakeMove(miniMax.MiniMax(board, 'X', maxDepth, 0).Item2);
                }
                else {
                    board = new Board('O');
                    board.MakeMove(miniMax.MiniMax(board, 'O', maxDepth, 0).Item2);
                }
            }
            Console.WriteLine(board.ToString());

            string input = "";
            while (!board.IsTerminal())
            {
                Console.WriteLine("Enter move(format: x y)");
                input = Console.ReadLine() + "";
                char[] inputArr = input.ToCharArray();
                if (input.Equals("exit")){
                    return;
                } else if (inputArr.Length < 3)
                {
                    Console.WriteLine("Invalid input!");
                }
                else  if (inputArr[0] - 49 < 0 || inputArr[0] - 49 > 7 || inputArr[2] - 49 < 0 || inputArr[0] - 49 > 7)
                {
                    Console.WriteLine("Invalid input!");
                } 
                else
                {
                    Move temp = new Move(inputArr[0] - 49, inputArr[2] - 49, playerSymbol);
                    if (board.IsMoveValid(temp))
                    {
                        board.MakeMove(new Move(inputArr[0] - 49, inputArr[2] - 49, playerSymbol));
                        Console.WriteLine(board.ToString());
                        //AI moves
                        Console.WriteLine("AI moves...");
                        if (playerSymbol == 'O')
                        {
                            board.MakeMove(miniMax.MiniMax(board, 'X', maxDepth, 0).Item2);
                            Console.WriteLine(board.ToString());
                            Console.WriteLine("Your score: " + board.GetScore(playerSymbol));
                            Console.WriteLine("AI score: " + board.GetScore('X'));
                        }
                        else
                        {
                            board.MakeMove(miniMax.MiniMax(board, 'O', maxDepth, 0).Item2);
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
