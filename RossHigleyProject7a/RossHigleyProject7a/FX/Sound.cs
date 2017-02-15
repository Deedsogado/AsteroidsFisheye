using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

/**
 * Sound.cs
 * Autors: Rob Merrick
 * This class is used to manage a single sound effect for the 
 * game. Includes controls for volume and panning.
 */

namespace RossHigleyProject7a
{
    class Sound
    {
        //Named Constants
        private DirectoryInfo CURRENT_DIRECTORY = (new DirectoryInfo(Directory.GetCurrentDirectory())).Parent.Parent;

        //Variable declarations
        private double currentVolume;
        private double currentPan;
        private MediaPlayer soundPlayer;
        private Uri soundFile;
        private bool isCurrentlyPlaying;

        ///***********************************************************************************************************
        ///<summary>Creates a new instance of the Sound class, adding it to the list of all sound instances.</summary>
        ///***********************************************************************************************************

        public Sound(string fileNameRelativeToSoundFXFolder)
        {
            if(fileNameRelativeToSoundFXFolder == "")
                return;

            currentVolume = 1.0;
            currentPan = 0.0;
            soundPlayer = new MediaPlayer();
            soundFile = new Uri(CURRENT_DIRECTORY.FullName + "\\SoundFX\\" + fileNameRelativeToSoundFXFolder);
            isCurrentlyPlaying = false;
        }

        ///**************************************************************************************************
        ///<summary>Sets the volume number for the sound (0.0 = absolute quiet, 1.0 = full volume).</summary>
        ///**************************************************************************************************

        public void setVolume(double volume)
        {
            if(volume < 0.0 || volume > 1.0)
                throw new Exception("Bad parameter for Sound.setVolume(). The value should be between 0.0 and 1.0.");

            currentVolume = volume;
            applyVolumePanUpdates();
        }

        ///****************************************************************************************************************
        ///<summary>Sets the pan number for the sound (-1.0 = absolute left, 0.0 = center, 1.0 = absolute right).</summary>
        ///****************************************************************************************************************

        public void setPan(double pan)
        {
            if(Math.Abs(pan) > 1.0)
                throw new Exception("Bad parameter for Sound.setPan(). The value should be between -1.0 and 1.0.");

            currentPan = pan;
            applyVolumePanUpdates();
        }

        ///*******************************************************************************************************************************
        ///<summary>Begins sound playback. Note that the sound card has a limit of either 8 or 16 simultaneously playing sounds.</summary>
        ///*******************************************************************************************************************************

        public void playSound(bool loopSound)
        {
            soundPlayer = new MediaPlayer();
            applyVolumePanUpdates();
            soundPlayer.Open(soundFile);
            soundPlayer.Play();
            isCurrentlyPlaying = true;

            if(loopSound)
                soundPlayer.MediaEnded += player_Loop;
            else
                soundPlayer.MediaEnded += player_Close;
        }

        ///**************************************************************************************************************************
        ///<summary>Pauses a sound if it is playing. If resumeSound() is called, the sound will continue where it left off.</summary>
        ///**************************************************************************************************************************

        public void pauseSound()
        {
            isCurrentlyPlaying = false;

            if(soundPlayer != null)
                soundPlayer.Pause();
        }

        ///************************************************************************************************************************
        ///<summary>Stops a sound if it was playing. Note that the playback position will not be saved with this command.</summary>
        ///************************************************************************************************************************

        public void stopSound()
        {
            isCurrentlyPlaying = false;

            if(soundPlayer != null)
                soundPlayer.Stop();
        }

        ///**************************************************************************************************************
        ///<summary>Returns true if the sound is currently playing (pausing and stopping a sound return false).</summary>
        ///**************************************************************************************************************

        public bool isPlaying()
        {
            return isCurrentlyPlaying;
        }

        ///***********************************************************
        ///<summary>Returns the current volume of the sound.</summary>
        ///***********************************************************

        public double getVolume()
        {
            return currentVolume;
        }

        ///********************************************************
        ///<summary>Returns the current pan of the sound.</summary>
        ///********************************************************

        public double getPan()
        {
            return currentPan;
        }

        ///******************************************************************************************************************
        ///<summary>Applies the current volume and pan values to the sound player, provided the player is not null.</summary>
        ///******************************************************************************************************************

        private void applyVolumePanUpdates()
        {
            if(soundPlayer == null)
                return;

            soundPlayer.Balance = currentPan;
            soundPlayer.Volume = currentVolume;
        }

        ///*****************************************************************************************************************
        ///<summary>This method is called if the loopSound flag was set to true and the media has stopped playing.</summary>
        ///*****************************************************************************************************************

        private void player_Loop(object sender, EventArgs e)
        {
            MediaPlayer player = sender as MediaPlayer;

            if (player == null)
                return;

            player.Position = new TimeSpan(0);
            player.Play();
        }

        ///******************************************************************************************************************
        ///<summary>This method is called if the loopSound flag was set to false and the media has stopped playing.</summary>
        ///******************************************************************************************************************

        private void player_Close(object sender, EventArgs e)
        {
            MediaPlayer player = sender as MediaPlayer;

            if(player == null)
                return;

            player.Stop();
            player.Close();
            player = null;
            isCurrentlyPlaying = false;
        }

    }
}