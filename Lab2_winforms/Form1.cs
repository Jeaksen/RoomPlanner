using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Globalization;
using System.Resources;

namespace Lab2_winforms
{

    public partial class RoomPlanner : Form
    {
        private Button selectedButton = null;
        private List<Point> wallPoints = new List<Point>();
        private BindingList<Data> planElements = new BindingList<Data>();
        private bool creatingWall = false;
        private bool movingObject = false;
        private Point? movingAnchor = null;
        private const int WALL_WIDTH = 10;
        private int selectedIndex = -1;
        private List<CultureInfo> cultures = new List<CultureInfo> ();


        /// <summary>
        /// Calculated the distance between two points
        /// </summary>
        /// <param name="p1">First point</param>
        /// <param name="p2">Second point</param>
        /// <returns>Distance rounded down</returns>
        private int Distance(Point p1, Point p2) => (int)Math.Sqrt((Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2)));

        public RoomPlanner()
        {
            cultures.Add(CultureInfo.GetCultureInfo("en-GB"));
            cultures.Add(CultureInfo.GetCultureInfo("pl-PL"));
            InitializeComponent();
            bindingSource1.DataSource = planElements;
            pictureBox1.Size = panel1.Size;
            RefreshBitmap();
        }

        /// <summary>
        /// Handles 'New Bitmap' button press - resets elements, clears flags and elements list, refreshes bitmap
        /// </summary>
        private void newBitmap_Click(object sender, EventArgs e)
        {
            bindingSource1.Clear();
            wallPoints.Clear();
            creatingWall = movingObject = false;
            movingAnchor = null;
            selectedIndex = -1;
            RefreshBitmap();
        }

        /// <summary>
        /// Assigns a new bitmap with size of PictureBox the Image property of the PictureBox 
        /// </summary>
        private void RefreshBitmap()
        {
            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = bmp;
            pictureBox1.Refresh();
        }


        /// <summary>
        /// Handles Click event for element buttons. Selects/deselects a button. If an element was selected removes the selection and refreshes the bitmap 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Furniture_Button_Click(object sender, EventArgs e)
        {
            ChangeButtonSelection((Button)sender, true);
            if (selectedIndex >= 0)
            {
                planElements[selectedIndex].IsSelected = false;
                furnitureList.SelectedIndex = selectedIndex = -1;
                RefreshBitmap();
            }
        }

        /// <summary>
        /// Change the selection of a button
        /// </summary>
        /// <param name="button">Button for which the selection will be changed</param>
        /// <param name="select">Flag indication whether the button should be selected(true) of deselected(false). 
        /// If true is given, and a button is already selected it will be deselected</param>
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

        /// <summary>
        /// Handles mouse buttons event for the PictueBox. If an element button is selected: drawn a element on left click, saves the wall on right click and wall selected. 
        /// If no button is selected tries to select an element.  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    AddWallPoint(e.Location);
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


