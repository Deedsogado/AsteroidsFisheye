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
     * STUB: This is going to be what takes care of the upgrades for the players ship
     * *****/
    static class UpgradeSystem
    {

        //This will show the upgrade system form
        public static void showForm()
        {
            UpgradeSystemForm ugs = new UpgradeSystemForm();
            ugs.ShowDialog();
        }
    }
}
