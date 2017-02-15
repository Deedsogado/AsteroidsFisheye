namespace RossHigleyProject7a
{
    partial class MainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.btn_NewGame = new System.Windows.Forms.Button();
            this.btn_Settings = new System.Windows.Forms.Button();
            this.pctr_MainLogo = new System.Windows.Forms.PictureBox();
            this.btn_Credits = new System.Windows.Forms.Button();
            this.difficultyGroupBox = new System.Windows.Forms.GroupBox();
            this.playerNameTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.alienApocolapseRadioButton = new System.Windows.Forms.RadioButton();
            this.eliteRadioButton = new System.Windows.Forms.RadioButton();
            this.veteranRadioButton = new System.Windows.Forms.RadioButton();
            this.normalRadioButton = new System.Windows.Forms.RadioButton();
            this.easyRadioButton = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.pctr_MainLogo)).BeginInit();
            this.difficultyGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_NewGame
            // 
            this.btn_NewGame.Location = new System.Drawing.Point(440, 582);
            this.btn_NewGame.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_NewGame.Name = "btn_NewGame";
            this.btn_NewGame.Size = new System.Drawing.Size(112, 35);
            this.btn_NewGame.TabIndex = 0;
            this.btn_NewGame.Text = "New Game";
            this.btn_NewGame.UseVisualStyleBackColor = true;
            this.btn_NewGame.Click += new System.EventHandler(this.btn_NewGame_Click);
            // 
            // btn_Settings
            // 
            this.btn_Settings.Location = new System.Drawing.Point(318, 582);
            this.btn_Settings.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_Settings.Name = "btn_Settings";
            this.btn_Settings.Size = new System.Drawing.Size(112, 35);
            this.btn_Settings.TabIndex = 1;
            this.btn_Settings.TabStop = false;
            this.btn_Settings.Text = "Settings";
            this.btn_Settings.UseVisualStyleBackColor = true;
            this.btn_Settings.Click += new System.EventHandler(this.btn_Settings_Click);
            // 
            // pctr_MainLogo
            // 
            this.pctr_MainLogo.Image = ((System.Drawing.Image)(resources.GetObject("pctr_MainLogo.Image")));
            this.pctr_MainLogo.Location = new System.Drawing.Point(4, 365);
            this.pctr_MainLogo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pctr_MainLogo.MinimumSize = new System.Drawing.Size(900, 154);
            this.pctr_MainLogo.Name = "pctr_MainLogo";
            this.pctr_MainLogo.Size = new System.Drawing.Size(900, 154);
            this.pctr_MainLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pctr_MainLogo.TabIndex = 2;
            this.pctr_MainLogo.TabStop = false;
            // 
            // btn_Credits
            // 
            this.btn_Credits.Location = new System.Drawing.Point(382, 626);
            this.btn_Credits.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_Credits.Name = "btn_Credits";
            this.btn_Credits.Size = new System.Drawing.Size(112, 35);
            this.btn_Credits.TabIndex = 3;
            this.btn_Credits.Text = "Credits";
            this.btn_Credits.UseVisualStyleBackColor = true;
            this.btn_Credits.Click += new System.EventHandler(this.btn_Credits_Click);
            // 
            // difficultyGroupBox
            // 
            this.difficultyGroupBox.Controls.Add(this.playerNameTextBox);
            this.difficultyGroupBox.Controls.Add(this.label1);
            this.difficultyGroupBox.Controls.Add(this.alienApocolapseRadioButton);
            this.difficultyGroupBox.Controls.Add(this.eliteRadioButton);
            this.difficultyGroupBox.Controls.Add(this.veteranRadioButton);
            this.difficultyGroupBox.Controls.Add(this.normalRadioButton);
            this.difficultyGroupBox.Controls.Add(this.easyRadioButton);
            this.difficultyGroupBox.Location = new System.Drawing.Point(18, 18);
            this.difficultyGroupBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.difficultyGroupBox.Name = "difficultyGroupBox";
            this.difficultyGroupBox.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.difficultyGroupBox.Size = new System.Drawing.Size(195, 269);
            this.difficultyGroupBox.TabIndex = 4;
            this.difficultyGroupBox.TabStop = false;
            this.difficultyGroupBox.Text = "Difficulty";
            // 
            // playerNameTextBox
            // 
            this.playerNameTextBox.Location = new System.Drawing.Point(9, 222);
            this.playerNameTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.playerNameTextBox.Name = "playerNameTextBox";
            this.playerNameTextBox.Size = new System.Drawing.Size(148, 26);
            this.playerNameTextBox.TabIndex = 6;
            this.playerNameTextBox.Text = "Arbitor";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(4, 197);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "Player Name";
            // 
            // alienApocolapseRadioButton
            // 
            this.alienApocolapseRadioButton.AutoSize = true;
            this.alienApocolapseRadioButton.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.alienApocolapseRadioButton.Location = new System.Drawing.Point(9, 166);
            this.alienApocolapseRadioButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.alienApocolapseRadioButton.Name = "alienApocolapseRadioButton";
            this.alienApocolapseRadioButton.Size = new System.Drawing.Size(157, 24);
            this.alienApocolapseRadioButton.TabIndex = 4;
            this.alienApocolapseRadioButton.Text = "Alien Apocolapse";
            this.alienApocolapseRadioButton.UseVisualStyleBackColor = true;
            // 
            // eliteRadioButton
            // 
            this.eliteRadioButton.AutoSize = true;
            this.eliteRadioButton.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.eliteRadioButton.Location = new System.Drawing.Point(9, 131);
            this.eliteRadioButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.eliteRadioButton.Name = "eliteRadioButton";
            this.eliteRadioButton.Size = new System.Drawing.Size(65, 24);
            this.eliteRadioButton.TabIndex = 3;
            this.eliteRadioButton.Text = "Elite";
            this.eliteRadioButton.UseVisualStyleBackColor = true;
            // 
            // veteranRadioButton
            // 
            this.veteranRadioButton.AutoSize = true;
            this.veteranRadioButton.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.veteranRadioButton.Location = new System.Drawing.Point(9, 100);
            this.veteranRadioButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.veteranRadioButton.Name = "veteranRadioButton";
            this.veteranRadioButton.Size = new System.Drawing.Size(91, 24);
            this.veteranRadioButton.TabIndex = 2;
            this.veteranRadioButton.Text = "Veteran";
            this.veteranRadioButton.UseVisualStyleBackColor = true;
            // 
            // normalRadioButton
            // 
            this.normalRadioButton.AutoSize = true;
            this.normalRadioButton.Checked = true;
            this.normalRadioButton.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.normalRadioButton.Location = new System.Drawing.Point(9, 65);
            this.normalRadioButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.normalRadioButton.Name = "normalRadioButton";
            this.normalRadioButton.Size = new System.Drawing.Size(84, 24);
            this.normalRadioButton.TabIndex = 1;
            this.normalRadioButton.TabStop = true;
            this.normalRadioButton.Text = "Normal";
            this.normalRadioButton.UseVisualStyleBackColor = true;
            // 
            // easyRadioButton
            // 
            this.easyRadioButton.AutoSize = true;
            this.easyRadioButton.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.easyRadioButton.Location = new System.Drawing.Point(9, 29);
            this.easyRadioButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.easyRadioButton.Name = "easyRadioButton";
            this.easyRadioButton.Size = new System.Drawing.Size(69, 24);
            this.easyRadioButton.TabIndex = 0;
            this.easyRadioButton.Text = "Easy";
            this.easyRadioButton.UseVisualStyleBackColor = true;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(936, 680);
            this.Controls.Add(this.difficultyGroupBox);
            this.Controls.Add(this.btn_Credits);
            this.Controls.Add(this.btn_Settings);
            this.Controls.Add(this.btn_NewGame);
            this.Controls.Add(this.pctr_MainLogo);
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimumSize = new System.Drawing.Size(949, 708);
            this.Name = "MainWindow";
            this.Text = "Asteroids 3.0";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MainForm_Paint);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MainForm_KeyPress);
            this.Layout += new System.Windows.Forms.LayoutEventHandler(this.MainForm_Layout);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.pctr_MainLogo)).EndInit();
            this.difficultyGroupBox.ResumeLayout(false);
            this.difficultyGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_NewGame;
        private System.Windows.Forms.Button btn_Settings;
        private System.Windows.Forms.PictureBox pctr_MainLogo;
        private System.Windows.Forms.Button btn_Credits;
        private System.Windows.Forms.GroupBox difficultyGroupBox;
        private System.Windows.Forms.RadioButton alienApocolapseRadioButton;
        private System.Windows.Forms.RadioButton eliteRadioButton;
        private System.Windows.Forms.RadioButton veteranRadioButton;
        private System.Windows.Forms.RadioButton normalRadioButton;
        private System.Windows.Forms.RadioButton easyRadioButton;
        private System.Windows.Forms.TextBox playerNameTextBox;
        private System.Windows.Forms.Label label1;
    }
}

