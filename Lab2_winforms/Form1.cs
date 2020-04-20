using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Globalization;

namespace Lab2_winforms
{

    public partial class Form1 : Form
    {
        private Button selectedButton = null;
        private List<Point> wallPoints = new List<Point>();
        private BindingList<Data> planElements = new BindingList<Data>();
        private bool creatingWall = false;
        private bool movingObject = false;
        private Point? movingAnchor = null;
        private const int WALL_WIDTH = 10;
        private int selectedIndex = -1;


        private int Distance(Point p1, Point p2) => (int)Math.Sqrt((Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2)));

        public Form1()
        {
            InitializeComponent();
            RefreshBitmap();
            bindingSource1.DataSource = planElements;
            //CultureInfo.CurrentUICulture;
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
            if (selectedIndex >= 0) planElements[selectedIndex].IsSelected = false;
            furnitureList.SelectedIndex = selectedIndex = -1;
            RefreshBitmap();
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
            // No buttons selected - checking for item selection
            if (selectedButton == null && e.Button == MouseButtons.Left)
            {
                TrySelect(e.Location);
            }
            else if (selectedButton == wall_button)
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

        private void TrySelect(Point location)
        {
            if (planElements.Count == 0) return;
            Data closestElement = planElements[0];
            int index = 0, closestIndex = -1;
            foreach (Data data in planElements)
            {
                if (data.Image != null)
                {
                    if (TestBitmapHit(data, location) && Distance(data.Point, location) <= Distance(closestElement.Point, location))
                    {
                        closestElement = data;
                        closestIndex = index;
                    }
                }
                else if (TestWallHit(data, location))
                {
                    closestElement = data;
                    closestIndex = index;
                    // if the wall is hit we want to select it right away
                    break;
                }
                index++;
            }
            // Deselect previous element if there is one
            if (selectedIndex >= 0) planElements[selectedIndex].IsSelected = false;
            
            if (closestIndex >= 0)
            {
                planElements[closestIndex].IsSelected = true;
                furnitureList.SelectedIndex = closestIndex;
                selectedIndex = closestIndex;
                movingObject = true;
                movingAnchor = new Point(location.X - closestElement.Point.X, location.Y - closestElement.Point.Y);
            } 
            else
                furnitureList.SelectedIndex = selectedIndex = -1; // If nothing was hit deselect current element and set list's position to default (-1)

            RefreshBitmap();
        }

        private bool TestBitmapHit(Data data, Point point)
        {
            return Math.Abs(data.Point.X - point.X) < data.Image.Width / 2 && Math.Abs(data.Point.Y - point.Y) < data.Image.Height / 2;
        }

        private bool TestWallHit (Data data, Point point)
        {
            return data.GraphicsPath.IsOutlineVisible(point, new Pen(Color.Black, WALL_WIDTH));
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            movingObject = false;
            pictureBox1.Capture = false;
            movingAnchor = null;
        }

        private void AddNewFurniture(Point location)
        {
            Bitmap bitmap = (Bitmap)selectedButton.Tag;

            Bitmap bmp = new Bitmap(bitmap.Width, bitmap.Height);

            Graphics g = Graphics.FromImage(bmp);
            DrawFurniture(g, bitmap, new Point(bitmap.Width / 2, bitmap.Height / 2));
            g.Dispose();

            g = Graphics.FromImage(pictureBox1.Image);

            DrawFurniture(g, bmp, location);
            g.Dispose();

            if (selectedButton == coffee_button)
                planElements.Add(new Data(bmp, location, "Coffe table"));
            else if (selectedButton == bed_button)
                planElements.Add(new Data(bmp, location, "Double bed"));
            else if (selectedButton == sofa_button)
                planElements.Add(new Data(bmp, location, "Sofa"));
            else if (selectedButton == table_button)
                planElements.Add(new Data(bmp, location, "Table"));

            furnitureList.SelectedIndex = -1;
            pictureBox1.Refresh();
            ChangeButtonSelection(selectedButton, false);
        }

        private void AddWallPoint(Point location, bool liveDrawing)
        {
            if (!creatingWall)
            {
                planElements.Add(new Data(location, "Wall"));
                wallPoints.Add(location);
                creatingWall = true;
                furnitureList.SelectedIndex = -1;
            }
            if (liveDrawing)
                wallPoints.RemoveAt(wallPoints.Count - 1);
            
            wallPoints.Add(location);
            RefreshBitmap();
        }

        private void SaveWall()
        {
            if (!creatingWall) return;
            int indexLast = planElements.Count - 1;
            if (planElements[indexLast].Image != null) throw new Exception("Last element of objects list is not an wall element");
            if (planElements[indexLast].GraphicsPath != null) throw new Exception("Last element of objects list already has a GraphicsPath");
            // Last point is the temporary one used for drawing
            if (wallPoints.Count > 2)
            {
                wallPoints.RemoveAt(wallPoints.Count - 1);
                byte[] pathPointTypes = new byte[wallPoints.Count];
                for (int i = 0; i < pathPointTypes.Length; i++)
                    pathPointTypes[i] = (byte)PathPointType.Line;

                planElements[indexLast].GraphicsPath = new GraphicsPath(wallPoints.ToArray(), pathPointTypes);
            }
            else
            {
                planElements.RemoveAt(planElements.Count - 1);
            }
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
            }

            foreach (var data in planElements)
            {
                if (data.Image == null) // if the wall is currently created it's graphics path is set to null
                {
                    if (data.GraphicsPath != null) DrawWall(g, data.GraphicsPath, data.IsSelected, data.RotationDelta, data.Point);
                }
                else //printing furniture
                    DrawFurniture(g, data.Image, data.Point, data.IsSelected, data.Rotation);
            }
        }

