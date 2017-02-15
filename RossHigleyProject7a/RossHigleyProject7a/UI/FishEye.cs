/* 
 Ross Higley 11/16/16
 This file contains the FishEye View Adapter class, which is a static class that converts 
 from absolute universe coordinates, to screen display coordinates, and back again. There 
 is some loss in precision when converting back, but it's very small (roughly 0.0003 in most cases). 

 Remember to run updateFormSize() whenever the window is resized, which is inside the Form Layout Event.
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RossHigleyProject7a
{
    public class FishEye
    {
        private static float ax;
        private static float bx;
        private static float cx;

        private static float ay;
        private static float by;
        private static float cy;


        private Rectangle _windowBounds;
        public Rectangle WindowBounds { get { return _windowBounds; } }

        private MainWindow _window;
        public MainWindow Window { get { return _window; } set { _window = value; UpdateFormSize(_window); } }

        private PointF _moveBoxSize;
        public PointF MoveBoxSize { get { return _moveBoxSize; } }

        private PointF _screenCenter;
        public PointF ScreenCenter { get { return _screenCenter; } }

        public FishEye(MainWindow window)
        {
            UpdateFormSize(window);
        }


        /// <summary>
        /// Ross Higley     11/29/16
        /// Call this whenever the window is resized. It adjusts the stretch and shift required to convert 
        /// between absolute coordinates and screen coordinates. 
        /// </summary>
        /// <param name="window">The Form you want to draw on (MainForm). </param>
        public void UpdateFormSize(MainWindow window)
        {
            _window = window;
            _windowBounds = window.ClientRectangle;
            ax = (float)(_windowBounds.Width / Math.PI);
            bx = (float)(Math.PI / _windowBounds.Width);
            cx = _windowBounds.Width / 2;

            ay = (float)(_windowBounds.Height / Math.PI);
            by = (float)(Math.PI / _windowBounds.Height);
            cy = _windowBounds.Height / 2;

            _moveBoxSize.X = (float)(_windowBounds.Width * 0.5);
            _moveBoxSize.Y = (float)(_windowBounds.Height * 0.5);

            _screenCenter.X = WindowBounds.Width / 2;
            _screenCenter.Y = WindowBounds.Height / 2;

        }

        /// <summary>
        /// Ross Higley     11/16/16
        /// Accepts absolute universe coordinate point and returns relative Fish-eye display coordinate point.
        /// </summary>
        public PointF AbsToScreen(PointF abs)
        {
            PointF tempPoint = new PointF();
            tempPoint.X = translateToFishEye(abs.X, ax, bx, cx);
            tempPoint.Y = translateToFishEye(abs.Y, ay, by, cy);
            return tempPoint;
        }

        /// <summary>
        /// Ross Higley     11/16/16
        /// Accepts absolute x coordinate of a universe point and returns the relative x coordinate in the Fish-eye display.  
        /// </summary>
        public float AbsToScreenX(float absX)
        {
            return translateToFishEye(absX, ax, bx, cx);
        }

        /// <summary>
        /// Ross Higley     11/16/16
        /// Accepts absolute y coordinate of a universe point and returns the relative y coordinate in the Fish-eye display. 
        /// </summary>
        public float AbsToScreenY(float absY)
        {
            return translateToFishEye(absY, ay, by, cy);
        }

        /// <summary>
        /// Ross Higley     11/29/16 
        /// Accepts relative coordinate in the screen, and returns the absolute
        /// coordinate in the universe, by "undo-ing" the fisheye math.  
        /// </summary>
        public PointF ScreenToAbs(PointF rel)
        {
            PointF tempPoint = new PointF();
            tempPoint.X = UndoTranslateToFishEye(rel.X, ax, bx, cx);
            tempPoint.Y = UndoTranslateToFishEye(rel.Y, ay, by, cy);
            return tempPoint;
        }

        /// <summary>
        /// Ross Higley     11/29/16 
        /// Accepts relative x coordinate in the screen, and returns the absolute 
        /// x coordinate in the universe, by "undo-ing" the fisheye math.  
        /// </summary>
        public float ScreenToAbsX(float relX)
        {
            return UndoTranslateToFishEye(relX, ax, bx, cx);
        }

        /// <summary>
        /// Ross Higley     11/29/16 
        /// Accepts relative y coordinate in the screen, and returns the absolute
        /// y coordinate in the universe, by "undo-ing" the fisheye math.  
        /// </summary>
        public float ScreenToAbsY(float relY)
        {
            return UndoTranslateToFishEye(relY, ay, by, cy);
        }

        /// <summary>
        /// Ross Higley     11/16/16 
        /// Uses fancy math to convert from absolute to fisheye. Even though this 
        /// method is used by both X and Y, note that they are calculated seperately. 
        /// When calculating x, only pass in ax, bx, and cx.  
        /// When calculating y, only pass in ay, by, and cy. 
        /// </summary>
        /// <param name="x">The coordinate you are converting to fishEye (either x or y).</param>
        /// <param name="scaleY">(ax or ay) scaleY of arctan graph. window width or height over pi.</param>
        /// <param name="scaleX">(bx or by) scaleX of arctan graph. reciprocal of scaleY. </param>
        /// <param name="offset">(cx or cy) horizontal shift of arctan graph. window width or height over 2. </param>
        /// <returns>FishEye coordinate. </returns>
        private float translateToFishEye(float x, float scaleY, float scaleX, float offset)
        {
            return (float)(scaleY * ArcTanCustom(scaleX * x) + offset);
        }

        /// <summary>
        /// Ross Higley     12/01/16
        /// Uses fancy math to convert from FishEye back to absolute. Even though this 
        /// method is used by both X and Y, note that they are calculated seperately. 
        /// When calculating x, only pass in ax, bx, and cx.  
        /// When calculating y, only pass in ay, by, and cy. 
        /// </summary>
        /// <param name="x">The coordinate you are converting to absolute (either x or y).</param>
        /// <param name="scaleY">(ax or ay) scaleY of arctan graph. window width or height over pi.</param>
        /// <param name="scaleX">(bx or by) scaleX of arctan graph. reciprocal of scaleY. </param>
        /// <param name="offset">(cx or cy) horizontal shift of arctan graph. window width or height over 2. </param>
        /// <returns>Absolute coordinate. </returns>
        private float UndoTranslateToFishEye(float x, float scaleY, float scaleX, float offset)
        {
            return (float)(UndoArcTanCustom((x - offset) / scaleY) / scaleX);
        }


        /// <summary>
        /// Ross Higley     11/16/16
        /// This function behaves like Math.Atan, the arctangent function, but is "straighter" in the middle, 
        /// so space is unbent in the center, but really bent near edges, which is what we need to create fisheye. 
        /// </summary>
        private float ArcTanCustom(float x)
        {
            return (float)(Math.Atan(x + (Math.Pow(x, 3.0) / 3.0)));
        }

        /// <summary>
        /// Ross Higley     12/01/16
        /// This is the inverse function of ArcTanCustom. It's pretty complicated, but is really accurate. 
        /// We do lose some precision, though, so keep that in mind when comparing coordinate points. 
        /// </summary>
        private float UndoArcTanCustom(float x)
        {
            double cosx = Math.Cos(x);
            double cos4x = Math.Pow(cosx, 4);

            double numerator = Math.Sqrt((13 * cos4x) - (5 * cos4x * Math.Cos(2 * x)));
            double denominator = Math.Sqrt(2);
            double term1 = 3 * Math.Sin(x) * Math.Pow(cosx, 2);

            double A = Math.Pow((numerator / denominator) + term1, (1.0 / 3.0));
            double B = Math.Pow(2.0, (1.0 / 3.0));

            double y = (A / (B * cosx)) - ((B * cosx) / A);
            return (float)y;
        }

        /// <summary>
        /// Ross Higley     12/01/16
        /// Returns as a string a table of sample values used to test the undoArcTanCustom() and ArcTanCustom() methods. 
        /// </summary>
        public string testUndoArcTanCustom()
        {
            StringBuilder results = new StringBuilder();
            results.AppendFormat("{0, -20}{1, -20}{2, -20}{3, -20} \n", "i", "undo", "custom", "undo(Custom)");
            for (int i = -10; i < 10; i += 1)
            {
                results.AppendFormat("{0, -20}{1, -20}{2, -20}{3, -20} \n", i, UndoArcTanCustom(i), ArcTanCustom(i), UndoArcTanCustom(ArcTanCustom(i)));
            }
            return results.ToString();
        }

        /// <summary>
        /// Ross Higley     12/01/16
        /// Returns as a string a table of sample values used to test the convertAbsoluteXToScreen() and convertScreenXToAbsolute() methods. 
        /// </summary>
        /// <returns></returns>
        public string testUndoConvert()
        {
            StringBuilder results = new StringBuilder();
            results.AppendFormat("{0, -20}{1, -20}{2, -20}{3, -20} \n", "i", "AbsToScreen", "ScreenToAbs", "ScreenToAbs(AbsToScreen)");
            for (int i = -3000; i < 3000; i += 195)
            {
                results.AppendFormat("{0, -20}{1, -20}{2, -20}{3, -20} \n", i, AbsToScreenX(i), ScreenToAbsX(i), ScreenToAbsX(AbsToScreenX(i)));
            }
            return results.ToString();
        }

    }
}
