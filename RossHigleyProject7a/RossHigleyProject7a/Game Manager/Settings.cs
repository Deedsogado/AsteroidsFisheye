using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 * Settings.cs
 * Autors: Rob Merrick
 * This class is used to manage gameplay settings.
 */

namespace RossHigleyProject7a
{
    class Settings
    {

        //Variable Declarations
        private static bool useMouseControls = true;

        //I wonder what this could be
        public static string playerName;

        //this is where the data to be upgraded lives
        public static float acceleration;
        public static float fireRate;
        public static float inertialDampening;
        public static int wormHoleStability;
        public static float fireRatePeanalty;
        public static float missles;
        public static float projectileSpeed;

        //these are the settings for the Enemy ships
        public static float enemyAcceleration;
        public static float enemyFireRate;
        public static float enemyInertialDampening;
        public static int enemyWormHoleStability;
        public static float enemyProjectileSpeed;

        public static Difficulty difficulty;

        public enum Difficulty
        {
            EASY, NORMAL, VETERAN, ELITE, ALIENAPOCOLAPSE
        }

        ///**********************************************************************************
        ///<summary>Do not allow public instantiation. This is a static-only class.</summary>
        ///**********************************************************************************
        
        private Settings()
        {

        }

        /******
         * Jacob Lehmer
         * 4/26/15
         * This will set the difficulty of the game
         * *******/
        public static void setInitialValues()
        {
            switch (difficulty)
            {
                case Difficulty.EASY: setToEasy(); break;
                case Difficulty.NORMAL: setToNormal(); break;
                case Difficulty.VETERAN: setToVeteran(); break;
                case Difficulty.ELITE: setToElite(); break;
                case Difficulty.ALIENAPOCOLAPSE: setToAlienApocolapse(); break;
            }
        }

        /*******
         * Jacob Lehmer
         * 4/26/15
         * These following methods set the values for the game
         * *******/
        //***********************************************BEGIN LEVELSETS
        private static void setToEasy()
        {
            acceleration = .75F;
            fireRate = 8F;
            inertialDampening = .8F;
            wormHoleStability = 15;
            fireRatePeanalty = 2F;
            missles = 20;
            projectileSpeed = 40F;

           enemyAcceleration = .2F;
           enemyFireRate = 20F;
           enemyInertialDampening = .15F;
           enemyWormHoleStability = 35;
           enemyProjectileSpeed = 12F;

        }
        private static void setToNormal()
        {
           acceleration = .5F;
           fireRate = 12F;
           inertialDampening = .6F;
           wormHoleStability = 20;
           fireRatePeanalty = 2.5F;
           missles = 20;
           projectileSpeed = 30F;

           enemyAcceleration = .3F;
           enemyFireRate = 15F;
           enemyInertialDampening = .3F;
           enemyWormHoleStability = 30;
           enemyProjectileSpeed = 15F;
        }

        private static void setToVeteran() 
        {
            acceleration = .3F;
            fireRate = 15F;
            inertialDampening = .3F;
            wormHoleStability = 30;
            fireRatePeanalty = 3F;
            missles = 10;
            projectileSpeed = 15F;

            enemyAcceleration = .5F;
            enemyFireRate = 12F;
            enemyInertialDampening = .6F;
            enemyWormHoleStability = 20;
            enemyProjectileSpeed = 30F;
        }
        private static void setToElite() 
        {
            acceleration = .25F;
            fireRate = 20F;
            inertialDampening = .15F;
            wormHoleStability = 35;
            fireRatePeanalty = 4.5F;
            missles = 5;
            projectileSpeed = 10F;

            enemyAcceleration = .75F;
            enemyFireRate = 8F;
            enemyInertialDampening = .8F;
            enemyWormHoleStability = 35;
            enemyProjectileSpeed = 40F;
        }
        private static void setToAlienApocolapse() 
        {
            setToElite();
        }
        //***********************************************END LEVELSETS


        ///***************************************************************************************************************
        ///<summary>Sets the static value which indicates whether or not the user is going to use the mouse to control the
        ///spaceship. If set to false, keyboard controls will be used.</summary>
        ///***************************************************************************************************************

        public static void setUseMouseControls(bool useMouse)
        {
            useMouseControls = useMouse;
        }

        ///***************************************************************************************************************
        ///<summary>Returns true if the user has selected to use the mouse to control the spaceship, false if the keyboard
        ///should be used instead.</summary>
        ///***************************************************************************************************************

        public static bool getUseMouseControls()
        {
            return useMouseControls;
        }

        /*********
         * Jacob Lehmer
         * 04/19/15
         * This Shows the settings form
         * ********/
        public static void showForm()
        {
            SettingsForm sf = new SettingsForm();
            sf.ShowDialog();
        }

    }
}