using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace комп_граф3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Graphics g;
        Bitmap buf;
        Pen vector = new Pen(Color.Black, 2);
        Pen rec = new Pen(Color.Black, 2);
        Pen ax = new Pen(Color.Red, 1);

        private void DrawAxes()
        {
            g.DrawLine(ax, 0, -pictureBox1.Height / 2, 0, pictureBox1.Height / 2);
            g.DrawLine(ax, -pictureBox1.Width / 2, 0, pictureBox1.Width / 2, 0);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            buf = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(buf);
            g = pictureBox1.CreateGraphics();
            g.TranslateTransform(pictureBox1.Width / 2, pictureBox1.Height / 2);
            g.ScaleTransform(1, -1);
            int w = int.Parse(textBox7.Text);
            int h = int.Parse(textBox7.Text);
            int xCells = 35;
            int yCells = 35;
            using (Pen pen = new Pen(Color.Aqua, 2))
            {
                for (int i = 1; i < xCells; i++)
                    g.DrawLine(pen, i * w - (pictureBox1.Width / 2), -pictureBox1.Width, i * w - (pictureBox1.Width / 2), h * yCells + pictureBox1.Width);
                for (int i = 1; i < yCells; i++)
                    g.DrawLine(pen, -pictureBox1.Height, i * h - (pictureBox1.Height / 2), w * xCells + pictureBox1.Height, i * h - (pictureBox1.Height / 2));
            }
            DrawAxes();
            int x1 = -40;
            int y1 = -20;
            int width = 16;
            int height = 16;
            g.DrawRectangle(rec, x1, y1, width, height);
            int x1v = Convert.ToInt32(textBox1.Text);
            int y1v = Convert.ToInt32(textBox2.Text);
            int x2v = Convert.ToInt32(textBox3.Text);
            int y2v = Convert.ToInt32(textBox4.Text);
            g.DrawLine(vector, x1v, y1v, x2v, y2v);
            int ytr = Convert.ToInt32(textBox5.Text);
            int xtr = Convert.ToInt32(textBox6.Text);
            int xn;
            int yn;
            TransferDecart(x1, y1, x1v, y1v, x2v, y2v, xtr, ytr, out xn, out yn);
            g.DrawRectangle(rec, xn, yn, width, height);


        }
        public void TransferDecart(int x, int y, int x1v, int y1v, int x2v, int y2v, int xtr, int ytr, out int xn, out int yn)
        {
            xn = 0;
            yn = 0;
            if (ytr != 0)
            {
                double tg = (y2v - y1v) / (x2v - x1v);
                //xn = Convert.ToInt32((y + ytr) / tg) - x;
                yn = y + ytr;
                xn = Convert.ToInt32(yn * tg);
            }
            else
            {
                if (xtr != 0)
                {
                    double tg = (y2v - y1v) / (x2v - x1v);
                    xn = x + xtr;
                    //yn = Convert.ToInt32(((x + xtr) * tg) - y);
                    yn = Convert.ToInt32(xn * tg);
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form ifrm = new Form2();

            ifrm.Show(); // отображаем Form2

        }

        private void button3_Click(object sender, EventArgs e)
        {

            buf = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(buf);
            g = pictureBox1.CreateGraphics();
            g.TranslateTransform(pictureBox1.Width / 2, pictureBox1.Height / 2);
            g.ScaleTransform(1, -1);
            int w = int.Parse(textBox7.Text);
            int h = int.Parse(textBox7.Text);
            int xCells = 35;
            int yCells = 35;
            using (Pen pen = new Pen(Color.Aqua, 2))
            {
                for (int i = 1; i < xCells; i++)
                    g.DrawLine(pen, i * w - (pictureBox1.Width / 2), -pictureBox1.Width, i * w - (pictureBox1.Width / 2), h * yCells + pictureBox1.Width);
                for (int i = 1; i < yCells; i++)
                    g.DrawLine(pen, -pictureBox1.Height, i * h - (pictureBox1.Height / 2), w * xCells + pictureBox1.Height, i * h - (pictureBox1.Height / 2));
            }
            DrawAxes();
            int x1 = -40;
            int y1 = -20;
            int width = 16;
            int height = 16;
            int[] m_old = new int[3] { x1, y1, 1 };
            g.DrawRectangle(rec, x1, y1, width, height);
            int x1v = Convert.ToInt32(textBox1.Text);
            int y1v = Convert.ToInt32(textBox2.Text);
            int x2v = Convert.ToInt32(textBox3.Text);
            int y2v = Convert.ToInt32(textBox4.Text);
            g.DrawLine(vector, x1v, y1v, x2v, y2v);
            int ytr = Convert.ToInt32(textBox5.Text);
            int xtr = Convert.ToInt32(textBox6.Text);
            int[] m_new;
            TransferOdn(m_old, x1v, y1v, x2v, y2v, xtr, ytr, out m_new);
            g.DrawRectangle(rec, m_new[0], m_new[1], width, height);
        }
        public void TransferOdn(int[] m_old, int x1v, int y1v, int x2v, int y2v, int xtr, int ytr, out int[] m_new)
        {
            int xn = 0;
            int yn = 0;
            m_new = new int[3] { xn, yn, 1 };
            int[,] m_preobr = new int[3, 3];
            double tg = (y2v - y1v) / (x2v - x1v);
            if (xtr != 0)
            {
                m_preobr[0, 0] = 1;
                m_preobr[0, 1] = 0;
                m_preobr[0, 2] = 0;
                m_preobr[1, 0] = 0;
                m_preobr[1, 1] = 1;
                m_preobr[1, 2] = 0;
                m_preobr[2, 0] = xtr;
                m_preobr[2, 1] = Convert.ToInt32((m_old[0] + xtr) * tg) - m_old[1];
                m_preobr[2, 2] = 1;
            }
            if (ytr != 0)
            {
                m_preobr[0, 0] = 1;
                m_preobr[0, 1] = 0;
                m_preobr[0, 2] = 0;
                m_preobr[1, 0] = 0;
                m_preobr[1, 1] = 1;
                m_preobr[1, 2] = 0;
                m_preobr[2, 0] = Convert.ToInt32((m_old[1] + ytr) * tg) - m_old[0];
                m_preobr[2, 1] = ytr;
                m_preobr[2, 2] = 1;
            }
            m_new[0] = m_old[0] + m_preobr[2, 0];
            m_new[1] = m_old[1] + m_preobr[2, 1];
            textBox8.Text = "Исходная       Преобразований         Полученная\r\n";
            textBox8.Text = "| ";
            for (int i = 0; i < m_old.Length; i++)
            {
                textBox8.Text += m_old[i] + " ";
            }
            textBox8.Text += " |\t\t| ";
            for (int i = 0; i < m_preobr.GetLength(0); i++)
            {
                for (int j = 0; j < m_preobr.GetLength(1); j++)
                {
                    textBox8.Text += m_preobr[i, j] + " ";
                }

                if (i == 0)
                {
                    textBox8.Text += " |\t\t| ";
                    textBox8.Text += m_new[0] + " ";
                    textBox8.Text += m_new[1] + " ";
                    textBox8.Text += m_new[2];
                }
                textBox8.Text += " | \n";
                if (i == m_preobr.GetLength(1) - 2)
                {
                    textBox8.Text += " \t\t\t";
                }
                if (i != m_preobr.GetLength(1) - 1)
                {
                    textBox8.Text += " \t\t\t\t| ";
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form ifrm = new Form3();

            ifrm.Show(); // отображаем Form2
        }
    }
}
    