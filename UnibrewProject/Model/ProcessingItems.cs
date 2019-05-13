namespace UnibrewProject.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class ProcessingItems
    {
        public ProcessingItems()
        {
            //TapOperator = new HashSet<TapOperator>();
        }

        public string ProcessNumber { get; set; }

        public int FinishedItemNumber { get; set; }

        public virtual FinishedItems FinishedItems { get; set; }

        public virtual ICollection<TapOperator> TapOperator { get; set; }
    }
}
