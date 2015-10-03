using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAiMM.Generators
{
    class Statistics
    {
        List<Double> values = null;
        Double mathExpect, dispertion, deviation, beg, end, interv;
        public double interval { get { return interv; } }
        public double _beg { get { return beg; } }
        public double _end { get { return end; } }

        public Statistics(List<Double> val)
        {
            values = val;
        }

        public double GetExpectation()
        {
            double t = 0;
            foreach (double i in values)
            {
                t += i;
            }
            mathExpect = (double)t / values.Count;
            return Math.Round(mathExpect,3);
        }

        public double GetDispersion()
        {
            double t = 0;
            foreach (double i in values)
            {
                t += Math.Pow((i - mathExpect), 2);
            }
            dispertion = (double)t / values.Count;
            return Math.Round(dispertion,3);
        }

        public double GetMeanSquareDeviation()
        {
            deviation = Math.Sqrt(dispertion);
            return Math.Round(deviation, 4); ;
        }

        public float[] GetDistr()
        {
            float[] distrib = new float[20];
            List<double> rstemp = new List<double>(values);
            rstemp.Sort();
            int c = 0;
            int index = 0;
            beg = rstemp[0];
            end = rstemp[rstemp.Count - 1];
            interv = (end - beg) / 20;
            for (int i = 0; i <= 19; i++)
            {
                while ((index <= rstemp.Count - 1) && (rstemp[index] <= beg + interv * (i + 1)))
                {
                    if (rstemp[index] >= beg) c++;
                    index++;
                }
                distrib[i] = (float)c / values.Count;
                c = 0;
            }
            return distrib;
        }
    }
}
