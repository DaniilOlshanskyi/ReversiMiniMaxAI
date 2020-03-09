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

            Board board = new Board();
            Console.WriteLine("Enter \"exit\" to exit\n");
            Console.WriteLine(board.ToString());
            MiniMaxClass miniMax = new MiniMaxClass();

            string input = "";
            while (true)
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
                    Move temp = new Move(inputArr[0] - 49, inputArr[2] - 49, 'O');
                    if (board.IsMoveValid(temp))
                    {
                        board.MakeMove(new Move(inputArr[0] - 49, inputArr[2] - 49, 'O'));
                        Console.WriteLine(board.ToString());
                        //AI moves
                        board.MakeMove(miniMax.MiniMax(board, 'X', 8, 0).Item2);
                        Console.WriteLine(board.ToString());
                    } else
                    {
                        Console.WriteLine("Invalid move!");
                    }
                }

            }
        }
    }
}
