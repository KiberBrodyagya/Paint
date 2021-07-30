using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Panle_Finish
{
    [Serializable]
    public class Save
    {
        private static List<Shape> MASS = new List<Shape>(); 

        public Shape this[int i]
        {
            get { return MASS[i]; }
            set { 
                MASS[i] = value;
                MASS[i].Save_Shape();
            }
        }

        public void Add_Shape(Shape shape)
        {
            Shape.Path.Add(new GraphicsPath());

            MASS.Add(shape);
        }

        public int Get_Count()
        {
            return MASS.Count;
        }

        public List<Shape> GIVE_MASS()
        {
            return MASS;
        }

        public void Saves (string name_file)
        {
            BinaryFormatter serialize = new BinaryFormatter();
                using (var file = new FileStream(name_file, FileMode.Create))
                {
                    serialize.Serialize(file, MASS);
                }
        }

        public void Load(string file_name)
        {
            BinaryFormatter serialize = new BinaryFormatter();
            using (var file = new FileStream(file_name, FileMode.Open))
            {
                MASS.Clear();
                Shape.Path.Clear();
                Shape.History.Clear();

                var tmp = serialize.Deserialize(file) as List<Shape>;
                for(int i=0;i<tmp.Count;i++)
                {
                    tmp[i].Pens = new Pen(Color.FromArgb(tmp[i].save_color),tmp[i].pens_weight);
                    Shape.Path.Add(new GraphicsPath());
                    tmp[i].Save_Shape();
                }


                MASS = tmp;
            }
        }
    }
}
