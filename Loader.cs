using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLAE_calc
{
    class Loader
    {
        private Matrix m;
        private Solution roots;
        public Matrix M
        {
            get { return m; }
            set { m = value; }
        }
        public Solution Roots
        {
            get { return roots; }
            set { roots = value; }
        }
        public Loader(Matrix M, Solution Roots)
        {
            this.m = M;
            this.roots = Roots;
        }
        
        public void Load(string path)
        {
            string[] str = null;
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                int NumOfEqn = int.Parse(sr.ReadLine());
                    m.Resize(NumOfEqn, NumOfEqn + 1);
                    for (int i = 0; i < NumOfEqn; i++)
                    {
                        str = sr.ReadLine().Split(';');
                        for (int j = 0; j < NumOfEqn + 1; j++)
                        {
                            m[i, j] = double.Parse(str[j]);
                        }
                    }
                roots.Solution_exist = bool.Parse(sr.ReadLine());
                if (roots.Solution_exist)
                {
                    roots.Solution_vector.Resize(NumOfEqn);
                    str = sr.ReadLine().Split(';');
                    for (int i = 0; i < NumOfEqn; i++)
                    {                        
                        roots.Solution_vector[i] = double.Parse(str[i]);
                    }
                    roots.Error = double.Parse(sr.ReadLine());
                }
                }
            }
            catch (Exception)
            {
                throw new LoadException();
            }
        }
    }
}
