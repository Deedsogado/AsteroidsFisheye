using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/**
 * SpecialEffects.cs
 * Autors: Rob Merrick
 * This class is used to create special effects for the game.
 * Currently, it can only draw an explosion, but it should be
 * easily expandable to contain additional content in future
 * implementations of this game.
 */

namespace RossHigleyProject7a
{
    class SpecialEffects
    {

        //Named Constants
        public const int EXPLOSION_KEY_FRAME = 3;
        public const int EXPLOSION_FRAME_COUNT = 16;
        public const int EXPLOSION_WIDTH = 256;
        public const int EXPLOSION_HEIGHT = 256;

        //Variable Declarations
        private bool isLongExplosion;
        private bool startedExplosionSound;
        private AnimatedGraphic blowUpAnimation;
        protected MainWindow parentWindow;

        //Static Variables
        private static AnimatedGraphic RAW_blowUpAnimation;
        private static List<AnimatedGraphic> effectsToDraw;
        private static List<float[]> parametersForEffectsToDraw;
        private static Sound smallExplosionSound = new Sound("Small Explosion.wav");
        private static Sound largeExplosionSound = new Sound("Large Explosion.wav");

        ///**********************************************************************
        ///<summary>Creates a new instance of the SpecialEffects class.</summary>
        ///**********************************************************************

        public SpecialEffects(MainWindow window)
        {
            parentWindow = window;
            if(RAW_blowUpAnimation == null)
                RAW_blowUpAnimation = new AnimatedGraphic("Explosion.png", EXPLOSION_WIDTH, EXPLOSION_HEIGHT, EXPLOSION_FRAME_COUNT);

            if(effectsToDraw == null)
                effectsToDraw = new List<AnimatedGraphic>();

            if(parametersForEffectsToDraw == null)
                parametersForEffectsToDraw = new List<float[]>();
        }

        ///*********************************************************************************************************************
        ///<summary>Sets up the animation image to draw an explosion. This method has been optimised to load the animation strip
        ///image from an internal stream, not from the hard drive itself. If isLongExplosion is set to true, the explosion will
        ///take longer to animate than if it is set to false.</summary>
        ///*********************************************************************************************************************

        public void setUpExplosion(bool isLongExplosion)
        {
            this.isLongExplosion = isLongExplosion;
            startedExplosionSound = false;
            blowUpAnimation = RAW_blowUpAnimation.cloneAnimatedGraphic();
            
            if(isLongExplosion)
                blowUpAnimation.setIterationsPerFrame(3);
            else
                blowUpAnimation.setIterationsPerFrame(1);
        }

        ///***************************************************************************************************************************
        ///<summary>Draws an exploding image at the provided location. Returns the current frame that is being drawn. Frame index 3 is
        ///when the explosion is the largest, and that is the ideal time to remove an entity that is being blown up. Frame index 15 is
        ///the last frame of the explosion.</summary>
        ///***************************************************************************************************************************

        public int drawExplosion(float xLocation, float yLocation, float rotation, float scaleX, float scaleY)
        {
            if(!startedExplosionSound)
            {
                startedExplosionSound = true;

                if(isLongExplosion)
                    largeExplosionSound.playSound(false);
                else
                    smallExplosionSound.playSound(false);
            }

            int currentFrame = blowUpAnimation.getCurrentAnimationFrameIndex();
            effectsToDraw.Add(blowUpAnimation);

            float dx = xLocation - parentWindow.camera.getXLocation();
            float dy = yLocation - parentWindow.camera.getYLocation();
            dx = parentWindow.fisheye.AbsToScreenX(dx);
            dy = parentWindow.fisheye.AbsToScreenY(dy);

            //      float[] parameters = {xLocation, yLocation, rotation, scaleX, scaleY}; 
            float[] parameters = { dx, dy, rotation, scaleX, scaleY };
            parametersForEffectsToDraw.Add(parameters);
            return currentFrame;
        }

        ///***************************************************************************************************************************
        ///<summary>Draws all of the special effects. This should be close to the end of the GameManager.drawFrame() method.</summary>
        ///***************************************************************************************************************************

        public static void drawAllSpecialEffects(PaintEventArgs e)
        {
            if(effectsToDraw == null)
                return;

            for(int i = 0; i < effectsToDraw.Count; i++)
            {
                float[] parametersList = parametersForEffectsToDraw.ElementAt(i);
                float xLocation = parametersList[0];
                float yLocation = parametersList[1];
                float rotation = parametersList[2];
                float scaleX = parametersList[3];
                float scaleY = parametersList[4];
                effectsToDraw.ElementAt(i).draw(xLocation, yLocation, rotation, scaleX, scaleY, e);
            }

            effectsToDraw.Clear();
            parametersForEffectsToDraw.Clear();
        }

    }
}