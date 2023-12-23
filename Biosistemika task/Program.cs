using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Biosistemika_task
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

        }

        static void GeneratePlateContent(
            int plateSize,
            List<List<string>> sampleNames,
            List<List<string>> reagentNames,
            List<int> numberOfReplicatesPerExperiment,
            int maxAllowedPlates
        )
        {
            
        }
    }

    class Plate
    {
        public Plate() { this.Wells = new List<Well>(); }
        public List<Well> Wells;
    }

    class Well
    {
        public Well() { }
        public string SampleName;
        public string ReagentName;
    }
}
