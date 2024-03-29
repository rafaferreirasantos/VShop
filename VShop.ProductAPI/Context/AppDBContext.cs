﻿using Microsoft.EntityFrameworkCore;
using VShop.ProductAPI.Models;

namespace VShop.ProductAPI.Context;

public class AppDBContext : DbContext
{
  public DbSet<Category>? Categories { get; set; }
  public DbSet<Product>? Products { get; set; }

  public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

  protected override void OnModelCreating(ModelBuilder mb)
  {
    //Category
    mb.Entity<Category>().HasKey(c => c.Id);
    mb.Entity<Category>()
      .Property(c => c.Name)
      .HasMaxLength(100)
      .IsRequired();

    //Product
    mb.Entity<Product>().HasKey(p => p.Id);
    mb.Entity<Product>()
      .Property(p => p.Name)
      .HasMaxLength(100)
      .IsRequired();
    mb.Entity<Product>()
          .Property(p => p.Description)
          .HasMaxLength(255)
          .IsRequired();
    mb.Entity<Product>()
          .Property(p => p.ImageURL)
          .HasMaxLength(255)
          .IsRequired();
    mb.Entity<Product>()
      .Property(p => p.Price)
      .HasPrecision(12, 2);

    mb.Entity<Category>()
      .HasMany(g=> g.Products)
      .WithOne(p => p.Category)
      .IsRequired()
      .OnDelete(DeleteBehavior.Cascade);

    //Initial Data
    mb.Entity<Category>()
      .HasData(
        new Category() { Id = 1, Name = "Material Escolar"},
        new Category() { Id = 2, Name = "Acessórios"}
      );
  }
}