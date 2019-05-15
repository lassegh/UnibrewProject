namespace UnibrewREST.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ProcessingItem
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProcessingItem()
        {
            TapOperators = new HashSet<TapOperator>();
        }

        [Key]
        [StringLength(10)]
        public string ProcessNumber { get; set; }

        public int FinishedItemNumber { get; set; }

        public virtual FinishedItem FinishedItem { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TapOperator> TapOperators { get; set; }
    }
}
