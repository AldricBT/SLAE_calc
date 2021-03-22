using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SLAE_calc
{
    class Zeidel : Solver
    {
        public override Solution Solve(Matrix m, ProgressBar bar)
        {
            double eps = 1e-8;
            int NumOfEqn = m.GetLength(0);
            Solution roots = new Solution(NumOfEqn);

            int k = 0; //количество итераций
            int N = 1000; //максимальное число итераций
            double diff = 1; //погрешность
            double s, Xi;
            while ((k <= N) && (diff >= eps))
            {
                k = k + 1;
                for (int i = 0; i < NumOfEqn; i++)
                {
                    s = 0;
                    for (int j = 0; j < NumOfEqn; j++)
                    {
                        if (i != j)
                        {
                            s += m[i, j] * roots.Solution_vector[j];
                        }
                    }
                    Xi = (m[i,NumOfEqn] - s) / m[i, i];
                    diff = Math.Abs(Xi - roots.Solution_vector[i]);
                    roots.Solution_vector[i] = Xi;
                }
            }
            roots.Solution_exist = true;
            for (int i = 0; i < NumOfEqn; i++)
            {
                if (roots.Solution_vector.Data[i] == double.NaN || roots.Solution_vector.Data[i] == double.NegativeInfinity || roots.Solution_vector.Data[i] == double.PositiveInfinity)
                {
                    roots.Solution_exist = false;
                    break;
                }
            }
            
            if (roots.Solution_exist)
            {
                roots.Error = this.Error(roots, m);
            }            
            return roots;
        }
        
    }
}
