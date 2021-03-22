using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLAE_calc
{
    class Saver
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
        public Saver(Matrix M, Solution Roots)
        {
            this.m = M;
            this.roots = Roots;
        }
        
        public void Save(string path)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(path))
                {
                    sw.WriteLine(m.GetLength(0));                    
                    for (int i = 0; i < m.GetLength(0); i++)
                    {
                        for (int j = 0; j < m.GetLength(1); j++)
                        {
                            sw.Write(m[i,j]+";");
                        }
                        sw.WriteLine();
                    }
                    if (roots.Solution_vector.Data != null)
                    {
                        sw.WriteLine("true");
                        for (int i = 0; i < m.GetLength(0); i++)
                        {
                            sw.Write(roots.Solution_vector[i] + ";");
                        }
                        sw.WriteLine();
                        sw.Write(roots.Error);
                    }
                    else
                    {
                        sw.Write("false");
                    }
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
