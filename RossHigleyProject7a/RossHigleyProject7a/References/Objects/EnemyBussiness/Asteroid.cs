using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 * Asteroid.cs
 * Autors: Rob Merrick
 * This class is used to create and manage the asteroids
 * (large and small) for each level.
 */

namespace RossHigleyProject7a
{
    class Asteroid : Enemy
    {

        //Variable Declarations
        private SIZE asteroidSize;
        private bool missleImpact;
       

        public enum SIZE
        {
            SMALL, MEDIUM, LARGE
        }

        ///***************************************************************************************************************************
        ///<summary>This private constructor is called to set up the values based on the public constructor that was called.</summary>
        ///***************************************************************************************************************************

        private Asteroid(Asteroid.SIZE asteroidSize, bool useStartLocation, float xLocation, float yLocation, MainWindow window): base(window)
        {
            this.asteroidSize = asteroidSize;
            setGraphic(new Graphic("Asteroid.png"));
            generateInitialValues(useStartLocation, xLocation, yLocation);

            switch(asteroidSize)
            {
                case SIZE.SMALL:
                    setScale(0.25F, 0.25F);
                    setCollisionRadius(35.0F);
                    break;
                case SIZE.MEDIUM:
                    setScale(0.5F, 0.5F);
                    setCollisionRadius(60.0F);
                    break;
                case SIZE.LARGE:
                    setScale(1.0F, 1.0F);
                    setCollisionRadius(100.0F);
                    break;
            }
        }

        ///***************************************************************************************
        ///<summary>Creates a new instance of the Asteroid class with the provided size.</summary>
        ///***************************************************************************************

        public Asteroid(Asteroid.SIZE asteroidSize, MainWindow window) : this(asteroidSize, false, -1.0F, -1.0F, window)
        {
            //Empty
            
        }

        public Asteroid(Asteroid.SIZE asteroidSize, float xLocation, float yLocation, MainWindow window) : this(asteroidSize, true, xLocation, yLocation, window)
        {
            //Empty
        }

        ///***********************************************************************************************
        ///<summary>Performs all actions that are required to update this instance of the class.
        /// RMH - 12/7/16 - now Respawns the asteroid if it floats more than 2 screen distances away from ship. 
        /// </summary>
        ///***********************************************************************************************

        public override void update()
        {
            setRotation(getRotation() + getRotationalSpeed());
            setPosition(getXLocation() + getXSpeed(), getYLocation() + getYSpeed(), false);

            float dx = Math.Abs(parentWindow.ship.getXLocation() - getXLocation());
            float dy = Math.Abs(parentWindow.ship.getYLocation() - getYLocation());

            float clearZoneX = parentWindow.fisheye.WindowBounds.Width * 2;
            float clearZoneY = parentWindow.fisheye.WindowBounds.Height * 2;

            if (dx > clearZoneX || dy > clearZoneY)
            {
                respawn();
            }

            updateWithRespectToPlayer();
        }

        /// <summary>
        /// Ross Higley     12/7/16
        /// Sets coordinates and speed of asteroid to somewhere just outside of the screen, 
        /// but not farther than 2 screens away.  
        /// </summary>
        private void respawn()
        {
            Random rand = GameManager.randomNumberGenerator;

            double angle = rand.NextDouble() * 360 * Constants.DEGREES_TO_RADIANS;
            float tempX = (float)(
                parentWindow.ship.getXLocation() + Math.Cos(angle) * rand.Next(parentWindow.fisheye.WindowBounds.Width, 
                parentWindow.fisheye.WindowBounds.Width * 2));
            float tempY = (float)(
                parentWindow.ship.getYLocation() + Math.Sin(angle) * rand.Next(parentWindow.fisheye.WindowBounds.Height, 
                parentWindow.fisheye.WindowBounds.Height * 2));

            setPosition(tempX, tempY, false);
            setSpeed((float)rand.NextDouble() * rand.Next(-2, 3),
                (float)rand.NextDouble() * rand.Next(-2, 3));
        }

        ///*************************************************************************************************
        ///<summary>Increases the current score by the set amount of points for a medium asteroid.</summary>
        ///*************************************************************************************************

        protected override void awardPoints()
        {
            // TODO: check for how accurate laser hit the asteroid, and spawn size, number, and speed of asteroids accordingly. 
            switch(asteroidSize)
            {
                case SIZE.SMALL:
                    Scores.addPoints(Scores.SMALL_ASTEROID_POINTS);
                    break;
                case SIZE.MEDIUM:
                    Scores.addPoints(Scores.MEDIUM_ASTEROID_POINTS);
                    if (!missleImpact)
                    {
                        GameManager.add2DEntity(new Asteroid(SIZE.SMALL, getXLocation(), getYLocation(), parentWindow));
                        GameManager.add2DEntity(new Asteroid(SIZE.SMALL, getXLocation(), getYLocation(), parentWindow));
                        GameManager.add2DEntity(new Asteroid(SIZE.SMALL, getXLocation(), getYLocation(), parentWindow));
                    }
                    break;
                case SIZE.LARGE:
                    Scores.addPoints(Scores.LARGE_ASTEROID_POINTS);
                    if (!missleImpact)
                    {
                        GameManager.add2DEntity(new Asteroid(SIZE.MEDIUM, getXLocation(), getYLocation(), parentWindow));
                        GameManager.add2DEntity(new Asteroid(SIZE.MEDIUM, getXLocation(), getYLocation(), parentWindow));
                    }
                    break;
            }
        }

        ///****************************************************************************************************************
        ///<summary>Creates a random location and velocity for the asteroid. If useStartLocation is true, then the provided
        ///start location values will be used for the initial position instead of randomly generating one.</summary>
        ///****************************************************************************************************************

        private void generateInitialValues(bool useStartLocation, float xLocation, float yLocation)
        {
            // spawn
            Random rand = GameManager.randomNumberGenerator;

            if(useStartLocation)
                setPosition(xLocation, yLocation, false);
            else
            {
                double angle = rand.NextDouble() * 360 * Constants.DEGREES_TO_RADIANS;
                float tempX = (float)Math.Cos(angle) * rand.Next(200, parentWindow.fisheye.WindowBounds.Width);
                float tempY = (float)Math.Sin(angle) * rand.Next(200, parentWindow.fisheye.WindowBounds.Height);
                setPosition(parentWindow.ship.getXLocation() + tempX,
                    parentWindow.ship.getYLocation() + tempY, false);
            }

            setSpeed( (float) (-5.0 + rand.NextDouble() * 10.0), (float) (-5.0 + rand.NextDouble() * 10.0));
            setRotationalSpeed( (float) (-5.0 + rand.NextDouble() * 10.0) );
        }

        protected override void updateWithRespectToPlayer()
        {
         
            if (blowUpAnimation == null)
            {
                if (playerShip.isCollidingWith(this))
                    playerShip.destroy();

                foreach (Entity2D laser in GameManager.getAll2DEntities())
                {
                    if (laser is Laser && ((Laser)laser).isFromPlayerShip() && laser.onlyonecollision == false && laser.isCollidingWith(this))
                    {
                        if (laser is Missle) missleImpact = true;
                        else missleImpact = false;
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