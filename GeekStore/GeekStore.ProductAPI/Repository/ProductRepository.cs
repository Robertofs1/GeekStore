using AutoMapper;
using GeekStore.ProductAPI.Data.VO;
using GeekStore.ProductAPI.Model;
using GeekStore.ProductAPI.Model.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace GeekStore.ProductAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly MySQLContext _context;
        private IMapper _mapper;

        public ProductRepository(MySQLContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
      
        public async Task<IEnumerable<ProductVO>> GetAll()
        {
            List<Product> products = await _context.Products.ToListAsync();
            return _mapper.Map<List<ProductVO>>(products);
        }

        public async Task<ProductVO> GetId(long Id)
        {
            Product product = await _context.Products.
                Where(p => p.Id == Id).FirstOrDefaultAsync() ?? new Product();
            return _mapper.Map<ProductVO>(product);
        }

        public async Task<ProductVO> Create(ProductVO VO)
        {
            Product product = _mapper.Map<Product>(VO);
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductVO>(product);

        }

        public async Task<ProductVO> Update(ProductVO VO)
        {
            Product product = _mapper.Map<Product>(VO);
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductVO>(product);
        }

        public async Task<bool> Delete(long Id)
        {
            try
            {
                Product product = await _context.Products.
                Where(p => p.Id == Id).FirstOrDefaultAsync() ?? new Product();

                if (product.Id <= 0) return false;
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                return true;
                
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