        private void DrawWall(Graphics g, GraphicsPath path, bool semitransparent = false, float rotation = 0, Point? location = null)
        {
            Color color = semitransparent ? Color.FromArgb(255 / 2, Color.Black) : Color.Black;
            Pen wallPen = new Pen(color, WALL_WIDTH);
            wallPen.LineJoin = LineJoin.Round;

            Matrix matrix = new Matrix();
            if (rotation != 0) 
                matrix.RotateAt(rotation, path.PathPoints[0]);
            if (location.HasValue) 
                matrix.Translate(location.Value.X - path.PathPoints[0].X,location.Value.Y - path.PathPoints[0].Y);
            path.Transform(matrix);

            g.DrawPath(wallPen, path);
            wallPen.Dispose();
        }

        private void DrawFurniture(Graphics g, Image image, Point center, bool semitransparent = false, float rotation = 0)
        {
            if (rotation != 0)
            {
                g.TranslateTransform(center.X, center.Y);
                g.RotateTransform(rotation);
                g.TranslateTransform(-center.X, -center.Y);
                g.InterpolationMode = InterpolationMode.High;
            }
            Point draw = center;
            draw.X -= image.Width / 2;
            draw.Y -= image.Height / 2;

            if (!semitransparent)
                g.DrawImage(image, draw);
            else
            {
                float[][] colorMatrixElements = {
                new float[] {1,  0,  0,  0, 0},
                new float[] {0,  1,  0,  0, 0},
                new float[] {0,  0,  1,  0, 0},
                new float[] {0,  0,  0,  0.5f, 0},
                new float[] {0, 0, 0, 0, 1}};
                ColorMatrix colorMatrix = new ColorMatrix(colorMatrixElements);
                ImageAttributes imageAttributes = new ImageAttributes();
                imageAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                g.DrawImage(image, new Rectangle(draw.X, draw.Y, image.Width, image.Height), 0, 0, image.Width,  image.Height, GraphicsUnit.Pixel, imageAttributes);
            }
            g.ResetTransform();
        }

        private void panel1_SizeChanged(object sender, EventArgs e)
        {
            if (panel1.Height > pictureBox1.Height || panel1.Width > pictureBox1.Width)
            {
                pictureBox1.Size = panel1.Size;
                RefreshBitmap();
            }
        }
        
        private void pictureBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            HandledMouseEventArgs arg = (HandledMouseEventArgs)e;
            arg.Handled = true;
            if (selectedIndex != -1)
            {
                planElements[selectedIndex].Rotation += e.Delta / 5;
                RefreshBitmap();
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (creatingWall)
                AddWallPoint(e.Location, true);
            else if (movingObject)
            {
                planElements[selectedIndex].Point = new Point(e.Location.X - movingAnchor.Value.X, e.Location.Y - movingAnchor.Value.Y);
                planElements.ResetItem(selectedIndex);
                RefreshBitmap();
            }

        }

        private void furnitureList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (selectedIndex >= 0 && selectedIndex < planElements.Count)
                planElements[selectedIndex].IsSelected = false;
            if (furnitureList.SelectedIndex >= 0 && furnitureList.SelectedIndex < planElements.Count)
            {
                planElements[furnitureList.SelectedIndex].IsSelected = true;
                selectedIndex = furnitureList.SelectedIndex;
                RefreshBitmap();
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (selectedIndex != -1 && e.KeyCode == Keys.Delete)
            {
                planElements.RemoveAt(selectedIndex);
                selectedIndex = -1;
                movingAnchor = null;
                movingObject = false;
                creatingWall = false;
                wallPoints.Clear();
                furnitureList.SelectedIndex = selectedIndex = -1;
                RefreshBitmap();
            }
        }

        private void Save_Click(object sender, EventArgs e)
        {
            Stream stream = null;
            SaveFileDialog fileDialog = new SaveFileDialog();

            fileDialog.Filter = "Room plans (*.bm)|*.bm";
            fileDialog.DefaultExt = ".bm";
            fileDialog.RestoreDirectory = true;

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                var parts = fileDialog.FileName.Split('.');
                if (parts[parts.Length - 1] != "bm")
                {
                    MessageBox.Show("Invalid file exstension!", "Saving info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                try
                {
                    if ((stream = fileDialog.OpenFile()) != null)
                    {
                        BinaryFormatter formatter = new BinaryFormatter();

                        formatter.Serialize(stream, planElements);
                        MessageBox.Show("File saved!", "Saving info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Failed to save the file!", "Saving info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (stream != null)
                        stream.Close();
                }
                
            }
        }

        private void Open_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Room plans (*.bm)|*.bm";
                var fileDialog = new OpenFileDialog();
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    var parts = fileDialog.FileName.Split('.');
                    if (parts[parts.Length - 1] != "bm")
                    {
                        MessageBox.Show("Invalid file exstension!", "Opening info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    Stream stream = null;
                    try
                    {
                        stream = openFileDialog.OpenFile();
                        BinaryFormatter formatter = new BinaryFormatter();
                        planElements.Clear();
                        foreach (var item in (BindingList<Data>)formatter.Deserialize(stream))
                            planElements.Add(item);
                        MessageBox.Show("File opened!", "Opening info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        RefreshBitmap();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Failed to open the file!", "Opening info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        if (stream != null)
                            stream.Close();
                    }
                    furnitureList.SelectedIndex = -1;
                }
            }
            
        }
    }
}
