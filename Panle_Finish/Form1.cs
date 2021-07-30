using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


namespace Panle_Finish
{
    [Serializable]
    public partial class Form1 : Form
    {
        //Классы
        Save save; // Сохранение фигур
        Bone bones;


        //Разные переменные

        int dlina_pri_peremesh_x, dlina_pri_peremesh_y;

        string mode; // Режим рисования

        bool check_down; // Проверка на нажатие мыши

        int catch_index_down_cursor; // Пойманная фигура index

        bool cursor_down_on_shape; // check for catch_shape

        bool BONE_PUT;

        bool bone_isVisible; // Нажата одна из квадртатов

        int catch_bone_visible_rectangle;

        // Карандаши и так далее
        Pen Pencil; // Основной карандаш

        SolidBrush Brush_of_shape;

        public Form1()
        {
            InitializeComponent();
            Init();
        }

        public void Init()
        {
            mode = "cursor"; // mode = Курсор

            Pencil = new Pen(Color.Black, 3); // Pencil = Black, 3

            save = new Save(); // Инициализация класса save

            check_down = false; // Изначально не нажато ничего

            catch_index_down_cursor = -1; // Изначально -1 так как нету пойманого

            cursor_down_on_shape = false;

            BONE_PUT = false;

            bones = new Bone();

            bone_isVisible = false;

            catch_bone_visible_rectangle = -1; // Изначально не нажата
        }


        //Нажатие мышки на холст
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            switch (mode)
            {
                case "cursor": // Был режим курсор

                    if(BONE_PUT)
                    {
                        for(int i=0;i<bones.BONE_PATH.Length;i++)
                        {
                            if(bones.BONE_PATH[i].IsVisible(e.Location))
                            {
                                bone_isVisible = true;
                                catch_bone_visible_rectangle = i;
                            }
                        }
                    }
                    /////

                    if (bone_isVisible == false)
                    {
                        for (int i = 0; i < Shape.History.Count; i++)
                        {
                            if (Shape.Path[Shape.History[i]].IsVisible(e.Location))
                            {
                                catch_index_down_cursor = Shape.History[i];
                                cursor_down_on_shape = true;
                            }
                        }
                        if (cursor_down_on_shape == false)
                        {
                            for (int i = 0; i < Shape.Path.Count; i++)
                            {
                                if (Shape.Path[i].IsVisible(e.Location))
                                {
                                    catch_index_down_cursor = i;
                                    cursor_down_on_shape = true;
                                }
                            }
                        }
                        if (catch_index_down_cursor != -1)
                        {  // Если хоть что-то поймала
                            Shape.History.Add(catch_index_down_cursor);
                            dlina_pri_peremesh_x = e.X - save[catch_index_down_cursor].Points.X;
                            dlina_pri_peremesh_y = e.Y - save[catch_index_down_cursor].Points.Y;
                        }
                    }
                    break;

                case "circle": // Был нажат круг
                    save.Add_Shape(new Circle(e.Location, 0, 0, (int)Pencil.Width, Pencil.Color));

                    check_down = true; // Мышка нажата

                    panel1.Invalidate();
                    break;

                case "rectangle": // Был нажат Квадрат
                    save.Add_Shape(new Rectangles(e.Location, 0, 0, (int)Pencil.Width, Pencil.Color));
                    
                    check_down = true; // Мышка нажата

                    panel1.Invalidate();
                    break;

                case "triangle": // Нажат треугольник
                    save.Add_Shape(new Triangle(e.Location, 0 , 0 , (int)Pencil.Width, Pencil.Color ));

                    check_down = true; // Мышка нажата

                    panel1.Invalidate();

                    break;

                case "brush":

                    bool for_fill = false;

                    for(int i=0; i < Shape.History.Count; i++)
                    {
                        if(Shape.Path[Shape.History[i]].IsVisible(e.Location))
                        {
                            save[Shape.History[i]].have_brush = true;
                            save[Shape.History[i]].Fill_brush = Brush_of_shape;

                            for_fill = true;

                            panel1.Invalidate();
                        }
                    }

                    if (for_fill == false)
                    {
                        for (int i = 0; i < Shape.Path.Count; i++)
                        {
                            if (Shape.Path[i].IsVisible(e.Location))
                            {
                                save[i].have_brush = true;
                                save[i].Fill_brush = Brush_of_shape;
                                panel1.Invalidate();
                            }
                        }
                    }

                    for_fill = false;
                    

                    break;
            }
        }

