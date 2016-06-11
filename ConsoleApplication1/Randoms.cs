using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class Randoms
    {
        private static readonly object syncLock = new object();
        Random generator;
        public Randoms()
        {
            generator = new Random();
        }

        public int Rand_pos_x(int weight)
        {
            int pos_x = generator.Next(weight);
            return pos_x;
        }

        public int Rand_pos_y(int height)
        {
            int pos_y = generator.Next(height);
            return pos_y;
        }

        public int Rand_move_x()
        {
            lock (syncLock)
            {
                int move_x = generator.Next(3) - 1;

                return move_x;
            }
        }

        public int Rand_move_y()
        {
            lock (syncLock)
            {
                int move_y = generator.Next(3) - 1;
                return move_y;
            }
        }

        public bool Rand_reproduce()
        {
            int reproduce_chance = generator.Next(101);
            if (reproduce_chance <= 3)
                return true;
            else return false;
        }

        public int randOrganism()
        {
            int randOrg = generator.Next(6);
            return randOrg;
        }

        public bool turtleMove()
        {
            int reproduce_chance = generator.Next(101);
            if (reproduce_chance <= 75)
                return false;
            else return true;
        }

        public bool antelopeEscape()
        {
            int reproduce_chance = generator.Next(101);
            if (reproduce_chance <= 50)
                return true;
            else return false;
        }
    }
}
