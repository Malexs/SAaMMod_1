using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using SAiMM.Generators;

namespace SAiMM
{
    public partial class Form1 : Form
    {

        Double a = 32767,
            r = 1,
            m = 65537;
        List<Double> values = new List<Double>();
        Int32[] countOfContaining;

        Boolean isFirst = true;

        public Form1()
        {
            InitializeComponent();
            comboBox1.Text = "Ламера";
        }

        //Return true is parameters were changed;
        private Boolean CheckChanging(Double _a, Double _r, Double _m)
        {
            return !(a == _a && r == _r && m == _m);
        }

        private void button1_Click(object sender, EventArgs e)
        {

            chart1.Series[0].Points.Clear();
            

            Int32 iter = 0;
            Double dispertion = 0;
            Double avKvadr = 0;
            Double matWait = 0;
            Lahmer lahmer = Lahmer.Instance;

            if (!CheckChanging(Double.Parse(textBox1.Text), Double.Parse(textBox2.Text), Double.Parse(textBox3.Text)))
            {
                if (isFirst)
                {
                    values = lahmer.getListOfDoubles(a,r,m);
                    isFirst = false;
                }
                else
                    values = lahmer.getListOfDoubles();
                Console.WriteLine("as");
            }
            else
            {
                a = Double.Parse(textBox1.Text);
                r = Double.Parse(textBox2.Text);
                m = Double.Parse(textBox3.Text);
                values = lahmer.getListOfDoubles(a, r, m);
            }

            switch (comboBox1.Text)
            {
                case "Ламера":
                    break;
                case "Равномерное":
                    if (tmpFirstTBox.Text != "" && tmpSecTBox.Text != "")
                    {
                        Double a = Double.Parse(tmpFirstTBox.Text),
                            b = Double.Parse(tmpSecTBox.Text);
                        values = new Ravnomernoe(values, a, b).GetNewList();
                    }
                    break;
            }

            Statistics stat = new Statistics(values);

            float[] distr = stat.GetDistr();
            for (int i = 0; i <= 19; i++)
            {
                chart1.Series[0].Points.AddXY(Math.Round(stat._beg + stat.interval * (i + 1), 3), distr[i]);
            }

            label4.Text = "Expectation: " + stat.GetExpectation().ToString() + "\n" +
                          "Dispertion: " + stat.GetDispersion().ToString() + "\n" +
                          "Deviation: " + stat.GetMeanSquareDeviation().ToString();

            Console.WriteLine(matWait + "  " + dispertion + " " + avKvadr + ";");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.Text)
            {
                case "Ламера":
                    tmpFirstLabel.Visible = false;
                    tmpSecLabel.Visible = false;
                    tmpFirstTBox.Visible = false;
                    tmpSecTBox.Visible = false;
                    break;
                case "Гауссовское":
                    tmpFirstLabel.Visible = true;
                    tmpFirstLabel.Text = "m";
                    tmpSecLabel.Visible = true;
                    tmpSecLabel.Text = "σ";
                    tmpFirstTBox.Visible = true;
                    tmpSecTBox.Visible = true;
                    break;
                case "Экспоненциальное":
                    tmpFirstLabel.Visible = true;
                    tmpFirstLabel.Text = "λ";
                    tmpSecLabel.Visible = false;
                    tmpFirstTBox.Visible = true;
                    tmpSecTBox.Visible = false;
                    break;
                case "Гамма":
                    tmpFirstLabel.Visible = true;
                    tmpFirstLabel.Text = "λ";
                    tmpSecLabel.Visible = true;
                    tmpSecLabel.Text = "η";
                    tmpFirstTBox.Visible = true;
                    tmpSecTBox.Visible = true;
                    break;
                case "Равномерное":
                case "Треугольное":
                case "Симпсона":
                    tmpFirstLabel.Visible = true;
                    tmpFirstLabel.Text = "a";
                    tmpSecLabel.Visible = true;
                    tmpSecLabel.Text = "b";
                    tmpFirstTBox.Visible = true;
                    tmpSecTBox.Visible = true;
                    break;
            }
        }
    }
}
