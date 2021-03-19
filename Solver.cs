using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLAE_calc
{
    public abstract class Solver
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
            set { solution_vector = value; }
        }

        public abstract Solution Solve(Matrix M);
    }
}
