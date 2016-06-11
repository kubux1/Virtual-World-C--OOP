using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
   public class myException : Exception
    {
       internal void animalNotBorn(World organismWorld, String name)
        {
            organismWorld.putMessage(name + " couldn't be born");
        }
    }
}
