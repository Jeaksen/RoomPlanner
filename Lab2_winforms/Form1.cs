using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;
using System.Drawing.Drawing2D;
using System.Collections.Generic;

namespace Lab2_winforms
{

    public partial class Form1 : Form
    {
        private Button selectedButton = null;
        private List<Point> wallPoints = new List<Point>();
        private BindingList<Data> ls = new BindingList<Data>();
        private bool creatingWall = false;

        public Form1()
        {
            InitializeComponent();
            RefreshBitmap();
            bindingSource1.DataSource = ls;
            //FILE SAVING!!
            /*         FileStream fs = new FileStream("DataFile.bm", FileMode.Create);
                     BinaryFormatter formatter = new BinaryFormatter();
                     formatter.Serialize(fs, ls);
                     fs.Close();*/
        }

        private void newBitmap_Click(object sender, EventArgs e)
        {
            bindingSource1.Clear();
            wallPoints.Clear();
            creatingWall = false;
            RefreshBitmap();
        }

        private void RefreshBitmap()
        {
            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = bmp;
            pictureBox1.Refresh();
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
                if (selectedButton == wall_button)
                    SaveWall();
                selectedButton = null;
                button.BackColor = Color.White;
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (selectedButton == null) return;
            //CAPTURE!
            if (selectedButton == wall_button)
            {
                if (e.Button == MouseButtons.Left)
                    AddWallPoint(e.Location, false);
                if (creatingWall && e.Button == MouseButtons.Right)
                {
                    SaveWall();
                    ChangeButtonSelection(selectedButton, false);
                }
            }
            else if (e.Button == MouseButtons.Left)
            {
                AddNewFurniture(e.Location);
                ChangeButtonSelection(selectedButton, false);
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBox1.Capture = false;
        }

        private void AddNewFurniture(Point location)
        {
            Bitmap bitmap = (Bitmap)selectedButton.Tag;
            /*
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
                        bitmap = bmp;
                        */

            Graphics g = Graphics.FromImage(pictureBox1.Image);
            DrawFurniture(g, bitmap, location);
            g.Dispose();

            if (selectedButton == coffee_button)
                ls.Add(new Data(bitmap, location, "Coffe table"));
            else if (selectedButton == bed_button)
                ls.Add(new Data(bitmap, location, "Double bed"));
            else if (selectedButton == sofa_button)
                ls.Add(new Data(bitmap, location, "Sofa"));
            else if (selectedButton == table_button)
                ls.Add(new Data(bitmap, location, "Table"));

            pictureBox1.Refresh();
        }

        private void AddWallPoint(Point location, bool liveDrawing)
        {
            if (!creatingWall)
            {
                ls.Add(new Data(location, "Wall"));
                wallPoints.Add(location);
                creatingWall = true;
            }
            if (liveDrawing)
                wallPoints.RemoveAt(wallPoints.Count - 1);
            
            wallPoints.Add(location);
            RefreshBitmap();
        }

        private void SaveWall()
        {
            int indexLast = ls.Count - 1;
            if (ls[indexLast].Image != null) throw new Exception("Last element of objects list is not an wall element");
            if (ls[indexLast].GraphicsPath != null) throw new Exception("Last element of objects list already has a GraphicsPath");
            // Last point is the temporary one used for drawing
            wallPoints.RemoveAt(wallPoints.Count - 1);
            byte[] pathPointTypes = new byte[wallPoints.Count];
            for (int i = 0; i < pathPointTypes.Length; i++)
                pathPointTypes[i] = (byte)PathPointType.Line;

            ls[indexLast].GraphicsPath = new GraphicsPath(wallPoints.ToArray(), pathPointTypes);
            wallPoints.Clear();
            creatingWall = false;
            RefreshBitmap();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if (creatingWall)
            {
                byte[] pathPointTypes = new byte[wallPoints.Count];
                for (int i = 0; i < pathPointTypes.Length; i++)
                    pathPointTypes[i] = (byte)PathPointType.Line;

                DrawWall(g, new GraphicsPath(wallPoints.ToArray(), pathPointTypes));
                
/*              Matrix mtr = new Matrix();
                mtr.Translate(50, 50);
                mtr.Rotate(90);
                wall.Transform(mtr);
                g.DrawPath(Pens.Blue, wall);*/
            }
            
            foreach (var data in ls)
            {
                //printing wall
                if (data.Image == null)
                {
                    // if the wall is currently created it's graphics path is set to null
                    if (data.GraphicsPath != null) DrawWall(g, data.GraphicsPath);
                }
                else //printing furniture
                {
                    DrawFurniture(g, data.Image, data.Point);
                }
            }
        }

        private void DrawWall(Graphics g, GraphicsPath path)
        {
            Pen wallPen = new Pen(Color.Black, 10);
            wallPen.LineJoin = LineJoin.Round;
            g.DrawPath(wallPen, path);
        }

        private void DrawFurniture(Graphics g, Image image, Point center)
        {
            center.X -= image.Width / 2;
            center.Y -= image.Height / 2;
            g.DrawImage(image, center);
        }

        private void panel1_SizeChanged(object sender, EventArgs e)
        {
            if (panel1.Height > pictureBox1.Height || panel1.Width > pictureBox1.Width)
            {
                pictureBox1.Size = panel1.Size;
                RefreshBitmap();
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (creatingWall)
                AddWallPoint(e.Location, true);
        }
    }
}
