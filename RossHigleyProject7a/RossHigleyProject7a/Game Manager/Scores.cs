using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

/**
 * Scores.cs
 * Autors: Rob Merrick, Jacob Lehmer
 * This class is used to keep track of the scoring system. This
 * is a static-only class so that it can be accessed anywhere in
 * the program.
 * Last Modified 04/12/15
 */

namespace RossHigleyProject7a
{
    class Scores
    {

        //Scoring System Constants
        public const int FIRE_BULLET_POINTS = 1;
        public const int LARGE_ASTEROID_POINTS = 50;
        public const int MEDIUM_ASTEROID_POINTS = 75;
        public const int SMALL_ASTEROID_POINTS = 100;
        public const int LARGE_ENEMY_SHIP_POINTS = 150;
        public const int SMALL_ENEMY_SHIP_POINTS = 200;
        public const int GAIN_LIFE_THRESHOLD = 20000;
        public const int SMALL_SAUCER_APPEARS_THRESHOLD = 40000;
        private static float difficultybonus;

        //Variable declarations
        private static int currentScore = 0;
        private static int numberOfLivesAwarded = 1;

        private static int highScore = 0;

        private static DirectoryInfo CURRENT_DIRECTORY = (new DirectoryInfo(Directory.GetCurrentDirectory())).Parent.Parent;
        private static String highScoreHistoryFilename = CURRENT_DIRECTORY.FullName + "\\ScoreFolder\\Scores.bin";
        private static List<Score> scoreHistory;


        #region Methods and data for score manipulation
        ///**********************************************************************************
        ///<summary>Do not allow public instantiation. This is a static-only class.</summary>
        ///**********************************************************************************

        private Scores()
        {
            //Empty
        }

        ///***********************************************************************
        ///<summary>Resets the scoring system back to its initial state.</summary>
        ///***********************************************************************

        public static void initialize()
        {
            currentScore = 0;
            numberOfLivesAwarded = 1;
            scoreHistory = new List<Score>();
            getScoresFromFile();

            switch (Settings.difficulty)
            {
                case Settings.Difficulty.EASY: difficultybonus = .5F; break;
                case Settings.Difficulty.NORMAL: difficultybonus = 1F; break;
                case Settings.Difficulty.VETERAN: difficultybonus = 2F; break;
                case Settings.Difficulty.ELITE: difficultybonus = 4F; break;
                case Settings.Difficulty.ALIENAPOCOLAPSE: difficultybonus = 8F; break;
            }

        }

        ///*******************************************************
        ///<summary>Sets the score for the current game.</summary>
        ///*******************************************************

        public static void setScore(int points)
        {
            if(points < 0)
                throw new Exception("When settings the score, the value must be a non-negative integer.");

            currentScore = points;
        }

        ///**********************************************************************************************************************
        ///<summary>Adds points to the current score. Try to make the points follow the public constants in this class.</summary>
        ///**********************************************************************************************************************

        public static void addPoints(int points)
        {
            if(points < 1)
                throw new Exception("When adding points to the current score, the value provided must be a positive integer.");

            currentScore += (int)(points*difficultybonus);

            if(currentScore > highScore)
                highScore = currentScore;
        }

        /*******
         * Jacob Lehmer
         * 4/19/15
         * This allows subtraction of scores
         * ******/
        public static void subtractPoints(int points)
        {
            currentScore -= points;
        }

        ///**************************************************************************************************
        ///<summary>Call this method after the GameManager has awarded the player with a new life so that the
        ///next score for the next new life threshold can be calculated.</summary>
        ///**************************************************************************************************

        public static void calculateNextNewLifeScore()
        {
            ++numberOfLivesAwarded;
        }

        ///*******************************************************************************************
        ///<summary>Returns true if the player has accumulated enough points for a new life.</summary>
        ///*******************************************************************************************

        public static bool shouldAwardNewLife()
        {
            return currentScore >= numberOfLivesAwarded * GAIN_LIFE_THRESHOLD;
        }

        ///***********************************************************
        ///<summary>Returns the current score in the system.</summary>
        ///***********************************************************

        public static int getCurrentScore()
        {
            return currentScore;
        }

        ///********************************************************
        ///<summary>Returns the high score in the system.</summary>
        ///********************************************************

        public static int getHighScore()
        {
            return highScore;
        }
        #endregion

        
        /*****
        * Jacob Lehmer
        * 4/06/15
        * This will get the scores from the file
        * ******/
        public static void getScoresFromFile()
        {
            try
            {
                BinaryFormatter binaryFileOpener = new BinaryFormatter();
                FileStream binaryFile = new FileStream(highScoreHistoryFilename, FileMode.Open, FileAccess.Read);

                scoreHistory = ((List<Score>)binaryFileOpener.Deserialize(binaryFile));
                binaryFile.Close();
                scoreHistory.Sort();
                scoreHistory.Reverse();
                highScore = scoreHistory[0].Scoreval;
            }
            catch { }
            }

        /*****
         * Jacob Lehmer
         * 4/06/15
         * This will add a score to the file
         * ******/
        public static void addScoreToFile()
        {
            scoreHistory.Add(new Score(currentScore,Settings.playerName));
            BinaryFormatter binaryFileWriter = new BinaryFormatter();

            FileStream binaryFile = new FileStream(highScoreHistoryFilename, FileMode.OpenOrCreate, FileAccess.Write);

            binaryFileWriter.Serialize(binaryFile, scoreHistory);
            binaryFile.Close();
        }

        //This will show the from
        public static void showScoreForm()
        {

            string ScoreString = "";

            foreach (Score scr in scoreHistory)
            {
                ScoreString += scr.ToString();
            }

            ScoreForm scoreForm = new ScoreForm(ScoreString);
            scoreForm.ShowDialog();
        }
        
    }
}