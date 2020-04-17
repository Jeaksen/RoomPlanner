using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Data;

namespace Lab2_winforms
{
    public partial class Form1 : Form
    {
        private Button selectedButton = null;
        public Form1()
        {
            InitializeComponent();
            newToolStripMenuItem_Click(null, null);
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            furnitureList.Items.Clear();
            Bitmap flag = new Bitmap(splitContainer1.Panel1.Width, splitContainer1.Panel1.Height);
            Graphics flagGraphics = Graphics.FromImage(flag);
            flagGraphics.FillRectangle(Brushes.White, 0, 0, flag.Width, flag.Height);
            pictureBox1.Image = flag;
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
            Bitmap bitmap = (Bitmap)selectedButton.Tag;
            Graphics g = Graphics.FromImage(pictureBox1.Image);
            Point point = e.Location;

            point.X -= bitmap.Width / 2;
            point.Y -= bitmap.Height / 2;
            g.DrawImage(bitmap, point);

            if (selectedButton == coffee_button)
                furnitureList.Items.Add($"Coffe table {e.Location}");
            else if (selectedButton == bed_button)
                furnitureList.Items.Add($"Double Bed {e.Location}");
            else if (selectedButton == sofa_button)
                furnitureList.Items.Add($"Sofa {e.Location}");
            else if (selectedButton == table_button)
                furnitureList.Items.Add($"Table {e.Location}");


            ChangeButtonSelection(selectedButton, false);
            pictureBox1.Refresh();
        }
    }
}
