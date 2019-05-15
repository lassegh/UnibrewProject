namespace UnibrewREST
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ProcessingItems
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProcessingItems()
        {
            TapOperator = new HashSet<TapOperator>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string ProcessNumber { get; set; }

        public int FinishedItemNumber { get; set; }

        public virtual FinishedItems FinishedItems { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TapOperator> TapOperator { get; set; }
    }
}
