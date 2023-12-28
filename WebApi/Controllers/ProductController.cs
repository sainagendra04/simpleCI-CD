using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Model;
using WebApi.Repository;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _repository;
        public ProductController(IProductRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAllProducts()
        {
            try
            {
                var allProducts = _repository.GetAllProducts();
                return Ok(allProducts);
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
            
        }

        [HttpGet("{productId}")]
        public IActionResult GetProductById(int productId)
        {
            try
            {
                var product = _repository.GetProductById(productId);
                if (product == null)
                    return NotFound($"Product with ID {productId} not found.");
                return Ok(product);
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
           
        }

        [HttpGet("byname/{productName}")]
        public IActionResult GetProductByName(string productName)
        {
            try
            {
                var products = _repository.GetProductByName(productName);
                if (products.Count != 0)
                    return Ok(products);
                return NotFound($"Product with Name {productName} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult AddProduct([FromBody] Product product)
        {
            try
            {
                if (product == null)
                    return BadRequest("Invalid product data.");
                _repository.AddProduct(product);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }

        }

        [HttpPut("{productId}")]
        public IActionResult UpdateProduct(int productId, [FromBody] Product updatedProduct)
        {
            try
            {
                var existingProduct = _repository.GetProductById(productId);

                if (existingProduct == null)
                {
                    return NotFound($"Product with ID {productId} not found.");
                }

                _repository.UpdateProduct(productId, updatedProduct);
                return Ok(updatedProduct);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }

        }

        [HttpDelete]
        public IActionResult DeleteProduct(int productId)
        {
            try
            {
                var existingProduct = _repository.GetProductById(productId);

                if (existingProduct == null)
                {
                    return NotFound($"Product with ID {productId} not found.");
                }

                _repository.DeleteProduct(productId);
                return Ok(productId + "deleted succesfully!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }
    }
}
