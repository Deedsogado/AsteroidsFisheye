using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 * EnemyShip.cs
 * Autors: Rob Merrick, Jacob Lehmer
 * This class emulates an enemy ship (both large and small) that
 * will attack the player's ship.
 * MODIFIED 04/15
 * ->Added Tactics targetingReticule
 */ 
          /********
         * Jacob Lehmer
         * 4/23/15
         * This class is very heavily modified by jake
         * It was a stub to begin with unfortunately
           * and there will be no large and small ships, just tactics and no tactics
         * ********/

namespace RossHigleyProject7a
{
    class EnemyShip : Enemy
    {

        //Variable declarations
        private Tactics enemyTactics;
        private Graphic fire1;
        private Graphic fire2;
        private Graphic normal;
        private bool animationswitch = false;
        public bool accelerating = false;

       

        ///*******************************************************************
        ///<summary>Creates a new instance of the EnemyShip() class.</summary>
        ///*******************************************************************

        public EnemyShip(bool isSmallShip, bool useStartLocal,float startx, float starty, MainWindow window):base(window)
        {
            generateInitialValues(useStartLocal,startx,starty);

            setScale(1.0F, 1.0F);
           
            Random rand = GameManager.randomNumberGenerator;
          
           if (isSmallShip) 
            {
                int decider = rand.Next(1, 4);
                switch (decider) 
                {
                    case 1:
                        {
                            normal = new Graphic("SniperEnemyShip.png");
                            fire1 = new Graphic("SniperEnemyShipFire1.png");
                            fire2 = new Graphic("SniperEnemyShipFire2.png");
                            enemyTactics = new Sniper(this, parentWindow);
                            break;
                        }
                    case 2:
                        {
                            normal = new Graphic("BoxerEnemyShip.png");
                            fire1 = new Graphic("BoxerEnemyShipFire1.png");
                            fire2 = new Graphic("BoxerEnemyShipFire2.png");
                            enemyTactics = new Boxer(this, parentWindow);
                            break;
                        }
                    case 3:
                        {
                            normal = new Graphic("GuerillaEnemyShip.png");
                            fire1 = new Graphic("GuerillaEnemyShipFire1.png");
                            fire2 = new Graphic("GuerillaEnemyShipFire2.png");
                            enemyTactics = new Guerilla(this, parentWindow);
                            break;
                        }
            }
            }
            else
            {
                enemyTactics = new BigDummy(this, parentWindow);
                normal = new Graphic("BigDummyShip.png");
                fire1 = new Graphic("BigDummyShipFire1.png");
                fire2 = new Graphic("BigDummyShipFire2.png");
            }
            setGraphic(normal);

        }

        public EnemyShip(MainWindow window)
            :this(false,false,0F,0F, window)
        {

        }

        public EnemyShip(bool isSmallShip, MainWindow window)
            : this(true, false, 0F, 0F, window)
        {

        }

        ///***********************************************************************************************
        ///<summary>Performs all actions that are required to update this instance of the class.</summary>
        ///***********************************************************************************************

        public override void update()
        {
            //Place update code here.
            if (!accelerating) setGraphic(normal);

            enemyTactics.run();
            //setRotation(getRotation() + getRotationalSpeed());
            setPosition(getXLocation() + getXSpeed(), getYLocation() + getYSpeed(), false);
            updateWithRespectToPlayer();
        }

        ///**************************************************************************************************
        ///<summary>Increases the current score by the set amount of points for a small enemy ship.</summary>
        ///**************************************************************************************************

        protected override void awardPoints()
        {
            Scores.addPoints(Scores.SMALL_ENEMY_SHIP_POINTS);
        }

        /******
        * Jacob Lehmer
        * 4/12/15
        * FireEffects
        * *******/
        public void switchFlame()
        {
            if (animationswitch)
            {
                setGraphic(fire2);
                animationswitch = false;
            }
            else
            {
                setGraphic(fire1);
                animationswitch = true;
            }
        }

        ///****************************************************************************************************************
        ///<summary>Creates a random location and velocity for the asteroid. If useStartLocation is true, then the provided
        ///start location values will be used for the initial position instead of randomly generating one.</summary>
        ///****************************************************************************************************************
        //JPL->4/23/15 this is robs code, I just put it here 
        private void generateInitialValues(bool useStartLocation, float xLocation, float yLocation)
        {
            Random random = GameManager.randomNumberGenerator;

            if (useStartLocation)
                setPosition(xLocation, yLocation, false);
            else
            {
                //int screenWidth = (int)(GameManager.canvas.ClipBounds.Width - GameManager.canvasOrigin.X);
                //int screenHeight = (int)(GameManager.canvas.ClipBounds.Height - GameManager.canvasOrigin.Y);
                //float centerScreenX = (GameManager.canvas.ClipBounds.Width + GameManager.canvasOrigin.X) / 2.0F;
                //float centerScreenY = (GameManager.canvas.ClipBounds.Height + GameManager.canvasOrigin.Y) / 2.0F;
                //float positionX = (float)(Math.Pow(-1, random.Next(1, 3)) * random.Next(200, screenWidth));
                //float positionY = (float)(Math.Pow(-1, random.Next(1, 3)) * random.Next(200, screenHeight));
            //    setPosition(centerScreenX + positionX, centerScreenY + positionY, true);

                double angle = random.NextDouble() * 360 * Constants.DEGREES_TO_RADIANS;
                float tempX = (float)Math.Cos(angle) * random.Next(200, parentWindow.fisheye.WindowBounds.Width);
                float tempY = (float)Math.Sin(angle) * random.Next(200, parentWindow.fisheye.WindowBounds.Height);
                setPosition(parentWindow.ship.getXLocation() + tempX,
                    parentWindow.ship.getYLocation() + tempY, false);
            }

            setSpeed((float)(-5.0 + random.NextDouble() * 10.0), (float)(-5.0 + random.NextDouble() * 10.0));
        }

