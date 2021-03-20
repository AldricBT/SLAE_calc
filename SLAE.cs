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
        private Loader load;
        private Matrix m;
        private Solution solu;
        private Solver solv;
        private DataGenerator data_gen;

        public SLAE(int NumOfEqn)
        {
            this.numofeqn = NumOfEqn;
            this.m = new Matrix(NumOfEqn, NumOfEqn + 1);
            this.save = new Saver(m);
            this.load = new Loader(m);
            this.solu = new Solution(NumOfEqn);
        }
        public SLAE()
        {
            this.numofeqn = NumOfEqn;
            this.m = new Matrix(3, 4);
            this.save = new Saver(m);
            this.load = new Loader(m);
        }
        public int NumOfEqn
        {
            get { return numofeqn; }
            set { numofeqn = value; }
        }
        public Matrix M
        {
            get { return m; }
            set { m = value; }
        }
        public Solution Solu
        {
            get { return solu; }
            set { solu = value; }
        }

        public void Resize(int NumOfEqn)
        {
            m.Resize(NumOfEqn, NumOfEqn + 1);
        }
        public void Save(string path)
        {
            save.Save(path);
        }
        
        public void Load(string path)
        {
            load.Load(path);
        }
        
        public void Solve(Matrix m, Solver method)
        {
            solu = method.Solve(m);
        }

        public void Generate(DataGenerator method)
        {
            m = method.Generate(m.GetLength(0));
        }
    }
}
