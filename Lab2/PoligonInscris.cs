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
    public partial class PoligonInscris : Form
    {
        TextBox InputNrLaturi = new TextBox();
        TextBox InputRaza = new TextBox();
        Graphics graphics;
        Bitmap bitmap;
        public PoligonInscris()
        {
            InitializeComponent();
            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.White);
            pictureBox1.Image = bitmap;
            this.Text = "Poligon Regulat";
            Label lbl = new Label();
            lbl.Text = "Introduceti numarul de varfuri";
            lbl.Left = 10;
            lbl.Top = 20;
            lbl.AutoSize = true;
            this.Controls.Add(lbl);

            //input
            InputNrLaturi.Left = 170;
            InputNrLaturi.Top = 15;
            InputNrLaturi.Width = 100;
            this.Controls.Add(InputNrLaturi);

            Label lbl2 = new Label();
            lbl2.Text = "Introduceti raza";
            lbl2.Left = 10;
            lbl2.Top = 50;
            lbl2.AutoSize = true;
            this.Controls.Add(lbl2);

            //input
            InputRaza.Left = 170;
            InputRaza.Top = 45;
            InputRaza.Width = 100;
            this.Controls.Add(InputRaza);

            Button drawButton = new Button();
            drawButton.Text = "Deseneaza";
            drawButton.Left = 10;
            drawButton.Top = 75;
            drawButton.Click += Draw;
            this.Controls.Add(drawButton);
        }

        private void Draw(object sender, EventArgs e)
        {
            this.DrawMyCircle();
            this.DrawMyPoligon();
        }

        private void DrawMyPoligon()
        {
            PointF[] points = GetCoordinatesPoligonRegulat(550, 250, int.Parse(InputRaza.Text), int.Parse(InputNrLaturi.Text), 0);
            graphics.FillPolygon(new SolidBrush(Color.Yellow), points);
            pictureBox1.Image = bitmap;
        }


        private void DrawMyCircle()
        {
            PointF[] points = GetCoordinatesPoligonRegulat(550, 250, int.Parse(InputRaza.Text), 36, 0);
            graphics.FillPolygon(new SolidBrush(Color.Black), points);
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

        private void PoligonInscris_Load(object sender, EventArgs e)
        {

        }
    }
}
