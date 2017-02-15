using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


/*******
 * Jacob Lehmer
 * 5/06/15
 * This is the form that handles the 
 * total highscore
 * ******/
namespace RossHigleyProject7a
{
    public partial class ScoreForm : Form
    {


        //standard constructor
        public ScoreForm(string scores)
        {
            InitializeComponent();

            updateText(scores);

        }

        //this will close the window
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        /******
         * Jacob Lehmer
         * 4/06/15
         * This will update the text in the text box
         * *****/
        private void updateText(string input)
        {
                scoreRichTextBox.Text = input;
        }


    }
}
