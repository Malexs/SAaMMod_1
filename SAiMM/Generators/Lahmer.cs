using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAiMM.Generators
{
    class Lahmer
    {
        Double a = 32767,//32767,//3, 
            r = 1,
            m = 65537;//65537;//5;
        List<Double> values = new List<Double>();
        static Lahmer inst = null;
        public static Lahmer Instance {get {
            if (inst==null) inst = new Lahmer();
            return inst;}}

        //Получу Rn, поделить на m
        private Double GetNextR(Double curr)
        {
            return ((a * curr) % m);
        }

        private Double GetNextRand(Double rnd)
        {
            return rnd/m;
        }

        public List<Double> getListOfDoubles()
        {
            return values;
        }

        public List<Double> getListOfDoubles(double a, double r, double m)
        {
            Double currFloat = r;
            Double rnd = 0;
            values.Clear(); 

            this.a = a;
            this.r = r;
            this.m = m;

            currFloat = GetNextR(currFloat);
            rnd = GetNextRand(currFloat);

            while (!values.Contains(rnd))
            {
                values.Add(rnd);
                currFloat = GetNextR(currFloat);
                rnd = GetNextRand(currFloat);
            }

            return values;
        }
    }
}
