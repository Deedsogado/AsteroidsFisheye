using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 * Level.cs
 * Autors: Rob Merrick
 * This class keeps track of the current level and is responsible
 * for generating the level at the start of each round.
 */

namespace RossHigleyProject7a
{
    class Level
    {

        //Variable declarations
        private static int currentLevel;

        ///**********************************************************************************
        ///<summary>Do not allow public instantiation. This is a static-only class.</summary>
        ///**********************************************************************************

        private Level()
        {

        }

        ///*******************************************************************
        ///<summary>Sets the current level that the user is playing.</summary>
        ///*******************************************************************

        public static void setCurrentLevel(int level)
        {
            if(level < 1)
                throw new Exception("When setting the current level, the value provided must be a positive integer.");

            currentLevel = level;
        }

        ///**************************************************************************************
        ///<summary>Returns an integer representing the current level the player is on.</summary>
        ///**************************************************************************************

        public static int getCurrentLevel()
        {
            return currentLevel;
        }

        ///*********************************************************************************************************************
        ///<summary>Returns the number of large asteroids that should be generated at the start of the level. This is determined
        ///based upon the current level.</summary>
        ///*********************************************************************************************************************

        public static int getInitialNumberOfLargeAsteroids()
        {
            if(currentLevel <= 5)
                return currentLevel;
            else if(currentLevel % 10 == 0)
                return 0;
            else
                return 5;
        }

        ///**********************************************************************************************************************
        ///<summary>Returns the number of medium asteroids that should be generated at the start of the level. This is determined
        ///based upon the current level.</summary>
        ///**********************************************************************************************************************

        public static int getInitialNumberOfMediumAsteroids()
        {
            if(currentLevel % 10 == 0)
                return 0;

            switch(currentLevel)
            {
                case 1: return 0;
                case 2: return 0;
                case 3: return 1;
                case 4: return 1;
                case 5: return 1;
                case 6: return 2;
                case 7: return 2;
                case 8: return 3;
                case 9: return 3;
                case 11: return 4;
                case 12: return 4;
                case 13: return 5;
                case 14: return 6;
                case 15: return 7;
                default: return 8;
            }
        }

        ///*********************************************************************************************************************
        ///<summary>Returns the number of small asteroids that should be generated at the start of the level. This is determined
        ///based upon the current level.</summary>
        ///*********************************************************************************************************************

        public static int getInitialNumberOfSmallAsteroids()
        {
            if(currentLevel % 10 == 0)
                return (currentLevel / 10) * 30;

            if(currentLevel >= 11)
                if(currentLevel <= 30)
                    return currentLevel - 11;
                else
                    return 20;

            switch(currentLevel)
            {
                case 1: return 0;
                case 2: return 0;
                case 3: return 0;
                case 4: return 0;
                case 5: return 1;
                case 6: return 0;
                case 7: return 1;
                case 8: return 2;
                case 9: return 3;
                default: return 20;
            }
        }

        ///***********************************************************************************************************************
        ///<summary>Returns the number of large enemy ships that should be generated at the start of the level. This is determined
        ///based upon the current level.</summary>
        ///***********************************************************************************************************************

        public static int getInitialNumberOfLargeEnemyShips()
        {
            
            if(currentLevel % 10 == 0)
                return 0;

            return (currentLevel - 1) / 2;
        }

        ///***********************************************************************************************************************
        ///<summary>Returns the number of small enemy ships that should be generated at the start of the level. This is determined
        ///based upon the current level.</summary>
        ///***********************************************************************************************************************

        public static int getInitialNumberOfSmallEnemyShips()
        {
            if(currentLevel % 10 == 0)
                return 0;

            return (currentLevel - 1) / 4;
        }

        

    }
}