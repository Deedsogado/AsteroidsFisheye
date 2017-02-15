using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/**
 * Entity.cs
 * Autors: Rob Merrick
 * This class has many of the basic properties that would be 
 * seen in any entity of the game. This includes properties 
 * such as position, speed, rotation, scale, and the graphic
 * associated with the entity. This class also includes a 
 * collision detection system.
 */

namespace RossHigleyProject7a
{
    public abstract class Entity2D
    {

        //Variable Declarations
        private bool entityIsShown;
        private float xLocation;
        private float yLocation;
        private float xSpeed;
        private float ySpeed;
        private float maximumSpeed;
        private float rotation;
        private float rotationalSpeed;
        private float scaleX;
        private float scaleY;
        private float collisionRadius;
        protected Graphic image;
        public MainWindow parentWindow;

         //this value is set so that only one object can be destroyed by this object
         //admittable meant only for lasers, but could also be used for asteroids provided 
         //multiplayer gets added
        public bool onlyonecollision = false;

        //added by Jacob Lehmer
        public bool isDestroyed = false;

        ///************************************************************************************************************
        ///<summary>Creates a new entity. Note that you cannot call this directly, only from a derived class.</summary>
        ///************************************************************************************************************

        public Entity2D(MainWindow window)
        {
            entityIsShown = true;
            xLocation = 0.0F;
            yLocation = 0.0F;
            xSpeed = 0.0F;
            ySpeed = 0.0F;
            maximumSpeed = 10.0F;
            rotation = 0.0F;
            rotationalSpeed = 0.0F;
            scaleX = 1.0F;
            scaleY = 1.0F;
            collisionRadius = 50.0F;
            parentWindow = window;
        }

        ///******************************************************************************************************************************
        ///<summary>Sets a boolean value to indicate whether or not this 2D entity should be drawn on the next draw frame call.</summary>
        ///******************************************************************************************************************************

        public void setEntityIsShown(bool showEntity)
        {
            entityIsShown = showEntity;
        }

        ///*************************************************************************************************************
        ///<summary>Sets the current screen position of the entity, correcting for screen wrapping if enabled.</summary>
        ///*************************************************************************************************************

        public void setPosition(float xLocation, float yLocation, bool wrappingEnabled)
        {
            if(wrappingEnabled)
                wrapEntityOnScreen(ref xLocation, ref yLocation);

            this.xLocation = xLocation;
            this.yLocation = yLocation;
        }

        ///********************************************************
        ///<summary>Sets the current speed of the entity.</summary>
        ///********************************************************

        public void setSpeed(float xSpeed, float ySpeed)
        {
            this.xSpeed = xSpeed;
            this.ySpeed = ySpeed;
        }

        ///**************************************************************************************************************
        ///<summary>Sets a value to indicate the maximum value that both xSpeed and ySpeed can take on. A value of ten is
        ///recommended for larger entities, and a value of twenty is recommended for smaller entities.</summary>
        ///**************************************************************************************************************

        public void setMaximumSpeed(float maximumSpeed)
        {
            if(maximumSpeed < 0.0)
                throw new Exception("Please provide a non-negative value when settings the maximum speed of an entity.");

            this.maximumSpeed = maximumSpeed;
        }

        ///***************************************************************************************************************
        ///<summary>Sets the current local yaw rotation of the entity, adjusted to be between 0 and 360 degrees.</summary>
        ///***************************************************************************************************************

        public void setRotation(float rotation)
        {
            while(rotation < 0.0 || rotation >= 360.0)
            {
                if(rotation < 0)
                    rotation += 360.0F;

                if(rotation >= 360.0)
                    rotation -= 360.0F;
            }

            this.rotation = rotation;
        }

        ///*****************************************************************************
        ///<summary>Sets the current local yaw rotational speed of the entity.</summary>
        ///*****************************************************************************

        public void setRotationalSpeed(float rotationalSpeed)
        {
            this.rotationalSpeed = rotationalSpeed;
        }

        ///********************************************************
        ///<summary>Sets the current scale of the entity.</summary>
        ///********************************************************

