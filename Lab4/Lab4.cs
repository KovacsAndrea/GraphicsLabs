using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphLab.Lab4
{
    public partial class Lab4 : Form
    {
        Graphics graphics;
        Bitmap bitmap;
        public Lab4()
        {
            InitializeComponent();
        }

        private void Lab4_Load(object sender, EventArgs e)
        {
            this.Text = "Umplere poligon (Scanline)";
            this.Size = new Size(800, 600);

            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.White);
            List<Point> polygon = new List<Point>
            {
                new Point(100, 100),
                new Point(300, 150),
                new Point(350, 300),
                new Point(200, 400),
                new Point(80, 250)
            };

            graphics.DrawPolygon(Pens.Black, polygon.ToArray());
            FillPolygonScanline(bitmap, polygon, Color.LightSkyBlue);
            pictureBox1.Image = bitmap;
        }

        private void FillPolygonScanline(Bitmap bmp, List<Point> vertices, Color fillColor)
        {
            if (vertices.Count < 3) return;

            int ymin = vertices.Min(p => p.Y);
            int ymax = vertices.Max(p => p.Y);

            using (Graphics g = Graphics.FromImage(bmp))
            using (Pen pen = new Pen(fillColor))
            {
                for (int y = ymin; y <= ymax; y++)
                {
                    List<int> intersections = new List<int>();

                    for (int i = 0; i < vertices.Count; i++)
                    {
                        Point p1 = vertices[i];
                        Point p2 = vertices[(i + 1) % vertices.Count];

                        if (p1.Y == p2.Y) continue; 
                        if (y < Math.Min(p1.Y, p2.Y) || y >= Math.Max(p1.Y, p2.Y)) continue;

                        
                        int x = (int)(p1.X + (float)(y - p1.Y) * (p2.X - p1.X) / (float)(p2.Y - p1.Y));
                        intersections.Add(x);
                    }

                    intersections.Sort();

                    
                    for (int i = 0; i < intersections.Count - 1; i += 2)
                    {
                        g.DrawLine(pen, intersections[i], y, intersections[i + 1], y);
                    }
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
