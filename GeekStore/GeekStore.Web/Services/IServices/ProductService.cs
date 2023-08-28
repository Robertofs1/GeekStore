using GeekStore.Web.Models;
using GeekStore.Web.Utils;

namespace GeekStore.Web.Services.IServices
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        public const string BasePath = "api/v1/product";
        public ProductService(HttpClient httpclient)
        {
            _httpClient = httpclient ?? throw new ArgumentNullException();
        }
        public async Task<IEnumerable<ProductModel>> GetAllProduct()
        {
            var response = await _httpClient.GetAsync(BasePath);
            return await response.ReadContentAs<List<ProductModel>>();
        }

        public async Task<ProductModel> GetProductById(long Id)
        {
            var response = await _httpClient.GetAsync($"{BasePath}/{Id}");
            return await response.ReadContentAs<ProductModel>();
        }

        public async Task<ProductModel> CreateProduct(ProductModel model)
        {
            var response = await _httpClient.PostAsJson(BasePath, model);
            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<ProductModel>();
            else 
                throw new Exception("something went wrong when calling the api");
        }

        public async Task<ProductModel> UpdateProduct(ProductModel model)
        {
            var response = await _httpClient.PutAsJson(BasePath, model);
            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<ProductModel>();
            else
                throw new Exception("something went wrong when calling the api");
        }

        public async Task<bool> DeleteProductById(long Id)
        {
            var response = await _httpClient.DeleteAsync($"{BasePath}/{Id}");
            if (response.IsSuccessStatusCode)
                return true;
            else
                throw new Exception("something went wrong when calling the api");
        }
    }
}
