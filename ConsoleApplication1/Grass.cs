using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Grass : Plant
    {
        string organismRepresentation = "G";
        public override string getOrganismRepresentation()
        {
            return organismRepresentation;
        }
        public override void setOrganismColor()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
        }

        public Grass(World organism_world, Container world_organisms, Organism[][] organisms_array, bool born, int parent_pos_x, int parent_pos_y)
        {
            int posX, posY;
            this.setWorld(organism_world);
            // Losowanie pozycji i sprawdzanie czy nie jest juz zajeta dla nie urodzonego organizmu
            if (born == false)
            {
                while (true)
                {
                    posX = generator.Rand_pos_x(this.getWeight());
                    posY = generator.Rand_pos_y(this.getHeight());
                    if (organisms_array[posX][posY] == null)
                    {
                        setOrganism(0, 0, 1, "Grass", posX, posY, organism_world);
                        world_organisms.insert_element(this);
                        organisms_array[posX][posY] = this;
                        break;
                    }
                }
            }
            // Dla urodzonego organizmu
            else
            {
                posX = parent_pos_x + generator.Rand_move_x();
                posY = parent_pos_y + generator.Rand_move_y();
                if ((posX != parent_pos_x || posY != parent_pos_y) && posX >= 0 && posX < this.getWeight() && posY >= 0 && posY < this.getHeight())
                {
                    if (organisms_array[posX][posY] == null)
                    {
                        organisms_array[posX][posY] = this;
                        setOrganism(0, 0, 1, "Grass", posX, posY, organism_world);
                        world_organisms.insert_element(this);
                        this.plantGrowMessage();
                    }
                }
            }
        }
        public Grass(World organism_world, Container world_organisms, Organism[][] organisms_array)
        {
            bool born = false;
            int parent_pos_x = 0, parent_pos_y = 0;
            new Grass(organism_world, world_organisms, organisms_array, born, parent_pos_x, parent_pos_y);
        }

        public Grass(World organism_world, Container world_organisms, Organism[][] organisms_array, int strength, int initiative, int age, String name, int posX, int posY)
        {
            setOrganism(strength, initiative, age, name, posX, posY, organism_world);
            world_organisms.insert_element(this);
            organisms_array[posX][posY] = this;
        }


        internal override void action(Container world_organisms, Organism[][] organisms_array)
        {
            if (generator.Rand_reproduce() == true) // Jesli szansa na rozmanzanie sie powiodla to sie rozmnaza
            {
                new Grass(this.getOrganismWorld(), world_organisms, organisms_array, true, this.getPosX(), this.getPosY());

            }
        }
    }
}
