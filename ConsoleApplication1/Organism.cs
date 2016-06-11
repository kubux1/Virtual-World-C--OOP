using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public abstract class Organism
    {
        internal abstract void action(Container world_organisms, Organism[][] organisms_array); // Zachowanie organizmu w trakcie tury
        protected abstract void collision(Container world_organisms, Organism[][] organisms_array, int moveX, int moveY); // Zachowanie w trakcie kontaktu z innym organizmem
        private int strength_;
        private int initiative_;
        private int age_;
        private String name_;
        private int posX_;
        private int posY_;
        internal World organismWorld_;
        public Randoms generator = new Randoms();
        public abstract string getOrganismRepresentation();
        public abstract void setOrganismColor();
        internal void setOrganism(int strength, int initiative, int age, String name, int posX, int posY, World organismWorld)
        {
            this.strength_ = strength;
            this.initiative_ = initiative;
            this.age_ = age;
            this.name_ = name;
            this.posX_ = posX;
            this.posY_ = posY;
            this.organismWorld_ = organismWorld;
        }

        public int getStrength()
        {
            return this.strength_;
        }

        public int getInitiative()
        {
            return this.initiative_;
        }

        public int getAge()
        {
            return this.age_;
        }

        public String getName()
        {
            return this.name_;
        }

        public int getPosX()
        {
            return this.posX_;
        }

        public int getPosY()
        {
            return this.posY_;
        }

        internal World getOrganismWorld()
        {
            return this.organismWorld_;
        }

        public void increaseStrength()
        {
            this.strength_ += 3;
            this.organismWorld_.messages.push(this.name_ + " ate a Guarana!");
        }

        public void increaseAge()
        {
            this.age_ += 1;
        }

        public void changePosition(int posX, int posY)
        {
            this.posX_ = posX;
            this.posY_ = posY;
        }

        internal void killOrganism(Organism deadOrganism)
        {
            this.organismWorld_.messages.push(deadOrganism.name_ + " is killed by " + this.name_);
            deadOrganism.organismWorld_.tmp = null;
        }
        
        public int getHumanMoveX()
        {
            return this.organismWorld_.humanControl.getMoveX();
        }

        public int getHumanMoveY()
        {
            return this.organismWorld_.humanControl.getMoveY();
        }
        public void setHumanSuperPower()
        {
            this.organismWorld_.humanControl.setHumanSuperPower();
        }
        public void decreaseSuperPowerDuration()
        {
            this.organismWorld_.humanControl.decreaseSuperPowerDuration();
        }
        public void decreaseSuperPowerCooldown()
        {
            this.organismWorld_.humanControl.decreaseSuperPowerCooldown();
        }
        public bool durationPowerStatus()
        {
            return this.organismWorld_.humanControl.durationPowerStatus();
        }
        public bool cooldownPowerStatus()
        {
            return this.organismWorld_.humanControl.cooldownPowerStatus();
        }
        
        public void animalBirthMessage()
        {
            this.organismWorld_.messages.push("A new " + this.name_ + " is born!");
        }
        public void plantGrowMessage()
        {
            this.organismWorld_.messages.push("A new " + this.name_ + " has grown!");
        }
        public void humanPowerkills(String name)
        {
            this.organismWorld_.messages.push(name + " is killed by human power");
        }
        internal virtual int defence(Container world_organisms, Organism[][] organisms_array, int move_x, int move_y)
        { // Walka organizmow

            if (organisms_array[this.posX_ - move_x][this.posY_ - move_y].strength_ >= this.strength_)
            { // Ginie organizm broniacy sie
                this.organismWorld_.messages.push(this.name_ + " is killed by " + organisms_array[this.posX_ - move_x][this.posY_ - move_y].name_);
                organisms_array[this.posX_ - move_x][this.posY_ - move_y].posX_ = this.posX_;
                organisms_array[this.posX_ - move_x][this.posY_ - move_y].posY_ = this.posY_;
                organisms_array[this.posX_][this.posY_] = organisms_array[this.posX_ - move_x][this.posY_ - move_y];
                organisms_array[this.posX_ - move_x][this.posY_ - move_y] = null;
                organismWorld_.worldOrganisms.delete_element(this);
                return 0;
            }

            if (organisms_array[this.posX_ - move_x][this.posY_ - move_y].strength_ < this.strength_)
            {       // Ginie organizm atakujacy
                this.organismWorld_.messages.push(organisms_array[this.posX_ - move_x][this.posY_ - move_y].name_ + " is killed by " + this.name_);
                organismWorld_.worldOrganisms.delete_element(organisms_array[this.posX_ - move_x][this.posY_ - move_y]);
                organisms_array[this.posX_ - move_x][this.posY_ - move_y] = null;
                organismWorld_.tmp = null;
                return 0;
            }
            return 0;
        }

        internal void setWorld(World organismWorld)
        {
            this.organismWorld_ = organismWorld;
        }
        public int getHeight()
        {
            return organismWorld_.getHeight();
        }
        public int getWeight()
        {
            return this.organismWorld_.getWeight();
        }

    }
}