        public void setScale(float scaleX, float scaleY)
        {
            if(Math.Abs(scaleX) < 0.000001 || Math.Abs(scaleY) < 0.000001)
                throw new Exception("You cannot set the entity's scaleX or scaleY to zero. This will destroy the graphical data.");

            this.scaleX = scaleX;
            this.scaleY = scaleY;
        }

        ///*******************************************************************
        ///<summary>Sets the image to be associated with the entity.</summary>
        ///*******************************************************************

        public void setGraphic(Graphic graphic)
        {
            image = graphic;
        }

        ///****************************************************************************************************************
        ///<summary>Sets the dimensions of the circle (measured in pixels) used to calculate collision detection.</summary>
        ///****************************************************************************************************************

        public void setCollisionRadius(float radius)
        {
            collisionRadius = radius;
        }

        ///*******************************************************************************************************************
        ///<summary>Draws the graphic associated with this class on the screen based on the current location values.
        /// RMH - 12/5/16 -  Fisheye Logic added.  
        /// </summary>
        ///*******************************************************************************************************************

        public virtual void draw(PaintEventArgs e, bool useFishEye)
        {
            if (entityIsShown)
            {
                float dx;
                float dy;
                if (useFishEye)
                {
                    dx = xLocation - parentWindow.camera.xLocation;
                    dy = yLocation - parentWindow.camera.yLocation;
                    dx = parentWindow.fisheye.AbsToScreenX(dx);
                    dy = parentWindow.fisheye.AbsToScreenY(dy);
                }
                else
                {
                    dx = xLocation - parentWindow.camera.xLocation + parentWindow.fisheye.ScreenCenter.X;
                    dy = yLocation - parentWindow.camera.yLocation + parentWindow.fisheye.ScreenCenter.Y;
                }

                if (image != null)
                {
                    image.draw(dx, dy, getRotation(), getScaleX(), getScaleY(), e);
                }
                else
                {
                    e.Graphics.FillEllipse(Brushes.Yellow, dx - 5, dy - 5, 10, 10);
                }

            }
        }

        ///*******************************************************************************************
        ///<summary>Returns true if the 2D entity will be drawn on the next draw frame call.</summary>
        ///*******************************************************************************************

        public bool isEntityShown()
        {
            return entityIsShown;
        }

        ///********************************************************************************************************************************
        ///<summary>Returns true if this Entity2D is intersecting the provided otherEntity based on the current collision radius.</summary>
        ///JPL-> mod-=> added in between frame collisions
        ///             see after action report on between frame collisions
        ///********************************************************************************************************************************

        public bool isCollidingWith(Entity2D target)
        {
            float rtwl = (float)Math.Sqrt((target.xLocation - xLocation) * (target.xLocation - xLocation) + (target.yLocation - yLocation) * (target.yLocation - yLocation));
            float beta =  (float)Math.Atan2((target.yLocation - yLocation), target.xLocation - xLocation) - (float)Math.Atan2(ySpeed, xSpeed) ;
            float vp = (float)Math.Sqrt(this.xSpeed * this.xSpeed + this.ySpeed * this.ySpeed);
            float ocr = this.getCollisionRadius();
            float tcr = target.getCollisionRadius();


            if (
                beta >= -Constants.PI_OVER_TWO &&
                beta <= Constants.PI_OVER_TWO &&
                Math.Abs(rtwl * Math.Cos(beta)) < vp &&
                rtwl < vp &&
                Math.Abs(rtwl * Math.Sin(beta)) - ocr < tcr ||
                rtwl < target.getCollisionRadius()
                ) { onlyonecollision = true;return true;  }
            else return false;

        }

         /******
          * Jacob Lehmer 4/21/15
          * This mathy little method determines if a target was hit inbetween frames
          * *****/
        private bool targetHitBetweenFrames(float tarx, float tary)
        {

            {
                return true;
            }
           // else return false;
        }

        ///***********************************************************************
        ///<summary>Returns the current screen x position of the entity.
        /// RMH - 12/5/16 - Now returns the current absolute x position of the entity. </summary>
        ///***********************************************************************

        public float getXLocation()
        {
            return xLocation;
        }

