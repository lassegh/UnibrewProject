namespace UnibrewREST
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TESTmoment")]
    public partial class TESTmoment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public double? bottle01 { get; set; }

        public double? bottle02 { get; set; }

        public double? bottle03 { get; set; }

        public double? bottle04 { get; set; }

        public double? bottle05 { get; set; }

        public double? bottle06 { get; set; }

        public double? bottle07 { get; set; }

        public double? bottle08 { get; set; }

        public double? bottle09 { get; set; }

        public double? bottle10 { get; set; }

        public double? bottle11 { get; set; }

        public double? bottle12 { get; set; }

        public double? bottle13 { get; set; }

        public double? bottle14 { get; set; }

        public double? bottle15 { get; set; }

        public DateTime? DateTime { get; set; }
    }
}
