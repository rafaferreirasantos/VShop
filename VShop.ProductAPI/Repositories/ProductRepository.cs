using Microsoft.EntityFrameworkCore;
using VShop.ProductAPI.Context;
using VShop.ProductAPI.Models;

namespace VShop.ProductAPI.Repositories;

public class ProductRepository : IProductRepository
{
  private AppDBContext _context;

  public ProductRepository(AppDBContext context)
  {
    _context = context;
  }

  public async Task<IEnumerable<Product>> GetAll()
    => await _context.Products!.Include(x => x.Category).ToListAsync();

  public async Task<Product?> GetById(int id)
    => await _context.Products!.Where(x => x.Id == id).FirstOrDefaultAsync();

  public async Task<Product> Create(Product product)
  {
    _context.Products!.Add(product);
    try
    {
      await _context.SaveChangesAsync();
    }
    catch (Exception ex)
    {
      Console.WriteLine(ex.Message);
      throw;
    }
    return product;
  }
  public async Task<Product> Update(Product product)
  {
    _context.Entry(product).State = EntityState.Modified;
    await _context.SaveChangesAsync();
    return product;
  }

  public async Task<Product> Delete(Product product)
  {
    _context.Products!.Remove(product);
    await _context.SaveChangesAsync();
    return product;
  }

  public async Task<Product?> Delete(int id)
  {
    var product = await GetById(id);
    _context.Products!.Remove(product!);
    await _context.SaveChangesAsync();
    return product;
  }
}
