using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace VichMatLaba3
{
    public partial class Grafcs : Form
    {

        FunctionApproximation functionApproximation;
        float step = (float)Math.Pow(10, -2);
        public Grafcs(List<float> xi, List<float> fxi)
        {

            functionApproximation = new FunctionApproximation(xi, fxi);

            InitializeComponent();
            chart1.ChartAreas[0].AxisX.Minimum = -5;
            chart1.ChartAreas[0].AxisX.Maximum = 5;
            chart1.ChartAreas[0].AxisX.Interval = 1;
            chart1.ChartAreas[0].AxisY.Minimum = -2;
            chart1.ChartAreas[0].AxisY.Maximum = 20;
            chart1.ChartAreas[0].AxisY.Interval = 2;

            dataGridView1.ColumnCount = 5;
            dataGridView1.RowCount = 2;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersVisible = true;

            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                dataGridView1.Columns[i].HeaderText = "a" + i.ToString();
            }
            this.chart1.Series[0].ToolTip = "X = #VALX, Y = #VALY";
            for (int i = 0; i < xi.Count; i++)
            {
                this.chart1.Series[0].Points.AddXY(xi[i], fxi[i]);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {


            if (checkedListBox1.GetItemChecked(0))
            {
                for (float i = -5; i <= 5; i += step)
                {
                    float y = functionApproximation.Lagrange(i);
                    this.chart1.Series[4].Points.AddXY(i, y);
                }
            }
            if (checkedListBox1.GetItemChecked(1))
            {
                for (float i = -5; i <= 5; i += step)
                {
                    float y = functionApproximation.Newton(i);
                    this.chart1.Series[5].Points.AddXY(i, y);
                }
            }
            if (checkedListBox1.GetItemChecked(2))
            {
                float[] ai = functionApproximation.LeastSquareMethod(1);
                for (float i = -5; i <= 5; i += step)
                {

                    this.chart1.Series[1].Points.AddXY(i, ai[1] * i + ai[0]);
                }
            }
            if (checkedListBox1.GetItemChecked(3))
            {
                float[] ai = functionApproximation.LeastSquareMethod(2);
                for (float i = -5; i <= 5; i += step)
                {
                    this.chart1.Series[2].Points.AddXY(i, ai[2] * Math.Pow(i, 2) + ai[1] * i + ai[0]);
                }
            }
            if (checkedListBox1.GetItemChecked(4))
            {
                float[] ai = functionApproximation.LeastSquareMethod(3);
                for (float i = -5; i <= 5; i += step)
                {
                    this.chart1.Series[3].Points.AddXY(i, ai[3] * Math.Pow(i, 3) + ai[2] * Math.Pow(i, 2) + ai[1] * i + ai[0]);
                }
            }
            if (checkedListBox1.GetItemChecked(5))
            {
                float[] ai = new float[dataGridView1.Columns.Count];
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    if (float.TryParse(dataGridView1[i, 0].Value.ToString(), out float coefficient))
                    {
                        ai[i] = coefficient;
                    }
                }
                for (float i = -5; i <= 5; i += step)
                {

                    this.chart1.Series[6].Points.AddXY(i, ai[4] * Math.Pow(i, 4) + ai[3] * Math.Pow(i, 3) + ai[2] * Math.Pow(i, 2) + ai[1] * i + ai[0]);
                }
            }
            if (checkedListBox1.GetItemChecked(6))
            {   
                for (float i = -5; i <= 5; i += 0.1f)
                {
                    this.chart1.Series[7].Points.AddXY(i, -3.19*i + 11.466);
                }
            }
            if (checkedListBox1.GetItemChecked(7))
            {   
                for (float i = -5; i <= 5; i += 0.1f)
                {
                    this.chart1.Series[8].Points.AddXY(i, 0.045 * Math.Pow(i,2) - 3.276*i+11.291);
                }
            }
            if (checkedListBox1.GetItemChecked(8))
            {
                for (float i = -5; i <= 5; i += 0.1f)
                {
                    this.chart1.Series[9].Points.AddXY(i, 0.059 * Math.Pow(i,3) - 0.138 * Math.Pow(i,2) -3.539 * i +11.789);
                }
            }




        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 1; i < chart1.Series.Count; i++)
            {
                this.chart1.Series[i].Points.Clear();
            }
            
        }
    }
}
