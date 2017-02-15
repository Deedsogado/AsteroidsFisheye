using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RossHigleyProject7a
{
    class VerticalGridLine : Entity2D
    {
        public System.Drawing.PointF top = new PointF();
        public PointF bottom = new PointF();
        public float xPos
        {
            get
            {
                return top.X;
            }
            set
            {
                top.X = bottom.X = value;
            }
        }

        public VerticalGridLine(double xPos, double yMin, double yMax, MainWindow window) : base(window)
        {
            setGraphic(new Graphic("VerticalLine.png"));

            setPosition((float)xPos, (float)yMin, false);
        }
        public VerticalGridLine(int xPos, int yMax, int yMin, MainWindow window) : base(window)
        {
            setGraphic(new Graphic("VerticalLine.png"));

            setPosition((float)xPos, (float)yMin, false);
        }

        public override void update()
        {
          //  throw new NotImplementedException();
        }

        public override void destroy()
        {
           // throw new NotImplementedException();
        }
    }
}
