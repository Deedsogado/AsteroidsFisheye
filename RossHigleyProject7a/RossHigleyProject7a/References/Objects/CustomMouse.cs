//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace RossHigleyProject7a
//{
//    class CustomMouse : Entity2D
//    {
//        public CustomMouse(MainWindow window):base (window)
//        {
//            setGraphic(new Graphic("Warp.png"));
//            setScale(0.1F, 0.1F);
//        }
//        public override void destroy()
//        {
//           // throw new NotImplementedException();
//        }

//        public override void update()
//        {
//            System.Drawing.Point tempMousePoint = System.Windows.Forms.Cursor.Position;
            
//            float mouseScreenX = parentWindow.PointToClient(tempMousePoint).X;
//            float mouseScreenY = parentWindow.PointToClient(tempMousePoint).Y;
            
//           setPosition(mouseScreenX, mouseScreenY, false);
//        }


//    }
//}
