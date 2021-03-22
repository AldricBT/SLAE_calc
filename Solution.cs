using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLAE_calc
{
    class Solution
    {
        private Vector solution_vector;
        private bool solution_exist;
        private double error;
        public Vector Solution_vector
        {
            get { return solution_vector; }
            internal set { solution_vector = value; }
        }
        public bool Solution_exist
        {
            get { return solution_exist; }
            set { solution_exist = value; }
        }
        public double Error
        {
            get { return error; }
            set { error = value; }
        }
                
        public Solution(int NumOfEqn)
        {
            this.solution_exist = false;
            this.solution_vector = new Vector(NumOfEqn);
        }
    }
}
