using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VShop.ProductAPI.DTOs;
using VShop.ProductAPI.Services;

namespace VShop.ProductAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
  private readonly ICategoryService _service;

  public CategoriesController(ICategoryService service)
  {
    _service = service;
  }
  [HttpGet]
  public async Task<ActionResult<IEnumerable<CategoryDTO>>> Get()
  {
    var categories = await _service.GetCategories();
    if (categories is null) return NotFound("Categories not found");
    return Ok(categories);
  }
  [HttpGet("{id:int}", Name = "GetCategory")]
  public async Task<ActionResult<CategoryDTO>> Get(int id)
  {
    var category = await _service.GetCategoryById(id);
    if (category is null) return NotFound("Category not found");
    return Ok(category);
  }
  [HttpGet("products")]
  public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategoriesProducts()
  {
    var categories = await _service.GetCategoriesProducts();
    if (categories is null) return NotFound("Categories not found");
    return Ok(categories);
  }
  [HttpPost]
  public async Task<ActionResult> Post([FromBody] CategoryDTO categoryDTO)
  {
    if (categoryDTO is null) return BadRequest("Invalid data");
    await _service.AddCategory(categoryDTO);
    return new CreatedAtRouteResult("GetCategory", new { id = categoryDTO.Id }, categoryDTO);
  }
  [HttpPut("{id:int}")]
  public async Task<ActionResult> Put(int id, [FromBody] CategoryDTO categoryDTO)
  {
    if (categoryDTO is null || id != categoryDTO.Id) return BadRequest("Invalid data");
    await _service.UpdateCategory(categoryDTO);
    return Ok(categoryDTO);
  }
  [HttpDelete("{id:int}")]
  public async Task<ActionResult> Delete(int id)
  {
    var category = await _service.GetCategoryById(id);
    if (category is null) return NoContent();
    await _service.RemoveCategory(id);
    return Ok(category);
  }
}