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
    public partial class SettingsForm : Form
    {


        /*****
         * Jacob Lehmer
         * 4/012/15
         * This is the form that is used for the settings
         * *****/
        public SettingsForm()
        {
            InitializeComponent();
            UpdateText();
        }

        //This method will update the text in the boxes
        public void UpdateText()
        {
            label1.Text = "Accelleration(M/S^2): " + Settings.acceleration.ToString();
            label2.Text = "Fire Rate(shots/sec): " + (30F/Settings.fireRate).ToString();
            label3.Text = "Inertial Dampening(KN): " + Settings.inertialDampening.ToString();
            label4.Text = "Worm Hole Stability (Bruhaugs): " + (100F / Settings.wormHoleStability).ToString();
            label5.Text = "Reactor Efficiency: " + (1 / Settings.fireRatePeanalty);
            label6.Text = "Missles: " + Settings.missles.ToString();
            label7.Text = "Projectile Speed(M/S): " + Settings.projectileSpeed.ToString();
        }
    }
}
