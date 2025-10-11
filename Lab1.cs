using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphLab
{
    public partial class Lab1 : Form
    {

        private PictureBox canvas;

        public Lab1()
        {
            InitializeComponent();
            this.Text = "Lab 1";
            this.Width = 520;
            this.Height = 550;

            canvas = new PictureBox();
            canvas.Width = 430;
            canvas.Height = 430;
            canvas.Left = 50;
            canvas.Top = 50;
            canvas.BackColor = Color.White;

            this.Controls.Add(canvas);

            this.Load += Lab1_Load;
        }

        private void Lab1_Load(object sender, EventArgs e)
        {
            // linie de la (50, 50) la (350, 300)
            Bitmap bmp = new Bitmap(canvas.Width, canvas.Height);

            // desenam axele
             DrawAxes(bmp);

            // desenam linia DDA
            DrawLineDDA(50, 50, 350, 300, Color.Blue, bmp);

            canvas.Image = bmp;
        }

        private void DrawAxes(Bitmap bmp)
        {
            using (Graphics g = Graphics.FromImage(bmp))
            {
                Pen axisPen = new Pen(Color.Gray, 1);
                Brush textBrush = Brushes.Black;
                Font font = new Font("Arial", 8);

                g.DrawLine(axisPen, 0, 0, bmp.Width, 0);

                g.DrawLine(axisPen, 0, 0, 0, bmp.Height);

                for (int x = 50; x <= bmp.Width; x += 50)
                {
                    g.DrawLine(axisPen, x, 0, x, 5); // tic mic
                    g.DrawString(x.ToString(), font, textBrush, x - 10, 5);
                }

    
                for (int y = 50; y <= bmp.Height; y += 50)
                {
                    g.DrawLine(axisPen, 0, y, 5, y); // tic mic
                    g.DrawString(y.ToString(), font, textBrush, 10, y - 5);
                }

                g.DrawString("X", font, textBrush, bmp.Width - 10, 0);
                g.DrawString("Y", font, textBrush, 10, bmp.Height -10);
            }
        }

        // Algoritmul DDA
        // Deseneaza o linie intre punctul de plecare (x1, y1) si punctul final (x2, y2)
        private void DrawLineDDA(int x1, int y1, int x2, int y2, Color color, Bitmap bmp)
        {
            int dx = x2 - x1; // diferenta pe x = 300
            int dy = y2 - y1; // diferenta pe y = 250

            // numarul de pasi este maximul dintre |dx| si |dy|, ceea ce asigura o reprezentare lina a pixelilor.
            int steps = Math.Max(Math.Abs(dx), Math.Abs(dy)); // steps va fi 300

            // calculam valorile incrementale
            float Xinc = dx / (float)steps; ///xinc va fi 300 pe 300 adica 1
            float Yinc = dy / (float)steps; ///yinc va fi 250 pe 300 adica 0.833333

            //incepem de la punctul de pornire
            float x = x1;
            float y = y1;

            ///301 steps
            for (int i = 0; i <= steps; i++)
            {
                // verificam sa nu iesim in afara canvas-ului
                if (x >= 0 && x < canvas.Width && y >= 0 && y < canvas.Height)
                    bmp.SetPixel((int)Math.Round(x), (int)Math.Round(y), color);

                x += Xinc; /// x va creste cu 1 in fiecare iteratie
                y += Yinc; ///y va creste cu 0.8333333 
            }

            // afisam bitmap-ul in PictureBox
            canvas.Image = bmp;
        }
    }
}
