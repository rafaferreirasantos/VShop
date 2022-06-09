using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VShop.Web.Models;
using VShop.Web.Services.Interfaces;

namespace VShop.Web.Controllers
{
  public class ProductsController : Controller
  {
    private readonly IProductService _serviceProduct;
    private readonly ICategoryService _serviceCategory;

    public ProductsController(IProductService serviceProduct, ICategoryService serviceCategory)
    {
      _serviceProduct = serviceProduct;
      _serviceCategory = serviceCategory;
    }

    public async Task<ActionResult<IEnumerable<ProductViewModel>>> Index()
    {
      var result = await _serviceProduct.GetAllProducts();
      if (result == null) return View("Error");
      return View(result);
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryViewModel>>> CreateProduct()
    {
      ViewBag.AllCategories = new SelectList(await _serviceCategory.GetAllCategories(), "Id", "Name");
      return View();
    }
    [HttpPost]
    public async Task<IActionResult> CreateProduct(ProductViewModel productViewModel)
    {
      if (ModelState.IsValid)
      {
        var result = await _serviceProduct.CreateProduct(productViewModel);
        if (result is not null) return RedirectToAction(nameof(Index));
      }
      else
      {
        ViewBag.AllCategories = new SelectList(await _serviceCategory.GetAllCategories(), "Id", "Name");
      }
      return View(productViewModel);
    }
  }
}
