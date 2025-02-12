using Newtonsoft.Json;
using MVC.Models;
using System.Net.Http.Headers;
using System.Text;

namespace MVC.Services
{
    public class ProductService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;

        public ProductService(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _apiBaseUrl = configuration["ApiBaseUrl"];
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            var response = await _httpClient.GetStringAsync(_apiBaseUrl + "products");
            return JsonConvert.DeserializeObject<List<Product>>(response);
        }

        public async Task<Product> GetProductAsync(int id)
        {
            var response = await _httpClient.GetStringAsync(_apiBaseUrl + $"products/{id}");
            return JsonConvert.DeserializeObject<Product>(response);
        }

        public async Task CreateProductAsync(Product product)
        {
            var content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
            await _httpClient.PostAsync(_apiBaseUrl + "products", content);
        }

        public async Task UpdateProductAsync(int id, Product product)
        {
            var content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
            await _httpClient.PutAsync(_apiBaseUrl + $"products/{id}", content);
        }

        public async Task DeleteProductAsync(int id)
        {
            await _httpClient.DeleteAsync(_apiBaseUrl + $"products/{id}");
        }
    }
}
