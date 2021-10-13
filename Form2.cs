using System;
using System.Drawing;
using System.Windows.Forms;

namespace комп_граф3
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        Bitmap buf1;
        Bitmap buf2;
        Graphics g1, g2;
        Pen ax = new Pen(Color.Red, 1);

        scalingMatrix scaling = new scalingMatrix();
        Pen pen = new Pen(Color.Red, 2);
        Pen pen2 = new Pen(Color.Blue, 2);
        int x1, x2, y1, y2;

        private void Form2_FormClosed_1(object sender, FormClosedEventArgs e)
        {
            // вызываем главную форму приложения, которая открыла текущую форму Form2, главная форма всегда = 0
            Form ifrm = Application.OpenForms[0];
            ifrm.Show();
        }

        private void DrawAxes()
        {
            g1.DrawLine(ax, 0, -pictureBox1.Height / 2, 0, pictureBox1.Height / 2);
            g1.DrawLine(ax, -pictureBox1.Width / 2, 0, pictureBox1.Width / 2, 0);
            g2.DrawLine(ax, 0, -pictureBox2.Height / 2, 0, pictureBox2.Height / 2);
            g2.DrawLine(ax, -pictureBox2.Width / 2, 0, pictureBox2.Width / 2, 0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            buf1 = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g1 = Graphics.FromImage(buf1);
            g1 = pictureBox1.CreateGraphics();
            g1.TranslateTransform(pictureBox1.Width / 2, pictureBox1.Height / 2);
            g1.ScaleTransform(1, -1);
            g1.Clear(Color.White);
            g1.DrawLine(pen, 0, 0, 1, 1);
            buf2 = new Bitmap(pictureBox2.Width, pictureBox2.Height);
            g2 = Graphics.FromImage(buf2);
            g2 = pictureBox2.CreateGraphics();
            g2.TranslateTransform(pictureBox2.Width / 2, pictureBox2.Height / 2);
            g2.ScaleTransform(1, -1);
            g2.Clear(Color.White);

            int w = int.Parse(textBox2.Text);
            int h = int.Parse(textBox2.Text);
            int xCells = 35;
            int yCells = 35;
            using (Pen pen = new Pen(Color.Aqua, 2))
            {
                for (int i = 1; i < xCells; i++)
                    g1.DrawLine(pen, i * w - (pictureBox1.Width / 2), -pictureBox1.Width, i * w - (pictureBox1.Width / 2), h * yCells + pictureBox1.Width);
                for (int i = 1; i < yCells; i++)
                    g1.DrawLine(pen, -pictureBox1.Height, i * h - (pictureBox1.Height / 2), w * xCells + pictureBox1.Height, i * h - (pictureBox1.Height / 2));
            }
            using (Pen pen = new Pen(Color.Aqua, 2))
            {
                for (int i = 1; i < xCells; i++)
                    g2.DrawLine(pen, i * w - (pictureBox2.Width / 2), -pictureBox2.Width, i * w - (pictureBox2.Width / 2), h * yCells + pictureBox2.Width);
                for (int i = 1; i < yCells; i++)
                    g2.DrawLine(pen, -pictureBox2.Height, i * h - (pictureBox2.Height / 2), w * xCells + pictureBox2.Height, i * h - (pictureBox2.Height / 2));
            }
            DrawAxes();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            PicRe(scaling.SetMatrix(), pen, g1);
            PicRe(scaling.Start(comboBox1), pen2, g2);
            textBox1 = scaling.AllMatrixForlab(textBox1);
        }



        private Graphics PicRe(int[][,] matrixs, Pen pen, Graphics g)
        {
            for (int i = 0; i < matrixs.Length; i++)
            {
                x1 = matrixs[i][0, 0];
                y1 = matrixs[i][0, 1];
                x2 = matrixs[i][1, 0];
                y2 = matrixs[i][1, 1];
                g.DrawLine(pen, x1, y1, x2, y2);
            }
            return g;
        }
        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            // вызываем главную форму приложения, которая открыла текущую форму Form2, главная форма всегда = 0
            Form ifrm = Application.OpenForms[0];
            ifrm.Show();
        }
    }
}