        /// <summary>
        /// Handles the MouseUp event for the PictureBox. Clears the flag indication that an element is moved and clears the anchor point
        /// </summary>
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            movingObject = false;
            movingAnchor = null;
        }

        /// <summary>
        /// This function checks if at the given location there is any element. If there is it is selected
        /// If a wall is hit directly it is immediately selected, else a furniture element whose center is the closest to the location is chosen among those
        /// for which the location is within the size of the image. If nothing was hit the current element is deselected.
        /// </summary>
        /// <param name="location"></param>
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

        /// <summary>
        /// Checks whether a furniture element was selected
        /// </summary>
        /// <param name="data">Data object describing the furniture element</param>
        /// <param name="point">Mouse click location</param>
        /// <returns>Flag indicating whether the was a hit</returns>
        private bool TestBitmapHit(Data data, Point point)
        {
            return Math.Abs(data.Point.X - point.X) < data.Image.Width / 2 && Math.Abs(data.Point.Y - point.Y) < data.Image.Height / 2;
        }

        /// <summary>
        /// Checks whether a wall element was selected
        /// </summary>
        /// <param name="data">Data object describing the wall element</param>
        /// <param name="point">Mouse click location</param>
        /// <returns>Flag indicating whether the was a hit</returns>
        private bool TestWallHit (Data data, Point point)
        {
            return data.GraphicsPath.IsOutlineVisible(point, new Pen(Color.Black, WALL_WIDTH));
        }

        /// <summary>
        /// Adds a new furniture to the planElements at a given location. The furniture type is deduced from the selected button
        /// Forces the PictureBox to redraw, but the bitmap is not refreshed.
        /// </summary>
        /// <param name="location">Center point of the furniture to be drawn</param>
        private void AddNewFurniture(Point location)
        {
            Bitmap bitmap = (Bitmap)selectedButton.Tag;

            Graphics g = Graphics.FromImage(pictureBox1.Image);
            DrawFurniture(g, bitmap, location);
            g.Dispose();

            if (selectedButton == coffee_button)
                planElements.Add(new Data(bitmap, location, "coffe_table"));
            else if (selectedButton == bed_button)
                planElements.Add(new Data(bitmap, location, "double_bed"));
            else if (selectedButton == sofa_button)
                planElements.Add(new Data(bitmap, location, "sofa"));
            else if (selectedButton == table_button)
                planElements.Add(new Data(bitmap, location, "table"));

            furnitureList.SelectedIndex = -1;
            pictureBox1.Refresh();
            ChangeButtonSelection(selectedButton, false);
        }

        /// <summary>
        /// Adds the given point to the currently drawn wall and refreshes the bitmap
        /// </summary>
        /// <param name="location">New point of the wall</param>
        /// <param name="liveDrawing">Optional parameter, if set to true the last point is removed. Used for drawing the wall at the mouse position</param>
        private void AddWallPoint(Point location, bool liveDrawing = false)
        {
            if (!creatingWall)
            {
                planElements.Add(new Data(location, "wall"));
                wallPoints.Add(location);
                creatingWall = true;
                furnitureList.SelectedIndex = -1;
            }
            if (liveDrawing)
                wallPoints.RemoveAt(wallPoints.Count - 1);
            
            wallPoints.Add(location);
            RefreshBitmap();
        }


        /// <summary>
        /// When a wall is no longer drawn this function shall be called to save it to the planElements list.
        /// If the wall consists of two points (start point and the current mouse position) it is removed.
        /// </summary>
        private void SaveWall()
        {
            if (!creatingWall) return;

            if (wallPoints.Count > 2)
            {
                wallPoints.RemoveAt(wallPoints.Count - 1);
                byte[] pathPointTypes = new byte[wallPoints.Count];
                for (int i = 0; i < pathPointTypes.Length; i++)
                    pathPointTypes[i] = (byte)PathPointType.Line;

                // Last point is the temporary one used for drawing the wall
                planElements[planElements.Count - 1].GraphicsPath = new GraphicsPath(wallPoints.ToArray(), pathPointTypes);
            }
            else
            {
                planElements.RemoveAt(planElements.Count - 1);
                furnitureList.SelectedIndex = -1;
            }

            wallPoints.Clear();
            creatingWall = false;
            RefreshBitmap();
        }


        /// <summary>
        /// Handles paint event for the PictureBox. Draws all elements belonging to the plan.
        /// </summary>
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
                if (data.GraphicsPath != null) // if the wall is currently created it's graphics path is set to null
                    DrawWall(g, data.GraphicsPath, data.IsSelected, data.RotationDelta, data.Point);
                else if (data.Image != null) //printing furniture
                    DrawFurniture(g, data.Image, data.Point, data.IsSelected, data.Rotation);
            }
        }

        /// <summary>
        /// Draws the passed GraphicsPath using the given Graphics
        /// </summary>
        /// <param name="g">Graphics used to draw the image</param>
        /// <param name="path">Graphics which shall be drawn</param>
        /// <param name="semitransparent">Optional flag, is set to true the path is drawn with 50% opacity</param>
        /// <param name="rotation">Optional parameter by which the path will be rotated</param>
        /// <param name="location">Optional parameter, the first point of the path is translated to this location</param>
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

        /// <summary>
        /// Draws the passed image using the given Graphics centered on the given location
        /// </summary>
        /// <param name="g">Graphics used to draw the image</param>
        /// <param name="image">Image which shall be drawn</param>
        /// <param name="center">Point indicating where the center of the image should be</param>
        /// <param name="semitransparent">Optional flag, is set to true the image is drawn with 50% opacity</param>
        /// <param name="rotation">Optional parameter by which the image will be rotated</param>
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


        /// <summary>
        /// Handles change of size of the Panel storing the PicutreBox. If the size has increased increases the size of the PictureBox and refreshes the bitmap.
        /// </summary>
        private void panel1_SizeChanged(object sender, EventArgs e)
        {
            if (panel1.Height > pictureBox1.Height || panel1.Width > pictureBox1.Width)
            {
                pictureBox1.Size = panel1.Size;
                RefreshBitmap();
            }
        }
        

        /// <summary>
        /// Handles mouse wheel event for the PictureBox. If an element is selected adds the scaled wheel movement to the total rotation of the object.
        /// Stops the wheel event from being propagated further. 
        /// </summary>
        private void pictureBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            HandledMouseEventArgs arg = (HandledMouseEventArgs)e;
            arg.Handled = true;
            if (selectedIndex != -1)
            {
                planElements[selectedIndex].Rotation += e.Delta / 20;
                RefreshBitmap();
            }
        }


        /// <summary>
        /// Handles mouse movement event for the PictureBox. If a wall is currently drawn adds the new mouse position to the temp wall. 
        /// If an element is moved translates its position and refreshes the bitmap
        /// </summary>
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


        /// <summary>
        /// Handles change of selected index in the ListBox. If an element was selected, deselects it first, then marks the new one as selected
        /// </summary>
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


        /// <summary>
        /// Handles keyboard input. If there is an selected element and Keys.Delete was pressed 
        /// removes the element, and clears appropriate flags and variables
        /// </summary>
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (selectedIndex != -1 && e.KeyCode == Keys.Delete)
            {
                planElements.RemoveAt(selectedIndex);
                movingAnchor = null;
                movingObject = false;
                furnitureList.SelectedIndex = selectedIndex = -1;
                if (planElements.Count > 0) // Removing an element launches the IndexChanged event, so the selection has to be removed 
                    planElements[planElements.Count - 1].IsSelected = false;
                RefreshBitmap();
            }
        }


        /// <summary>
        /// Handles Save button press, displays a SaveFileDilog, tries to open the file and serialize the planElements list to it.
        /// Displays MessageBoxes indicating success/failure.
        /// </summary>
        private void Save_Click(object sender, EventArgs e)
        {
            ResourceManager resources = new ResourceManager(typeof(RoomPlanner));
            SaveFileDialog fileDialog = new SaveFileDialog();
            Stream stream = null;
            

            fileDialog.Filter = resources.GetString("file_dialog_filter");

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                var parts = fileDialog.FileName.Split('.');
                if (parts[parts.Length - 1] != "bm") // Checks whether the extension is correct
                {
                    MessageBox.Show(resources.GetString("file_dialog_extenstion_error"), resources.GetString("save_file_dialog_message_title"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                try
                {
                    if ((stream = fileDialog.OpenFile()) != null)
                    {
                        BinaryFormatter formatter = new BinaryFormatter();
                        if (planElements.Count > 0)
                            planElements[0].BitmapSize = this.pictureBox1.Size;
                        formatter.Serialize(stream, planElements);
                        MessageBox.Show(resources.GetString("save_file_dialog_success"), resources.GetString("save_file_dialog_message_title"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show(resources.GetString("save_file_dialog_error"), resources.GetString("save_file_dialog_message_title"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (stream != null)
                        stream.Close();
                }
                
            }
        }


        /// <summary>
        /// Handles Open button press, displays a OpenFileDilog, tries to open the file and deserialize it to the planElements variable.
        /// Displays MessageBoxes indicating success/failure.
        /// </summary>
        private void Open_Click(object sender, EventArgs e)
        {
            ResourceManager resources = new ResourceManager(typeof(RoomPlanner));
            OpenFileDialog fileDialog = new OpenFileDialog();
            Stream stream = null;

            fileDialog.Filter = resources.GetString("file_dialog_filter");

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                var parts = fileDialog.FileName.Split('.');
                if (parts[parts.Length - 1] != "bm") // Checks whether the extension is correct
                {
                    MessageBox.Show(resources.GetString("file_dialog_extenstion_error"), resources.GetString("open_file_dialog_message_title"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                try
                {
                    stream = fileDialog.OpenFile();
                    BinaryFormatter formatter = new BinaryFormatter();
                    var items = (BindingList<Data>)formatter.Deserialize(stream);
                    planElements.Clear();
                    foreach (var item in items)
                    {
                        planElements.Add(item);
                    }
                    if (planElements.Count > 0)
                        this.pictureBox1.Size = planElements[0].BitmapSize;
                    RefreshBitmap();

                    MessageBox.Show(resources.GetString("open_file_dialog_success"), resources.GetString("open_file_dialog_message_title"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception)
                {
                    MessageBox.Show(resources.GetString("open_file_dialog_error"), resources.GetString("open_file_dialog_message_title"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (stream != null)
                        stream.Close();
                }
                furnitureList.SelectedIndex = -1;
            }
            
        }


        /// <summary>
        /// Handles English language button press, changes the app language to English
        /// </summary>
        private void englishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CultureInfo.CurrentCulture.Equals(cultures[0])) return;
            CultureInfo.CurrentCulture = cultures[0];
            CultureInfo.CurrentUICulture = cultures[0];
            RefreshForm();
        }


        /// <summary>
        /// Handles Polish language button press, changes the app language to Polish
        /// </summary>
        private void polskiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CultureInfo.CurrentCulture.Equals(cultures[1])) return;
            CultureInfo.CurrentCulture = cultures[1];
            CultureInfo.CurrentUICulture = cultures[1];
            RefreshForm();
        }

        /// <summary>
        /// Reloads the form
        /// </summary>
        private void RefreshForm()
        {
            Size windowSize = Size;
            Size pictureSize = pictureBox1.Size;
            Size listSize = furnitureList.Size;
            Controls.Clear();
            InitializeComponent();
            bindingSource1.DataSource = planElements;
            Size = windowSize;
            furnitureList.Size = listSize;
            pictureBox1.Size = pictureSize;
            furnitureList.SelectedIndex = selectedIndex;
            RefreshBitmap();
        }
    }
}
