using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLAE_calc
{
    public class Saver
    {
        private Matrix m;
        //public Matrix M
        //{
        //    get { return m; }
        //    set { m = value; }
        //}
        public Saver(Matrix M)
        {
            this.m = M;
        }
        public void Save(string path)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(path))
                {
                    sw.WriteLine(m.GetLength(0));
                    sw.WriteLine(m.GetLength(1));
                    for (int i = 0; i < m.GetLength(0); i++)
                    {
                        for (int j = 0; j < m.GetLength(1); j++)
                        {
                            sw.Write(m[i,j]+";");
                        }
                        sw.WriteLine();
                    }                    
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
