using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingWeb.Shared.DTOs;
using ShoppingWeb.Shared.Interfaces;

namespace ShoppingWeb.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductService productService, ILogger<ProductsController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            if (products == null || !products.Any())
            {
                return NotFound(new { Message = "No products available." });
            }
            return Ok(products);
        }



        [HttpGet("{id}")]
        public ActionResult<ProductDto> GetProductById(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null)
            {
                _logger.LogWarning($"Product with id {id} not found.");
                return NotFound(new { Message = "No products available in current product id." });
            }
            return Ok(product);
        }

        [HttpPost]
        public ActionResult AddProduct(ProductDto productDto)
        {
            _productService.AddProduct(productDto);
            return CreatedAtAction(nameof(GetProductById), new { id = productDto.Id }, productDto);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateProduct(int id, ProductDto productDto)
        {
            var existingProduct = _productService.GetProductById(id);
            if (existingProduct == null)
            {
                _logger.LogWarning($"Product with id {id} Can't update.");
                return NotFound(new { Message = "Can not update product details." });
            }

            _productService.UpdateProduct(id, productDto);
            return Ok(new { Message = "Product successfully modified." });
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteProduct(int id)
        {
            var existingProduct = _productService.GetProductById(id);
            if (existingProduct == null)
            {
                _logger.LogWarning($"Product with id {id} can't delete..");
                return NotFound(new { Message = "Can not delete product" });
            }

            _productService.DeleteProduct(id);
            return Ok(new { Message = "Product successfully deleted." });
        }
    }
}
