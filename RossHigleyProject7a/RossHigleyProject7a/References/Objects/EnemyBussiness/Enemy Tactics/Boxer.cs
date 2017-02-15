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
     * This is the boxer, it uses my favorite piece of artwork, however the algorithim is very simple
     * *****/
    class Boxer : Tactics
    {               

        float acceptabledistance;

        int lasertimer;
        int burncounter;
        float interceptangle;

        
        /*****
         * Jacob Lehmer
         * 4/26/15
         * This is the constructor for the Boxer enemy
         * *******/
        public Boxer(EnemyShip eneref, MainWindow window)
            :base(eneref, window)
        {
            acceptabledistance = eneref.parentWindow.fisheye.WindowBounds.Height / 4F;
        }
        
        /******
         * Jacob Lehmer
         * 4/26/15
         * This is the run method for the guerilla, it will avoid the mouse and move close to the player ship
         * then fire and run again
         * *****/
        public override void run() 
        {
            if (burncounter > 0 && burncounter <= 19) burn();
            else if (burncounter == 0) { EnemyShipRef.accelerating = false; burncounter = 20;}

            survey();
            move();
            aim();
            shoot();

            lasertimer--;
        }

        /******
         * Jacob Lehmer
         * 4/26/15
         * This is the survey method that will determine the next behavior of the ship
         * *******/
        private void survey()
        {

            //this will tell the ship if it needs to reposition itself, thats rbar of the player with respect to the ship

            interceptangle = 90F + (float)Constants.RADIANS_TO_DEGREES * (float)Math.Atan2((PlayerShip.Shipposy - EnemyShipRef.getYLocation()), PlayerShip.Shippox - EnemyShipRef.getXLocation());

        }


        /*******
         * Jacob Lehmer
         * 4/26/15
         * This will move the ship
         * *******/
        private void move()
        {
               EnemyShipRef.setRotation(interceptangle);
               burn();
        }

        /******
        * Jacob Lehmer 
        * 4/26/15
        * This uses targeting system mathematics in order to aim at the player
        * *******/
        private void aim()
        {
            EnemyShipRef.pointEntity(PlayerShip.Shippox, PlayerShip.Shipposy, false);
        }

        /*******
         * Jacob Lehmer
         * 4/26/15
         * This will fire the hot plasma at the player
         * ******/
        private void shoot()
        {
            if (lasertimer <= 0)
            {
                EnemyShipRef.fireHotPlasma();
                resetFireTimer();
            }
        }    


        /******
         * Jacob Lehmer
         * 4/23/15
         * This will reset the laser timer
         * ******/
        private void resetFireTimer()
        {
            lasertimer = (int)(Settings.enemyFireRate * 4F);
        }

        /*****
        * Jacob Lehmer
        * 4/23/15
        * This Burn method fires the thrusters
        * *****/
        private void burn()
        {
            EnemyShipRef.accelerating = true;
            EnemyShipRef.switchFlame();
            EnemyShipRef.accelerateShip();
            burncounter--;
        }

    }
}
