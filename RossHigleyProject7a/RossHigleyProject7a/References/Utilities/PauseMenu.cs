using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RossHigleyProject7a
{


    /*****
     * Jacob Lehmer
     * 4/012/15
     * This is the pause menu
     * *****/
    public class PauseMenu
    {
        PauseMenuForm pauseForm;
        public GameManager refGameManger;
        /******
         * Jacob Lehmer
         * 4/19/15
         * This is the cosntructor for the pause menu
         * ******/
        public PauseMenu(GameManager gm)
        {
            refGameManger = gm;
            pauseForm = new PauseMenuForm(this);
        }

        //this pauses the game
        public void pause()
        {
            pauseForm.ShowDialog();
        }
    }
}
