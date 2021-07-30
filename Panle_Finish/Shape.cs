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
    public class Shape
    {
        public static List<int> History = new List<int>();

        public Point tmp_1 = Point.Empty; // First point 

        public Point tmp_2 = Point.Empty; // Second point

        public Point Points;

        public int Weight, Height;

        public int save_color, pens_weight;

        [NonSerialized] public static List<GraphicsPath> Path = new List<GraphicsPath>();

        [NonSerialized] public Pen Pens = new Pen(Color.Black, 3);

        public bool have_brush = false;

        [NonSerialized] public SolidBrush Fill_brush;

        public Shape(Point point, int weight, int height, int width_pen, Color color)
        {
            Points = point;
            
            Weight = weight;
            
            Height = height;

            Pens.Color = color;
            
            Pens.Width = width_pen;

            save_color = Pens.Color.ToArgb();

            pens_weight = (int)Pens.Width;
        }

        public virtual void Drawing(Graphics canvas) { }

        public virtual void Save_Shape() { }

        public virtual void Change_Point (Point point, int index) { }

        public virtual void Fill_Shape(Graphics canvas) { }
    }
}
