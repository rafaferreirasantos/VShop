using Microsoft.EntityFrameworkCore;
using VShop.ProductAPI.Context;
using VShop.ProductAPI.Models;

namespace VShop.ProductAPI.Repositories;

public class CategoryRepository : ICategoryRepository
{
  private readonly AppDBContext _context;
  public CategoryRepository(AppDBContext context) { _context = context; }
  public async Task<IEnumerable<Category>> GetAll()
    => await _context.Categories!.ToListAsync();

  public async Task<Category> GetById(int id)
    => await _context.Categories!.Where(x => x.Id == id).FirstOrDefaultAsync();

  public async Task<Category> GetByName(string name)
    => await _context.Categories!.Where(x => x.Name == name).FirstOrDefaultAsync();

  public async Task<IEnumerable<Category>> GetCategoriesProducts()
    => await _context.Categories!.Include(x => x.Products).ToListAsync();

  public async Task<Category> Create(Category category)
  {
    _context.Categories!.Add(category);
    await _context.SaveChangesAsync();
    return category;
  }
  public async Task<Category> Update(Category category)
  {
    _context.Entry(category).State = EntityState.Modified;
    await _context.SaveChangesAsync();
    return category;
  }
  public async Task<Category> Delete(Category category)
  {
    _context.Categories!.Remove(category);
    await _context.SaveChangesAsync();
    return category;
  }
  public async Task<Category> Delete(int id)
  {
    var category = await GetById(id);
    _context.Categories!.Remove(category);
    await _context.SaveChangesAsync();
    return category;
  }
}
