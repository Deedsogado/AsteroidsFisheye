using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace RossHigleyProject7a
{

    /*****
     * Jacob Lehmer
     * 4/012/15
     * STUB: This is going to be The targeting system
     * *****/

    public class TargetingSystem
    {
        GameManager refGameManager;
        public static object targetedItem;
        private PlayerShip playerShipRef;

        /*******
         * Targeting mathematics
         * ******/
        //naming conventions//r= rbar v= vbar t= target w= with respect to s= player ship
        //naming conventions//p= projectile; i,j= respective directions in x or y; c= component
        float rtws;
       
        float vp;
        
        float time;
        float rtwsci;
        float rtwscj;
        float vtwsci;
        float vtwscj;
        Point aimingPoint;

        MainWindow parentWindow; 
        
        /*****
         * Jacob Lehmer
         * 4/15/15
         * This is the property for The targeted Item
         ******/
        public object TargetedItem
        {
            get { return targetedItem; }
            set { targetedItem = value; }
        }

        //player ship reference
        public object PlayerShipRef
        {
            set { playerShipRef = (PlayerShip)value; }
        }


        /****
         * Jacob Lehmer
         * 4/15/15
         * This is the main start of the targeting system
         * *****/
        public TargetingSystem(object managerIn, object playerShip, MainWindow window)
        {

            refGameManager = (GameManager)managerIn;
            playerShipRef = (PlayerShip)playerShip;
            parentWindow = window;
        }


        /****
         * Jacob Lehmer
         * 4/19/15
         * This draws the reticule to the screen
         * RMH - 12/5/16 changed to draw reticle in universe, rather than screen. 
         ******/
        public void Target(PaintEventArgs e)
        {
            if((PlayerShip.targetDestroyed == false)&& ((Entity2D)targetedItem).isDestroyed == false)
            {
                findTime();
                findPoint();

                PointF targetPoint = new PointF(
                    ((Entity2D)targetedItem).getXLocation() - parentWindow.camera.getXLocation(), 
                    ((Entity2D)targetedItem).getYLocation() - parentWindow.camera.getYLocation());

                targetPoint = parentWindow.fisheye.AbsToScreen(targetPoint);

          //      e.Graphics.FillEllipse(Brushes.Purple, targetPoint.X-10, targetPoint.Y-10, 20, 20);


                PointF tempAimingPoint = new PointF(
                    aimingPoint.X - parentWindow.camera.getXLocation() ,
                    aimingPoint.Y - parentWindow.camera.getYLocation() );

                tempAimingPoint = parentWindow.fisheye.AbsToScreen(tempAimingPoint);
              
                e.Graphics.DrawLine(Pens.Lime, targetPoint, tempAimingPoint);

                e.Graphics.DrawEllipse(Pens.Red, tempAimingPoint.X - 10, tempAimingPoint.Y - 10, 20, 20);

            }
        }

        /*******
         * Jacob Lehmer
         * 4/19/15
         * This method will find the time that will be needed for 
         * the projecttile and target to hit
         * ******/
        private void findTime()
        {
            
            vp = Settings.projectileSpeed;
            rtwsci = ((Entity2D)targetedItem).getXLocation() - playerShipRef.getXLocation();
            rtwscj = ((Entity2D)targetedItem).getYLocation() - playerShipRef.getYLocation();
            rtws = (float) Math.Sqrt(rtwsci*rtwsci + rtwscj*rtwscj);

            vtwsci = ((Entity2D)targetedItem).getXSpeed();
            vtwscj = ((Entity2D)targetedItem).getYSpeed();

            time = rtws / vp;
        }

        /******
         * Jacob Lehmer
         * 4/19/15
         * This method will assign the point based on the time for the projectile to reach it
         ******/
        private void findPoint()
        {
            aimingPoint = new Point(
                (int)((Entity2D)targetedItem).getXLocation(), 
                (int)((Entity2D)targetedItem).getYLocation());
            aimingPoint.X = aimingPoint.X + (int)( time * vtwsci);
            aimingPoint.Y = aimingPoint.Y + (int)( time * vtwscj);

        }
    }
}
