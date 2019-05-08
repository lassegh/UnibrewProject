namespace UnibrewREST
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class FullDBmodel : DbContext
    {
        public FullDBmodel()
            : base("name=FullDBmodel")
        {
            base.Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<FinishedItems> FinishedItems { get; set; }
        public virtual DbSet<LiquidTanks> LiquidTanks { get; set; }
        public virtual DbSet<ProcessingItems> ProcessingItems { get; set; }
        public virtual DbSet<TapOperator> TapOperator { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FinishedItems>()
                .Property(e => e.Name)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<FinishedItems>()
                .HasMany(e => e.ProcessingItems)
                .WithRequired(e => e.FinishedItems)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LiquidTanks>()
                .Property(e => e.Name)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<LiquidTanks>()
                .HasMany(e => e.TapOperator)
                .WithOptional(e => e.LiquidTanks)
                .HasForeignKey(e => e.LiquidTank);

            modelBuilder.Entity<TapOperator>()
                .Property(e => e.LidMaterialNo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<TapOperator>()
                .Property(e => e.ProcessNumber)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<TapOperator>()
                .Property(e => e.PreformMaterialNo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<TapOperator>()
                .Property(e => e.LiquidTank)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<TapOperator>()
                .Property(e => e.HeuftLid)
                .IsFixedLength();

            modelBuilder.Entity<TapOperator>()
                .Property(e => e.HeuftFillingHeight)
                .IsFixedLength();

            modelBuilder.Entity<TapOperator>()
                .Property(e => e.ProductTasted)
                .IsFixedLength();

            modelBuilder.Entity<TapOperator>()
                .Property(e => e.SukkerStickTest)
                .IsFixedLength();

            modelBuilder.Entity<TapOperator>()
                .Property(e => e.DropTest)
                .IsFixedLength();

            modelBuilder.Entity<TapOperator>()
                .Property(e => e.Comments)
                .IsUnicode(false);

            modelBuilder.Entity<TapOperator>()
                .Property(e => e.Operator)
                .IsFixedLength()
                .IsUnicode(false);
        }
    }
}
