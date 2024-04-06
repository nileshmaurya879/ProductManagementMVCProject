using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ProductManagementMVCProject.Models
{
    public partial class DemoIdentityMVCProjectContext : DbContext
    {
        public DemoIdentityMVCProjectContext()
        {
        }

        public DemoIdentityMVCProjectContext(DbContextOptions<DemoIdentityMVCProjectContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Sale> Sales { get; set; } = null!;
        public virtual DbSet<TblCategory> TblCategories { get; set; } = null!;
        public virtual DbSet<TblProduct> TblProducts { get; set; } = null!;
        public virtual DbSet<TblUser> TblUsers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=DemoDefault");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sale>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.ProductName).HasMaxLength(100);

                entity.Property(e => e.SalesAmount).HasColumnType("decimal(10, 2)");
            });

            modelBuilder.Entity<TblCategory>(entity =>
            {
                entity.HasKey(e => e.CategoryId)
                    .HasName("PK__tblCateg__19093A0BD8B13215");

                entity.ToTable("tblCategory");

                entity.Property(e => e.CategoryDescription).IsUnicode(false);

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblProduct>(entity =>
            {
                entity.HasKey(e => e.ProductId)
                    .HasName("PK__tblProdu__B40CC6CDCDDF651C");

                entity.ToTable("tblProduct");

                entity.Property(e => e.ProductDescription).IsUnicode(false);

                entity.Property(e => e.ProductName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.TblProducts)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_Products_CategoryId");
            });

            modelBuilder.Entity<TblUser>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__tblUsers__1788CC4C67BB6EF3");

                entity.ToTable("tblUsers");

                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.Property(e => e.Email).HasMaxLength(225);

                entity.Property(e => e.FirstName).HasMaxLength(225);

                entity.Property(e => e.LastName).HasMaxLength(225);

                entity.Property(e => e.SendEmailConfirmed).HasMaxLength(225);

                entity.Property(e => e.UserPassword).HasMaxLength(225);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
