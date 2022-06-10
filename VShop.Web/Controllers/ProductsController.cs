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
      ViewBag.AllCategories = await GetAllCategoriesList();
      return View();
    }
    [HttpPost]
    public async Task<IActionResult> CreateProduct(ProductViewModel productVM)
    {
      if (ModelState.IsValid)
      {
        var result = await _serviceProduct.CreateProduct(productVM);
        if (result is not null) return RedirectToAction(nameof(Index));
      }
      else
      {
        ViewBag.AllCategories = await GetAllCategoriesList();
      }
      return View(productVM);
    }
    [HttpGet]
    public async Task<IActionResult> UpdateProduct(int id)
    {
      ViewBag.AllCategories = await GetAllCategoriesList();
      var result = await _serviceProduct.FindProductById(id);
      if (result is null) return View("Error");
      return View(result);
    }
    [HttpPost]
    public async Task<ActionResult> UpdateProduct(ProductViewModel productVM)
    {
      if (ModelState.IsValid)
      {
        var result = await _serviceProduct.UpdateProduct(productVM);
        if (result is not null)
        {
          return RedirectToAction(nameof(Index));
        }
      }
      ViewBag.AllCategories = await GetAllCategoriesList();
      return View(productVM);
    }
    [HttpGet]
    public async Task<ActionResult> DeleteProduct(int id)
    {
      var product = await _serviceProduct.FindProductById(id);
      if (product is not null) return View(product);
      return View("Error");
    }
    [HttpPost, ActionName("DeleteProduct")]
    public async Task<ActionResult> DeleteProductPost(int id)
    {
      var result = await _serviceProduct.Delete(id);
      if (result) return RedirectToAction(nameof(Index));
      return View("Error");
    }
    private async Task<SelectList> GetAllCategoriesList()
    {
      return new SelectList(await _serviceCategory.GetAllCategories(), "Id", "Name");
    }
  }
}
