using Microsoft.AspNetCore.Mvc;
using VShop.ProductAPI.DTOs;
using VShop.ProductAPI.Services;

namespace VShop.ProductAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ProductsController : ControllerBase
  {
    private readonly IProductService _service;

    public ProductsController(IProductService service)
    {
      _service = service;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDTO>>> Get()
    {
      var products = await _service.GetProducts();
      if (products is null) return NotFound("Products not found");
      return Ok(products);
    }
    [HttpGet("{id:int}", Name = "GetProduct")]
    public async Task<ActionResult<ProductDTO>> GetProduct(int id)
    {
      var product = await _service.GetProductById(id);
      if (product is null) return NotFound("Product not found");
      return Ok(product);
    }
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] ProductDTO productDTO)
    {
      if (productDTO is null) return BadRequest("Invalid data");
      await _service.AddProduct(productDTO);
      return new CreatedAtRouteResult("GetProduct", new { id = productDTO.Id }, productDTO);
    }
    [HttpPut("{id:int}")]
    public async Task<ActionResult> Put(int id, [FromBody] ProductDTO productDTO)
    {
      if (productDTO is null || productDTO.Id != id) return BadRequest("Invalid data");
      await _service.UpdateProduct(productDTO);
      return Ok(productDTO);
    }
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
      var product = await _service.GetProductById(id);
      if (product is null) return NoContent();
      await _service.RemoveProduct(id);
      return Ok(product);
    }
  }
}
