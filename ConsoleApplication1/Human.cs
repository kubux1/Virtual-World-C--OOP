using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Human : Animal
    {
        string organismRepresentation = "H";
        public override string getOrganismRepresentation()
        {
            return organismRepresentation;
        }
        public override void setOrganismColor()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
        }

        public Human(World organism_world, Container world_organisms, Organism[][] organisms_array)
        {
            int posX, posY;
            this.setWorld(organism_world);
            // Losowanie pozycji i sprawdzanie czy nie jest juz zajeta dla nie urodzonego organizmu
            while (true)
            {
                posX = generator.Rand_pos_x(this.getWeight());
                posY = generator.Rand_pos_y(this.getHeight());
                if (organisms_array[posX][posY] == null)
                {
                    setOrganism(5, 4, 1, "Human", posX, posY, organism_world);
                    world_organisms.insert_element(this);
                    organisms_array[posX][posY] = this;
                    break;
                }
            }
        }

   protected override int reproduce(Container world_organisms, Organism[][] organisms_array, int pos_x, int pos_y) { return 0; } // jak robic ta tablice org

        Human(World organism_world, Container world_organisms, Organism[][] organisms_array, int strength, int initiative, int age, String name, int posX, int posY)
        {
            setOrganism(strength, initiative, age, name, posX, posY, organism_world);
            world_organisms.insert_element(this);
            organisms_array[posX][posY] = this;
        }

        internal override void action(Container world_organisms, Organism[][] organisms_array)
        {
            int posX = this.getPosX();
            int posY = this.getPosY();
            int moveX = this.getHumanMoveX();
            int moveY = this.getHumanMoveY();

            this.increaseAge();
            if (this.cooldownPowerStatus()) // Redukowanie opoznienia super umiejetnosci jesli byla wlaczona
                this.decreaseSuperPowerCooldown();

            if (this.durationPowerStatus())
            { // Sprawdzanie czy jest wlaczona super umiejetnosc
                for (int i = -1; i < 2; i++)
                {
                    for (int j = -1; j < 2; j++)
                    {
                        if (i == 0 && j == 0)
                            break;
                        else
                        {
                            if ((posX - i) >= 0 && (posX - i) < this.getWeight() && (posY - j) >= 0 && (posY - j) < this.getHeight())
                            {
                                if (organisms_array[posX - i][posY - j] != null)
                                {
                                    this.humanPowerkills(organisms_array[posX - i][posY - j].getName());
                                    world_organisms.delete_element(organisms_array[posX - i][posY - j]);
                                    organisms_array[posX - i][posY - j] = null;
                                }
                            }
                        }
                    }
                }
                this.decreaseSuperPowerDuration();
            }
            if ((posX + moveX) >= 0 && (posX + moveX) < this.getWeight() && (posY + moveY) >= 0 && (posY + moveY) < this.getHeight())
            {
                if (organisms_array[posX + moveX][posY + moveY] == null)
                {
                    organisms_array[posX + moveX][posY + moveY] = this;
                    organisms_array[posX][posY] = null;
                    this.changePosition(posX + moveX, posY + moveY);
                }

                else
                    collision(world_organisms, organisms_array, moveX, moveY);
            }
        }

        protected override void collision(Container world_organisms, Organism[][] organisms_array, int moveX, int moveY)
        {
            int posX = this.getPosX();
            int posY = this.getPosY();
            fight(world_organisms, organisms_array, moveX, moveY);
        }
     
    internal override int defence(Container world_organisms, Organism[][] organisms_array, int moveX, int moveY)
        {
            if (this.durationPowerStatus() == true)
            {
                int posX = this.getPosX();
                int posY = this.getPosY();
                this.killOrganism(organisms_array[posX - moveX][posY - moveY]);
                world_organisms.delete_element(organisms_array[posX - moveX][posY - moveY]);
                organisms_array[posX - moveX][posY - moveY] = null;
            }
            return 0;
        }

    }
}
