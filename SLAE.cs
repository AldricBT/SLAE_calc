using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SLAE_calc
{
    
    class SLAE
    {
        private int numofeqn;
        private Saver save;
        private Loader load;
        private Matrix m;
        private Solution roots;
        private bool load_error;
        //private Solver solv;
        //private DataGenerator data_gen;

        public SLAE(int NumOfEqn)
        {
            this.numofeqn = NumOfEqn;
            this.m = new Matrix(NumOfEqn, NumOfEqn + 1);
            this.roots = new Solution(NumOfEqn);
            this.save = new Saver(m, null);
            this.load = new Loader(m, roots);
            load_error = false;
        }
        //public SLAE()
        //{
        //    this.numofeqn = NumOfEqn;
        //    this.m = new Matrix(3, 4);
        //    this.save = new Saver(m);
        //    this.load = new Loader(m);
        //}
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
            get { return roots; }
            set { roots = value; }
        }
        public bool Load_error
        {
            get { return load_error; }
        }
        public void Resize(int NumOfEqn)
        {
            m.Resize(NumOfEqn, NumOfEqn + 1);
        }
        public void Save(string path)
        {
            save.M = m;
            save.Roots = roots;
            save.Save(path);
        }
        
        public void Load(string path)
        {
            try
            {
                load.Load(path);
            }
            catch (LoadException)
            {
                load_error = true;
            }
            
        }
        
        public void Solve(Matrix m, Solver method, ProgressBar bar)
        {
            roots = method.Solve(m, bar);
        }

        public void Generate(DataGenerator method)
        {
            m = method.Generate(m.GetLength(0));
        }
    }
}
