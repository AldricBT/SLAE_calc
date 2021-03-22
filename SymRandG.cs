using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace SLAE_calc
{
    class SymRandG : DataGenerator
    {
        public override Matrix Generate(int NumOfEqn)
        {
            Random r = new Random();
            Matrix m = new Matrix(NumOfEqn, NumOfEqn + 1);
            for (int i = 0; i < m.GetLength(0); i++)
            {
                for (int j = 0; j < m.GetLength(0); j++)
                {
                    if (i == j)
                    {
                        m[i, j] = r.Next(30, 101);
                    }
                    else if (j > i)
                    {
                        m[i, j] = r.Next(-10, 11);
                    }
                    else
                    {
                        m[i, j] = m[j, i];
                    }                    
                    Thread.Sleep(1);
                }
            }
            for (int i = 0; i < m.GetLength(0); i++)
            {
                m[i, m.GetLength(1) - 1] = r.Next(-50, 51);
                Thread.Sleep(1);
            }
            return m;
        }
    }
}
