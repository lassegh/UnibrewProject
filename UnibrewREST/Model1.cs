namespace UnibrewREST
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Col7Model")
        {
        }

        public virtual DbSet<ColSeven> ColSeven { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ColSeven>()
                .Property(e => e.LidMaterialNo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ColSeven>()
                .Property(e => e.PreformMaterialNo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ColSeven>()
                .Property(e => e.LiquidTank)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ColSeven>()
                .Property(e => e.HeuftLid)
                .IsFixedLength();

            modelBuilder.Entity<ColSeven>()
                .Property(e => e.HeuftFillingHeight)
                .IsFixedLength();

            modelBuilder.Entity<ColSeven>()
                .Property(e => e.ProductTasted)
                .IsFixedLength();

            modelBuilder.Entity<ColSeven>()
                .Property(e => e.SukkerStickTest)
                .IsFixedLength();

            modelBuilder.Entity<ColSeven>()
                .Property(e => e.DropTest)
                .IsFixedLength();

            modelBuilder.Entity<ColSeven>()
                .Property(e => e.Comments)
                .IsUnicode(false);

            modelBuilder.Entity<ColSeven>()
                .Property(e => e.Operator)
                .IsFixedLength()
                .IsUnicode(false);
        }
    }
}
