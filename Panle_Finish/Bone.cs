using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Panle_Finish
{
    public class Bone
    {
        public GraphicsPath[] BONE_PATH = new GraphicsPath[4];
        
        public void Bone_Put(Shape shape, Graphics canvas)
        {
            for(int i=0;i<BONE_PATH.Length;i++) { BONE_PATH[i] = new GraphicsPath(); }
            if (!(shape is Triangle))
            {
                // Левый верхний угол  1-ый элемент
                canvas.DrawRectangle(new Pen(Color.Black, 2), new Rectangle(shape.Points.X - 5, shape.Points.Y - 5, 10, 10));
                BONE_PATH[0].AddRectangle(new Rectangle(shape.Points.X - 5, shape.Points.Y - 5, 10, 10));
                


                for (int i = shape.Points.X + 4; i < shape.Points.X + shape.Weight; i += 3)
                {
                    if (i + 5 >= shape.Points.X + shape.Weight) break;
                    if (i % 2 == 0)
                    {
                        canvas.DrawLine(new Pen(Color.Black, 2), i, shape.Points.Y, i + 3, shape.Points.Y);
                    }
                }

                // Правый верхний угол 2-ой элемент
                canvas.DrawRectangle(new Pen(Color.Black, 2), new Rectangle(shape.Points.X + shape.Weight - 5, shape.Points.Y - 5, 10, 10));
                BONE_PATH[1].AddRectangle(new Rectangle(shape.Points.X + shape.Weight - 5, shape.Points.Y - 5, 10, 10));

                for (int i = shape.Points.Y + 3; i < shape.Points.Y + shape.Height; i += 3)
                {
                    if (i + 5 >= shape.Points.Y + shape.Height) break;
                    if (i % 2 == 0)
                    {
                        canvas.DrawLine(new Pen(Color.Black, 2), shape.Points.X + shape.Weight + 1, i, shape.Points.X + shape.Weight + 1, i + 3);
                    }
                }

                // Правый нижний угол 3-ий элемент
                canvas.DrawRectangle(new Pen(Color.Black, 2), new Rectangle(shape.Points.X + shape.Weight - 5, shape.Points.Y + shape.Height - 5, 10, 10));
                BONE_PATH[2].AddRectangle(new Rectangle(shape.Points.X + shape.Weight - 5, shape.Points.Y + shape.Height - 5, 10, 10));

                for (int i = shape.Points.Y + 3; i < shape.Points.Y + shape.Height; i += 3)
                {
                    if (i + 5 >= shape.Points.Y + shape.Height) break;
                    if (i % 2 == 0)
                    {
                        canvas.DrawLine(new Pen(Color.Black, 2), shape.Points.X + 1, i, shape.Points.X + 1, i + 3);
                    }
                }

                // Левый нижний угол 4-ий элемент
                canvas.DrawRectangle(new Pen(Color.Black, 2), new Rectangle(shape.Points.X - 5, shape.Points.Y + shape.Height - 5, 10, 10));
                BONE_PATH[3].AddRectangle(new Rectangle(shape.Points.X - 5, shape.Points.Y + shape.Height - 5, 10, 10));

                for (int i = shape.Points.X + 4; i < shape.Points.X + shape.Weight; i += 3)
                {
                    if (i + 5 >= shape.Points.X + shape.Weight) break;
                    if (i % 2 == 0)
                    {
                        canvas.DrawLine(new Pen(Color.Black, 2), i, shape.Points.Y + shape.Height - 2, i + 3, shape.Points.Y + shape.Height - 2);
                    }
                }
            }
            else ///////// ELSE
            {
                int x = shape.Points.X - shape.Weight;
                int y = shape.Points.Y - shape.Height;

                // Левый верхний угол  1 - ый элемент
                canvas.DrawRectangle(new Pen(Color.Black, 2), new Rectangle(x - 5, y - 5, 10, 10));
                BONE_PATH[0].AddRectangle(new Rectangle(x - 5, y - 5, 10, 10));

                for (int i = x + 4; i < x + shape.Weight; i += 3)
                {
                    if (i + 5 >= x + shape.Weight) break;
                    if (i % 2 == 0)
                    {
                        canvas.DrawLine(new Pen(Color.Black, 2), i, y, i + 3, y);
                    }
                }

                // Правый верхний угол  2 - ый элемент
                canvas.DrawRectangle(new Pen(Color.Black, 2), new Rectangle(x + shape.Weight - 5, y - 5, 10, 10));
                BONE_PATH[1].AddRectangle(new Rectangle(x + shape.Weight - 5, y - 5, 10, 10));

                for (int i = y + 3; i < y + shape.Height; i += 3)
                {
                    if (i + 5 >= y + shape.Height) break;
                    if (i % 2 == 0)
                    {
                        canvas.DrawLine(new Pen(Color.Black, 2), x + shape.Weight + 1, i, x + shape.Weight + 1, i + 3);
                    }
                }
                // Правый нижний угол  3 - ый элемент
                canvas.DrawRectangle(new Pen(Color.Black, 2), new Rectangle(x + shape.Weight - 5, y + shape.Height - 5, 10, 10));
                BONE_PATH[2].AddRectangle(new Rectangle(x + shape.Weight - 5, y + shape.Height - 5, 10, 10));

                for (int i = y + 3; i < y + shape.Height; i += 3)
                {
                    if (i + 5 >= y + shape.Height) break;
                    if (i % 2 == 0)
                    {
                        canvas.DrawLine(new Pen(Color.Black, 2), x + 1, i, x + 1, i + 3);
                    }
                }

                // Левый нижний угол  3 - ый элемент
                canvas.DrawRectangle(new Pen(Color.Black, 2), new Rectangle(x - 5, y + shape.Height - 5, 10, 10));
                BONE_PATH[3].AddRectangle(new Rectangle(x - 5, y + shape.Height - 5, 10, 10));

                for (int i = x + 4; i < x + shape.Weight; i += 3)
                {
                    if (i + 5 >= x + shape.Weight) break;
                    if (i % 2 == 0)
                    {
                        canvas.DrawLine(new Pen(Color.Black, 2), i, y + shape.Height - 1, i + 3, y + shape.Height - 1);
                    }
                }
            }
                
        }
    }
}
