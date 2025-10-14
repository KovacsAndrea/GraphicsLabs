using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphLab.Lab2
{
    public partial class Semafor : Form
    {
        Graphics graphics;
        Bitmap bitmap;
        PointF[] circle1;
        PointF[] circle2;
        PointF[] circle3;
        int step = 1;
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        public Semafor()
        {
            InitializeComponent();
            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.White);
            this.Text = "Semafor";
            this.circle1 = GetCoordinatesPoligonRegulat(500, 150, 80, 36, 0);
            this.circle2 = GetCoordinatesPoligonRegulat(500, 350, 80, 36, 0);
            this.circle3 = GetCoordinatesPoligonRegulat(500, 550, 80, 36, 0);

            graphics.FillRectangle(new SolidBrush(Color.LightGray), 300, 50, 400, 600);
            var timer = new System.Windows.Forms.Timer();
            timer.Interval = 2000;
            int step = 0;
            greenLight();
            timer.Tick += Timer_Tick;
            timer.Start();

        }

        private void Timer_Tick(object sender, EventArgs e)
        {

            if (step == 1)
            {
                yellowLight();
                step++;
            }
            else if (step == 2)
            {
                redLight();
                step++;
            }
            else if (step == 3)
            {
                yellowLight();
                step++;
            }
            else if (step == 4)
            {
                greenLight();
                step++;
            }
            else if (step == 4)
            {
                step = 1;
            }

        }
        private void DrawMyCircle(PointF[] points, Color color)
        {

            graphics.FillPolygon(new SolidBrush(color), points);
            pictureBox1.Image = bitmap;
        }
        private PointF[] GetCoordinatesPoligonRegulat(float cx, float cy, float raza, int n, float f)
        {

            PointF[] coordonatePoligon = new PointF[n];
            //se ia o lista de PointF, unde pointF e o structura de date deja implementata = puncte float in plan 

            float alpha = (float)(Math.PI * 2) / n;
            //se calculeaza alfa, adica unghiul dintre segmentele egale ale poligonului regulat
            //se imparte cercul complet (2π radiani = 360) in n unghiuri egale
            //Dacă n = 4(pătrat), atunci alpha = 2π / 4 = π / 2(adică 90° între vârfuri)
            //Dacă n = 6(hexagon), alpha = 2π / 6 = 60°
            for (int i = 0; i < n; i++) //Mergem prin fiecare vârf al poligonului. i reprezintă indicele fiecărui vârf(0, 1, 2, ..., n - 1).
            {
                float x = cx + raza * (float)Math.Cos(alpha * i + f);
                float y = cy + raza * (float)Math.Sin(alpha * i + f);
                coordonatePoligon[i] = new PointF(x, y);
                //Math.Cos(...) controlează poziția pe axa X (dreapta–stânga)
                //Math.Sin(...) controlează poziția pe axa Y(sus–jos)
                //cx și cy mută tot poligonul acolo unde vrei(centrul)
                //f doar „rotește” totul în jurul centrului
            }
            return coordonatePoligon;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void noLight()
        {
            DrawMyCircle(this.circle3, Color.Black);
            DrawMyCircle(this.circle2, Color.Black);
            DrawMyCircle(this.circle1, Color.Black);
        }
        private void redLight()
        {
            DrawMyCircle(this.circle3, Color.Red);
            DrawMyCircle(this.circle2, Color.Black);
            DrawMyCircle(this.circle1, Color.Black);
        }

        private void yellowLight()
        {
            DrawMyCircle(this.circle3, Color.Black);
            DrawMyCircle(this.circle2, Color.Yellow);
            DrawMyCircle(this.circle1, Color.Black);
        }

        private void greenLight()
        {
            DrawMyCircle(this.circle3, Color.Black);
            DrawMyCircle(this.circle2, Color.Black);
            DrawMyCircle(this.circle1, Color.Green);
        }


        private void Semafor_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
