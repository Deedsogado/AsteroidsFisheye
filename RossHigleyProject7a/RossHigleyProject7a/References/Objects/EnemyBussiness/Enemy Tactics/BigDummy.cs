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
     * STUB: This is going to be the big ship that bloops around and fires lasers
     * *****/
    class BigDummy : Tactics
    {
        //these are the private data that will be used by the algorithims
        private int movementintervalcounter;
        private int burncounter;
        private int firecounter;
        private int rotatecounter;

        private Random rand;

        /****
         * Jacob Lehmer
         * 4/23/15
         * This is the constuctor
         * these will not always be literal values
         * *****/
        public BigDummy(EnemyShip eneref, MainWindow window)
            :base(eneref, window)
        {
            movementintervalcounter = 23;
            firecounter = (int)Settings.enemyFireRate;
            burncounter = 10;
            rotatecounter = 12;
            rand = GameManager.randomNumberGenerator;
        }

        /*******
         * Jacob Lehmer
         * This is the actual part of the algortithim that does something
         * ******/
        public override void run()
        {
            int decideValue = rand.Next(0, 4);

            if (burncounter > 0 && burncounter <= 9) burn();
            else if(burncounter == 0){ EnemyShipRef.accelerating = false; burncounter = 10; }

            if (EnemyShipRef.accelerating == false) EnemyShipRef.decelerateShip();

            switch (decideValue)
            {
                case 1: rotate(); break;
                case 2: move(); break;
                case 3: fire(); break;
            }
            

            if (movementintervalcounter < -7) move();
            if (rotatecounter < -2) move();

            firecounter--;
            movementintervalcounter--;
            rotatecounter--;

        }

        /*****
         * Jacob Lehmer
         * 4/23/15
         * This will move the ship
         * *******/
        private void move()
        {

            if (movementintervalcounter < 17)
            {
                if (rand.Next(0, 4) == 3)
                {
                    burn();
                    resetMovement();
                }
               
            }
            else if (movementintervalcounter < 10)
            {
                if (rand.Next(0, 2) == 1)
                {
                    burn();
                    resetMovement();
                }
            }
            else if (movementintervalcounter < 5)
            {
                if (rand.Next(0, 3) == 1)
                {
                    burn();
                    resetMovement();
                }
            }
            else if (movementintervalcounter <= 0)
            {
                burn();
                resetMovement();
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

        /****
         * Jacob Lehmer
         * 4/23/15
         * This will rotate the ship
         * *****/
        private void rotate()
        {
            if(rotatecounter < 6)
            {
                if (rand.Next(0, 30) == 1)
                {
                    EnemyShipRef.pointEntity(PlayerShip.Shippox,PlayerShip.Shipposy, false);
                    resetRotationTimer();
                }
            }
            else if (rotatecounter < 0)
            {
               EnemyShipRef.setRotation(rand.Next(90, 360) + EnemyShipRef.getRotation());
               resetRotationTimer();
            }

        }

        /*****
         * Jacob Lehmer
         * 4/23/15
         * This will fire the laser
         * *****/
        private void fire()
        {
            if (firecounter < 23)
            {
                if (rand.Next(0, 5) == 3)
                {
                    EnemyShipRef.fireHotPlasma();
                    resetFireTimer();
                }
            }
            else if (firecounter <= 0)
            {
                EnemyShipRef.fireHotPlasma();
                resetFireTimer();
            }
        }

        /********
         * Jacob Lehmer
         * 4/23/15
         * This will reset movement
         * *******/
        private void resetMovement()
        {
            movementintervalcounter = 23;
        }

        /******
         * Jacob Lehmer
         * 4/23/15
         * This will reset the laser timer
         * ******/
        private void resetFireTimer()
        {
            firecounter = (int)Settings.enemyFireRate * 5 ;
        }

        /*******
         * Jacob Lehmer
         * This will reset the rotation timer
         * ******/
        private void resetRotationTimer(){
            rotatecounter = 12;
        }

    }
}
