namespace UnibrewREST
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class TESTmomentContext : DbContext
    {
        public TESTmomentContext()
            : base("name=TESTmomentContext")
        {
            base.Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<TESTmoment> TESTmoment { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
