using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Berries : Plant
    {
        string organismRepresentation = "B";
        public override string getOrganismRepresentation()
        {
            return organismRepresentation;
        }
        public override void setOrganismColor()
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
        }

       public Berries(World organism_world, Container world_organisms, Organism[][] organisms_array, bool born, int parent_pos_x, int parent_pos_y)
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
                        setOrganism(99, 0, 1, "Berries", posX, posY, organism_world);
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
                        setOrganism(99, 0, 1, "Berries", posX, posY, organism_world);
                        world_organisms.insert_element(this);
                        this.plantGrowMessage();
                    }
                }
            }
        }
        public Berries(World organism_world, Container world_organisms, Organism[][] organisms_array)
        {
            bool born = false;
            int parent_pos_x = 0, parent_pos_y = 0;
            new Berries(organism_world, world_organisms, organisms_array, born, parent_pos_x, parent_pos_y);
        }

        public Berries(World organism_world, Container world_organisms, Organism[][] organisms_array, int strength, int initiative, int age, String name, int posX, int posY)
        {
            setOrganism(strength, initiative, age, name, posX, posY, organism_world);
            world_organisms.insert_element(this);
            organisms_array[posX][posY] = this;
        }


    internal override void action(Container world_organisms, Organism[][] organisms_array)
        {
            Randoms random_number = new Randoms();
            if (random_number.Rand_reproduce() == true)
            { // Jesli szansa na rozmanzanie sie powiodla to sie rozmnaza
                new Berries(this.getOrganismWorld(), world_organisms, organisms_array, true, this.getPosX(), this.getPosY());
            }
        }

        internal override int defence(Container world_organisms, Organism[][] organisms_array, int moveX, int moveY)
        {
            int posX = this.getPosX();
            int posY = this.getPosY();
            this.killOrganism(organisms_array[posX - moveX][posY - moveY]);
            world_organisms.delete_element(organisms_array[posX - moveX][posY - moveY]);
            organisms_array[posX - moveX][posY - moveY] = null;
            return 0;
        }
    }
}
