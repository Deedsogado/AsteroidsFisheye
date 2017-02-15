namespace RossHigleyProject7a
{
    partial class ScoreForm
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
            this.scoreRichTextBox = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.resumeButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // scoreRichTextBox
            // 
            this.scoreRichTextBox.Location = new System.Drawing.Point(12, 51);
            this.scoreRichTextBox.Name = "scoreRichTextBox";
            this.scoreRichTextBox.Size = new System.Drawing.Size(374, 319);
            this.scoreRichTextBox.TabIndex = 0;
            this.scoreRichTextBox.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(145, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "High Scores";
            // 
            // resumeButton
            // 
            this.resumeButton.Location = new System.Drawing.Point(311, 17);
            this.resumeButton.Name = "resumeButton";
            this.resumeButton.Size = new System.Drawing.Size(75, 23);
            this.resumeButton.TabIndex = 2;
            this.resumeButton.Text = "Resume";
            this.resumeButton.UseVisualStyleBackColor = true;
            this.resumeButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // ScoreForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(398, 382);
            this.Controls.Add(this.resumeButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.scoreRichTextBox);
            this.Name = "ScoreForm";
            this.Text = "ScoreForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox scoreRichTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button resumeButton;
    }
}