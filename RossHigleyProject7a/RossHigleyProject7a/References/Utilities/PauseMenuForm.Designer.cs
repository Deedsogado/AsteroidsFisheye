namespace RossHigleyProject7a
{
    partial class PauseMenuForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PauseMenuForm));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pauseMenuSettingsButton = new System.Windows.Forms.Button();
            this.pauseMenuUpgradeSystem = new System.Windows.Forms.Button();
            this.returnToGame = new System.Windows.Forms.Button();
            this.mainMenuButton = new System.Windows.Forms.Button();
            this.scoreButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(661, 405);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // pauseMenuSettingsButton
            // 
            this.pauseMenuSettingsButton.Location = new System.Drawing.Point(145, 447);
            this.pauseMenuSettingsButton.Name = "pauseMenuSettingsButton";
            this.pauseMenuSettingsButton.Size = new System.Drawing.Size(75, 23);
            this.pauseMenuSettingsButton.TabIndex = 1;
            this.pauseMenuSettingsButton.Text = "Setttings";
            this.pauseMenuSettingsButton.UseVisualStyleBackColor = true;
            this.pauseMenuSettingsButton.Click += new System.EventHandler(this.pauseMenuSettingsButton_Click);
            // 
            // pauseMenuUpgradeSystem
            // 
            this.pauseMenuUpgradeSystem.Location = new System.Drawing.Point(387, 447);
            this.pauseMenuUpgradeSystem.Name = "pauseMenuUpgradeSystem";
            this.pauseMenuUpgradeSystem.Size = new System.Drawing.Size(99, 23);
            this.pauseMenuUpgradeSystem.TabIndex = 2;
            this.pauseMenuUpgradeSystem.Text = "Upgrade System";
            this.pauseMenuUpgradeSystem.UseVisualStyleBackColor = true;
            this.pauseMenuUpgradeSystem.Click += new System.EventHandler(this.pauseMenuUpgradeSystem_Click);
            // 
            // returnToGame
            // 
            this.returnToGame.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.returnToGame.Location = new System.Drawing.Point(527, 447);
            this.returnToGame.Name = "returnToGame";
            this.returnToGame.Size = new System.Drawing.Size(91, 23);
            this.returnToGame.TabIndex = 3;
            this.returnToGame.Text = "Resume";
            this.returnToGame.UseVisualStyleBackColor = true;
            this.returnToGame.Click += new System.EventHandler(this.returnToGame_Click);
            // 
            // mainMenuButton
            // 
            this.mainMenuButton.Location = new System.Drawing.Point(35, 447);
            this.mainMenuButton.Name = "mainMenuButton";
            this.mainMenuButton.Size = new System.Drawing.Size(75, 23);
            this.mainMenuButton.TabIndex = 4;
            this.mainMenuButton.Text = "Main Menu";
            this.mainMenuButton.UseVisualStyleBackColor = true;
            this.mainMenuButton.Click += new System.EventHandler(this.mainMenuButton_Click);
            // 
            // scoreButton
            // 
            this.scoreButton.Location = new System.Drawing.Point(263, 447);
            this.scoreButton.Name = "scoreButton";
            this.scoreButton.Size = new System.Drawing.Size(75, 23);
            this.scoreButton.TabIndex = 5;
            this.scoreButton.Text = "Scores";
            this.scoreButton.UseVisualStyleBackColor = true;
            this.scoreButton.Click += new System.EventHandler(this.scoreButton_Click);
            // 
            // PauseMenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.returnToGame;
            this.ClientSize = new System.Drawing.Size(685, 504);
            this.Controls.Add(this.scoreButton);
            this.Controls.Add(this.mainMenuButton);
            this.Controls.Add(this.returnToGame);
            this.Controls.Add(this.pauseMenuUpgradeSystem);
            this.Controls.Add(this.pauseMenuSettingsButton);
            this.Controls.Add(this.pictureBox1);
            this.Name = "PauseMenuForm";
            this.Text = "PauseMenuForm";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button pauseMenuSettingsButton;
        private System.Windows.Forms.Button pauseMenuUpgradeSystem;
        private System.Windows.Forms.Button returnToGame;
        private System.Windows.Forms.Button mainMenuButton;
        private System.Windows.Forms.Button scoreButton;
    }
}