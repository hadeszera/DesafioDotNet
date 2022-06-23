using DesafioDotNet.DAL;
using DesafioDotNet.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DesafioDotNet.Controller
{
    [Route("api/v1/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductDal productDal;
        public ProductController(IProductDal _productDal)
        {
            productDal = _productDal;
        }
        [HttpGet]
        public IActionResult GetAllProducts() 
        {
            var result = productDal.GetAllProducts();

            return StatusCode(200, result);
        }
        [HttpGet("{id}")]
        public IActionResult GetAllById(int id)
        {
            var result = productDal.GetProduct(id);
            return StatusCode(200, result);
        }
        [HttpPut("updateProduct")]
        public IActionResult UpdateProduct([FromBody]Product product)
        {
            productDal.UpdateProduct(product);
            return StatusCode(200);
        }
        [HttpPost("addProduct")]
        public IActionResult addProduct([FromBody] Product product)
        {
            productDal.AddProduct(product);
            return StatusCode(200);
        }
        [HttpDelete("{id}")]
        public IActionResult deleteProduct(int id)
        {
            productDal.DeleteProduct(id);
            return StatusCode(200);
        }





    }
}
