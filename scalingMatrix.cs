using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace комп_граф3
{
    public class scalingMatrix
    {
        int[][,] matrixsFigr;
        int Sx, Sy;

        public int[][,] SetMatrix()
        {
            int[][,] mat = new int[4][,]
            {
            new int[,] { { 0, 0 }, { 0, 20 } },//left
            new int[,] { { 0, 20 }, { 20, 20 } },//down
            new int[,] { { 20, 20 }, { 20, 0 } },//right
            new int[,] { { 20, 0 }, { 0, 0 } }//up
            };
            return mat;
        }

        public int[][,] Start(ComboBox cb)
        {
            matrixsFigr = SetMatrix();
            if (cb.Text == "Правая")
            {
                Sx = 2; Sy = 1;
                ScalingMatrix(2, 1, matrixsFigr);
            }
            else if (cb.Text == "Нижняя")
            {
                Sx = 1; Sy = 2;
                ScalingMatrix(1, 2, matrixsFigr);
            }
            else if (cb.Text == "Верхняя")
            {
                Sx = 1; Sy = 2;
                matrixsFigr = T(matrixsFigr);
                ScalingMatrix(2, 1, matrixsFigr);
                matrixsFigr = T(matrixsFigr);
            }
            else if (cb.Text == "Левая")
            {
                Sx = 2; Sy = 1;
                matrixsFigr = T(matrixsFigr);
                ScalingMatrix(1, 2, matrixsFigr);
                matrixsFigr = T(matrixsFigr);
            }
            else MessageBox.Show("Выберите сторону");

            return matrixsFigr;
        }

        private void ScalingMatrix(int Sx, int Sy, int[][,] matrix)
        {
            for (int i = 0; i < matrix.Length; i++)
            {
                matrix[i][0, 0] *= Sx;
                if (i - 1 == -1)
                    matrix[i + matrix.Length - 1][1, 0] = matrix[i][0, 0];
                else
                    matrix[i - 1][1, 0] = matrix[i][0, 0];

                matrix[i][1, 1] *= Sy;
                if (i + 1 >= matrix.Length)
                    matrix[i - matrix.Length + 1][0, 1] = matrix[i][1, 1];
                else
                    matrix[i + 1][0, 1] = matrix[i][1, 1];
            }
        }

        private int[][,] T(int[][,] matrixs)
        {
            for (int i = 0; i < matrixs.Length; i++)
            {
                for (int j = 0; j < matrixs[i].GetLength(0); j++)
                {
                    int temp = matrixs[i][j, 0];
                    matrixs[i][j, 0] = matrixs[i][j, 1];
                    matrixs[i][j, 1] = temp;
                }
            }
            return matrixs;
        }

        public TextBox AllMatrixForlab(TextBox text)
        {
            int[][,] m = SetMatrix();
            text.Text = "Исходная       Преобразований         Полученная\r\n";
            for (int i = 0; i < matrixsFigr.Length; i++)
            {
                for (int j = 0; j < matrixsFigr[i].GetLength(0); j++)
                {
                    int count = 0;
                    text.Text += "| " + m[i][j, count].ToString();
                    count++;
                    text.Text += "  " + m[i][j, count].ToString() + " |\t\t| ";
                    if (j == 0) text.Text += Sx.ToString() + " " + 0.ToString() + " |\t\t| ";
                    else text.Text += 0.ToString() + " " + Sy.ToString() + " |\t\t| ";
                    count--;
                    text.Text += matrixsFigr[i][j, count].ToString();
                    count++;
                    text.Text += "  " + m[i][j, count].ToString() + " |\r\n";
                }
                text.Text += "\r\n";
            }
            return text;
        }
    }
}
