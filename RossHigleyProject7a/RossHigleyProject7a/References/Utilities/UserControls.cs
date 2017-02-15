using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 * Controls.cs
 * Autors: Rob Merrick
 * This class is used to capture all events from the mouse and
 * keyboard and present them in a way so that the rest of the
 * program can use it.
 */

namespace RossHigleyProject7a
{
    class UserControls
    {

        //Variable Declarations
        private static bool leftMouseDown;
        private static bool rightMouseDown;

        ///************************************************************************************************
        ///<summary>Do not allow public instantiation of this class. This is a static-only class.</summary>
        ///************************************************************************************************
        
        private UserControls()
        {

        }

        public static void setLeftMouseDown(bool leftMouseDown)
        {
            UserControls.leftMouseDown = leftMouseDown;
        }

        public static void setRightMouseDown(bool rightMouseDown)
        {
            UserControls.rightMouseDown = rightMouseDown;
        }

        public static bool getLeftMouseDown()
        {
            return leftMouseDown;
        }

        public static bool getRightMouseDown()
        {
            return rightMouseDown;
        }

    }
}