using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;
using System.Drawing.Drawing2D;

namespace Lab2_winforms
{

    public partial class Form1 : Form
    {
        private Button selectedButton = null;
        private GraphicsPath drawnWall = null;
        private BindingList<Data> ls = new BindingList<Data>();
        private bool redrawFurniture = false;
        public Form1()
        {
            InitializeComponent();
            ResizeBitmap();
            bindingSource1.DataSource = ls;
            //FILE SAVING!!
   /*         FileStream fs = new FileStream("DataFile.bm", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(fs, ls);
            fs.Close();*/
            
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bindingSource1.Clear();
            ResizeBitmap();
        }

        private void ResizeBitmap()
        {
            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics g = Graphics.FromImage(bmp);
            g.FillRectangle(Brushes.White, 0, 0, bmp.Width, bmp.Height);
            GraphicsPath graphicsPath = new GraphicsPath();
            graphicsPath = new GraphicsPath(new Point[] { new Point(50, 50), new Point(100, 100), new Point(150,120) },
                new byte[] { (byte)PathPointType.Line, (byte)PathPointType.Line, (byte)PathPointType.Line });
            graphicsPath.AddBezier(1, 1, 50, 50, 100, 100, 120, 170);
            
            g.DrawPath(Pens.Red, graphicsPath);
            Matrix mtr = new Matrix();
            mtr.Translate(50, 50);
            mtr.Rotate(90);
            graphicsPath.Transform(mtr);
            g.DrawPath(Pens.Blue, graphicsPath);
            pictureBox1.Image = bmp;
            redrawFurniture = true;
            pictureBox1.Refresh();
            g.Dispose();
        }

        private void Furniture_Button_Click(object sender, EventArgs e)
        {
            ChangeButtonSelection((Button)sender, true);     
        }

        private void ChangeButtonSelection(Button button, bool select)
        {
            if (button == null) return;
            if (select)
            {
                if (button.BackColor == Color.White)
                {
                    ChangeButtonSelection(selectedButton, false);
                    selectedButton = button;
                    button.BackColor = Color.Brown;
                }
                else
                    ChangeButtonSelection(selectedButton, false);
            }
            else
            {
                if (selectedButton != button) return;
                selectedButton = null;
                button.BackColor = Color.White;
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (selectedButton == null) return;
            //if (selectedButton)
            AddNewFurniture(e.Location);
        }

        private void AddNewFurniture(Point location)
        {
            Bitmap bitmap = (Bitmap)selectedButton.Tag;

            int size = (int)Math.Ceiling(Math.Sqrt(Math.Pow(bitmap.Width, 2) + Math.Pow(bitmap.Height, 2)));

            Bitmap bmp = new Bitmap(size, size);
            Graphics gfx = Graphics.FromImage(bmp);
            Random random = new Random();

            gfx.TranslateTransform((float)bmp.Width / 2, (float)bmp.Height / 2);
            gfx.RotateTransform(random.Next(0, 360));
            gfx.TranslateTransform(-(float)bmp.Width / 2, -(float)bmp.Height / 2);
            gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;

            gfx.DrawImage(bitmap, new Point((size - bitmap.Width) / 2, (size - bitmap.Height) / 2));
            gfx.Dispose();

            Graphics g = Graphics.FromImage(pictureBox1.Image);

            Point point = location;
            point.X -= bmp.Width / 2;
            point.Y -= bmp.Height / 2;
            g.DrawImage(bmp, point);

            g.Dispose();

            if (selectedButton == coffee_button)
                ls.Add(new Data(bmp, location, "Coffe table"));
            else if (selectedButton == bed_button)
                ls.Add(new Data(bmp, location, "Double bed"));
            else if (selectedButton == sofa_button)
                ls.Add(new Data(bmp, location, "Sofa"));
            else if (selectedButton == table_button)
                ls.Add(new Data(bmp, location, "Table"));


            ChangeButtonSelection(selectedButton, false);
            pictureBox1.Refresh();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (!redrawFurniture) return;
            Graphics g = Graphics.FromImage(pictureBox1.Image);
            foreach (var data in ls)
            {
                Point point = data.Point;
                point.X -= data.Image.Width / 2;
                point.Y -= data.Image.Height / 2;
                g.DrawImage(data.Image, point);
            }
            redrawFurniture = false;
        }

        private void panel1_SizeChanged(object sender, EventArgs e)
        {
            if (panel1.Height > pictureBox1.Height || panel1.Width > pictureBox1.Width)
            {
                pictureBox1.Size = panel1.Size;
                ResizeBitmap();
                redrawFurniture = true;
                pictureBox1.Refresh();
            }
        }
    }

    [Serializable]
    public class Data
    {
        public GraphicsPath GraphicsPath { get; set; }
        public Image Image { get; set; }
        public Point Point { get; set; }
        public string Text { get; set; }

        public Data(Image img, Point pt, string text)
        {
            Image = img;
            Point = pt;
            Text = text;
        }

        public override string ToString()
        {
            return $"{Text} {Point}";
        }
    }
}