        // Мышка пересекает холст
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            toolStripTextBox1.Text = e.X.ToString();
            toolStripTextBox2.Text = e.Y.ToString();
            switch (mode)
            {
                case "cursor": // Был режим курсор

                    if (bone_isVisible)
                    {
                        if (!(save[catch_index_down_cursor] is Triangle))
                        {
                            switch (catch_bone_visible_rectangle)
                            {
                                case (0):
                                    save[catch_index_down_cursor].Weight -= e.X - save[catch_index_down_cursor].Points.X;

                                    save[catch_index_down_cursor].Height -= e.Y - save[catch_index_down_cursor].Points.Y;

                                    save[catch_index_down_cursor].Change_Point(new Point(e.X, e.Y), catch_index_down_cursor);

                                    panel1.Invalidate();

                                    break;

                                case (1):

                                    save[catch_index_down_cursor].Weight = e.X - save[catch_index_down_cursor].Points.X;

                                    save[catch_index_down_cursor].Height -= e.Y - save[catch_index_down_cursor].Points.Y;

                                    save[catch_index_down_cursor].Change_Point(new Point(e.X - save[catch_index_down_cursor].Weight, e.Y), catch_index_down_cursor);

                                    panel1.Invalidate();

                                    break;

                                case (2):

                                    save[catch_index_down_cursor].Weight = e.X - save[catch_index_down_cursor].Points.X;

                                    save[catch_index_down_cursor].Height = e.Y - save[catch_index_down_cursor].Points.Y;

                                    save[catch_index_down_cursor].Change_Point(save[catch_index_down_cursor].Points, catch_index_down_cursor);

                                    panel1.Invalidate();
                                    
                                    break;
                                case (3):

                                    save[catch_index_down_cursor].Weight -= e.X - save[catch_index_down_cursor].Points.X;

                                    save[catch_index_down_cursor].Height = e.Y - save[catch_index_down_cursor].Points.Y;

                                    save[catch_index_down_cursor].Change_Point(new Point(e.X, e.Y - save[catch_index_down_cursor].Height),catch_index_down_cursor);

                                    panel1.Invalidate();

                                    break;
                            }
                        }
                        else if(save[catch_index_down_cursor] is Triangle)
                        {
                            switch (catch_bone_visible_rectangle)
                            {
                                case (0):

                                    save[catch_index_down_cursor].Weight -= e.X - (save[catch_index_down_cursor].Points.X - save[catch_index_down_cursor].Weight);

                                    save[catch_index_down_cursor].Height = -(e.Y - save[catch_index_down_cursor].Points.Y);

                                    save[catch_index_down_cursor].Change_Point(new Point(save[catch_index_down_cursor].Points.X, save[catch_index_down_cursor].Points.Y), catch_index_down_cursor);

                                    panel1.Invalidate();

                                    break;

                                case (1):

                                    save[catch_index_down_cursor].Weight += e.X - save[catch_index_down_cursor].Points.X;

                                    save[catch_index_down_cursor].Height = -(e.Y - save[catch_index_down_cursor].Points.Y);

                                    save[catch_index_down_cursor].Change_Point(new Point(e.X, save[catch_index_down_cursor].Points.Y), catch_index_down_cursor);

                                    panel1.Invalidate();

                                    break;

                                case (2):

                                    save[catch_index_down_cursor].Weight = e.X - (save[catch_index_down_cursor].Points.X - save[catch_index_down_cursor].Weight);

                                    save[catch_index_down_cursor].Height = e.Y - (save[catch_index_down_cursor].Points.Y - save[catch_index_down_cursor].Height);

                                    save[catch_index_down_cursor].Change_Point(new Point(e.X, e.Y), catch_index_down_cursor);

                                    panel1.Invalidate();

                                    break;

                                case (3):

                                    save[catch_index_down_cursor].Weight -= e.X - (save[catch_index_down_cursor].Points.X - save[catch_index_down_cursor].Weight);

                                    save[catch_index_down_cursor].Height = e.Y - (save[catch_index_down_cursor].Points.Y - save[catch_index_down_cursor].Height);

                                    save[catch_index_down_cursor].Change_Point(new Point(save[catch_index_down_cursor].Points.X, e.Y), catch_index_down_cursor);

                                    panel1.Invalidate();

                                    break;
                            }
                        }
                    }

                    else
                    {
                        if (cursor_down_on_shape)
                        {
                            save[catch_index_down_cursor].Change_Point(new Point(e.X - dlina_pri_peremesh_x, e.Y - dlina_pri_peremesh_y), catch_index_down_cursor);

                            panel1.Invalidate();
                        }
                    }
                    break;

                case "circle": // Был нажат круг
                    if (check_down)
                    {
                        save[save.Get_Count() - 1] = new Circle(save[save.Get_Count() - 1].Points, e.X - save[save.Get_Count() - 1].Points.X, e.Y - save[save.Get_Count() - 1].Points.Y, (int)Pencil.Width, Pencil.Color);

                        panel1.Invalidate();
                    }
                    break;

                case "rectangle": // Нажат квадрат
                    if (check_down)
                    {
                        save[save.Get_Count() - 1] = new Rectangles(save[save.Get_Count() - 1].Points, e.X - save[save.Get_Count() - 1].Points.X, e.Y - save[save.Get_Count() - 1].Points.Y, (int)Pencil.Width, Pencil.Color);

                        panel1.Invalidate();
                    }
                    break;

                case "triangle": // Нажат Треугольник
                    if (check_down)
                    {
                        save[save.Get_Count() - 1] = new Triangle(e.Location, e.Location.X - save[save.Get_Count() - 1].tmp_1.X, e.Location.Y - save[save.Get_Count() - 1].tmp_2.Y, (int)Pencil.Width, Pencil.Color);

                        panel1.Invalidate();
                    }
                    break;
            }
        }

        // Отпускание кнопки мыши
        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            switch (mode)
            {
                case "cursor": // Был режим курсор

                    if (bone_isVisible)
                    {
                        if (!(save[catch_index_down_cursor] is Triangle))
                        {
                            switch (catch_bone_visible_rectangle)
                            {
                                case (0):
                                    save[catch_index_down_cursor].Weight -= e.X - save[catch_index_down_cursor].Points.X;

                                    save[catch_index_down_cursor].Height -= e.Y - save[catch_index_down_cursor].Points.Y;

                                    save[catch_index_down_cursor].Change_Point(new Point(e.X, e.Y), catch_index_down_cursor);

                                    panel1.Invalidate();

                                    break;

                                case (1):

                                    save[catch_index_down_cursor].Weight = e.X - save[catch_index_down_cursor].Points.X;

                                    save[catch_index_down_cursor].Height -= e.Y - save[catch_index_down_cursor].Points.Y;

                                    save[catch_index_down_cursor].Change_Point(new Point(e.X - save[catch_index_down_cursor].Weight, e.Y), catch_index_down_cursor);

                                    panel1.Invalidate();

                                    break;

                                case (2):

                                    save[catch_index_down_cursor].Weight = e.X - save[catch_index_down_cursor].Points.X;

                                    save[catch_index_down_cursor].Height = e.Y - save[catch_index_down_cursor].Points.Y;

                                    save[catch_index_down_cursor].Change_Point(save[catch_index_down_cursor].Points, catch_index_down_cursor);

                                    panel1.Invalidate();

                                    break;
                                case (3):

                                    save[catch_index_down_cursor].Weight -= e.X - save[catch_index_down_cursor].Points.X;

                                    save[catch_index_down_cursor].Height = e.Y - save[catch_index_down_cursor].Points.Y;

                                    save[catch_index_down_cursor].Change_Point(new Point(e.X, e.Y - save[catch_index_down_cursor].Height), catch_index_down_cursor);

                                    panel1.Invalidate();

                                    break;
                            }
                            bone_isVisible = false;
                        }
                        else if (save[catch_index_down_cursor] is Triangle)
                        {
                            switch(catch_bone_visible_rectangle)
                            {
                                case (0):

                                    save[catch_index_down_cursor].Weight -= e.X - (save[catch_index_down_cursor].Points.X - save[catch_index_down_cursor].Weight);

                                    save[catch_index_down_cursor].Height = -(e.Y - save[catch_index_down_cursor].Points.Y);

                                    save[catch_index_down_cursor].Change_Point(new Point(save[catch_index_down_cursor].Points.X, save[catch_index_down_cursor].Points.Y), catch_index_down_cursor);

                                    panel1.Invalidate();

                                    break;

                                case (1):

                                    save[catch_index_down_cursor].Weight += e.X - save[catch_index_down_cursor].Points.X;

                                    save[catch_index_down_cursor].Height = -(e.Y - save[catch_index_down_cursor].Points.Y);

                                    save[catch_index_down_cursor].Change_Point( new Point(e.X, save[catch_index_down_cursor].Points.Y),catch_index_down_cursor);

                                    panel1.Invalidate();

                                    break;

                                case (2):

                                    save[catch_index_down_cursor].Weight = e.X - (save[catch_index_down_cursor].Points.X - save[catch_index_down_cursor].Weight);

                                    save[catch_index_down_cursor].Height = e.Y - (save[catch_index_down_cursor].Points.Y - save[catch_index_down_cursor].Height);

                                    save[catch_index_down_cursor].Change_Point(new Point(e.X, e.Y), catch_index_down_cursor);

                                    panel1.Invalidate();

                                    break;

                                case (3):

                                    save[catch_index_down_cursor].Weight -= e.X - (save[catch_index_down_cursor].Points.X - save[catch_index_down_cursor].Weight);

                                    save[catch_index_down_cursor].Height = e.Y - (save[catch_index_down_cursor].Points.Y - save[catch_index_down_cursor].Height);

                                    save[catch_index_down_cursor].Change_Point(new Point(save[catch_index_down_cursor].Points.X, e.Y), catch_index_down_cursor);

                                    panel1.Invalidate();

                                    break;
                            }
                            bone_isVisible = false;
                        }
                    }

                    ////////////////

                    else
                    {
                        if (cursor_down_on_shape) // Если была поймана фигура
                        {
                            save[catch_index_down_cursor].Change_Point(new Point(e.X - dlina_pri_peremesh_x, e.Y - dlina_pri_peremesh_y), catch_index_down_cursor);

                            cursor_down_on_shape = false;

                            BONE_PUT = true;

                            panel1.Invalidate();
                        }
                        else
                        {
                            BONE_PUT = false;
                            panel1.Invalidate();
                        }
                    }
                    break;

                case "circle": // Был нажат круг
                    if (check_down)
                    {
                        save[save.Get_Count() - 1] = new Circle(save[save.Get_Count() - 1].Points, e.X - save[save.Get_Count() - 1].Points.X, e.Y - save[save.Get_Count() - 1].Points.Y, (int)Pencil.Width, Pencil.Color);

                        check_down = false; // Кнопка отпущена

                        panel1.Invalidate();
                    }
                    break;

                case "rectangle": // Был нажат квадрат
                    if (check_down)
                    {
                        save[save.Get_Count() - 1] = new Rectangles(save[save.Get_Count() - 1].Points, e.X - save[save.Get_Count() - 1].Points.X, e.Y - save[save.Get_Count() - 1].Points.Y, (int)Pencil.Width, Pencil.Color);

                        check_down = false;

                        panel1.Invalidate();
                    }
                    break;

                case "triangle":
                    if (check_down)
                    {
                        save[save.Get_Count() - 1] = new Triangle(e.Location, e.Location.X - save[save.Get_Count() - 1].tmp_1.X, e.Location.Y - save[save.Get_Count() - 1].tmp_2.Y, (int)Pencil.Width, Pencil.Color);

                        check_down = false;

                        panel1.Invalidate();
                    }
                    break;
            }
        }

        //Размер карандаша
        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            Pencil.Width = trackBar1.Value;
        }


        // Выбор фигуры цвета заливки
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            switch ((sender as ToolStripButton).Text)
            {
                case "Курсор":

                    mode = "cursor";

                    break;

                case "Круг":

                    mode = "circle";

                    break;

                case "Квадрат":

                    mode = "rectangle";

                    break;

                case "Треугольник":

                    mode = "triangle";

                    break;

                case "Цвет": // Pencil Color

                    ColorDialog tmp = new ColorDialog();

                    tmp.ShowDialog();
                    toolStripButton7.BackColor = tmp.Color;
                    Pencil.Color = tmp.Color;

                    break;

                case "Заливка":

                    ColorDialog colorDialog = new ColorDialog();

                    if(colorDialog.ShowDialog() == DialogResult.OK)
                    {
                        Brush_of_shape = new SolidBrush(colorDialog.Color);
                        mode = "brush";
                    }

                    break;

                case "Сохранить":

                    SaveFileDialog saving = new SaveFileDialog();

                    saving.Filter = "PNG|.png";

                    if (saving.ShowDialog() == DialogResult.OK)
                    {
                        save.Saves(saving.FileName);
                    }

                    panel1.Invalidate();
                    break;

                case "Загрузить":

                    OpenFileDialog opens = new OpenFileDialog();

                    opens.Filter = "PNG|*.png";

                    if(opens.ShowDialog() == DialogResult.OK)
                    {
                        save.Load(opens.FileName);
                    }

                    bone_isVisible = false;

                    BONE_PUT = false;

                    cursor_down_on_shape = false;

                    catch_index_down_cursor = -1;

                    panel1.Invalidate();

                    break;

            }
        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            int first = -1;

            if (Shape.History.Count != 0) { first = Shape.History[Shape.History.Count - 1]; }  

            for(int i=0;i<save.Get_Count();i++)
            {
                if(i == first) { continue; }
                if (!save[i].have_brush) { save[i].Drawing(e.Graphics); }
                else
                {
                    save[i].Drawing(e.Graphics);
                    save[i].Fill_Shape(e.Graphics);
                }
            }

            if (first != -1)
            {
                if (save[first].have_brush)
                {
                    save[first].Drawing(e.Graphics);
                    save[first].Fill_Shape(e.Graphics);
                }
                else
                {
                    save[first].Drawing(e.Graphics);
                }
            }

            if (BONE_PUT) { bones.Bone_Put(save[catch_index_down_cursor], e.Graphics); }
        }
    }
}
