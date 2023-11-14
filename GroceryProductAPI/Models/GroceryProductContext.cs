using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GroceryProductAPI.Models;

public partial class GroceryProductContext : DbContext
{
    public GroceryProductContext()
    {
    }

    public GroceryProductContext(DbContextOptions<GroceryProductContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Ingredient> Ingredients { get; set; }

    public virtual DbSet<Product> Products { get; set; }
   
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Categori__23CAF1D8250A2480");

            entity.Property(e => e.CategoryId).HasColumnName("categoryId");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date")
                .HasColumnName("createdAt");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date")
                .HasColumnName("updatedAt");
        });

        modelBuilder.Entity<Ingredient>(entity =>
        {
            entity.HasKey(e => e.IngredientId).HasName("PK__Ingredie__2753A527FDB4AF4E");

            entity.Property(e => e.IngredientId).HasColumnName("ingredientId");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date")
                .HasColumnName("createdAt");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(512)
                .IsUnicode(false)
                .HasColumnName("imageUrl");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date")
                .HasColumnName("updatedAt");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Upc).HasName("PK__Products__DD77EB4232B34FD4");

            entity.Property(e => e.Upc)
                .HasMaxLength(13)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("upc");
            entity.Property(e => e.Brand)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("brand");
            entity.Property(e => e.CategoryId).HasColumnName("categoryId");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date")
                .HasColumnName("createdAt");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(512)
                .IsUnicode(false)
                .HasColumnName("imageUrl");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.NutrionInfo)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("nutrionInfo");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Unit)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("unit");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Products__catego__0697FACD");

            entity.HasMany(d => d.Ingredients).WithMany(p => p.Products)
                .UsingEntity<Dictionary<string, object>>(
                    "ProductIngredient",
                    r => r.HasOne<Ingredient>().WithMany()
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__ProductIn__ingre__0E391C95"),
                    l => l.HasOne<Product>().WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__ProductIn__produ__0D44F85C"),
                    j =>
                    {
                        j.HasKey("ProductId", "IngredientId").HasName("PK__ProductI__4F65EB3840666E26");
                        j.ToTable("ProductIngredients");
                        j.IndexerProperty<string>("ProductId")
                            .HasMaxLength(13)
                            .IsUnicode(false)
                            .IsFixedLength()
                            .HasColumnName("productId");
                        j.IndexerProperty<int>("IngredientId").HasColumnName("ingredientId");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
