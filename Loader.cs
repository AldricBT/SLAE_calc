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
        //public Matrix M
        //{
        //    get { return m; }
        //    set { m = value; }
        //}
        public Loader(Matrix M)
        {
            this.m = M;
        }
        public void Load(string path)
        {
            string[] str = null;
            //try
            //{
                using (StreamReader sr = new StreamReader(path))
                {
                int NumOfEqn = int.Parse(sr.ReadLine());
                    m.Resize(NumOfEqn, NumOfEqn + 1);
                    for (int i = 0; i < m.GetLength(0); i++)
                    {
                        str = sr.ReadLine().Split(';');
                        for (int j = 0; j < m.GetLength(1); j++)
                        {
                            m[i, j] = int.Parse(str[j]);
                        }
                    }
                }
            //}
            //catch (Exception)
            //{

            //}
        }
    }
}
