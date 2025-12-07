using System;
using System.Drawing;
using System.Windows.Forms;

namespace GraphLab.Lab8
{
    public partial class Lab8 : Form
    {
        Graphics g;
        Bitmap b;
        Random rnd = new Random();

        public Lab8()
        {
            InitializeComponent();
            this.Text = "LAB 8 – Feriga lui Barnsley";
        }

        private void Lab8_Load(object sender, EventArgs e)
        {
            int width = pictureBox1.Width;
            int height = pictureBox1.Height;

            b = new Bitmap(width, height);
            g = Graphics.FromImage(b);
            g.Clear(Color.White); 

            int iterations = 120000;
            double x = 0.0;
            double y = 0.0;

            double xmin = -2.1820;
            double xmax = 2.6558;
            double ymin = 0.0;
            double ymax = 9.9983;

            double fractalWidth = xmax - xmin;
            double fractalHeight = ymax - ymin;

            double scale = Math.Min(width / fractalWidth, height / fractalHeight);

            double xOffset = (width - fractalWidth * scale) / 2 - xmin * scale;
            double yOffset = (height - fractalHeight * scale) / 2 + ymax * scale; 

            Color[] palette = new Color[]
            {
                Color.FromArgb(0, 150, 0),
                Color.FromArgb(0, 200, 0),
                Color.FromArgb(50, 255, 50),
                Color.FromArgb(0, 255, 120)
            };

            for (int i = 0; i < iterations; i++)
            {
                double r = rnd.NextDouble();
                double xNew, yNew;

                if (r < 0.01)
                {
                    xNew = 0.0;
                    yNew = 0.16 * y;
                }
                else if (r < 0.86)
                {
                    xNew = 0.85 * x + 0.04 * y;
                    yNew = -0.04 * x + 0.85 * y + 1.6;
                }
                else if (r < 0.93)
                {
                    xNew = 0.2 * x - 0.26 * y;
                    yNew = 0.23 * x + 0.22 * y + 1.6;
                }
                else
                {
                    xNew = -0.15 * x + 0.28 * y;
                    yNew = 0.26 * x + 0.24 * y + 0.44;
                }

                x = xNew;
                y = yNew;

                int px = (int)(x * scale + xOffset);
                int py = (int)(yOffset - y * scale); 

                if (px >= 0 && px < width && py >= 0 && py < height)
                {
                    b.SetPixel(px, py, palette[i % palette.Length]);
                }
            }

            pictureBox1.Image = b;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }
    }
}
