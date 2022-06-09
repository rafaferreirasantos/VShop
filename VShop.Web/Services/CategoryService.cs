using System.Text.Json;
using VShop.Web.Models;
using VShop.Web.Services.Interfaces;

namespace VShop.Web.Services;

public class CategoryService : ICategoryService
{
  private readonly IHttpClientFactory _clientFactory;
  private const string apiEndPoint = "/api/categories";
  private readonly JsonSerializerOptions _options;
  private IEnumerable<CategoryViewModel?> categoriesViewModel;

  public CategoryService(IHttpClientFactory clientFactory)
  {
    _clientFactory = clientFactory;
    _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
  }
  public async Task<IEnumerable<CategoryViewModel?>?>? GetAllCategories()
  {
    var client = _clientFactory.CreateClient("ProductAPI");
    using (var response = await client.GetAsync(apiEndPoint))
    {
      if (!response.IsSuccessStatusCode) return null;
      var apiResponse = await response.Content.ReadAsStreamAsync();
      categoriesViewModel = await JsonSerializer.DeserializeAsync<IEnumerable<CategoryViewModel>>(apiResponse, _options);
      return categoriesViewModel;
    }
  }
}
