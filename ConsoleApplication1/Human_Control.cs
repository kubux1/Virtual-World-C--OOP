using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Human_Control
    {
        int moveX_;
        int moveY_;
        bool humanSuperPower_;
        int duration_;
        int cooldown_;
        bool used_;

        public Human_Control()
        {
            this.moveX_ = 0;
            this.moveY_ = 0;
            this.humanSuperPower_ = false;
            this.duration_ = 0;
            this.cooldown_ = 5;
            this.used_ = false;
        }
       public int getMoveX()
        {
            return this.moveX_;
        }
        public int getMoveY()
        {
            return this.moveY_;
        }
        public void setMoveX(int moveX)
        {
            this.moveX_ = moveX;
        }
        public void setMoveY(int moveY)
        {
            this.moveY_ = moveY;
        }
        public void deleteCoordinates()
        {
            this.moveX_ = 0;
            this.moveY_ = 0;
        }

        public bool setHumanSuperPower()
        {
            if (this.cooldown_ == 5 && this.duration_ == 0 && used_ == false)
            {
                this.duration_ = 5;
                return true;
            }
            else
                return false;
        }
        public bool durationPowerStatus()
        {
            if (this.cooldown_ == 5 && this.duration_ > 0)
                return true;
            else return false;
        }
        public bool cooldownPowerStatus()
        {
            if (used_ == true)
                return true;
            else return false;
        }
        public void decreaseSuperPowerDuration()
        {
            if (this.duration_ > 0)
                this.duration_ -= 1;
            if (this.duration_ == 0)
                used_ = true;
        }
        public void decreaseSuperPowerCooldown()
        {
            if (this.duration_ == 0 && this.cooldown_ > 0)
                this.cooldown_ -= 1;
            else
            {
                cooldown_ = 5;
                used_ = false;
            }
        }
        public int getDuration()
        {
            return this.duration_;
        }
        public int getCooldwon()
        {
            return this.cooldown_;
        }
        public bool getUsed()
        {
            return this.used_;
        }
        public void setControls(int duration, int cooldown, bool used)
        {
            this.duration_ = duration;
            this.cooldown_ = cooldown;
            this.used_ = used;
        }
    }
}
