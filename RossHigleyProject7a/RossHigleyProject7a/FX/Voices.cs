using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 * Voices.cs
 * Autors: Rob Merrick
 * This class is used to play voice recordings based upon game status. Enjoy!
 */

namespace RossHigleyProject7a
{
    class Voices
    {

        //Static Variables
        public static Sound asteroidsThreePointOh = new Sound("Voice GUI\\Asteroids 3 Point 0.wav");
        public static Sound bossApproaching = new Sound("Voice GUI\\Boss Approaching.wav");
        public static Sound enemyShipApproaching = new Sound("Voice GUI\\Enemy Ship Approaching.wav");
        public static Sound extraLifeAwarded = new Sound("Voice GUI\\Extra Live Awarded.wav");
        public static Sound finalLevel = new Sound("Voice GUI\\Final Level.wav");
        public static Sound gameOver = new Sound("Voice GUI\\Game Over.wav");
        public static Sound levelComplete = new Sound("Voice GUI\\Level Complete.wav");
        public static Sound youWon = new Sound("Voice GUI\\You Won, Congratulations.wav");
        public static Sound yourShipHasBeenDestroyed = new Sound("Voice GUI\\Your Ship has been Destroyed.wav");

        ///*************************************************************************************************************
        ///<summary>Do not allow public instantiation. Just use this class statically to play each sound file.</summary>
        ///*************************************************************************************************************

        private Voices()
        {
            //Empty
        }

        ///*****************************************************************************************************************
        ///<summary>Call this method to set the volume of each voice to "full". I'm using 0.9 because there's a bug with the
        ///windows media player, and for some reason, 0.9 is full volume.</summary>
        ///*****************************************************************************************************************

        public static void intitialize()
        {
            asteroidsThreePointOh.setVolume(0.9);
            bossApproaching.setVolume(0.9);
            enemyShipApproaching.setVolume(0.9);
            extraLifeAwarded.setVolume(0.9);
            finalLevel.setVolume(0.9);
            gameOver.setVolume(0.9);
            levelComplete.setVolume(0.9);
            youWon.setVolume(0.9);
            yourShipHasBeenDestroyed.setVolume(0.9);
        }
    }
}
