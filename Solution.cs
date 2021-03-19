using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLAE_calc
{
    public class Solution
    {
        private Vector solution_vector;

        public Vector Solution_vector
        {
            get { return solution_vector; }
            set { solution_vector = value; }
        }
                
        public Solution(int NumOfEqn)
        {
            this.Solution_vector = new Vector(NumOfEqn);
        }
    }
}
