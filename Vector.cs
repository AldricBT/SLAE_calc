using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLAE_calc
{
    public class Vector
    {
        private double[] data;

        public double[] Data
        {
            get { return data; }
            set { data = value; }
        }
        public Vector(int Length)
        {
            Data = new double[Length];
            for (int i = 0; i < Length; i++)
            {
                Data[i] = 0;
            }
        }

        public double this[int i]
        {
            get { return Data[i]; }
            set { Data[i] = value; }
        }
        ///// <summary>
        ///// Изменяет размер текущего массива
        ///// </summary>
        ///// <param name="RowCount">Количество строк нового массива</param>
        ///// <param name="ColCount">Количество столбцов нового массива</param>
        //public void Resize(int RowCount, int ColCount)
        //{
        //    double[,] Data_help = new double[RowCount, ColCount];
        //    for (int i = 0; i < Data.GetLength(0); i++)
        //    {
        //        for (int j = 0; j < Data.GetLength(1); j++)
        //        {
        //            try
        //            {
        //                Data_help[i, j] = Data[i, j];
        //            }
        //            catch (Exception)
        //            {

        //            }

        //        }
        //    }
        //    for (int i = Data.GetLength(0); i < RowCount; i++)
        //    {
        //        for (int j = Data.GetLength(1); j < ColCount; j++)
        //        {
        //            Data_help[i, j] = 0;
        //        }
        //    }
        //    Data = Data_help;
        //}
        public int GetLength()
        {
            return Data.Length;
        }
    }
}