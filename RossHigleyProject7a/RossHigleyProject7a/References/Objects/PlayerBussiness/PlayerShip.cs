using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/**
 * PlayerShip.cs
 * Autors: Rob Merrick
 * This class holds all of the information regarding the player's
 * spaceship.
 */

namespace RossHigleyProject7a
{
    public class PlayerShip : Entity2D
    {

        //Variable Declarations
        //Jacob Lehmer added the new fire effects
        private bool startedThrusterSound;
        private SpecialEffects blowUpAnimation;
        private Graphic fire1;
        private Graphic fire2;
        private Graphic normal;

        private Graphic fire1stor;
        private Graphic fire2stor;
        private Graphic normalstor;

        private Graphic p51f1;
        private Graphic p51f2;
        private Graphic p51;

        //added by Jacob Lehmer
        private bool animationswitch = false;
        public bool justwarped = false;
        public bool inertialDampeners = false;
        public int laserTimer;
        public int liveThroughStart;

        //Static Variables
        private static Sound thrusterSound;
        private static Sound p51zoom;
        private static float shiposx;
        private static float shiposy;
        private static float shiprot;
        private static float shipspeedx;
        private static float shipspeedy;

        public static bool targetDestroyed = false;

        /*****
         * Jacob Lehmer
         * 4/23/15
         * These are the properties for the ship position and rotations
         * *****/
        public static float Shippox { get { return shiposx; } }
        public static float Shipposy { get { return shiposy; } }
        public static float Shiprot { get { return shiprot; } }
        public static float Shipspeedx { get { return shipspeedx; } }
        public static float ShipSpeedy { get { return shipspeedy; } }


        ///******************************************************************
        ///<summary>Creates a new instance of the PlayerShip class.</summary>
        ///******************************************************************

        public PlayerShip(MainWindow window): base(window)
        {
            startedThrusterSound = false;
            laserTimer = 0;
            normalstor = new Graphic("PlayerShip.png");
            normal = normalstor;
            this.setGraphic(normal);
            
            // RMH - 12/5/16 - Spawn location changed to account for fisheye. Also turned off wrapping. 
            float positionX = 0; // (GameManager.canvas.ClipBounds.Width + GameManager.canvasOrigin.X) / 2.0F;
            float positionY = 0; // (GameManager.canvas.ClipBounds.Height + GameManager.canvasOrigin.Y) / 2.0F;
            setPosition(positionX, positionY, false); // true);

            setMaximumSpeed(10.0F);
            thrusterSound = new Sound("Thruster.wav");
            p51zoom = new Sound("b29_flyby.wav");
            fire1stor = new Graphic("PlayerShipFire1.png");
            fire2stor = new Graphic("PlayerShipFire2.png");

            p51 = new Graphic("p51.png");
            p51f1 = new Graphic("p51Fire1.png");
            p51f2 = new Graphic("p51Fire2.png");


            fire1 = fire1stor;
            fire2 = fire2stor;
            liveThroughStart = 30;
        }

        ///***********************************************************************************************
        ///<summary>Performs all actions that are required to update this instance of the class.</summary>
        ///***********************************************************************************************

        public override void update()
        {
            if (!MainWindow.p51mustang)
            {
                normal = normalstor;
                fire1 = fire1stor;
                fire2 = fire2stor;
            }
            else
            {
                normal = p51;
                fire2 = p51f2;
                fire1 = p51f1;
            }


            shiposx = getXLocation();
            shiposy = getYLocation();
            shiprot = (float)((getRotation() - 90) * Constants.DEGREES_TO_RADIANS);
            shipspeedx = getXSpeed();
            shipspeedy = getYSpeed();
            if (blowUpAnimation == null)
            {
                if (Settings.getUseMouseControls())
                    controlShipWithMouse();
                else
                    controlShipWithKeyboard();

                setPosition(getXLocation() + getXSpeed(), getYLocation() + getYSpeed(), false); // true);

                foreach (Entity2D laser in GameManager.getAll2DEntities())
                {
                    if (laser is Laser && !((Laser)laser).isFromPlayerShip() && laser.onlyonecollision == false && laser.isCollidingWith(this))
                    {

                        laser.destroy();

                        if (liveThroughStart <= 0)
                        {
                            this.destroy();
                        }

                    }
                }
            }
            else
            {
                int currentFrame = blowUpAnimation.drawExplosion(getXLocation(), getYLocation(), getRotation(), getScaleX(), getScaleY());

                if (currentFrame >= SpecialEffects.EXPLOSION_KEY_FRAME)
                    this.setEntityIsShown(false);

                if (currentFrame >= SpecialEffects.EXPLOSION_FRAME_COUNT + 10)
                    parentWindow.gameManager.killPlayer();
            }
        }

        ///******************************************************************
        ///<summary>Destroys the ship after it is no longer needed.</summary>
        ///******************************************************************

