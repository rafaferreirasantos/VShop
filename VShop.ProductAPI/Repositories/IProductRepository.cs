﻿using VShop.ProductAPI.Models;

namespace VShop.ProductAPI.Repositories;

public interface IProductRepository
{
  Task<IEnumerable<Product>> GetAll();
  Task<IEnumerable<Product>> GetCategoriesProducts();
  Task<Product> GetById(int id);
  Task<Product> Create(Product category);
  Task<Product> Update(Product category);
  Task<Product> Delete(Product category);
  Task<Product> Delete(int id);
}
