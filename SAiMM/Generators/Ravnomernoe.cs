using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAiMM.Generators
{
    class Ravnomernoe
    {
        List<Double> values = null;
        Double a = 0,
            b = 0;

        public Ravnomernoe(List<Double> _values, Double _a, Double _b)
        {
            values = _values;
            a = _a;
            b = _b;
        }

        public List<Double> GetNewList()
        {
            List<double> rsq = new List<double>();
            foreach (double tmp in values)
            {
                rsq.Add(a + (b - a) * tmp);
            }
            return rsq;
        }

    }
}
