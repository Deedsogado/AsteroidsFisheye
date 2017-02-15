using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/**
 * GUI.cs
 * Autors: Rob Merrick
 * This class is used to draw an on screen Graphical User 
 * Interface (GUI) so that the user is better able to 
 * understand the gameplay.
 */

namespace RossHigleyProject7a
{
    class GUI
    {

        //Variable declarations
        private Font drawFont;
        private SolidBrush drawBrush;
        private StringFormat drawFormat;
        private GameManager refGameManager;
        
        ///***********************************************************
        ///<summary>Creates a new instance of the GUI class.</summary>
        ///***********************************************************

        public GUI(GameManager gm)
        {
            drawFont = new System.Drawing.Font("Copperplate Gothic Bold", 20);
            drawBrush = new System.Drawing.SolidBrush(System.Drawing.Color.White);
            drawFormat = new System.Drawing.StringFormat();
            refGameManager = gm;
        }

        ///*****************************************************************************************
        ///<summary>Draws the updated GUI to the screen based on the provided information.</summary>
        ///*****************************************************************************************

        public void drawGUI(int remainingLives, PaintEventArgs e)
        {
            drawString("Lives: " + remainingLives.ToString(), 10, 10, e);
            drawString("Score: " + Scores.getCurrentScore().ToString(), 10, drawFont.GetHeight() + 10, e);
            drawString("High Score: " + Scores.getHighScore().ToString(), 10, 2.0F * drawFont.GetHeight() + 10, e);
            drawString("Level: " + Level.getCurrentLevel().ToString(), 10, 3.0F * drawFont.GetHeight() + 10, e);
            drawString("Missles: " + Settings.missles.ToString(), 10, 5.0F * drawFont.GetHeight() + 10, e);
            string inertialDampeners;
                if(refGameManager._PlayerShip.inertialDampeners == true) inertialDampeners = "ON";
                else inertialDampeners = "OFF";
           drawString("Inertial Dampeners: " + inertialDampeners, 10, 4.0F * drawFont.GetHeight() + 10, e);
        }

        ///**************************************************************************************************************
        ///<summary>Draws the provided string to the xLocation and yLocation. xLocation and yLocation are relative to the
        ///form's top-left corner.</summary>
        ///**************************************************************************************************************

        private void drawString(string valueToDraw, float xLocation, float yLocation, PaintEventArgs e)
        {
           e.Graphics.DrawString(valueToDraw, drawFont, drawBrush, xLocation, yLocation, drawFormat);
        }

    }
}