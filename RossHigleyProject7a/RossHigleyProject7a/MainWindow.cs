using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO;

/**
 * MainWindow.cs
 * Autors: Rob Merrick
 * This is the main class which invokes the entire game set of
 * methods.
 */

namespace RossHigleyProject7a
{
    public partial class MainWindow : Form
    {

        //Variable declarations
        private bool isGameManagerInitialized;
        private Sound backgroundMusic;
        public GameManager gameManager;
        System.Diagnostics.Stopwatch framesPerSecondTimer;

        public static bool shotgunblast;
        public static bool p51mustang;
        private static List<char> cheat;

        public PlayerShip ship;
        public Camera camera;
        public FishEye fisheye;

        ///*************************************************************************************************************
        ///<summary>This constructor initializes the form. Do no place custom code here as it may be unstable.</summary>
        ///*************************************************************************************************************

        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Ross Higley     12/5/16
        /// This method fires every time the form is resized, such as when dragged to a new monitor. 
        /// It also fires immediately before MainForm_Load
        /// </summary>
        private void MainForm_Layout(object sender, LayoutEventArgs e)
        {
            if (fisheye == null)
            {
                fisheye = new FishEye(this);
            }
            else
            {
                fisheye.UpdateFormSize(this);
            }
        }

        ///********************************************************************************************
        ///<summary>After the constructor, program flow moves here to set up the form window.</summary>
        ///MODIFIED BY JACOB to add a thread for the thread start
        ///
        ///********************************************************************************************
        private void MainForm_Load(object sender, EventArgs e)
        {
            isGameManagerInitialized = false;
            framesPerSecondTimer = new System.Diagnostics.Stopwatch();
            Voices.intitialize();
            Voices.asteroidsThreePointOh.playSound(false);
            backgroundMusic = new Sound("Music\\Title Theme Song.wav");
            backgroundMusic.playSound(true);
            backgroundMusic.setVolume(0.9);
            btn_NewGame.Anchor = AnchorStyles.Bottom;
            btn_Settings.Anchor = AnchorStyles.Bottom;
            btn_Credits.Anchor = AnchorStyles.Bottom;
            pctr_MainLogo.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            difficultyGroupBox.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            this.WindowState = FormWindowState.Maximized;
            this.DoubleBuffered = true;
            cheat = new List<char>();

        }



        ///*******************************************************************************
        ///<summary>Begins playing a new game with the current settings applied.</summary>
        ///*******************************************************************************

        private void btn_NewGame_Click(object sender, EventArgs e)
        {
            if (easyRadioButton.Checked == true) Settings.difficulty = Settings.Difficulty.EASY;
            else if (normalRadioButton.Checked == true) Settings.difficulty = Settings.Difficulty.NORMAL;
            else if (veteranRadioButton.Checked == true) Settings.difficulty = Settings.Difficulty.VETERAN;
            else if (eliteRadioButton.Checked == true) Settings.difficulty = Settings.Difficulty.ELITE;
            else Settings.difficulty = Settings.Difficulty.ALIENAPOCOLAPSE;

            (new Sound("Thruster.wav")).playSound(false);

            for (int i = 0; i < 100; i++)
            {
                if (i % 16 <= 8)
                    this.BackColor = Color.DarkRed;
                else
                    this.BackColor = Color.Black;

                backgroundMusic.setVolume(1.0 - i / 100.0);
                Application.DoEvents();
                Thread.Sleep(15);
            }
            Settings.playerName = playerNameTextBox.Text;
            this.BackColor = Color.Black;
            backgroundMusic.stopSound();
            gameManager = new GameManager(this);
            Settings.setInitialValues();
            UpgradeSystemForm.setInitialValues();
            label1.Hide(); // says "Player Name"
            playerNameTextBox.Hide();
            btn_Credits.Hide();
            btn_NewGame.Hide();
            btn_Settings.Hide();
            pctr_MainLogo.Hide();
            easyRadioButton.Hide();
            normalRadioButton.Hide();
            veteranRadioButton.Hide();
            eliteRadioButton.Hide();
            alienApocolapseRadioButton.Hide();
            difficultyGroupBox.Hide();

            UpgradeSystemForm.setInitialValues(); // again??
            Scores.getScoresFromFile();

        }

        ///***********************************************************************************
        ///<summary>Shows a window that allows the user to change gameplay settings.</summary>
        ///***********************************************************************************

        private void btn_Settings_Click(object sender, EventArgs e)
        {
            Settings.showForm();
        }

        ///******************************************
        ///<summary>Shows a credits screen.</summary>
        ///******************************************

        private void btn_Credits_Click(object sender, EventArgs e)
        {
            string credits = "";
            credits += "Credits\r\n\r\n";
            credits += "Directed by Dr. David Beard\r\n";
            credits += "Lead Programmer - Rob Merrick\r\n";
            credits += "Assistant Programmer - Ted Delezene\r\n";
            credits += "Music Composed by Rob Merrick\r\n";
            credits += "Image Processing - Ted Delezene, Rob Merrick, Thomas Veyrat, Qiao Wei\r\n";
            credits += "Sound Effects - Rob Merrick, Yoji Inagaki (Nintendo, Star Fox 64 copyright 1997)\r\n";
            credits += "Updated, modified and Jakeified - Jacob Lehmer\r\n";
            credits += "Universe Expanded and Curved by Ross Higley";
            MessageBox.Show(credits);
        }

        ///*********************************************************************************************************
        ///<summary>Every time the main form goes to redraw, this method is called. This is where the main
        ///loop exists for the game. All frame drawing occurs here. Note that there is a call to invalidate
        ///the graphics at the end, which causes it to loop until the game is over, beginning as soon as gameManager
        ///is not null.</summary>
        ///*********************************************************************************************************

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            if (gameManager == null)
                return;

