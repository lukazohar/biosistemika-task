using System;
using System.Collections.Generic;
using System.Linq;

namespace Biosistemika_task
{
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
            List<Well> unassignedWells = GenerateWells(sampleNames, reagentNames, numberOfReplicatesPerExperiment);

            try
            {
                while (unassignedWells.Any())
                {
                    if (maxAllowedPlates <= plates.Count)
                        throw new System.Exception("Maxium allowed plates reached");

                    // If there are less unsigned wells than size of plate, take only those. Otherwise, `plateSize` many, since plate is empty and there are enough wells for whole plate.
                    int numberOfWellsForThisPlate = Math.Min(unassignedWells.Count, plateSize);
                    plates.Add(new Plate(plateSize, unassignedWells.GetRange(0, numberOfWellsForThisPlate)));
                    unassignedWells.RemoveRange(0, numberOfWellsForThisPlate);
                }
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e);
                throw e;
            }

            return plates;
        }
        public static List<Well> GenerateWells(List<List<string>> sampleNames, List<List<string>> reagentNames, List<int> numberOfReplicatesPerExperiment)
        {
            List<Well> wells = new List<Well>();

            for (int i = 0; i < sampleNames.Count; i++)
            {
                List<string> sampleNamesForCurrentExperiment = sampleNames.ElementAt(i);
                List<string> reagentNamesForCurrentExperiment = reagentNames.ElementAt(i);
                int numberOfReplicatesForExperiment = numberOfReplicatesPerExperiment.ElementAt(i);

                List<Well> currentExperimentWells = GenerateExperimentWells(sampleNamesForCurrentExperiment, reagentNamesForCurrentExperiment, numberOfReplicatesForExperiment);
                wells.AddRange(currentExperimentWells);
            }

            return wells;
        }
        private static List<Well> GenerateExperimentWells(List<string> sampleNames, List<string> reagentNames, int numberOfReplicates)
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
}
