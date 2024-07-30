using Cache_asidePattern.Entities;
using Cache_asidePattern.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cache_asidePattern.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("{productId}")]
        public IActionResult Get(int productId)
        {
            var productDetails = _productService.GetProductDetails(productId);
            return Ok(productDetails);
        }

        [HttpPut]
        public IActionResult Update(Product product)
        {
            _productService.UpdateProductDetails(product);
            return NoContent();
        }
    }
}
