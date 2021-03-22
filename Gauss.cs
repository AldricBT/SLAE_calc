using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Controls;

namespace SLAE_calc
{
    class Gauss : Solver
    {
        
        /// <summary>
        /// Решить методом Гаусса без выбора главного элемента
        /// </summary>
        /// <param name="m">Матрица коэф. и столбец правой части</param>
        /// <returns></returns>
        public override Solution Solve(Matrix m, ProgressBar bar)
        {
            Matrix m_help = m.Copy();
            int NumOfEqn = m.GetLength(0);            
            double d,s;
            Solution roots = new Solution(NumOfEqn);
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

                //bar.Value++;
                //Thread.Sleep(1000);
            }
            
            for (int k = NumOfEqn - 1; k >= 0; k--)
            {
                d = 0;
                for (int j = k; j < NumOfEqn; j++)
                {
                    s = m_help[k, j] * roots.Solution_vector[j];
                    d += s;
                }
                roots.Solution_vector[k] = (m_help[k,NumOfEqn] - d) / m_help[k, k];

                //Thread.Sleep(1000);
                //bar.Value++;
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
            roots.Error = this.Error(roots, m);
            return roots;
        }
    }
}
