using Microsoft.EntityFrameworkCore;
using VShop.ProductAPI.Models;

namespace VShop.ProductAPI.Context;

public class AppDBContext : DbContext
{
  public DbSet<Category> Categories { get; set; }
  public DbSet<Product> Products { get; set; }

  public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }
}