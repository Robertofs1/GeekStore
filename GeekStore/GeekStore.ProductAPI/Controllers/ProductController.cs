using GeekStore.ProductAPI.Data.VO;
using GeekStore.ProductAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GeekStore.ProductAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private IProductRepository _repository;
        public ProductController(IProductRepository repository)
        {
         _repository = repository ?? 
                throw new ArgumentNullException(nameof(repository));

        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductVO>>> GetAll()
        {
            var products = await _repository.GetAll();
            return Ok(products);
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<ProductVO>> GetId(long Id)
        {
           var product = await _repository.GetId(Id);
            if (product.Id <= 0) return NotFound();
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<ProductVO>> Create(ProductVO VO)
        {
            if (VO == null) return BadRequest();

            var product = await _repository.Create(VO);
            return Ok(product);
        }

        [HttpPut]
        public async Task<ActionResult<ProductVO>> Update(ProductVO VO)
        {
            if (VO == null) return BadRequest();

            var product = await _repository.Update(VO);
            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long Id)
        {
            var status = await _repository.Delete(Id);
            if (!status) return BadRequest();
            return Ok();
        }
    }
}