        public override void destroy()
        {
            if (blowUpAnimation == null)
            {
                blowUpAnimation = new SpecialEffects(parentWindow);
                blowUpAnimation.setUpExplosion(true);
            }
        }

        ///****************************************************
        ///<summary>Controls the ship with the mouse.</summary>
        ///****************************************************

        private void controlShipWithMouse()
        {
            // RMH - 12/5/16 changed mouse from using screen coordinates, to coordinates in universe.  
            float mouseX = parentWindow.PointToClient(System.Windows.Forms.Cursor.Position).X;
            float mouseY = parentWindow.PointToClient(System.Windows.Forms.Cursor.Position).Y;

            mouseX = parentWindow.fisheye.ScreenToAbsX(mouseX) + parentWindow.camera.getXLocation();
            mouseY = parentWindow.fisheye.ScreenToAbsY(mouseY) + parentWindow.camera.getYLocation();
            
        //    pointEntity(mouseX + 23, mouseY + 23); // mouseX + 20, mouseY); // why follow 20 off mouse? 
            pointEntity(mouseX, mouseY, true);

            if (!justwarped)
            {
                setGraphic(normal);
            }
            if (UserControls.getLeftMouseDown())
            {
                switchFlame();
                accelerateShip();
            }
            else if (inertialDampeners)
            {
                decelerateShip();
            }


            //with a rework of the fire rate, the fireing is only capble when the game manager says so
            //via subtracting one from the laser timer
            if (UserControls.getRightMouseDown())
            {
                if (laserTimer <= 0)
                {

                    if (!MainWindow.shotgunblast) fireLaser();
                    else fireLaserShotgun();

                    //this little if statement pair is for punishment for those who use inertial dampeners
                    //and rewards for those that dont with fire rate
                    if (!MainWindow.shotgunblast)
                    {
                        if (inertialDampeners)
                            laserTimer = (int)(Settings.fireRate * Settings.fireRatePeanalty);
                        else
                            laserTimer = (int)Settings.fireRate;
                    }
                    else laserTimer = 3;
                }
            }

        }

        ///*******************************************************
        ///<summary>Controls the ship with the keyboard.</summary>
        ///*******************************************************

        private void controlShipWithKeyboard()
        {

        }

        /*******
         * Jacob Lehmer
         * 4/19/15
         * this toggles inertial dampeners
         * ******/
        public void toggleInertialDampeners()
        {
            if (inertialDampeners)
            {
                inertialDampeners = false;
            }
            else inertialDampeners = true;
        }

        /******
         * Jacob Lehmer
         * 4/12/15
         * FireEffects
         * *******/
        private void switchFlame()
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

        ///************************************************************************************************************
        ///<summary>Increases the ship speed in the direction it is pointed unless it's at its maximum speed.</summary>
        ///************************************************************************************************************

        private void accelerateShip()
        {

            if (!startedThrusterSound)
            {
                if (!MainWindow.p51mustang) thrusterSound.playSound(false); else p51zoom.playSound(false);
                startedThrusterSound = true;
            }

            float correctedRotation = (float)((getRotation() - 90) * Constants.DEGREES_TO_RADIANS);
            float xSpeed = (float)(getXSpeed() + Math.Cos(correctedRotation) * Settings.acceleration);
            float ySpeed = (float)(getYSpeed() + Math.Sin(correctedRotation) * Settings.acceleration);

           // float maximumSpeed = getMaximumSpeed();

            /****this is left in the code for legacy reasons, but will be commented out
             * JPL -> THERE IS NO TOP SPEED IN SPACE
            if(xSpeed > maximumSpeed)
                 xSpeed = maximumSpeed;
            ******/

            //if (xSpeed < -maximumSpeed)
            //    xSpeed = -maximumSpeed;


            /****this is left in the code for legacy reasons, but will be commented out
             * JPL -> THERE IS NO TOP SPEED IN SPACE
                if(ySpeed > maximumSpeed)
                  ySpeed = 10.0F;
            ******/


            //if (ySpeed < -maximumSpeed)
            //    ySpeed = -maximumSpeed;

            setSpeed(xSpeed, ySpeed);
        }

        ///**************************************************************************************************************
        ///<summary>Decreases the ship speed unless it's close to zero, in which case the speed is set to zero.</summary>
        ///JPL-Rework in the spirit of using rockets, also might add a better inertial dampener system to the upgrade system
        ///**************************************************************************************************************

