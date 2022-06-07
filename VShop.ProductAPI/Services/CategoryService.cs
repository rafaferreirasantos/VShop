using AutoMapper;
using VShop.ProductAPI.DTOs;
using VShop.ProductAPI.Models;
using VShop.ProductAPI.Repositories;

namespace VShop.ProductAPI.Services;

public class CategoryService : ICategoryService
{
  private readonly ICategoryRepository _repo;
  private readonly IMapper _mapper;

  public CategoryService(ICategoryRepository repo, IMapper mapper)
  {
    _repo = repo;
    _mapper = mapper;
  }

  public async Task<IEnumerable<CategoryDTO>> GetCategories()
    => _mapper.Map<IEnumerable<CategoryDTO>>(await _repo.GetAll());

  public async Task<CategoryDTO> GetCategoryById(int id)
    => _mapper.Map<CategoryDTO>(await _repo.GetById(id));
  
  public async Task<IEnumerable<CategoryDTO>> GetCategoriesProducts()
    => _mapper.Map<IEnumerable<CategoryDTO>>(await _repo.GetCategoriesProducts());
  
  public async Task AddCategory(CategoryDTO categoryDTO)
  {
    var category = await _repo.Create(_mapper.Map<Category>(categoryDTO));
    categoryDTO.Id = category.Id;
  }
  public async Task UpdateCategory(CategoryDTO categoryDTO)
    => await _repo.Update(_mapper.Map<Category>(categoryDTO));
  
  public async Task RemoveCategory(int id)
    => await _repo.Delete(_mapper.Map<Category>(id));
}
