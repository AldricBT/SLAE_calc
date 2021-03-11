using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLAE_calc
{
    
    public class SLAE
    {
        private int numofeqn;
        private Saver save;
        private Matrix m;
        public SLAE(int NumOfEqn)
        {
            this.numofeqn = NumOfEqn;
            this.m = new Matrix(NumOfEqn, NumOfEqn + 1);
            this.save = new Saver(m);
        }
        public SLAE()
        {
            this.numofeqn = NumOfEqn;
            this.m = new Matrix(3, 4);
            this.save = new Saver(m);
        }
        public int NumOfEqn
        {
            get { return numofeqn; }
            private set { numofeqn = value; }
        }
        public Matrix M
        {
            get { return m; }
            set { m = value; }
        }

        //public void Resize(int NumOfEqn)
        //{
        //    m.Resize(NumOfEqn, NumOfEqn + 1);
        //}
        public void Save(string path)
        {
            save.Save(path);
        }
        
    }
}
