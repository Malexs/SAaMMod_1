using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SAiMM
{
    public partial class Form1 : Form
    {

        Double a = 32767,//32767,//3, 
            r = 1,
            m = 65537;//65537;//5;
        List<Double> values = new List<Double>();
        Double[] countOfContaining = new Double[21] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        public Form1()
        {
            InitializeComponent();
        }

        //Получу Rn, поделить на m
        private Double GetNextR(Double curr)
        {
            return ((a * curr) % m);
        }

        private Double GetNextRand(Double rnd)
        {
            return rnd/m;
        }

        private void CheckContaining(Double element)
        {
            Int32 i = (int)(element / 0.05);
            countOfContaining[i]++;
        }

        private Double GetMath(Int32 iter ,out Double disp, out Double kvad)
        {
            Double wait = 0;
            Double summ = 0;
  
            foreach (Double db in values)
            {
                summ += db;
            }
            wait = Math.Round(summ / iter,4);

            summ = 0;

            foreach (Double db in values)
            {
                summ += Math.Pow(db-wait,2);
            }
            disp = Math.Round(summ / iter,4);

            kvad = Math.Round(Math.Sqrt(disp),4);

            return wait;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            chart1.Series[0].Points.Clear();

            a = Double.Parse(textBox1.Text);
            r = Double.Parse(textBox2.Text);
            m = Double.Parse(textBox3.Text);

            Double currFloat = r;
            Int32 iter = 0;
            Double rnd = 0;

            Double dispertion = 0;
            Double avKvadr = 0;
            Double matWait = 0;

            currFloat = GetNextR(currFloat);
            rnd = GetNextRand(currFloat);

            while (!values.Contains(rnd))
            {
                CheckContaining(rnd);
                iter++;
                values.Add(rnd);
                currFloat = GetNextR(currFloat);
                rnd = GetNextRand(currFloat);
            }

            for (int i = 0; i < countOfContaining.Length - 1; i++)
            {
                Double f = countOfContaining[i] / iter;
                chart1.Series[0].Points.AddY(f);
            }

            //periodLabel.Text = iter.ToString();

            matWait = GetMath(iter,out dispertion,out avKvadr);

            label4.Text = "Period: " + iter.ToString() + "\n" +
                            "Dispertion: " + dispertion + "\n" +
                            "Math Expecting: " + matWait + "\n" +
                            "Average: " + avKvadr + ";";

            Console.WriteLine(matWait +"  "+ dispertion+" "+ avKvadr+";");
            //for (Int32 i = 0; i < countOfContaining.Length; i++)
            //{
            //    Console.WriteLine(parts[i] + ": " + countOfContaining[i]);
            //}
        }
    }
}
