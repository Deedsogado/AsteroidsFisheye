using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/**
 * AnimatedGraphic.cs
 * Autors: Rob Merrick
 * This class is used to draw animated graphic strips on the screen.
 * In order to use this class, you must create an animation strip
 * in which each animation frame is a constant width and height, and
 * is laid out in a grid on a single image with the first frame
 * beginning at the top-left corner and the last frame being in the
 * bottom-right corner (see Explosion.png). The program will load
 * the entire animation strip and display a portion of that image 
 * based upon which frame should be displayed at that moment.
 */

namespace RossHigleyProject7a
{
    class AnimatedGraphic
    {

        //Variable Declarations
        private bool loopingEnabled;
        private int totalFrames;
        private int frameCounter;
        private int iterationsPerFrame;
        private float singleFrameWidth;
        private float singleFrameHeight;
        private List<Graphic> individualFrames;

        ///**********************************************************************************************************************
        ///<summary>Creates a new instance of the AnimatedGraphic class. See the class level comments for more details.</summary>
        ///**********************************************************************************************************************

        public AnimatedGraphic(string fileNameRelativeToGraphicsFolder, float singleFrameWidth, float singleFrameHeight, int totalFrames)
        {
            Graphic mainImage = new Graphic(fileNameRelativeToGraphicsFolder);

            if(singleFrameWidth * singleFrameHeight * totalFrames > mainImage.getWidth(false) * mainImage.getHeight(false))
                throw new Exception("You have an invalid number of frames and/or an invalid frame size for the provided animated image.");

            this.singleFrameWidth = singleFrameWidth;
            this.singleFrameHeight = singleFrameHeight;
            this.totalFrames = totalFrames;
            loopingEnabled = false;
            frameCounter = 0;
            iterationsPerFrame = 3;
            stripIndividualImagesFromMainImage(mainImage);
        }

        ///*******************************************************************************************************************
        ///<summary>This special constructor is called from the AnimatedGraphic.createFromExistingAnimatedGraphic() method.
        ///It is here only to create a new instance of the AnimatedGraphic class with no parameters set. They will be set from
        ///the AnimatedGraphic.createFromExistingAnimatedGraphic() method after the new, blank instance is created.</summary>
        ///*******************************************************************************************************************

        private AnimatedGraphic()
        {
            //Empty
        }

        ///***************************************************************************************************************
        ///<summary>This method is identical to creating an entirely new AnimatedGraphic, but rather than loading and
        ///splicing the animation strip image from the hard drive, it uses the existing animation strip in the provided
        ///AnimatedGraphic to help speed up the process. Call this method if you have loaded a static, template animation
        ///previously and wish to use that template for the new AnimatedGraphic. Note that variables not related to the
        ///frame width, frame height, total frames, and individual frames are not transferred over to the clone.</summary>
        ///***************************************************************************************************************

        public AnimatedGraphic cloneAnimatedGraphic()
        {
            AnimatedGraphic animatedGraphicToReturn = new AnimatedGraphic();
            animatedGraphicToReturn.singleFrameWidth = this.singleFrameWidth;
            animatedGraphicToReturn.singleFrameHeight = this.singleFrameHeight;
            animatedGraphicToReturn.totalFrames = this.totalFrames;
            animatedGraphicToReturn.loopingEnabled = false;
            animatedGraphicToReturn.frameCounter = 0;
            animatedGraphicToReturn.iterationsPerFrame = 3;
            animatedGraphicToReturn.individualFrames = new List<Graphic>();

            foreach(Graphic frame in this.individualFrames)
                animatedGraphicToReturn.individualFrames.Add(frame);

            return animatedGraphicToReturn;
        }

        ///*********************************************************************************************************************
        ///<summary>Draws the animated image at the location provided. The animation frame is automatically calculated based off
        ///of the current iterationsPerFrame value and the loopingEnabled value.</summary>
        ///*********************************************************************************************************************

        public void draw(float xLocation, float yLocation, float rotation, float scaleX, float scaleY, PaintEventArgs e)
        {
            int indexOfImageToDraw = frameCounter / iterationsPerFrame;

            if(indexOfImageToDraw < totalFrames)
                individualFrames.ElementAt(indexOfImageToDraw).draw(xLocation, yLocation, rotation, scaleX, scaleY, e);

            ++frameCounter;

            if(loopingEnabled && frameCounter >= totalFrames * iterationsPerFrame - 1)
                frameCounter = 0;
        }

        ///********************************************************************************************************************************
        ///<summary>Sets the value which indicates whether or not the animation should start from the beginning once it finishes.</summary>
        ///********************************************************************************************************************************

        public void setLooping(bool loopingEnabled)
        {
            this.loopingEnabled = loopingEnabled;
        }

        ///***************************************************************************************************************
        ///<summary>Call this method to change how frequently the image animation should move to the next frame.</summary>
        ///***************************************************************************************************************

        public void setIterationsPerFrame(int iterationsPerFrame)
        {
            if(iterationsPerFrame < 1)
                throw new Exception("Please provide a position integer when setting the AnimatedGraphic iterations per frame value.");

            this.iterationsPerFrame = iterationsPerFrame;
        }

        ///***********************************************************************************************************************
        ///<summary>Returns the frame in the animation strip that is currently being drawn to the screen. Note that if looping is
        ///disabled, the value returned may be higher than the actual number of frames in the animation. The reason for this is to
        ///allow the value returned to serve as a timer, should you want to add extra blank frames at the end of the animation 
        ///before acting on the end of the animation.</summary>
        ///***********************************************************************************************************************

        public int getCurrentAnimationFrameIndex()
        {
            return frameCounter / iterationsPerFrame;
        }

        ///*******************************************************************************
        ///<summary>Returns true if the animated image strip will loop playback.</summary>
        ///*******************************************************************************

        public bool isLoopingEnabled()
        {
            return loopingEnabled;
        }

        ///********************************************************************************************************************************
        ///<summary>Creates each individual image from the animation strip from the instance width, height, and number of frames.</summary>
        ///********************************************************************************************************************************

        private void stripIndividualImagesFromMainImage(Graphic mainImage)
        {
            individualFrames = new List<Graphic>();
            int totalCounter = 0;
            int xCounter = 0;
            int yCounter = 0;

            while(totalCounter < totalFrames)
            {
                float currentX = xCounter * singleFrameWidth;

                if(currentX > mainImage.getWidth(false) - singleFrameWidth)
                {
                    xCounter = 0;
                    currentX = 0;
                    ++yCounter;
                }

                float currentY = yCounter * singleFrameHeight;
                individualFrames.Add(new Graphic(mainImage.getCroppedImage(currentX, currentY, singleFrameWidth, singleFrameHeight)));
                ++xCounter;
                ++totalCounter;
            }
        }

    }
}