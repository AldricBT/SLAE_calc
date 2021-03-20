using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLAE_calc
{
    public abstract class DataGenerator
    {
        //private Matrix m;
        //public Matrix M
        //{
        //    get { return m; }
        //    set { m = value; }
        //}
        abstract public Matrix Generate(int NumOfEqn);
    }
}
