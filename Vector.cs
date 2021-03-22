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
        //public Vector(int length, bool solution_exist)
        //{
        //    if (solution_exist)
        //    {
        //        Data = new double[length];
        //        for (int i = 0; i < length; i++)
        //        {
        //            Data[i] = 0;
        //        }
        //    }
        //    else
        //    {
        //        Data = null;
        //    }
        //}

        public double this[int i]
        {
            get { return Data[i]; }
            set { Data[i] = value; }
        }
        public virtual void Resize(int length)
        {
            double[] data_help = new double[length];
            for (int i = 0; i < Data.GetLength(0); i++)
            { 
                try
                {
                    data_help[i] = data[i];
                }
                catch (Exception)
                {

                }
            }
            for (int i = Data.GetLength(0); i < length; i++)
            {
                data_help[i] = 0;
               
            }
            Data = data_help;
        }
        public int GetLength()
        {
            return Data.Length;
        }
    }
}