using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLAE_calc
{
    public class Matrix
    {
        private double[,] data;

        public double[,] Data
        {
            get { return data; }
            set { data = value; }
        }
        public Matrix(int RowCount, int ColCount)
        {
            Data = new double[RowCount, ColCount];
            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColCount; j++)
                {
                    Data[i, j] = 0;
                }
            }
        }

        public double this[int i, int j]
        {
            get { return Data[i, j]; }
            set { Data[i, j] = value; }
        }        
        /// <summary>
        /// Изменяет размер текущего массива
        /// </summary>
        /// <param name="RowCount">Количество строк нового массива</param>
        /// <param name="ColCount">Количество столбцов нового массива</param>
        public void Resize(int RowCount, int ColCount)
        {
            double[,] Data_help = new double[RowCount, ColCount];
            for (int i = 0; i < Data.GetLength(0); i++)
            {
                for (int j = 0; j < Data.GetLength(1); j++)
                {
                    try
                    {
                        Data_help[i, j] = Data[i, j];
                    }
                    catch (Exception)
                    {

                    }
                    
                }
            }
            for (int i = Data.GetLength(0); i < RowCount; i++)
            {
                for (int j = Data.GetLength(1); j < ColCount; j++)
                {
                    Data_help[i, j] = 0;
                }
            }
            Data = Data_help;
        }
        public int GetLength(int dim)
        {
            return Data.GetLength(dim);     
        }
        /// <summary>
        /// Создает другой объект-копию текущей матрицы
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public Matrix Copy()
        {
            Matrix new_m = new Matrix(this.GetLength(0), this.GetLength(1));
            for (int i = 0; i < this.GetLength(0); i++)
            {
                for (int j = 0; j < this.GetLength(1); j++)
                {
                    new_m[i, j] = this[i, j];
                }
            }
            return new_m;
        }
        /// <summary>
        /// Печать матрицы коэффициентов в консоль
        /// </summary>
        public void Print()
        {
            for (int i = 0; i < Data.GetLength(0); i++)
            {
                for (int j = 0; j < Data.GetLength(1); j++)
                {
                    Console.Write(Data[i,j]+" ");
                }
                Console.WriteLine();
            }
        }        
    }
}
