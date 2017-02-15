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
     * This is one of the three types of enemies faced, this is the sniper which will stay as far away from the player as possible, 
     * target the player, and shoot at the target, it will also dodge lasers and run from missles 
     * *****/

    class Sniper : Tactics
    {
        float acceptabledistance;

        int lasertimer;
        int burncounter;

        bool needToDodge;
        bool pathChosen;
        bool locationcompromised;

        float avoisionangle;
        float projectileavoisionangle;


        Random rand;
        

        public Sniper(EnemyShip eneref, MainWindow window)
            :base(eneref, window)
        {
            acceptabledistance = eneref.parentWindow.fisheye.WindowBounds.Height / 1.4F;
            rand = GameManager.randomNumberGenerator;
            resetFireTimer();
            burncounter = 20;
        }

        /*****
         * Jacob Lehmer
         * 4/23/15
         * This is the run method for the algorithim
         * *****/
        public override void run()
        {
            if (burncounter > 0 && burncounter <= 19) burn();
            else if (burncounter == 0) { EnemyShipRef.accelerating = false; burncounter = 20; pathChosen = false;}
            if (!pathChosen) EnemyShipRef.decelerateShip();

            survey();
            if(needToDodge) dodge();
            if(locationcompromised) move();
            if(!needToDodge && !locationcompromised) aim();
            if (!needToDodge && !locationcompromised) shoot();

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
            if (rpws < acceptabledistance) locationcompromised = true;
            else  locationcompromised = false; 

            avoisionangle = 270F+ (float)Constants.RADIANS_TO_DEGREES * (float)Math.Atan2((PlayerShip.Shipposy - EnemyShipRef.getYLocation()),PlayerShip.Shippox - EnemyShipRef.getXLocation());

            //this will tell the sniper if it needs to dodge a laser
            foreach (Entity2D laser in GameManager.getAll2DEntities())
            {
                if (laser is Laser && ((Laser)laser).isFromPlayerShip())
                {
                    if(!(laser is Missle)){
                    float rlws = (float)Math.Sqrt((laser.getXLocation() - EnemyShipRef.getXLocation())* (laser.getXLocation() - EnemyShipRef.getXLocation()) + (laser.getYLocation() - EnemyShipRef.getYLocation())*(laser.getYLocation() - EnemyShipRef.getYLocation()));
                    if (rlws < (float)(Settings.projectileSpeed * Settings.projectileSpeed + EnemyShipRef.getCollisionRadius())) 
                    { 
                        needToDodge = true;
                        projectileavoisionangle = 180F + (float)Constants.RADIANS_TO_DEGREES * (float)Math.Atan2((laser.getYLocation() - EnemyShipRef.getYLocation()), laser.getXLocation() - EnemyShipRef.getXLocation());
                        break;
                    }
                    else needToDodge = false;
                    }
                    else if (TargetingSystem.targetedItem == EnemyShipRef) { locationcompromised = true; break; }
                }
            }        
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

        /******
         * Jacob Lehmer
         * 4/26/15
         * This will dodge a projectile coming toward the ship
         * ******/
        private void dodge()
        {
            if (!pathChosen)
            {
                EnemyShipRef.setRotation(projectileavoisionangle);
                burn();
                pathChosen = true;
            }
        }

        /*******
         * Jacob Lehmer
         * 4/26/15
         * This will move the ship
         * *******/
        private void move()
        {
            if (!pathChosen)
            {
                EnemyShipRef.setRotation(avoisionangle);
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
            float vp = Settings.projectileSpeed;
            float rtwsci = (PlayerShip.Shippox - EnemyShipRef.getXLocation());
            float rtwscj = (PlayerShip.Shipposy - EnemyShipRef.getYLocation());
            float rtws = (float)Math.Sqrt(rtwsci * rtwsci + rtwscj * rtwscj);

            float vtwsci = PlayerShip.Shipspeedx;
            float vtwscj = PlayerShip.ShipSpeedy;

            float time = rtws / vp;

            float aimingPointX = PlayerShip.Shippox + vtwsci * time;
            float aimingPointY = PlayerShip.Shipposy + vtwscj * time;

            EnemyShipRef.pointEntity(aimingPointX, aimingPointY, false);
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
        }



    }
}
