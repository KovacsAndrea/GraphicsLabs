using System;
using System.Drawing;
using System.Windows.Forms;

namespace GraphLab.Lab5
{
    public partial class Lab5 : Form
    {
        private Bitmap canvas;
        private Graphics g;

        // coordonatele ferestrei de clipping
        private int xMin = 100, yMin = 100, xMax = 400, yMax = 300;

        // coduri de regiune
        private const int INSIDE = 0; // 0000
        private const int LEFT = 1;   // 0001
        private const int RIGHT = 2;  // 0010
        private const int BOTTOM = 4; // 0100
        private const int TOP = 8;    // 1000

        public Lab5()
        {
            InitializeComponent();
        }

        private void Lab5_Load(object sender, EventArgs e)
        {
            canvas = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(canvas);
            g.Clear(Color.White);

            // fereastra de test
            using (Pen clipPen = new Pen(Color.Red, 2))
            {
                g.DrawRectangle(clipPen, xMin, yMin, xMax - xMin, yMax - yMin);
            }

            // desenam si aplicam clipping pe liniile pe care le-am desenat
            DrawAndClipLine(50, 150, 450, 250, Color.Blue);
            DrawAndClipLine(150, 50, 350, 350, Color.Green);
            DrawAndClipLine(10, 10, 600, 400, Color.Purple);
            DrawAndClipLine(250, 150, 350, 200, Color.Orange);

            pictureBox1.Image = canvas;
        }

        // calculam codul de regiune
        private int ComputeCode(double x, double y)
        {
            int code = INSIDE;
            if (x < xMin) code |= LEFT;
            else if (x > xMax) code |= RIGHT;
            if (y < yMin) code |= TOP;
            else if (y > yMax) code |= BOTTOM;
            return code;
        }

        //Cohen–Sutherland
        private bool CohenSutherlandClip(ref double x1, ref double y1, ref double x2, ref double y2)
        {
            int code1 = ComputeCode(x1, y1);
            int code2 = ComputeCode(x2, y2);
            bool accept = false;

            while (true)
            {
                if ((code1 | code2) == 0)
                {
                    // ambele in interior
                    accept = true;
                    break;
                }
                else if ((code1 & code2) != 0)
                {
                    // ambele in afara aceleiasi regiuni
                    break;
                }
                else
                {
                    double x = 0, y = 0;
                    int codeOut = (code1 != 0) ? code1 : code2;

                    if ((codeOut & TOP) != 0)
                    {
                        x = x1 + (x2 - x1) * (yMin - y1) / (y2 - y1);
                        y = yMin;
                    }
                    else if ((codeOut & BOTTOM) != 0)
                    {
                        x = x1 + (x2 - x1) * (yMax - y1) / (y2 - y1);
                        y = yMax;
                    }
                    else if ((codeOut & RIGHT) != 0)
                    {
                        y = y1 + (y2 - y1) * (xMax - x1) / (x2 - x1);
                        x = xMax;
                    }
                    else if ((codeOut & LEFT) != 0)
                    {
                        y = y1 + (y2 - y1) * (xMin - x1) / (x2 - x1);
                        x = xMin;
                    }

                    if (codeOut == code1)
                    {
                        x1 = x;
                        y1 = y;
                        code1 = ComputeCode(x1, y1);
                    }
                    else
                    {
                        x2 = x;
                        y2 = y;
                        code2 = ComputeCode(x2, y2);
                    }
                }
            }

            return accept;
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }


        // desenam linia gri
        private void DrawAndClipLine(double x1, double y1, double x2, double y2, Color c)
        {
            using (Pen penOriginal = new Pen(Color.LightGray, 1))
            {
                g.DrawLine(penOriginal, (float)x1, (float)y1, (float)x2, (float)y2);
            }

            double cx1 = x1, cy1 = y1, cx2 = x2, cy2 = y2;
            if (CohenSutherlandClip(ref cx1, ref cy1, ref cx2, ref cy2))
            {
                using (Pen penClipped = new Pen(c, 2))
                {
                    g.DrawLine(penClipped, (float)cx1, (float)cy1, (float)cx2, (float)cy2);
                }
            }
        }
    }
}
