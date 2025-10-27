using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphLab.Lab3
{
    public partial class Lab3 : Form
    {
        Graphics graphics;
        Bitmap bitmap;
        float h;
        float w; 

        public Lab3()
        {
            InitializeComponent();
            this.Text = "Lab 3 - Translație poligon cu matrici";
            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(bitmap);
            h = pictureBox1.Height;
            w = pictureBox1.Width;
        }

        public void DrawASquare()
        {
            graphics.Clear(Color.White);
            List<PointF> square = new List<PointF>
            {
                new PointF(100, 100),
                new PointF(200, 100),
                new PointF(200, 200),
                new PointF(100, 200)
            };
            graphics.DrawPolygon(Pens.Black, square.ToArray());

            float Tx = 150;  
            float Ty = 80;  
            float[,] translationMatrix = new float[3, 3]
            {
                { 1, 0, Tx },
                { 0, 1, Ty },
                { 0, 0, 1 }
            };
            List<PointF> translatedSquare = ApplyTransformation(square, translationMatrix);
            graphics.DrawPolygon(new Pen(Color.Red, 2), translatedSquare.ToArray());
            pictureBox1.Image = bitmap;
        }
        private List<PointF> ApplyTransformation(List<PointF> points, float[,] matrix)
        {
            List<PointF> transformed = new List<PointF>();
            foreach (PointF p in points)
            {
                float x = p.X;
                float y = p.Y;

                float newX = matrix[0, 0] * x + matrix[0, 1] * y + matrix[0, 2] * 1;
                float newY = matrix[1, 0] * x + matrix[1, 1] * y + matrix[1, 2] * 1;

                transformed.Add(new PointF(newX, newY));
            }
            return transformed;
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Lab3_Load(object sender, EventArgs e)
        {
            DrawASquare();
        }
    }
}
