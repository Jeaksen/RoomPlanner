using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Resources;
using System.Runtime.Serialization;

namespace Lab2_winforms
{
    /// <summary>
    /// This class is used to save data needed for drawing furniture and paths
    /// </summary>
    [Serializable]
    public class Data
    {
        private int previousRotation = 0;
        private int rotation = 0;
        [NonSerialized]
        private GraphicsPath graphicsPath = null;
        private PointF[] wallPoints = null;
        
        public Image Image { get; set; }
        public Point Point { get; set; }
        public string Text { get; set; }
        public bool IsSelected { get; set; }
        public Size BitmapSize { get; set; }
        public int RotationDelta 
        { 
            get
            {
                int delta = Rotation - previousRotation;
                previousRotation = Rotation;
                return delta;
            }
        }
        public GraphicsPath GraphicsPath { get => graphicsPath; set => graphicsPath = value; }

        public int Rotation { get => rotation; set { rotation = value % 360; } }

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
            ResourceManager resources = new ResourceManager(typeof(RoomPlanner));
            return $"{resources.GetString(Text), -15} {Point}";
        }


        /// <summary>
        /// Transforms the GraphicsPath into an array of point, so that they can be serialized
        /// </summary>
        [OnSerializing()]
        internal void OnSerializingMethod(StreamingContext context)
        {
            if (graphicsPath != null)
                wallPoints = graphicsPath.PathData.Points;
        }


        /// <summary>
        /// Transforms the array of points into a graphics path and clears the array
        /// </summary>
        [OnDeserialized()]
        internal void OnDeserializingMethod(StreamingContext context)
        {
            if (wallPoints != null)
            {
                byte[] pathPointTypes = new byte[wallPoints.Length];
                for (int i = 0; i < wallPoints.Length; i++)
                    pathPointTypes[i] = (byte)PathPointType.Line;

                graphicsPath = new GraphicsPath(wallPoints, pathPointTypes);
                wallPoints = null;
            }
            IsSelected = false;
        }
    }
}
