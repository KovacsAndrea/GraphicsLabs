using System;
using System.Drawing;
using System.Windows.Forms;

namespace GraphLab.Lab7
{
    public partial class Lab7 : Form
    {
        Graphics g;
        Bitmap b;

        Color[] palette = new Color[]
        {
            Color.Red,
            Color.Green,
            Color.Blue,
            Color.Magenta,
            Color.Cyan,
            Color.Yellow,
            Color.Orange
        };

        public Lab7()
        {
            InitializeComponent();
            this.Text = "LAB 7 - Sierpinski";
        }

        private void Lab7_Load(object sender, EventArgs e)
        {
            b = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(b);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.Clear(Color.White);

            float margin = 20f;
            float width = pictureBox1.Width - 2 * margin;
            float height = pictureBox1.Height - 2 * margin;

            float side = Math.Min(width, (float)(2.0 / Math.Sqrt(3.0) * height));
            float triHeight = (float)(Math.Sqrt(3.0) / 2.0 * side);

            PointF top = new PointF(pictureBox1.Width / 2f, margin);
            PointF left = new PointF((pictureBox1.Width / 2f) - side / 2f, margin + triHeight);
            PointF right = new PointF((pictureBox1.Width / 2f) + side / 2f, margin + triHeight);

            int depth = 6;

            DrawSierpinski(top, left, right, depth, depth);
            pictureBox1.Image = b;
        }

        private void FillTriangle(PointF a, PointF b, PointF c, Brush br)
        {
            PointF[] pts = new PointF[] { a, b, c };
            g.FillPolygon(br, pts);
        }

        private void DrawSierpinski(PointF a, PointF b, PointF c, int depth, int maxDepth)
        {
            int idx = (maxDepth - depth) % palette.Length;
            Brush br = new SolidBrush(palette[idx]);

            if (depth == 0)
            {
                FillTriangle(a, b, c, br);
                return;
            }

            FillTriangle(a, b, c, br);

            PointF ab = MidPoint(a, b);
            PointF bc = MidPoint(b, c);
            PointF ca = MidPoint(c, a);

            DrawSierpinski(a, ab, ca, depth - 1, maxDepth);
            DrawSierpinski(ab, b, bc, depth - 1, maxDepth);
            DrawSierpinski(ca, bc, c, depth - 1, maxDepth);
        }

        private PointF MidPoint(PointF p1, PointF p2)
        {
            return new PointF((p1.X + p2.X) / 2f, (p1.Y + p2.Y) / 2f);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
