using System;
using System.Collections.Generic;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Contexts;

public partial class DataContext : DbContext
{
    public DataContext()
    {
    }

    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Inventory> Inventories { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Property> Properties { get; set; }

    public virtual DbSet<Store> Stores { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
        //optionsBuilder.UseInMemoryDatabase($"{Guid.NewGuid()}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Categori__3214EC076BCEB6D7");

            entity.HasIndex(e => e.CategoryName, "UQ__Categori__8517B2E02BF608C5").IsUnique();

            entity.Property(e => e.CategoryName).HasMaxLength(50);
        });

        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.HasKey(e => new { e.StoreId, e.ProductId }).HasName("PK__Inventor__F0C23D6DC566C86C");

            entity.HasOne(d => d.Product).WithMany(p => p.Inventories)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Inventori__Produ__45F365D3");

            entity.HasOne(d => d.Store).WithMany(p => p.Inventories)
                .HasForeignKey(d => d.StoreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Inventori__Store__44FF419A");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Products__3214EC0708F6CAC1");

            entity.Property(e => e.Price).HasColumnType("money");
            entity.Property(e => e.Title).HasMaxLength(100);

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Products__Catego__3A81B327");

            entity.HasMany(d => d.Properties).WithMany(p => p.Products)
                .UsingEntity<Dictionary<string, object>>(
                    "ProductProperty",
                    r => r.HasOne<Property>().WithMany()
                        .HasForeignKey("PropertyId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__ProductPr__Prope__403A8C7D"),
                    l => l.HasOne<Product>().WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__ProductPr__Produ__3F466844"),
                    j =>
                    {
                        j.HasKey("ProductId", "PropertyId").HasName("PK__ProductP__F3005CBEA211E04B");
                        j.ToTable("ProductProperties");
                    });
        });

        modelBuilder.Entity<Property>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Properti__3214EC079168D48A");

            entity.Property(e => e.PropertyValue).HasMaxLength(100);
            entity.Property(e => e.Title).HasMaxLength(100);
        });

        modelBuilder.Entity<Store>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Stores__3214EC0766E3F89E");

            entity.Property(e => e.StoreName).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
