namespace UnibrewREST
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class FinishedItems
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FinishedItems()
        {
            ProcessingItems = new HashSet<ProcessingItems>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FinishedItemNumber { get; set; }

        public int? LiquidNumber { get; set; }

        [StringLength(30)]
        public string Name { get; set; }

        public double? WeightMin { get; set; }

        public double? WeightMiddle { get; set; }

        public double? WeightMax { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProcessingItems> ProcessingItems { get; set; }
    }
}
