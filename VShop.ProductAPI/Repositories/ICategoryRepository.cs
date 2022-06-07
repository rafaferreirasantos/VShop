using VShop.ProductAPI.Models;

namespace VShop.ProductAPI.Repositories;

public interface ICategoryRepository
{
  Task<IEnumerable<Category>> GetAll();
  Task<IEnumerable<Category>> GetCategoriesProducts();
  Task<Category> GetById(int id);
  Task<Category> GetByName(string name);
  Task<Category> Create(Category category);
  Task<Category> Update(Category category);
  Task<Category> Delete(Category category);
  Task<Category> Delete(int id);
}
