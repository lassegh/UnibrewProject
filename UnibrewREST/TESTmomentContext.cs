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
            modelBuilder.Entity<TESTmoment>()
                .Property(e => e.DateTime);

            modelBuilder.Entity<TESTmoment>()
                .Property(e => e.bottle01);

            modelBuilder.Entity<TESTmoment>()
                .Property(e => e.bottle02);

            modelBuilder.Entity<TESTmoment>()
                .Property(e => e.bottle03);

            modelBuilder.Entity<TESTmoment>()
                .Property(e => e.bottle04);

            modelBuilder.Entity<TESTmoment>()
                .Property(e => e.bottle05);

            modelBuilder.Entity<TESTmoment>()
                .Property(e => e.bottle06);

            modelBuilder.Entity<TESTmoment>()
                .Property(e => e.bottle07);

            modelBuilder.Entity<TESTmoment>()
                .Property(e => e.bottle08);

            modelBuilder.Entity<TESTmoment>()
                .Property(e => e.bottle09);

            modelBuilder.Entity<TESTmoment>()
                .Property(e => e.bottle10);

            modelBuilder.Entity<TESTmoment>()
                .Property(e => e.bottle11);

            modelBuilder.Entity<TESTmoment>()
                .Property(e => e.bottle12);

            modelBuilder.Entity<TESTmoment>()
                .Property(e => e.bottle13);

            modelBuilder.Entity<TESTmoment>()
                .Property(e => e.bottle14);

            modelBuilder.Entity<TESTmoment>()
                .Property(e => e.bottle15);
        }
    }
}
