namespace UnibrewProject.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class LiquidTanks
    {
        public LiquidTanks()
        {
            TapOperator = new HashSet<TapOperator>();
        }

        public string Name { get; set; }

        public virtual ICollection<TapOperator> TapOperator { get; set; }
    }
}