        ///***********************************************************************
        ///<summary>Returns the current screen y position of the entity.
        ///RMH - 12/5/16 - Now returns the current absolute y position of the entity. </summary>
        ///***********************************************************************

        public float getYLocation()
        {
            return yLocation;
        }

        ///*************************************************************
        ///<summary>Returns the current x speed of the entity.</summary>
        ///*************************************************************

        public float getXSpeed()
        {
            return xSpeed;
        }

        ///*************************************************************
        ///<summary>Returns the current y speed of the entity.</summary>
        ///*************************************************************

        public float getYSpeed()
        {
            return ySpeed;
        }

        ///************************************************************************
        ///<summary>Returns the current local yaw rotation of the entity.</summary>
        ///************************************************************************

        public float getRotation()
        {
            return rotation;
        }

        ///********************************************************************************
        ///<summary>Returns the current local yaw rotational speed of the entity.</summary>
        ///********************************************************************************

        public float getRotationalSpeed()
        {
            return rotationalSpeed;
        }

        ///*************************************************************
        ///<summary>Returns the maximum speed for this entity.</summary>
        ///*************************************************************

        public float getMaximumSpeed()
        {
            return maximumSpeed;
        }

        ///*************************************************************
        ///<summary>Returns the current x scale of the entity.</summary>
        ///*************************************************************

        public float getScaleX()
        {
            return scaleX;
        }

        ///*************************************************************
        ///<summary>Returns the current y speed of the entity.</summary>
        ///*************************************************************

        public float getScaleY()
        {
            return scaleY;
        }

        ///********************************************************************************************
        ///<summary>Returns the radius of the collision detection circle set for this entity.</summary>
        ///********************************************************************************************

        public float getCollisionRadius()
        {
            return collisionRadius;
        }

        ///***********************************************************************************************************************
        ///<summary>Sets the entity's rotational value so that the "top" of the entity is pointed at the provided point.</summary>
        ///***********************************************************************************************************************

        public void pointEntity(float pointX, float pointY, bool isFollowingMouse)
        {

            if (isFollowingMouse)
            {
               
                float screenMouseDeltaX = pointX - getXLocation();
                float screenMouseDeltaY = pointY - getYLocation();
                float newRotation = (float)(Math.Atan(screenMouseDeltaY / screenMouseDeltaX) * Constants.RADIANS_TO_DEGREES + 90);
                if (pointX > getXLocation())
                    setRotation(newRotation);
                else
                    setRotation(newRotation + 180);
            }
            else
            {
                float currentX = (float)(getXLocation() + image.getWidth(false) / 2.0F); 
                float currentY = (float)(getYLocation() + image.getHeight(false) / 2.0F); 

                if (Math.Abs(pointX - currentX) < 0.01)
                    return;

                float newRotation = (float)(Math.Atan((pointY - currentY) / (pointX - currentX)) * Constants.RADIANS_TO_DEGREES + 90);

                if (pointX > currentX)
                    setRotation(newRotation);
                else
                    setRotation(newRotation + 180);
            }
        }

        ///****************************************************************************
        ///<summary>Returns the Graphic instance associated with this entity.</summary>
        ///****************************************************************************
        
        protected Graphic getGraphic()
        {
            return image;
        }

        ///*******************************************************************************************************************
        ///<summary>If the entity moves off the screen, this method will wrap it on the opposite side of the screen.</summary>
        ///*******************************************************************************************************************

        private void wrapEntityOnScreen(ref float xLocation, ref float yLocation)
        {
            float width = parentWindow.fisheye.WindowBounds.Width;
            float height = parentWindow.fisheye.WindowBounds.Height;

            if(xLocation > width)
                xLocation =  xLocation - width;

            if(xLocation < 0)
                xLocation = xLocation + width;

            if(yLocation > height)
                yLocation = yLocation - height;

            if(yLocation < 0)
                yLocation =  yLocation + height;
        }

        //This method should be called once per game loop on every derived class.
        public abstract void update();
        //This method should be called when the entity is destroyed.
        public abstract void destroy();


        //this will cause the different asteroids that hit eachother to bouce off
        public void Bounce()
        {
            throw new System.NotImplementedException();
        }
    }
}