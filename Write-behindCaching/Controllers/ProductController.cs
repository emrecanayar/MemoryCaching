using Microsoft.AspNetCore.Mvc;
using Write_behindCaching.Entities;
using Write_behindCaching.Services;

namespace Write_behindCaching.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ICacheService _cacheService;

        public ProductController(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        [HttpPost]
        public IActionResult AddOrUpdateProduct(Product product)
        {
            _cacheService.AddOrUpdateProduct(product);
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            var product = _cacheService.GetProduct(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
    }
}
