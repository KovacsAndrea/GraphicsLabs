using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphLab.Lab6
{
    public partial class Lab6 : Form
    {
        Graphics g;
        Bitmap b;
       
        public Lab6()
        {
            InitializeComponent();
            this.Text = "LABU 6";
            b = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(b);
        }

        private void DrawBezier()
        {
            PointF p0 = new PointF(100, 500);
            PointF p1 = new PointF(400, 100);
            PointF p2 = new PointF(700, 500);

            g.FillEllipse(Brushes.Red, p0.X - 4, p0.Y - 4, 8, 8);
            g.FillEllipse(Brushes.Red, p1.X - 4, p1.Y - 4, 8, 8);
            g.FillEllipse(Brushes.Red, p2.X - 4, p2.Y - 4, 8, 8);

            PointF prev = p0;

            for (float u = 0; u <= 1.0f; u += 0.01f)
            {
                float x = (float)(Math.Pow(1 - u, 2) * p0.X +
                                  2 * u * (1 - u) * p1.X +
                                  Math.Pow(u, 2) * p2.X);

                float y = (float)(Math.Pow(1 - u, 2) * p0.Y +
                                  2 * u * (1 - u) * p1.Y +
                                  Math.Pow(u, 2) * p2.Y);

                PointF cur = new PointF(x, y);
                g.DrawLine(Pens.Blue, prev, cur);
                prev = cur;
            }

            pictureBox1.Image = b;
        }
        private void Lab6_Load(object sender, EventArgs e)
        {
            DrawBezier();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
