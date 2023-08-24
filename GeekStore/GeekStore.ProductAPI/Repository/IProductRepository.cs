using GeekStore.ProductAPI.Data.VO;

namespace GeekStore.ProductAPI.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductVO>>  GetAll();

        Task<ProductVO>  GetId(long Id);

        Task<ProductVO>  Create(ProductVO VO);

        Task<ProductVO>  Update(ProductVO VO);

        Task<bool> Delete(long Id);

    }
}
