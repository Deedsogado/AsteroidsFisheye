using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RossHigleyProject7a
{

    /*****
     * Jacob Lehmer
     * 4/012/15
     * This class is The missle, which is effectvely an accelerating, steering laser
     * *****/
    class Missle : Laser
    {

        /*****
         * Jacob Lehmer
         * 4/21/15
         * This is the constructor For the missle
         * *****/
        public Missle(MainWindow window, bool val)
            :base(window, val)
        {
            setGraphic(new Graphic("Missle.png"));
            setMaximumSpeed(Settings.projectileSpeed);
            
        }

        /*******
         * Jacob Lehmer
         * 4/21/15
         * This code is taken from rob's playership class
         * This will accelerate the missle toward the currently targeted item
         * this will be called by the update method
         * MAY NEED TO BE MODIFIED WHEN ENEMIES COME,
         * but then again the enemies missles could be different and be all kinds of crazy
         * ********/
        public void accelerateMissle()
        {
            if (PlayerShip.targetDestroyed == false)
            {
                //see targeting system for naming convention
                float rtwsi = ((Entity2D)TargetingSystem.targetedItem).getXLocation() - this.getXLocation();
                float rtwsj = ((Entity2D)TargetingSystem.targetedItem).getYLocation() - this.getYLocation();
                float correctedRotation = (float)((Math.Atan2(rtwsj, rtwsi)));
                float xSpeed = (float)(getXSpeed() + Math.Cos(correctedRotation) * Settings.projectileSpeed * .4);
                float ySpeed = (float)(getYSpeed() + Math.Sin(correctedRotation) * Settings.projectileSpeed * .4);

               float maximumSpeed = getMaximumSpeed();

                /****this is left in the code for legacy reasons, but will be commented out
                 * JPL -> THERE IS NO TOP SPEED IN SPACE   ******/
                if (xSpeed > maximumSpeed)
                    xSpeed = maximumSpeed;


                if (xSpeed < -maximumSpeed)
                    xSpeed = -maximumSpeed;


                /****this is left in the code for legacy reasons, but will be commented out
                 * JPL -> THERE IS NO TOP SPEED IN SPACE          ******/
                if (ySpeed > maximumSpeed)
                    ySpeed = 10.0F;


                if (ySpeed < -maximumSpeed)
                    ySpeed = -maximumSpeed;

                pointEntity(((Entity2D)TargetingSystem.targetedItem).getXLocation(), // + 20, 
                    ((Entity2D)TargetingSystem.targetedItem).getYLocation(), false); 
                setSpeed(xSpeed, ySpeed);
            }
        }

        ///***********************************************************************************************
        ///<summary>Performs all actions that are required to update this instance of the class.</summary>
        ///***********************************************************************************************

        public override void update()
        {
            accelerateMissle();
            setPosition(getXLocation() + getXSpeed(), getYLocation() + getYSpeed(), false); // true);
            //laserLife++;

            //if (laserLife >= 300)
              //  this.destroy();
        }


        /******
         * Jacob Lehmer
         * 4/28/15
         * This will play the missle sound
         * ******/
        public void playMissleSound()
        {
            (new Sound("wpn_missilelauncher_fire_2d.wav")).playSound(false);
        }

    }
}
