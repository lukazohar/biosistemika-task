using System;
using System.Collections.Generic;

namespace Biosistemika_task
{
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
        public string SampleName { get; set; }
        public string ReagentName { get; set; }
    }
}
