using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

/**
 * Graphic.cs
 * Autors: Rob Merrick, Ted Delezene
 * This class is used to draw graphics to the screen.
 */

namespace RossHigleyProject7a
{
    public class Graphic
    {

        //Named constants
        private static DirectoryInfo CURRENT_DIRECTORY = (new DirectoryInfo(Directory.GetCurrentDirectory())).Parent.Parent;

        //Variable declarations
        private Image image;
        private float currentImageWidth;
        private float currentImageHeight;

        ///********************************************************************************************************
        ///<summary>Creates a new instance of the Graphic class with the provided file name as a resource. The file
        ///name should be relative to the project's "Graphics" folder, so just the name of the file is sufficient
        ///so long as the graphic exists in the "Graphics" folder. Be sure to include the extension of the 
        ///image, or else it will not load.</summary>
        ///********************************************************************************************************
        
        public Graphic(string fileNameRelativeToGraphicsFolder)
        {
            image = Image.FromFile(CURRENT_DIRECTORY.FullName + "\\Graphics\\" + fileNameRelativeToGraphicsFolder);
            currentImageWidth = image.Width;
            currentImageHeight = image.Height;
        }

        ///***************************************************************************************************************************
        ///<summary>Creates a new instance of the Graphic class with the provided image as the image to use for the graphic.</summary>
        ///***************************************************************************************************************************

        public Graphic(Image imageToUseAsGraphic)
        {
            image = imageToUseAsGraphic;
        }

        ///*******************************************************************************************************************
        ///<summary>Draws the graphic instance on the screen. xLocation and yLocation should be the very center of the object, 
        ///measured in pixels with (0, 0) representing the top-left corner of the main screen. The rotation needs to be
        ///provided in degrees between 0 and 359.99999, with 0 representing a starting angle of straight up and a positive 
        ///value representing a clock-wise rotation from the starting angle. Be careful, this is a little different than what
        ///you learned in trigonometry. The scaleX and scaleY should be a non-zero number with 1.0 representing no scaling on
        ///the image, 2.0 representing am image that is twice as big, 0.5 representing an image that is half as big, and -1.0
        ///representing an image that is flipped and not scaled on that given axis. Likewise, a value of -2.0 would flip the 
        ///image on that axis and then scale it to be twice as big.</summary>
        ///******************************************************************************************************************
        public void draw(float xLocation, float yLocation, float rotation, float scaleX, float scaleY, PaintEventArgs e)
        {
            currentImageWidth = image.Width * scaleX;
            currentImageHeight = image.Height * scaleY;
            float halfWidth = currentImageWidth / 2.0F;
            float halfHeight = currentImageHeight / 2.0F;

            PointF upperLeft = new PointF(xLocation - halfWidth, yLocation - halfHeight); // begin center image on point
            PointF upperRight = new PointF(xLocation + halfWidth, yLocation - halfHeight);
            PointF lowerLeft = new PointF(xLocation - halfWidth, yLocation + halfHeight);
            PointF[] newPoints = { upperLeft, upperRight, lowerLeft };
            Matrix transformMatrix = new Matrix();
            transformMatrix.RotateAt(rotation, new PointF(xLocation, yLocation));
            transformMatrix.TransformPoints(newPoints); // end center image on  point. 
            e.Graphics.DrawImage(image, newPoints);

        }

        ///****************************************************************************************************************
        ///<summary>Returns the width of the image associated with this instance of the Graphic class, with -1 representing
        ///a value from a null image. If transformedImage is set to true, the width returned will be the width of the image
        ///after transformations have been applied, otherwise the original image width is used.</summary>
        ///****************************************************************************************************************

        public float getWidth(bool transformedImage)
        {
            if(image == null)
                return -1.0F;

            return (transformedImage ? currentImageWidth : image.Width);
        }

        ///******************************************************************************************************************
        ///<summary>Returns the height of the image associated with this instance of the Graphic class, with -1 representing
        ///a value from a null image. If transformedImage is set to true, the height returned will be the height of the image
        ///after transformations have been applied, otherwise the original image height is used.</summary>
        ///******************************************************************************************************************

        public float getHeight(bool transformedImage)
        {
            if(image == null)
                return -1.0F;

            return (transformedImage ? currentImageHeight : image.Height);
        }

        ///********************************************************************************************************************
        ///<summary>Returns a portion of the original image which is a crop beginning at the point startX, startY and extending
        ///width by height from that point. The startX and startY values should be the local points on the original image
        ///with 0, 0 representing the top-left corner of the image.</summary>
        ///********************************************************************************************************************

        public Image getCroppedImage(float startX, float startY, float width, float height)
        {
            Rectangle cropRect = new Rectangle((int) startX, (int) startY, (int) width, (int) height);
            Bitmap src = image.Clone() as Bitmap;
            Bitmap target = new Bitmap(cropRect.Width, cropRect.Height);

            using(Graphics g = Graphics.FromImage(target))
            {
               g.DrawImage(src, new Rectangle(0, 0, target.Width, target.Height), cropRect, GraphicsUnit.Pixel);
            }

            return target;
        }

    }
}