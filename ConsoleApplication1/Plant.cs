using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    abstract class Plant : Organism
    {
        internal override abstract void action(Container world_organisms, Organism[][] organisms_array); // Proba rozmnozenia
        protected override void collision(Container world_organisms, Organism[][] organisms_array, int moveX, int moveY) { }
        public override abstract string getOrganismRepresentation();
        public override abstract void setOrganismColor();
    }
}
