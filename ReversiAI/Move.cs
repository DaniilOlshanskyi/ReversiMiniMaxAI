using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiAI
{
    class Move
    {
        public int x, y;
        public char symbol;

        public Move()
        {
            x = 0;
            y = 0;
            symbol = '.';
        }

        public Move(int x, int y, char symbol)
        {
            this.x = x;
            this.y = y;
            this.symbol = symbol;
        }

    }
}
