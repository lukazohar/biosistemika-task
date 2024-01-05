using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Biosistemika_task
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Experiments experiments = new Experiments();
            List<Plate> plates = experiments.GeneratePlateContent(
                96,
                new List<List<string>>() { new List<string>() { "Sample-1", "Sample-2", "Sample-3" }, new List<string>() { "Sample-1", "Sample-2", "Sample-3" } },
                new List<List<string>>() { new List<string>() { "<Pink>" }, new List<string>() { "<Green>" } },
                new List<int>() { 3, 2 },
                1
            );

            this.dataGridView1.DataSource = plates.First().Wells;
        }
    }
}
