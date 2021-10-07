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

        private void button1_Click(object sender, EventArgs e)
        {
            buf = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(buf);
            g = pictureBox1.CreateGraphics();
            g.TranslateTransform(pictureBox1.Width / 2, pictureBox1.Height / 2);
            g.ScaleTransform(1, -1);
            int x1 = -30;
            int y1 = -30;
            int width = 16;
            int height= 16;
            g.DrawRectangle(rec, x1,y1,width,height);
            int x1v = Convert.ToInt32(textBox1.Text);
            int y1v = Convert.ToInt32(textBox2.Text);
            int x2v= Convert.ToInt32(textBox3.Text);
            int y2v = Convert.ToInt32(textBox4.Text);
            g.DrawLine(vector, x1v, y1v, x2v, y2v);
            int ytr = Convert.ToInt32(textBox5.Text);
            int xtr = Convert.ToInt32(textBox6.Text);
            int xn;
            int yn;
            TransferDecart(x1, y1, x1v, y1v, x2v, y2v, xtr,ytr,out xn,out yn);
            g.DrawRectangle(rec, xn, yn, width, height);



        }
        public void TransferDecart(int x,int y, int x1v, int y1v, int x2v, int y2v, int xtr,int ytr, out int xn,out int yn)
        {
            xn = 0;
            yn = 0;
            if (xtr == 0)
            {
                double tg = (y2v - y1v) / (x2v - x1v);
                xn = Convert.ToInt32((y + ytr) / tg) - x;
                yn = y + ytr;
                //g.FillRectangle(new SolidBrush(Color.Black), new Rectangle(Convert.ToInt32((y + ytr) / tg) - x, y + ytr, 2, 2));
            }
            if (ytr == 0)
            {
                double tg = (y2v - y1v) / (x2v - x1v);
                xn = x + xtr;
                yn= Convert.ToInt32(((x + xtr) * tg)-y);
                //g.FillRectangle(new SolidBrush(Color.Black), new Rectangle(x+xtr,  Convert.ToInt32((x + xtr)*tg)-y, 2, 2));
            }
            
        }
    }
}
