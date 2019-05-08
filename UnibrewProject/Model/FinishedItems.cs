namespace UnibrewREST
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class FinishedItems
    {
        public FinishedItems()
        {
            ProcessingItems = new HashSet<ProcessingItems>();
        }

        public int FinishedItemNumber { get; set; }

        public int LiquidNumber { get; set; }

        public string Name { get; set; }

        public double WeightMin { get; set; }

        public double WeightMiddle { get; set; }

        public double WeightMax { get; set; }

        public virtual ICollection<ProcessingItems> ProcessingItems { get; set; }
    }
}
