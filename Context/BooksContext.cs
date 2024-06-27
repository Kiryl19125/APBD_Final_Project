using System;
using System.Collections.Generic;
using FinalProjectAPBD.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectAPBD.Context;

public partial class BooksContext : DbContext
{
    public BooksContext()
    {
    }

    public BooksContext(DbContextOptions<BooksContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AppUser> AppUsers { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<Contract> Contracts { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Discount> Discounts { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Software> Softwares { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=Books;User ID=SA;Password=12052004MYdr!;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AppUser>(entity =>
        {
            entity.HasKey(e => e.AppUserId).HasName("AppUsers_pk");

            entity.Property(e => e.AppUserId).HasColumnName("AppUserID");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Login)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.RefreshTockenExp).HasColumnType("datetime");
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.CompanyId).HasName("PK__Companie__2D971C4C582F4AB7");

            entity.HasIndex(e => e.Krs, "UQ__Companie__C41880E1A8EC2111").IsUnique();

            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.CompanyName).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Krs)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("KRS");
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
        });

        modelBuilder.Entity<Contract>(entity =>
        {
            entity.HasKey(e => e.ContractId).HasName("PK__Contract__C90D34091C187DFD");

            entity.Property(e => e.ContractId).HasColumnName("ContractID");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.DiscountId).HasColumnName("DiscountID");
            entity.Property(e => e.SoftwareId).HasColumnName("SoftwareID");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Customer).WithMany(p => p.Contracts)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Contracts__Custo__68487DD7");

            entity.HasOne(d => d.Discount).WithMany(p => p.Contracts)
                .HasForeignKey(d => d.DiscountId)
                .HasConstraintName("FK__Contracts__Disco__6A30C649");

            entity.HasOne(d => d.Software).WithMany(p => p.Contracts)
                .HasForeignKey(d => d.SoftwareId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Contracts__Softw__693CA210");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64B852F83AEC");

            entity.HasIndex(e => e.Pesel, "UQ__Customer__4F16EE7FA21E661C").IsUnique();

            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Pesel)
                .HasMaxLength(11)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("PESEL");
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
        });

        modelBuilder.Entity<Discount>(entity =>
        {
            entity.HasKey(e => e.DiscountId).HasName("PK__Discount__E43F6DF61365F5A8");

            entity.Property(e => e.DiscountId).HasColumnName("DiscountID");
            entity.Property(e => e.DiscountPercentage).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payments__9B556A58AD82ABDE");

            entity.Property(e => e.PaymentId).HasColumnName("PaymentID");
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ContractId).HasColumnName("ContractID");

            entity.HasOne(d => d.Contract).WithMany(p => p.Payments)
                .HasForeignKey(d => d.ContractId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Payments__Contra__6D0D32F4");
        });

        modelBuilder.Entity<Software>(entity =>
        {
            entity.HasKey(e => e.SoftwareId).HasName("PK__Software__25EDB8DCBA738EA9");

            entity.ToTable("Software");

            entity.Property(e => e.SoftwareId).HasColumnName("SoftwareID");
            entity.Property(e => e.Category).HasMaxLength(50);
            entity.Property(e => e.CurrentVersion).HasMaxLength(20);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
