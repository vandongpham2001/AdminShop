using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace ModelEF.Model
{
    public partial class PhamVanDongContext : DbContext
    {
        public PhamVanDongContext()
            : base("name=PhamVanDongContext")
        {
        }

        public virtual DbSet<tblCategory> tblCategories { get; set; }
        public virtual DbSet<tblProduct> tblProducts { get; set; }
        public virtual DbSet<tblUserAccount> tblUserAccounts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tblCategory>()
                .Property(e => e.Id)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tblCategory>()
                .HasMany(e => e.tblProducts)
                .WithOptional(e => e.tblCategory)
                .HasForeignKey(e => e.CategoryId)
                .WillCascadeOnDelete();

            modelBuilder.Entity<tblProduct>()
                .Property(e => e.Id)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tblProduct>()
                .Property(e => e.UnitCost)
                .HasPrecision(19, 4);

            modelBuilder.Entity<tblProduct>()
                .Property(e => e.CategoryId)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tblUserAccount>()
                .Property(e => e.Id)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tblUserAccount>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<tblUserAccount>()
                .Property(e => e.Password)
                .IsUnicode(false);
        }
    }
}
