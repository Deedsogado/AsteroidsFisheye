using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 * Debris.cs
 * Autors: Rob Merrick
 * This class is used to create a debris entity to be used in
 * the parallaxing background effect.
 */

namespace RossHigleyProject7a
{
    class Debris : Entity2D
    {

        //Static Variables
        private static float parallaxSpeed = 1.50F;
        private float zValue;

        ///************************************************************************************************************
        ///<summary>Creates a new instance of the Debris class. This instance will automatically generate random values
        ///for position, movement, and rotation.</summary>
        ///************************************************************************************************************
        
        public Debris(MainWindow window): base(window)
        {
            setGraphic(new Graphic("Debris.png"));
            spawn();
        }

        ///*****************************************************************************************
        ///<summary>Changes the debris location to reappear at the top of the game window.
        ///RMH - 12/8/16 - Now spawns to somwhere just outside of screen. </summary>
        ///*****************************************************************************************

        public void spawn()
        {
            Random rand = GameManager.randomNumberGenerator;
            double angle = rand.NextDouble() * 360 * Constants.DEGREES_TO_RADIANS;
            float tempX = (float)( Math.Cos(angle) * rand.Next(0, 
                parentWindow.fisheye.WindowBounds.Width * 3 / 2));
            float tempY = (float)(Math.Sin(angle) * rand.Next(0,
                parentWindow.fisheye.WindowBounds.Height * 3 / 2));
            tempX += parentWindow.ship.getXLocation();
            tempY += parentWindow.ship.getYLocation();

            setPosition(tempX, tempY, false);
            zValue = (float) (0.21F + rand.NextDouble() * 0.79F);
            setRotationalSpeed((float) (rand.NextDouble() * 360.0F));
            setScale(zValue, zValue);
        }

        public void respawn()
        {
            Random rand = GameManager.randomNumberGenerator;
            double angle = rand.NextDouble() * 360 * Constants.DEGREES_TO_RADIANS;
            float tempX = (float)( Math.Cos(angle) * rand.Next( parentWindow.fisheye.WindowBounds.Width ,
                parentWindow.fisheye.WindowBounds.Width * 3 / 2));
            float tempY = (float)( Math.Sin(angle) * rand.Next( parentWindow.fisheye.WindowBounds.Height,
                parentWindow.fisheye.WindowBounds.Height * 3 / 2));
            tempX += parentWindow.ship.getXLocation(); 
            tempY += parentWindow.ship.getYLocation(); 

            setPosition(tempX, tempY, false);
        }

        ///****************************************************************************************
        ///<summary>Returns true if the debris has moved below the drawing screen region.</summary>
        ///****************************************************************************************

        public bool isReadyToBeDestroyed()
        {
            float dx = Math.Abs(parentWindow.ship.getXLocation() - getXLocation());
            float dy = Math.Abs(parentWindow.ship.getYLocation() - getYLocation());

            float clearZoneX = parentWindow.fisheye.WindowBounds.Width * 3 / 2;
            float clearZoneY = parentWindow.fisheye.WindowBounds.Height * 3 / 2;

            return (dx > clearZoneX || dy > clearZoneY);
        }

        ///***********************************************************************************************
        ///<summary>Performs all actions that are required to update this instance of the class.</summary>
        ///***********************************************************************************************

        public override void update()
        {
            float tempSpeedX = Math.Min(Math.Abs(parentWindow.ship.getXSpeed()), Math.Abs(parentWindow.camera.getXSpeed()));
            float tempSpeedY = Math.Min(Math.Abs(parentWindow.ship.getYSpeed()), Math.Abs(parentWindow.camera.getYSpeed()));

            setPosition(getXLocation() + tempSpeedX * -zValue / 2, 
                getYLocation() + tempSpeedY * -zValue / 2, false);
            setRotation(getRotationalSpeed());
        }

        ///***************************************************************************************************************
        ///<summary>Sets the speed of the parallaxing effect. Pick a value between 1.0 and 30.0. Note that the change will
        ///be observed when the current debris moves below the screen height and is respawned.</summary>
        ///***************************************************************************************************************

        public static void setParallaxSpeed(float newSpeed)
        {
            if(newSpeed < 1.0 || newSpeed > 30.0F)
                throw new Exception("You probably don't want to set the parallax speed to that value. It would not look pretty.");

            parallaxSpeed = newSpeed;
        }

        ///*****************************************************
        ///<summary>Throws a NotImplementedExeption().</summary>
        ///*****************************************************

        public override void destroy()
        {
            throw new NotImplementedException();
        }

    }
}