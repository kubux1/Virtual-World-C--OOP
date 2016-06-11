using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    abstract class Animal : Organism
    {
        protected abstract int reproduce(Container world_organisms, Organism[][] organisms_array, int pos_x, int pos_y);
        public override abstract string getOrganismRepresentation();
        public override abstract void setOrganismColor();
        internal override void action(Container world_organisms, Organism[][] organisms_array)
        { // Przesuwa organizm na losowe pole
          // Randoms randomNumber = new Randoms();
            int moveX = 0, moveY = 0;
            int posX = this.getPosX();
            int posY = this.getPosY();
            this.increaseAge();
            while (true)
            {
                moveX = generator.Rand_move_x();
                moveY = generator.Rand_move_y();
                if (moveX != 0 || moveY != 0)
                {
                    if ((posX + moveX) >= 0 && (posX + moveX) < this.getWeight() && (posY + moveY) >= 0 && (posY + moveY) < this.getHeight()) // Sprawdzanie czy nie wyjdzie poza mape
                        break;
                }
            }
            if (organisms_array[posX + moveX][posY + moveY] == null)
            {
                organisms_array[posX + moveX][posY + moveY] = this;
                organisms_array[posX][posY] = null;
                this.changePosition(posX + moveX, posY + moveY);
            }
            else
                collision(world_organisms, organisms_array, moveX, moveY);
        }

        protected override void collision(Container world_organisms, Organism[][] organisms_array, int moveX, int moveY)
        {// Walka lub kolizja
            int posX = this.getPosX();
            int posY = this.getPosY();
            if (object.Equals(organisms_array[posX + moveX][posY + moveY].getName(), this.getName()) == true)
            { // Jesli te same organizmy to rodzi sie nowy organizm
                reproduce(world_organisms, organisms_array, posX, posY);
            }

            else
            { // Jesli inne to nastepuje walka
                fight(world_organisms, organisms_array, moveX, moveY);
            }
        }

       internal int fight(Container world_organisms, Organism[][] organisms_array, int moveX, int moveY)
        {
            int posX = this.getPosX();
            int posY = this.getPosY();
            organisms_array[posX + moveX][posY + moveY].defence(world_organisms, organisms_array, moveX, moveY);
            return 0;
        }
    }
}
