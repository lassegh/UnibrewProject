namespace UnibrewREST.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class FinishedItem
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FinishedItem()
        {
            ProcessingItems = new HashSet<ProcessingItem>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FinishedItemNumber { get; set; }

        [StringLength(10)]
        public string LiquidNumber { get; set; }

        [StringLength(30)]
        public string Name { get; set; }

        public double? WeightMin { get; set; }

        public double? WeightMiddle { get; set; }

        public double? WeightMax { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProcessingItem> ProcessingItems { get; set; }
    }
}
