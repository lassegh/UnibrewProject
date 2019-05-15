namespace UnibrewREST.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Quayzer : DbContext
    {
        public Quayzer()
            : base("name=Quayzer")
        {
            base.Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<FinishedItem> FinishedItems { get; set; }
        public virtual DbSet<LiquidTank> LiquidTanks { get; set; }
        public virtual DbSet<ProcessingItem> ProcessingItems { get; set; }
        public virtual DbSet<TapOperator> TapOperators { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FinishedItem>()
                .Property(e => e.LiquidNumber)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<FinishedItem>()
                .Property(e => e.Name)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<FinishedItem>()
                .HasMany(e => e.ProcessingItems)
                .WithRequired(e => e.FinishedItem)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LiquidTank>()
                .Property(e => e.Name)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<LiquidTank>()
                .HasMany(e => e.TapOperators)
                .WithOptional(e => e.LiquidTank1)
                .HasForeignKey(e => e.LiquidTank);

            modelBuilder.Entity<ProcessingItem>()
                .Property(e => e.ProcessNumber)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ProcessingItem>()
                .HasMany(e => e.TapOperators)
                .WithRequired(e => e.ProcessingItem)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TapOperator>()
                .Property(e => e.LidMaterialNo)
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
                .Property(e => e.Comments)
                .IsUnicode(false);

            modelBuilder.Entity<TapOperator>()
                .Property(e => e.Operator)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<TapOperator>()
                .Property(e => e.ProcessNumber)
                .IsFixedLength()
                .IsUnicode(false);
        }
    }
}
