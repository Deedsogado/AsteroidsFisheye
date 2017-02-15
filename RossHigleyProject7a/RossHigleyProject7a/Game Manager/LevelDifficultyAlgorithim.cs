using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RossHigleyProject7a
{

    /*****
     * Jacob Lehmer
     * 4/012/15
     * This is going to be how the game increases difficluty on a level by level basis.
     * *****/
    public static class LevelDifficultyAlgorithim
    {
        
        /*****
         * Jacob Lehmer
         * 4/26/15
         * This is going to set the difficulty of the enemies based on the current level
         * ******/
        public static void increaseDifficulty(int CurrentLevel)
        {
            if (CurrentLevel % 2 == 0 && CurrentLevel >= 3)
            {
                Settings.enemyAcceleration *= 1.2F;
                Settings.enemyFireRate /= 1.2F;
                Settings.enemyInertialDampening *= 1.2F;
                Settings.enemyProjectileSpeed *= 1.2F;
                Settings.enemyInertialDampening *= 1.2F;
            }
        }
        
    }
}
