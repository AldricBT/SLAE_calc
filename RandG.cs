using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace SLAE_calc
{
    class RandG : DataGenerator
    {
        public override Matrix Generate(int NumOfEqn)
        {
            Random r = new Random();
            Matrix m = new Matrix(NumOfEqn, NumOfEqn + 1);
            for (int i = 0; i < m.GetLength(0); i++)
            {
                for (int j = 0; j < m.GetLength(1); j++)
                {
                    m[i, j] = r.Next(-20, 21);
                    Thread.Sleep(1);
                }                
            }
            return m;
        }
        

    }
}
