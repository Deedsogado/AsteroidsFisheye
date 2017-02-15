using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RossHigleyProject7a
{
    class HorizontalGridLine : Entity2D
    {
        public PointF left = new PointF();
        public PointF right = new PointF();

        public float yPos
        {
            get
            {
                return left.Y;
            }
            set
            {
                left.Y = right.Y = value;
            }
        }


        public HorizontalGridLine(double yPos, double xMin, double xMax, MainWindow window): base(window)
        {
            setGraphic(new Graphic("HorizontalLine.png"));

            setPosition((float)xMin, (float)yPos, false);
        }

        public HorizontalGridLine(int yPos, int xMin, int xMax, MainWindow window) : base(window)
        {
            setGraphic(new Graphic("HorizontalLine.png"));

            setPosition((float)xMin, (float)yPos, false);
        }

        public override void update()
        {
            //throw new NotImplementedException();
        }

        public override void destroy()
        {
            //throw new NotImplementedException();
        }
    }
}
