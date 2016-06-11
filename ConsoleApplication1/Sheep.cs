using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Sheep : Animal
    {
        string organismRepresentation = "S";

        public override string getOrganismRepresentation()
        {
            return organismRepresentation;
        }
        public override void setOrganismColor()
        {
            Console.ForegroundColor = ConsoleColor.White;
        }

      public  Sheep(World organism_world, Container world_organisms, Organism [][]organisms_array, bool born, int parent_pos_x, int parent_pos_y)
        {
            myException notBorn = new myException();
            int posX, posY;
            this.setWorld(organism_world);
            if (born == false)
            { // Losowanie pozycji i sprawdzanie czy nie jest juz zajeta dla nie urodzonego organizmu
                while (true)
                {
                    posX = generator.Rand_pos_x(this.getWeight());
                    posY = generator.Rand_pos_y(this.getHeight());
                    if (organisms_array[posX][posY] == null)
                    {
                        setOrganism(4, 4, 1, "Sheep", posX, posY, organism_world);
                        world_organisms.insert_element(this);
                        organisms_array[posX][posY] = this;
                        break;
                    }
                }
            }

            else
            { // -||- dla urodzonego organizmu
                posX = parent_pos_x + generator.Rand_move_x();
                posY = parent_pos_y + generator.Rand_move_y();
                setOrganism(4, 4, 1, "Sheep", posX, posY, organism_world);
                if ((posX != parent_pos_x || posY != parent_pos_y) && posX >= 0 && posX < this.getWeight() && posY >= 0 && posY < this.getHeight())
                {
                    try
                    {
                        if (organisms_array[posX][posY] == null)
                        {
                            organisms_array[posX][posY] = this;
                            world_organisms.insert_element(this);
                            this.animalBirthMessage();
                        }
                        else throw notBorn;
                    }
                    catch (myException notBornAnimal)
                    {
                        notBornAnimal.animalNotBorn(this.getOrganismWorld(), this.getName());
                    }
                }
            }
        }
       public Sheep(World organism_world, Container world_organisms, Organism[][] organisms_array)
        {
            bool born = false;
            int parent_pos_x = 0, parent_pos_y = 0;
            new Sheep(organism_world, world_organisms, organisms_array, born, parent_pos_x, parent_pos_y);
        }

      public  Sheep(World organism_world, Container world_organisms, Organism[][] organisms_array, int strength, int initiative, int age, String name, int posX, int posY)
        {
            setOrganism(strength, initiative, age, name, posX, posY, organism_world);
            world_organisms.insert_element(this);
            organisms_array[posX][posY] = this;
        }


        protected override int reproduce(Container world_organisms, Organism[][] organisms_array, int parentPosX, int parentPosY)
        {
            new Sheep(this.getOrganismWorld(), world_organisms, organisms_array, true, parentPosX, parentPosY);
            return 0;
        }
    }
}
