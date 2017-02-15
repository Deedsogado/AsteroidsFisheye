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

    /*****
     * Jacob Lehmer
     * 4/012/15
     *This is the form that implements the upgrade system
     * *****/
    public partial class UpgradeSystemForm : Form
    {
        static float accelCost;
        static float fireratecost;
        static float inertialdampeningcost;
        static float whscost;
        static float reactoreffcost;
        static float misslecost;
        static float projectilespeedcost;
        
        //constructor
        public UpgradeSystemForm()
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
            label8.Text = "Score = " + Scores.getCurrentScore().ToString();

            accelerationButton.Text = "Upgrade Accelleration (COST " + accelCost.ToString() + ")";
            fireRateButton.Text = "Upgrade FireRate (COST " + fireratecost.ToString() + ")";
            inertialDameningButton.Text = "Upgrade inertial dampening (COST " + inertialdampeningcost.ToString() + ")";
            wormHoleStabilityButton.Text = "Upgrade worm hole stability (COST " + whscost.ToString() + ")";
            ReatorEfficencyButton.Text = "Upgrade reactor efficiency (COST " + reactoreffcost.ToString() + ")";
            misslesButton.Text = "buy more missles (COST " + misslecost.ToString() + ")";
            projectileSpeedButton.Text = "Upgrade projectile speed (COST " + projectilespeedcost.ToString() + ")";

        }

        //This method will Set All the costs back to default
        public static void setInitialValues()
        {
          accelCost = 100;
          fireratecost = 100;
          inertialdampeningcost = 100;
          whscost = 100;
          reactoreffcost = 100;
          misslecost = 1000;
          projectilespeedcost = 100;
        }

        //This method Checks to see if there is enough score to buy it 
        public bool priceCheck(float cost)
        {
            float account = Scores.getCurrentScore();

            if (account >= cost)
            {
                return true;
            }
            else
            {
                MessageBox.Show("Need more score to purchase");
                return false;
            }
        }

        //this will upgrade the acceleration 
        private void accelerationButton_Click(object sender, EventArgs e)
        {
            if (priceCheck(accelCost))
            {
                Settings.acceleration *= 1.15F;
                Scores.subtractPoints((int)accelCost);
                accelCost = (float)Math.Pow(accelCost, 1.1);
            }
            UpdateText();
        }

        //this will upgrade the fire rate
        private void fireRateButton_Click(object sender, EventArgs e)
        {
            if (priceCheck(fireratecost))
            {
                Settings.fireRate /= 1.15F;
                Scores.subtractPoints((int)fireratecost);
                fireratecost = (float)Math.Pow(fireratecost, 1.1);
            }
            UpdateText();
        }

        //this will upgrade inertial dampening
        private void inertialDameningButton_Click(object sender, EventArgs e)
        {
            if (priceCheck(inertialdampeningcost))
            {
                Settings.inertialDampening *= 1.15F;
                Scores.subtractPoints((int)inertialdampeningcost);
                inertialdampeningcost = (float)Math.Pow(inertialdampeningcost, 1.1);
            }
            UpdateText();
        }

        // this will upgrade the wormhole stability
        private void wormHoleStabilityButton_Click(object sender, EventArgs e)
        {
            if (priceCheck(whscost))
            {
                Settings.wormHoleStability =(int) ((float)Settings.wormHoleStability /1.5F);
                Scores.subtractPoints((int)whscost);
                whscost = (float)Math.Pow(whscost, 1.1);
            }
            UpdateText();
        }

        //this will upgrade reacor efficincy
        private void ReatorEfficencyButton_Click(object sender, EventArgs e)
        {
            if (priceCheck(reactoreffcost))
            {
                Settings.fireRatePeanalty /= 1.15F;
                Scores.subtractPoints((int)reactoreffcost);
                reactoreffcost = (float)Math.Pow(reactoreffcost, 1.1);
            }
            UpdateText();
        }

        //you buy more missles with this
        private void misslesButton_Click(object sender, EventArgs e)
        {
            if (priceCheck(misslecost))
            {
                Settings.missles ++;
                Scores.subtractPoints((int)misslecost);
               
            }
            UpdateText();
        }

        //this will upgrade the projectile speed
        private void projectileSpeedButton_Click(object sender, EventArgs e)
        {
            if (priceCheck(projectilespeedcost))
            {
                Settings.projectileSpeed *= 1.15F;
                Scores.subtractPoints((int)projectilespeedcost);
                projectilespeedcost = (float)Math.Pow(projectilespeedcost, 1.1);
            }
            UpdateText();
        }




    }
}
