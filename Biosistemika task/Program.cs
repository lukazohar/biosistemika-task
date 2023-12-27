using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            Experiments experiments = new Experiments();
            List<Plate> plates = experiments.GeneratePlateContent(
                96,
                new List<List<string>>() { new List<string>() { "Sample-1", "Sample-2", "Sample-3" }, new List<string>() { "Sample-1", "Sample-2", "Sample-3" } },
                new List<List<string>>() { new List<string>() { "<Pink>" }, new List<string>() { "<Green>" } },
                new List<int>() { 3, 2 },
                1
            );

            // Default code for Windows Forms Application
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
