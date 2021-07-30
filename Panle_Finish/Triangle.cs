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
    public class Triangle: Shape
    {
        [NonSerialized]public GraphicsPath new_path = new GraphicsPath();

        public Triangle(Point point, int weight, int height, int width_pen, Color color) : base(point, weight, height, width_pen, color) 
        {
            tmp_1.X = point.X - weight;
            tmp_1.Y = point.Y;

            tmp_2.X = point.X - weight / 2;
            tmp_2.Y = point.Y - height;
        }

        public override void Save_Shape()
        {
            Path[Path.Count - 1] = new GraphicsPath();

            Point[] tmp = new Point[3] { Points, tmp_1, tmp_2 };

            new_path = new GraphicsPath();
            new_path.AddPolygon(tmp);

            Path[Path.Count - 1].AddPolygon(tmp);
        }

        public override void Drawing(Graphics canvas)
        {
            canvas.DrawPath(Pens, new_path);
        }

        public override void Change_Point(Point point, int index)
        {
            Points.X = point.X;
            Points.Y = point.Y;

            tmp_1.X = point.X - Weight;
            tmp_1.Y = point.Y;

            tmp_2.X = point.X - Weight / 2;
            tmp_2.Y = point.Y - Height;

            Point[] tmp = new Point[3] { Points, tmp_1, tmp_2 };

            new_path = new GraphicsPath();

            new_path.AddPolygon(tmp);

            Path[index] = new GraphicsPath();

            Path[index].AddPolygon(tmp);
        }

        public override void Fill_Shape(Graphics canvas)
        {
            canvas.FillPath(Fill_brush,new_path);
        }
    }
}
