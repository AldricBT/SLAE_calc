using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLAE_calc
{
    public class Gauss : Solver
    {
        
        public override Solution Solve(Matrix m)
        {            
            int NumOfEqn = m.GetLength(0);            
            double d,s;
            Solution x = new Solution(NumOfEqn);
            for (int k = 0; k < NumOfEqn; k++)
            {
                for (int j = k + 1; j < NumOfEqn; j++)
                {
                    d = m[j, k] / m[k, k];
                    for (int i = k; i < NumOfEqn + 1; i++)
                    {
                        m[j, i] = m[j, i] - d * m[k, i];
                    }
                }
            }

            for (int k = NumOfEqn - 1; k >= 0; k--)
            {
                d = 0;
                for (int j = k; j < NumOfEqn; j++)
                {
                    s = m[k, j] * x.Solution_vector[j];
                    d += s;
                }
                x.Solution_vector[k] = (m[k,NumOfEqn] - d) / m[k, k];
            }
            return x;
        }
    }
}
