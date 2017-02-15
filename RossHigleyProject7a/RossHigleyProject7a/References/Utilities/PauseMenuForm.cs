using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RossHigleyProject7a
{
    public partial class PauseMenuForm : Form
    {
        PauseMenu pauseMenuControls;

        //this is the constructor for the game manager
        public PauseMenuForm(object pm)
        {
            InitializeComponent();
            pauseMenuControls =(PauseMenu) pm;
        }

        //this will return to the game
        private void returnToGame_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //This will show the settings screen
        private void pauseMenuSettingsButton_Click(object sender, EventArgs e)
        {
            Settings.showForm();
        }

        //this shows the upgrade system
        private void pauseMenuUpgradeSystem_Click(object sender, EventArgs e)
        {
            UpgradeSystem.showForm();
        }

        //this is going to return to the main menu
        private void mainMenuButton_Click(object sender, EventArgs e)
        {
            pauseMenuControls.refGameManger.gameOver(false, true);
            this.Close();
        }

        //This will show the scores form
        private void scoreButton_Click(object sender, EventArgs e)
        {
            Scores.showScoreForm();
        }
    }
}
