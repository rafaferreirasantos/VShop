using System.Text;
using System.Text.Json;
using VShop.Web.Models;
using VShop.Web.Services.Interfaces;

namespace VShop.Web.Services;

public class ProductService : IProductService
{
  private readonly IHttpClientFactory _clientFactory;
  private const string apiEndPoint = "/api/products";
  private readonly JsonSerializerOptions _options;
  private ProductViewModel? productViewModel;
  private IEnumerable<ProductViewModel?> productsViewModel;

  public ProductService(IHttpClientFactory clientFactory)
  {
    _clientFactory = clientFactory;
    _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
  }

  public async Task<IEnumerable<ProductViewModel?>?> GetAllProducts()
  {
    var client = _clientFactory.CreateClient("ProductAPI");
    using (var response = await client.GetAsync(apiEndPoint))
    {
      if (!response.IsSuccessStatusCode) return null;

      var apiResponse = await response.Content.ReadAsStreamAsync();
      productsViewModel = await JsonSerializer.DeserializeAsync<IEnumerable<ProductViewModel>>(apiResponse, _options);
      return productsViewModel!;
    }

  }
  public async Task<ProductViewModel?> FindProductById(int id)
  {
    var client = _clientFactory.CreateClient("ProductAPI");
    using (var response = await client.GetAsync($"{apiEndPoint}{id}"))
    {
      if (!response.IsSuccessStatusCode) return null;

      var apiResponse = await response.Content.ReadAsStreamAsync();
      productViewModel = await JsonSerializer.DeserializeAsync<ProductViewModel>(apiResponse, _options);
      return productViewModel!;
    }
  }
  public async Task<ProductViewModel?> CreateProduct(ProductViewModel productViewModel)
  {
    var client = _clientFactory.CreateClient("ProductAPI");
    StringContent content = new StringContent(JsonSerializer.Serialize(productViewModel), Encoding.UTF8, "application/json");
    using (var response = await client.PostAsync(apiEndPoint, content))
    {
      if (!response.IsSuccessStatusCode) return null;

      var apiResponse = await response.Content.ReadAsStreamAsync();
      productViewModel = await JsonSerializer.DeserializeAsync<ProductViewModel>(apiResponse, _options);
      return productViewModel;
    }
  }
  public async Task<ProductViewModel?> UpdateProduct(ProductViewModel productViewModel)
  {
    var client = _clientFactory.CreateClient("ProductAPI");
    StringContent content = new StringContent(JsonSerializer.Serialize(productViewModel), Encoding.UTF8, "application/json");
    using (var response = await client.PutAsync(apiEndPoint, content))
    {
      if (!response.IsSuccessStatusCode) return null;

      var apiResponse = await response.Content.ReadAsStreamAsync();
      productViewModel = await JsonSerializer.DeserializeAsync<ProductViewModel>(apiResponse, _options);
      return productViewModel;
    }
  }
  public async Task<bool> Delete(int id)
  {
    var client = _clientFactory.CreateClient("ProductAPI");
    using (var response = await client.DeleteAsync($"{apiEndPoint}{id}"))
    {
      if (response.IsSuccessStatusCode) return true;
    }
    return false;
  }
}
