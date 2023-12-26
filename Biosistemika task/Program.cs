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

    class Experiments
    {
        public Experiments() { }

        public List<Plate> GeneratePlateContent(
            int plateSize,
            List<List<string>> sampleNames,
            List<List<string>> reagentNames,
            List<int> numberOfReplicatesPerExperiment,
            int maxAllowedPlates
        )
        {
            List<Plate> plates = new List<Plate>();
            List<Well> unassignedWells = new List<Well>();

            for (int i = 0; i < sampleNames.Count; i++)
            {
                List<string> sampleNamesForCurrentExperiment = sampleNames.ElementAt(i);
                List<string> reagentNamesForCurrentExperiment = reagentNames.ElementAt(i);
                int numberOfReplicatesForExperiment = numberOfReplicatesPerExperiment.ElementAt(i);

                List<Well> currentExperimentWells = GenerateExperimentWells(sampleNamesForCurrentExperiment, reagentNamesForCurrentExperiment, numberOfReplicatesForExperiment);
                unassignedWells.AddRange(currentExperimentWells);
            }

            while (unassignedWells.Any())
            {
                if (plates.Any() && plates.Last().IsFull())
                {
                    plates.Add(new Plate(plateSize));
                    continue;
                }
                if (unassignedWells.Count < plateSize)
                {
                    plates.Add(new Plate(plateSize, unassignedWells));
                    return plates;
                }
                else
                {
                    plates.Add(new Plate(plateSize, unassignedWells.GetRange(0, plateSize)));
                    unassignedWells.RemoveRange(0, plateSize);
                }
            }

            return plates;
        }
        public static List<Well> GenerateExperimentWells(List<string> sampleNames, List<string> reagentNames, int numberOfReplicates)
        {
            List<Well> newWells = new List<Well>();
            foreach (var sample in sampleNames)
            {
                foreach (var reagent in reagentNames)
                {
                    for (int i = 0; i < numberOfReplicates; i++)
                    {
                        Well newWell = new Well(sample, reagent);
                        newWells.Add(newWell);
                    }
                }
            }
            return newWells;
        }
    }

    class Plate
    {
        public List<Well> Wells = new List<Well>();
        public Plate(int plateSize)
        {
            this.Wells = new List<Well>();
            this.SetSize(plateSize);
        }
        public Plate(int plateSize, List<Well> wells)
        {
            this.SetSize(plateSize);
            this.InsertWells(wells);
        }

        private void SetSize(int plateSize) => this.Wells.Capacity = plateSize;

        public List<Well> InsertWells(List<Well> wellsToInsert)
        {
            if (wellsToInsert.Count < this.GetNumberOfEmptyWells())
            {
                this.Wells.AddRange(wellsToInsert);
                return new List<Well>();
            }
            else
            {
                this.Wells.AddRange(wellsToInsert.GetRange(0, this.GetNumberOfEmptyWells()));
                return wellsToInsert.GetRange(this.GetNumberOfEmptyWells(), wellsToInsert.Count);
            }
        }

        public Boolean IsFull() => this.Wells.Capacity == this.Wells.Count;
        public int GetNumberOfEmptyWells() => this.Wells.Capacity - this.Wells.Count;
    }

    class Well
    {
        public Well(string sample, string reagent)
        {
            this.SampleName = sample;
            this.ReagentName = reagent;
        }
        public string SampleName;
        public string ReagentName;
    }
}
