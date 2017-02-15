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
     * This is the guerilla, this is the most complicated algorithim yet surprisingly ineffective
     * *****/
    class Guerilla : Tactics
    {
        float acceptabledistance;

        int lasertimer;
        int burncounter;

        bool needToDodge;
        bool pathChosen;
        bool getCloser;
        bool runningAway;

        float interceptangle;
        float mouseavoisionangle;

        
        /*****
         * Jacob Lehmer
         * 4/26/15
         * This is the constructor for the guerilla enemy
         * *******/
        public Guerilla(EnemyShip eneref, MainWindow window)
            :base(eneref, window)
        {
            acceptabledistance = eneref.parentWindow.fisheye.WindowBounds.Height / 3F;
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
            else if (burncounter == 0) { EnemyShipRef.accelerating = false; burncounter = 20; pathChosen = false; }
            if (!pathChosen) EnemyShipRef.decelerateShip();

            if (lasertimer <= 0) runningAway = false;

            survey();
            if (needToDodge) dodge();
            if (getCloser && lasertimer <= 0) move(false);
            else move(true);
            if (!needToDodge && !getCloser && !runningAway) aim();
            if (!needToDodge && !getCloser && !runningAway) shoot();

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

            float rpws = (float)Math.Sqrt((PlayerShip.Shippox - EnemyShipRef.getXLocation()) * (PlayerShip.Shippox - EnemyShipRef.getXLocation()) + (PlayerShip.Shipposy - EnemyShipRef.getYLocation()) * (PlayerShip.Shipposy - EnemyShipRef.getYLocation()));
            if (rpws > acceptabledistance && !runningAway) getCloser = true;
            else getCloser = false;

            interceptangle = 90F + (float)Constants.RADIANS_TO_DEGREES * (float)Math.Atan2((PlayerShip.Shipposy - EnemyShipRef.getYLocation()), PlayerShip.Shippox - EnemyShipRef.getXLocation());

            //this will tell the guerilla if it needs to dodge the cursor
            // TODO: investigate if the mouse should be on screen, or in universe for this. 

            float mouseX = parentWindow.PointToClient(System.Windows.Forms.Cursor.Position).X;
            float mouseY = parentWindow.PointToClient(System.Windows.Forms.Cursor.Position).Y;

            float shipX = EnemyShipRef.getXLocation() - parentWindow.camera.getXLocation() ;
            float shipY = EnemyShipRef.getYLocation() - parentWindow.camera.getYLocation() ;

            float deltaX = mouseX - shipX;
            float deltaY = mouseY - shipY;

            float rmws = (float)Math.Sqrt((deltaX * deltaX) + (deltaY * deltaY));
                //(mouseX - EnemyShipRef.getXLocation() ) * 
                //(mouseX - EnemyShipRef.getXLocation() ) + 
                //(mouseY - EnemyShipRef.getYLocation() ) *  
                //(mouseY - EnemyShipRef.getYLocation() ) );

            if(rmws < acceptabledistance)
            {
                needToDodge = true;
                mouseavoisionangle = 270F + (float)Constants.RADIANS_TO_DEGREES * (float)Math.Atan2((mouseY - EnemyShipRef.getYLocation()), mouseX - EnemyShipRef.getXLocation());               
            }
           else needToDodge = false;
                    
        }

        /******
         * Jacob Lehmer
         * 4/26/15
         * This will dodge the mouse coming toward the ship
         * ******/
        private void dodge()
        {
            if (!pathChosen)
            {
                EnemyShipRef.setRotation(mouseavoisionangle);
                burn();
                pathChosen = true;
            }
        }

        /*******
         * Jacob Lehmer
         * 4/26/15
         * This will move the ship
         * *******/
        private void move(bool isRetreating)
        {
            if (!pathChosen)
            {
                if(!isRetreating)EnemyShipRef.setRotation(interceptangle);
                else EnemyShipRef.setRotation(interceptangle -90);
                burn();
                pathChosen = true;

            }
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
            lasertimer = (int)(Settings.enemyFireRate * 3F);
            runningAway = true;
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
