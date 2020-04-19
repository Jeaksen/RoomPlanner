using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Lab2_winforms
{
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

        public Data(Point pt, string text)
        {
            Point = pt;
            Text = text;
        }

        public override string ToString()
        {
            return $"{Text} {Point}";
        }
    }
}
