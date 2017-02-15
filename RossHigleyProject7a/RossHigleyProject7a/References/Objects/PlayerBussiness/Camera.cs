/* Ross Higley  12/7/16
This file contains the Camera class, which is an invisible object that moves around 
in the universe following the ship. All visible objects are drawn on the screen by grabbing 
their distance from the Camera, and passing that into the parentWindow.fisheyeViewAdapter. 
for example: float shipsScreenLocationX = parentWindow.fisheyeViewAdapter.convertAbsoluteXToScreen(ship.Location.X - Camera.X);

 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RossHigleyProject7a
{
    public class Camera :Entity2D
    {
        public bool ShouldChaseShip = false;
        private float initialDistanceX = 0;
        private float initialDistanceY = 0;

        /// <summary>
        /// Ross Higley     12/7/16
        /// Constructor. Creates new Camera in the center of the universe. 
        /// </summary>
        public Camera(MainWindow window):base(window)
        {
            setPosition(0, 0, false);
            setSpeed(0, 0);
        }

        /// <summary>
        /// Ross Higley     12/14/16
        /// Calculates how far the camera is from the ship when it first begins to follow the ship. 
        /// These figures are used by customSinX() and customSinY().
        /// </summary>
        public void calculateInitialDistance()
        {
            ShouldChaseShip = true;
            float deltaX = parentWindow.ship.getXLocation() - getXLocation();
            float deltaY = parentWindow.ship.getYLocation() - getYLocation();
            initialDistanceX = Math.Abs(deltaX);
            initialDistanceY = Math.Abs(deltaY);
        }

        /// <summary>
        /// Ross Higley     12/7/16
        /// moves the Camera towards the ship if the ship has left the bounding box, or just coast along with the ship if
        /// the ship is still inside the bounding box. 
        /// </summary>
        public void moveTowardsShip()
        {
            PlayerShip ship = parentWindow.ship;
            float deltaX = ship.getXLocation() - getXLocation();
            float deltaY = ship.getYLocation() - getYLocation(); 

            if (ShouldChaseShip)
            {
                // move camera to ship. 

                double theta = Math.Atan2(deltaY, deltaX);

                double cosX = Math.Cos(theta);
                double sinY = Math.Sin(theta);

                float speedX = getReasonableSpeedX(deltaX);
                float speedY = getReasonableSpeedY(deltaY);

                float trueDeltaX = (float)((speedX * cosX) + ship.getXSpeed() );
                float trueDeltaY = (float)((speedY * sinY) + ship.getYSpeed() );

                this.setPosition(getXLocation() + trueDeltaX, getYLocation() + trueDeltaY, false);

                if (Math.Abs(deltaX) < (1) && Math.Abs(deltaY) < (1)) // is camera near ship?   
                {
                    ShouldChaseShip = false;
                    setSpeed(parentWindow.ship.getXSpeed(), parentWindow.ship.getYSpeed());
                   
                }
            }
            else
            {
                // let camera coast.
                setPosition(getXLocation() + getXSpeed(), getYLocation() + getYSpeed(), false);
                
              
                if (Math.Abs(deltaX) > (parentWindow.fisheye.MoveBoxSize.X / 2) || Math.Abs(deltaY) > (parentWindow.fisheye.MoveBoxSize.Y / 2)) // is ship far away from camera? 
                {
                    calculateInitialDistance(); 
                }
            }
        }

        /// <summary>
        /// Ross Higley     12/7/16
        /// Moves the camera. 
        /// </summary>
        public override void update()
        {
            moveTowardsShip();
        }
        
        #region speed Calculations
        /// <summary>
        /// Ross Higley     12/7/16
        /// Figures out camera's speed in X-direction. 
        /// </summary>
        /// <param name="distance"></param>
        /// <returns></returns>
        private float getReasonableSpeedX(float distance)
        {
            distance = Math.Abs(distance);
            return customSinX(distance);
        }

        /// <summary>
        /// Ross Higley     12/7/16
        /// Figures out camera's speed in Y-direction. 
        /// </summary>
        /// <param name="distance"></param>
        /// <returns></returns>
        private float getReasonableSpeedY(float distance)
        {
            distance = Math.Abs(distance);
            return customSinY(distance);
        }

        /// <summary>
        /// Ross Higley     12/7/16
        /// This is similar to Math.Sin(), but is not periodic. Actually, it's more related to the normal distribution function. 
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private float customSinX(double x)
        {
            PlayerShip ship = parentWindow.ship;
            double A = Math.Abs(ship.getXSpeed());
            double E = Math.E;
            double W = initialDistanceX / Math.PI;  // initialDistanceBetweenCameraAndShip;
            if (W <= 0)
            {
                W = 1;
            }
            double inside = (x - Math.PI * (W / 2)) / (W / 2);

            double outside = inside * inside * -0.5;
            double result = A * Math.Pow(E, outside);

            return (float)(result + 0.5);
        }


        /// <summary>
        /// Ross Higley     12/7/16
        /// This is similar to Math.Sin(), but is not periodic. Actually, it's more related to the normal distribution function.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private float customSinY(double x)
        {
            PlayerShip ship = parentWindow.ship;
            double A = Math.Abs(ship.getYSpeed());
            double E = Math.E;
            double W = initialDistanceY / Math.PI;  // initialDistanceBetweenCameraAndShip;
            if (W <= 0)
            {
                W = 1;
            }
            double inside = (x - Math.PI * (W / 2)) / (W / 2);

            double outside = inside * inside * -0.5;
            double result = A * Math.Pow(E, outside);

            return (float)(result + 0.5);
        }
        #endregion


        /// <summary>
        /// Ross Higley     12/7/16
        /// This method is empty because the camera is supposed to be invisible. 
        /// </summary>
        /// <param name="e"></param>
        /// <param name="useFishEye"></param>
        public override void draw(PaintEventArgs e, bool useFishEye)
        {
            //// draw Camera on universe
            //float dxcamera = parentWindow.fisheye.AbsToScreenX(parentWindow.camera.getXLocation());
            //float dycamera = parentWindow.fisheye.AbsToScreenY(parentWindow.camera.getYLocation());
            //e.Graphics.DrawRectangle(Pens.LawnGreen, dxcamera - 10, dycamera - 10, 20, 20);

            //// draw camera bounds 
            //e.Graphics.DrawEllipse(
            //    Pens.Coral, dxcamera - (parentWindow.fisheye.MoveBoxSize.X / 2),
            //    dycamera - (parentWindow.fisheye.MoveBoxSize.Y / 2),
            //    parentWindow.fisheye.MoveBoxSize.X,
            //    parentWindow.fisheye.MoveBoxSize.Y);
            //e.Graphics.DrawEllipse(
            //    Pens.Coral, dxcamera - (parentWindow.fisheye.MoveBoxSize.X / 4),
            //    dycamera - (parentWindow.fisheye.MoveBoxSize.Y / 4),
            //    parentWindow.fisheye.MoveBoxSize.X / 2,
            //    parentWindow.fisheye.MoveBoxSize.Y / 2);


            //// draw distance / speed sin waves (x-axis)
            //for (int i = 0; i < parentWindow.fisheye.MoveBoxSize.X / 2; i += 2)
            //{
            //    float x = i; // getReasonableSpeedX(i);
            //    float y = getReasonableSpeedX(i);
            //    e.Graphics.FillEllipse(Brushes.DeepPink, x * 4, y * 12, 5, 5);
            //}
            //float wx = Math.Abs(parentWindow.ship.getXLocation() - getXLocation());
            //float wy = getReasonableSpeedX(wx);
            //e.Graphics.FillEllipse(Brushes.DeepPink, wx * 4, wy * 12, 12, 12);



            //// draw distance / speed sin waves (y-axis) 
            //for (int i = 0; i < parentWindow.fisheye.MoveBoxSize.Y / 2; i += 2)
            //{
            //    float x = getReasonableSpeedY(i);
            //    float y = i; // getReasonableSpeedY(i);
            //    e.Graphics.FillEllipse(Brushes.DeepPink, x * 12, y * 4, 5, 5);
            //}
            //float hy = Math.Abs(parentWindow.ship.getYLocation() - getYLocation());
            //float hx = getReasonableSpeedY(hy);
            //e.Graphics.FillEllipse(Brushes.DeepPink, hx * 12, hy * 4, 12, 12);

        }
        public override void destroy()
        {
            throw new NotImplementedException();
        }
    }
}
