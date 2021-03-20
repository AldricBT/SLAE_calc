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
            Matrix m_help = m.Copy();
            int NumOfEqn = m.GetLength(0);            
            double d,s;
            Solution x = new Solution(NumOfEqn);
            for (int k = 0; k < NumOfEqn; k++)
            {
                for (int j = k + 1; j < NumOfEqn; j++)
                {
                    d = m_help[j, k] / m_help[k, k];
                    for (int i = k; i < NumOfEqn + 1; i++)
                    {
                        m_help[j, i] = m_help[j, i] - d * m_help[k, i];
                    }
                }
            }

            for (int k = NumOfEqn - 1; k >= 0; k--)
            {
                d = 0;
                for (int j = k; j < NumOfEqn; j++)
                {
                    s = m_help[k, j] * x.Solution_vector[j];
                    d += s;
                }
                x.Solution_vector[k] = (m_help[k,NumOfEqn] - d) / m_help[k, k];
            }
            return x;
        }
    }
}
