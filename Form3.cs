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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        Graphics g;
        Bitmap buf;
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
            int w = int.Parse(textBox2.Text);
            int h = int.Parse(textBox2.Text);
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
            int[] m_old = new int[3] { x1+width, y1, 1 };
            int[] m_new;
            Reflection(m_old, out m_new);
            g.DrawRectangle(rec, m_new[0], m_new[1], width, height);
        }
        public void Reflection(int[] m_old, out int[] m_new)
        {
            int xn = 0;
            int yn = 0;
            m_new = new int[3] { xn, yn, 1 };
            int[,] m_preobr = new int[3, 3];
            m_preobr[0, 0] = -1;
            m_preobr[0, 1] = 0;    
            m_preobr[0, 2] = 0;    
            m_preobr[1, 0] = 0;    
            m_preobr[1, 1] = 1;
            m_preobr[1, 2] = 0;    
            m_preobr[2, 0] = 0;
            m_preobr[2, 1] = 0;    
            m_preobr[2, 2] = 1;            
            m_new[0] = m_old[0]* m_preobr[0, 0];
            m_new[1] = m_old[1] * m_preobr[1, 1];
            textBox1.Text = "Исходная       Преобразований         Полученная\r\n";
            textBox1.Text = "| ";
            for (int i = 0; i < m_old.Length; i++)
            {
                textBox1.Text += m_old[i] + " ";
            }
            textBox1.Text += " |\t| ";
            for (int i = 0; i < m_preobr.GetLength(0); i++)
            {
                for (int j = 0; j < m_preobr.GetLength(1); j++)
                {
                    textBox1.Text += m_preobr[i, j] + " ";
                }
                textBox1.Text += "|\t\t";
                if (i == 0)
                {
                    textBox1.Text += "| ";
                    textBox1.Text += m_new[0] + " ";
                    textBox1.Text += m_new[1] + " ";
                    textBox1.Text += m_new[2];
                    textBox1.Text += " |";
                }
                textBox1.Text += "\n";
                if (i == m_preobr.GetLength(1) - 2)
                {
                    textBox1.Text += " \t\t\t\t| ";
                }
                if (i == 0)
                {
                    textBox1.Text += " \t\t\t| ";
                }
            }
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form ifrm = Application.OpenForms[0];
            ifrm.Show();
        }
    }
}
