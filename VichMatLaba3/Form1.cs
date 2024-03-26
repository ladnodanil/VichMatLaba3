using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace VichMatLaba3
{
    public partial class Form1 : Form
    {
        List<float> xi = new List<float>(){-2,0,2,3,4};
        List<float> fxi = new List<float>(){18,12,7,-1,0 };

        

        public Form1()
        {
            

            InitializeComponent();


            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            xi.Clear();
            fxi.Clear();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                
                if (dataGridView1[0, i].Value != null && dataGridView1[0, i].Value.ToString() != "")
                {
                    xi.Add(Convert.ToInt32(dataGridView1[0, i].Value));
                }
                if (dataGridView1[1, i].Value != null && dataGridView1[1, i].Value.ToString() != "")
                {
                    fxi.Add(Convert.ToInt32(dataGridView1[1, i].Value));
                }
            }
            Grafcs grafcs = new Grafcs(xi, fxi);
            grafcs.Show();
        }
    }
}
