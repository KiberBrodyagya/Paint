using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Panle_Finish
{
    [Serializable]
    public class Rectangles: Shape
    {
        public Rectangles(Point point, int weight, int height, int width_pen, Color color) : base(point, weight, height, width_pen, color) { }

        public override void Save_Shape()
        {
            Path[Path.Count - 1] = new GraphicsPath();
            Path[Path.Count - 1].AddRectangle(new Rectangle(Points.X, Points.Y, Weight, Height));
        }

        public override void Drawing(Graphics canvas)
        {
            canvas.DrawRectangle(Pens, new Rectangle(Points.X, Points.Y, Weight, Height));
        }

        public override void Change_Point(Point point, int index)
        {
            Points.X = point.X;
            Points.Y = point.Y;

            Path[index] = new GraphicsPath();
            Path[index].AddRectangle(new Rectangle(Points.X, Points.Y, Weight, Height));
        }

        public override void Fill_Shape(Graphics canvas)
        {
            canvas.FillRectangle(Fill_brush, new Rectangle(Points.X, Points.Y, Weight, Height));
        }
    }
}
