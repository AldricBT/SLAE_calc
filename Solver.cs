using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SLAE_calc
{
    abstract class Solver
    {
        private Matrix m;
        private Solution solution_vector;

        public Matrix M
        {
            get { return m; }
            set { m = value; }
        }
        public Solution Solution_vector
        {
            get { return solution_vector; }
            private set { solution_vector = value; }
        }

        public abstract Solution Solve(Matrix m, ProgressBar bar);
        /// <summary>
        /// Погрешность решения СЛАУ
        /// </summary>
        /// <param name="roots">Найденные корни</param>
        /// <param name="m">Матрица коэффициентов</param>
        /// <returns>Максимальное отклонение</returns>
        protected double Error(Solution roots, Matrix m)
        {
            double err;
            double err_max = 0;
            int NumOfEqn = m.GetLength(0);
            Vector b = new Vector(NumOfEqn);    //правая часть как Ax

            for (int i = 0; i < NumOfEqn; i++)
            {
                for (int j = 0; j < NumOfEqn; j++)
                {
                    b[i] += m[i, j] * roots.Solution_vector[j];
                }
                err = Math.Abs(m[i, NumOfEqn] - b[i]);
                if (err > err_max)
                {
                    err_max = err;
                }
            }
            return err_max;
        }
    }
}
