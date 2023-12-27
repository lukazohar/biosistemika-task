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
                    if (maxAllowedPlates == plates.Count)
                    {
                        // THROW ERROR
                    }
                    else
                    {
                        plates.Add(new Plate(plateSize));
                        continue;
                    }
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
}
