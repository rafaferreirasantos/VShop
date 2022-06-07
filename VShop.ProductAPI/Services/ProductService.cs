using AutoMapper;
using VShop.ProductAPI.DTOs;
using VShop.ProductAPI.Models;
using VShop.ProductAPI.Repositories;

namespace VShop.ProductAPI.Services;

public class ProductService : IProductService
{
  private readonly IProductRepository _repo;
  private readonly IMapper _mapper;

  public ProductService(IProductRepository repo, IMapper mapper)
  {
    _repo = repo;
    _mapper = mapper;
  }

  public async Task<IEnumerable<ProductDTO>> GetProducts()
    => _mapper.Map<IEnumerable<ProductDTO>>(await _repo.GetAll());

  public async Task<ProductDTO> GetProductById(int id)
    => _mapper.Map<ProductDTO>(await _repo.GetById(id));

  public async Task AddProduct(ProductDTO productDTO)
  {
    var product = await _repo.Create(_mapper.Map<Product>(productDTO));
    productDTO.Id = product.Id;
  }
  public async Task UpdateProduct(ProductDTO productDTO)
    => await _repo.Update(_mapper.Map<Product>(productDTO));

  public async Task RemoveProduct(int id)
    => await _repo.Delete(id); 
}