        /*****
         * Jacob Lehmer
         * This is just rob's fire laser method with hot plasma instead of laser, still a laser though
         * *****/
        public void fireHotPlasma()
        {

            Laser laser = new Laser(parentWindow, false);
            laser.playLaserSound();
            laser.setRotation(getRotation());
            float correctedRotation = (float)((getRotation() - 90) * Constants.DEGREES_TO_RADIANS);
            float xSpeed = (float)(Math.Cos(correctedRotation) * Settings.enemyProjectileSpeed);
            float ySpeed = (float)(Math.Sin(correctedRotation) * Settings.enemyProjectileSpeed);
            laser.setCollisionRadius(25);
            laser.setSpeed(xSpeed, ySpeed);
            laser.setPosition(getXLocation(), getYLocation(), false);
            laser.setGraphic(new Graphic("EnemyShot.png"));
            GameManager.add2DEntity(laser);
        }

        /******
         * Jacob Lehmer
         * 4/23/15
         * This is robs accelerate ship method
         * ******/
        ///************************************************************************************************************
        ///<summary>Increases the ship speed in the direction it is pointed unless it's at its maximum speed.</summary>
        ///************************************************************************************************************

        public void accelerateShip()
        {

            float correctedRotation = (float)((getRotation() - 90) * Constants.DEGREES_TO_RADIANS);
            float xSpeed = (float)(getXSpeed() + Math.Cos(correctedRotation) * Settings.enemyAcceleration);
            float ySpeed = (float)(getYSpeed() + Math.Sin(correctedRotation) * Settings.enemyAcceleration);

            float maximumSpeed = getMaximumSpeed();

            /****this is left in the code for legacy reasons, but will be commented out
             * JPL -> THERE IS NO TOP SPEED IN SPACE    ******/
            if (xSpeed > maximumSpeed)
                 xSpeed = maximumSpeed;
         

            if (xSpeed < -maximumSpeed)
                xSpeed = -maximumSpeed;


            /****this is left in the code for legacy reasons, but will be commented out
             * JPL -> THERE IS NO TOP SPEED IN SPACE      ******/
            if (ySpeed > maximumSpeed)
                  ySpeed = maximumSpeed;
     
            
            if (ySpeed < -maximumSpeed)
                ySpeed = -maximumSpeed;

            setSpeed(xSpeed, ySpeed);
        }

        ///**************************************************************************************************************
        ///<summary>Decreases the ship speed unless it's close to zero, in which case the speed is set to zero.</summary>
        ///JPL-Rework in the spirit of using rockets, also might add a better inertial dampener system to the upgrade system
        ///**************************************************************************************************************

        public void decelerateShip()
        {

            float xSpeed = getXSpeed();
            float ySpeed = getYSpeed();

            if (xSpeed > 0)
            {
                if (Math.Abs(xSpeed - Settings.enemyInertialDampening) > Settings.enemyInertialDampening * 2) xSpeed -= Settings.enemyInertialDampening;
                else xSpeed = 0;
            }
            else
            {
                if (Math.Abs(xSpeed + Settings.enemyInertialDampening) > Settings.enemyInertialDampening * 2) xSpeed += Settings.enemyInertialDampening;
                else xSpeed = 0;
            }


            if (ySpeed > 0)
            {
                if (Math.Abs(ySpeed - Settings.enemyInertialDampening) > Settings.enemyInertialDampening * 2) ySpeed -= Settings.enemyInertialDampening;
                else ySpeed = 0;
            }
            else
            {
                if (Math.Abs(ySpeed + Settings.enemyInertialDampening) > Settings.enemyInertialDampening * 2) ySpeed += Settings.enemyInertialDampening;
                else ySpeed = 0;
            }


            setSpeed(xSpeed, ySpeed);
        }

        /*****
         * Jacob Lehmer
         * 4/26/15
         * This is an override for the enemy ship's update with respect to the player,
         * they should not impact the player
         * *******/
        protected override void updateWithRespectToPlayer()
        {
            if (blowUpAnimation == null)
            {
                

                foreach (Entity2D laser in GameManager.getAll2DEntities())
                {
                    if (laser is Laser && ((Laser)laser).isFromPlayerShip() && laser.onlyonecollision == false && laser.isCollidingWith(this))
                    {
                        if (TargetingSystem.targetedItem == this) PlayerShip.targetDestroyed = true;
                        laser.destroy();
                        this.destroy();

                    }
                }
            }
            else
            {
                int currentFrame = blowUpAnimation.drawExplosion(getXLocation(), getYLocation(), getRotation(), getScaleX(), getScaleY());

                if (currentFrame >= SpecialEffects.EXPLOSION_KEY_FRAME)
                    this.setEntityIsShown(false);

                if (currentFrame >= SpecialEffects.EXPLOSION_FRAME_COUNT - 1)
                    GameManager.remove2DEntity(this);
            }
        }

    }
}