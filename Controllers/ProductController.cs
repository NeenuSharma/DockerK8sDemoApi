using DockerK8sDemoApi.Data;
using DockerK8sDemoApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace DockerK8sDemoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private  readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult GetProducts(int id)
        {
            var products = _productService.GetProducts(id);
            return Ok(products);
        }

        [HttpPost]
        public IActionResult CreateProduct([FromBody] Products product)
        {
            var newProduct = _productService.CreateProduct(product);
            return Ok();
        }
    }
}