            framesPerSecondTimer.Start();

            if (!isGameManagerInitialized)
            {
                gameManager.initialize();
                isGameManagerInitialized = true;
            }

            gameManager.clearScreen(e);
            gameManager.updateFrame();

            gameManager.drawFrame(e);

            if (gameManager.targetingSystem.TargetedItem != null)
                gameManager.targetingSystem.Target(e);

            framesPerSecondTimer.Stop();

            if (framesPerSecondTimer.ElapsedMilliseconds < 33)
            {
                Application.DoEvents();
                Thread.Sleep((int)(33 - framesPerSecondTimer.ElapsedMilliseconds)); //This simulates roughly 30 fps.
                Application.DoEvents();
            }
            framesPerSecondTimer.Reset();

            if (gameManager.isGameOver())
                resetForm(e);
            else
                this.Invalidate();
        }

        ///*******************************************************************************************************
        ///<summary>Once the game finishes, call this method to reset the form back to the title screen.</summary>
        ///*******************************************************************************************************

        private void resetForm(PaintEventArgs e)
        {
            isGameManagerInitialized = false;
            gameManager.clearScreen(e);
            //   GameManager.canvas = null;
            gameManager = null;
            btn_Credits.Show();
            btn_NewGame.Show();
            btn_Settings.Show();
            pctr_MainLogo.Show();
            difficultyGroupBox.Show();
            easyRadioButton.Show();
            playerNameTextBox.Show();
            label1.Show();
            normalRadioButton.Show();
            veteranRadioButton.Show();
            eliteRadioButton.Show();
            alienApocolapseRadioButton.Show();
            backgroundMusic = new Sound("Music\\Title Theme Song.wav");
            backgroundMusic.playSound(true);
            backgroundMusic.setVolume(0.9);
        }

        ///******************************************************************************************
        ///<summary>This event is called if the user holds the mouse down on the main form.</summary>
        ///******************************************************************************************

        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (!UserControls.getLeftMouseDown())
                UserControls.setLeftMouseDown(e.Button.Equals(MouseButtons.Left));

            if (!UserControls.getRightMouseDown())
                UserControls.setRightMouseDown(e.Button.Equals(MouseButtons.Right));
        }

        ///****************************************************************************************
        ///<summary>This event is called if the user releases the mouse on the main form.</summary>
        ///****************************************************************************************

        private void MainForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (UserControls.getLeftMouseDown())
                UserControls.setLeftMouseDown(!e.Button.Equals(MouseButtons.Left));

            if (UserControls.getRightMouseDown())
                UserControls.setRightMouseDown(!e.Button.Equals(MouseButtons.Right));
        }

        //ADDED By Jacob Lehmer
        //This will implement the hyperspace capability of the game, along with the pause menu
        private void MainForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (isGameManagerInitialized)
                switch (e.KeyChar)
                {
                    //escape button for pause
                    case '\x1b': gameManager.pm.pause(); break;
                    //tab for hyperspace
                    case '\t': gameManager.hyperspaceThePlayer(); break;
                    //tilde for inertial dampeners
                    case '`': gameManager._PlayerShip.toggleInertialDampeners(); break;
                    //'1' for missle
                    case '1':
                        try { ship.fireMissle(); }
                        catch { }
                        break;
                }
            addToCode(e.KeyChar);
        }

        //this is the mouse movement detector for when the mouse has entered an asteroid in order to target it
        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (isGameManagerInitialized)
            {
                determineCursorPosition(e);
            }
        }

        /**********
         * Jacob Lehmer
         * 4/15/15
         * This is the method that will be threaded in order to determine if the mouse cursor is in an asteroid
         * The code is very similar to the code used by the collision detection
         *****/
        private void determineCursorPosition(object data)
        {
            //    MouseEventArgs e = (MouseEventArgs)data;
            float mouseX = fisheye.Window.PointToClient(System.Windows.Forms.Cursor.Position).X;
            float mouseY = fisheye.Window.PointToClient(System.Windows.Forms.Cursor.Position).Y;

            mouseX = fisheye.ScreenToAbsX(mouseX) + camera.getXLocation();
            mouseY = fisheye.ScreenToAbsY(mouseY) + camera.getYLocation();


            float dy;
            float dx;
            foreach (Entity2D item in gameManager.EntityList)
            {
                dx = item.getXLocation() - mouseX; // - e.X;
                dy = item.getYLocation() - mouseY; // - e.Y;
                if (Math.Sqrt(dx * dx + dy * dy) < item.getCollisionRadius())
                {
                    //this line of booleans is actually a friend or foe system interestingly enough
                    if (!(item is Laser) && !(item is PlayerShip) && !(item is Camera) && (gameManager.targetingSystem.TargetedItem != item))
                    {
                        gameManager.targetingSystem.TargetedItem = item;
                        PlayerShip.targetDestroyed = false;
                    }
                }

            }

        }
        /*****
         * Jacob Lehmer
         * 4/27/15
         * This is a cheat code handler
         * *****/
        private void addToCode(char input)
        {

            if (cheat.Count < 5)
            {
                cheat.Add(input);
            }
            else cheat.Clear();
            if (input.Equals('=')) cheat.Clear();
            string Cheat = new string(cheat.ToArray());

            if (Cheat.Equals("iop[]")) { if (!shotgunblast) shotgunblast = true; else shotgunblast = false; Console.Beep(); }
            if (Cheat.Equals("wwii")) { if (!p51mustang) p51mustang = true; else p51mustang = false; Console.Beep(); }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }


    }
}