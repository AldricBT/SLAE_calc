using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLAE_calc
{
    interface ISolution
    {        
        Vector Solution_vector
        {
            get;
            set;
        }
        bool Solution_exist
        {
            get;
            set;
        }
        double Error
        {
            get;
            set;
        }
    }
}
