using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            World game = new World(30,30);
            while(game.getGameStatus() == true)
            {
                game.makeTour();
            }
        }
    }
}