        private void decelerateShip()
        {
            startedThrusterSound = false;

            float xSpeed = getXSpeed();
            float ySpeed = getYSpeed();

            if (xSpeed > 0)
            {
                if (Math.Abs(xSpeed - Settings.inertialDampening) > Settings.inertialDampening * 2) xSpeed -= Settings.inertialDampening;
                else xSpeed = 0;
            }
            else
            {
                if (Math.Abs(xSpeed + Settings.inertialDampening) > Settings.inertialDampening * 2) xSpeed += Settings.inertialDampening;
                else xSpeed = 0;
            }


            if (ySpeed > 0)
            {
                if (Math.Abs(ySpeed - Settings.inertialDampening) > Settings.inertialDampening * 2) ySpeed -= Settings.inertialDampening;
                else ySpeed = 0;
            }
            else
            {
                if (Math.Abs(ySpeed + Settings.inertialDampening) > Settings.inertialDampening * 2) ySpeed += Settings.inertialDampening;
                else ySpeed = 0;
            }


            setSpeed(xSpeed, ySpeed);
        }

        ///****************************************************************************************************************************
        ///<summary>Creates a new laser to add to the lasers list, setting the initial fields so that it fires from the ship.</summary>
        ///****************************************************************************************************************************

        private void fireLaser()
        {

            Scores.subtractPoints(Scores.FIRE_BULLET_POINTS); //Nay for pity points! JPL modified
            Laser laser = new Laser(parentWindow, true);
            laser.playLaserSound();
            laser.setRotation(getRotation());
            float correctedRotation = (float)((getRotation() - 90) * Constants.DEGREES_TO_RADIANS);
            float xSpeed = (float)(Math.Cos(correctedRotation) * Settings.projectileSpeed);
            float ySpeed = (float)(Math.Sin(correctedRotation) * Settings.projectileSpeed);
            laser.setCollisionRadius(25);
            laser.setSpeed(xSpeed, ySpeed);
            laser.setPosition(getXLocation(), getYLocation(), false);

            GameManager.add2DEntity(laser);

        }



        /*******
         * Jacob Lehmer
         * 4/27/15
         * This fires the laser in a shotgun pattern
         * ********/

        private void fireLaserShotgun()
        {
            Laser laser = new Laser(parentWindow, true);
            for (int i = 0; i <= 10; i++)
            {
                Scores.subtractPoints(Scores.FIRE_BULLET_POINTS); //Nay for pity points! JPL modified
                laser = new Laser(parentWindow, true);

                laser.setRotation(getRotation() - 30F + i * 5);
                float correctedRotation = (float)((getRotation() - 90 - 30F + i * 5) * Constants.DEGREES_TO_RADIANS);
                float xSpeed = (float)(Math.Cos(correctedRotation) * Settings.projectileSpeed);
                float ySpeed = (float)(Math.Sin(correctedRotation) * Settings.projectileSpeed);
                laser.setCollisionRadius(25);
                laser.setSpeed(xSpeed, ySpeed);
                laser.setPosition(getXLocation(), getYLocation(), false);

                GameManager.add2DEntity(laser);
            }
            laser.playLaserSound();
        }

        /*******
         * Jacob Lehmer
         * 4/21/15
         * This method will fire a missle, its permission levels are set in such a way
         * so that it can be called from the main form under a keystroke
         * ******/
        public void fireMissle()
        {
            if (Settings.missles > 0 && isEntityShown())
            {
                Settings.missles--;
                Missle missle = new Missle(parentWindow, true);
                

                missle.playMissleSound();
                float xSpeed = (float)(10 * Math.Cos(shiprot));
                float ySpeed = (float)(10 * Math.Sin(shiprot));
                missle.setSpeed(xSpeed, ySpeed);
                missle.setRotation(shiprot * (float)Constants.RADIANS_TO_DEGREES + 90);
                missle.setPosition(shiposx, shiposy, false);
                GameManager.add2DEntity(missle);
            }
        }

        public override void draw(PaintEventArgs e, bool useFishEye)
        {
            if (isEntityShown())
            {
                float dx = getXLocation() - parentWindow.camera.getXLocation();
                float dy = getYLocation() - parentWindow.camera.getYLocation();

                dx = parentWindow.fisheye.AbsToScreenX(dx);
                dy = parentWindow.fisheye.AbsToScreenY(dy);

                if (image != null)
                {
                    image.draw(dx, dy, getRotation(), getScaleX(), getScaleY(), e);
                }
                else
                {
                    e.Graphics.FillEllipse(Brushes.ForestGreen, dx - 8, dy - 8, 16F, 16F);
                }




                // draw line from ship to mouse
                float screenMouseX = System.Windows.Forms.Cursor.Position.X;
                float screenMouseY = System.Windows.Forms.Cursor.Position.Y;
                Point tempMouse = parentWindow.PointToClient(new Point((int)screenMouseX, (int)screenMouseY));
                screenMouseX = tempMouse.X;
                screenMouseY = tempMouse.Y;
                //float screenShipX = getXLocation() - parentWindow.camera.getXLocation() + parentWindow.fisheye.ScreenCenter.X;
                //float screenShipY = getYLocation() - parentWindow.camera.getYLocation() + parentWindow.fisheye.ScreenCenter.Y;

                e.Graphics.DrawLine(Pens.HotPink, screenMouseX, screenMouseY, dx, dy);
            }
        }


    }
}