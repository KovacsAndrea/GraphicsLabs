using System;
using System.Drawing;
using System.Windows.Forms;

namespace GraphLab.Lab9
{
    public partial class Lab9 : Form
    {
        Bitmap b;

        public Lab9()
        {
            InitializeComponent();
            this.Text = "LAB 9 – Iluminare Difuză (Lambert)";
        }

        private void Lab9_Load(object sender, EventArgs e)
        {
            int width = pictureBox1.Width;
            int height = pictureBox1.Height;

            b = new Bitmap(width, height);

            double k_d = 0.8;  
            double I_L = 1.0;  

            Vector3 l = new Vector3(-1, -1, 1).Normalized();

            double cx = width / 2.0;
            double cy = height / 2.0;
            double radius = Math.Min(width, height) / 2.0;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    double dx = (x - cx) / radius;
                    double dy = (y - cy) / radius;
                    double dz2 = 1 - dx * dx - dy * dy;
                    if (dz2 < 0) continue; 

                    double dz = Math.Sqrt(dz2);

                    Vector3 n = new Vector3(dx, dy, dz).Normalized();

                    double dot = n.Dot(l);
                    double I_dif = k_d * I_L * Math.Max(0, dot);

                    int intensity = (int)(I_dif * 255);
                    intensity = Math.Max(0, Math.Min(255, intensity));

                    b.SetPixel(x, y, Color.FromArgb(intensity, intensity, intensity));
                }
            }

            pictureBox1.Image = b;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }

    public class Vector3
    {
        public double X, Y, Z;
        public Vector3(double x, double y, double z)
        {
            X = x; Y = y; Z = z;
        }

        public Vector3 Normalized()
        {
            double len = Math.Sqrt(X * X + Y * Y + Z * Z);
            if (len == 0) return new Vector3(0, 0, 0);
            return new Vector3(X / len, Y / len, Z / len);
        }

        public double Dot(Vector3 other)
        {
            return X * other.X + Y * other.Y + Z * other.Z;
        }
    }
}
