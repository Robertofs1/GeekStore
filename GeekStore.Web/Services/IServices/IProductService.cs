using GeekStore.Web.Models;

namespace GeekStore.Web.Services.IServices
{
    public interface IProductService
    {
        Task<IEnumerable<ProductModel>> GetAllProduct();

        Task<ProductModel> GetProductById(long Id);

        Task<ProductModel> CreateProduct(ProductModel model);

        Task<ProductModel> UpdateProduct(ProductModel model);

        Task<bool> DeleteProductById(long Id);
    }
}
