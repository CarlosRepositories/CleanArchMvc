using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService ProductService;
        public ProductsController(IProductService productService)
        {
            ProductService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
        {
            var products = await ProductService.GetProductsAsync();

            if (products == null)
                NotFound("Products not found");

            return Ok(products);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDTO>> GetProductById(int id)
        {
            var product = await ProductService.GetByIdAsync(id);

            if (product == null)
                NotFound("Product not found");

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<ProductDTO>> CreateProduct([FromBody] ProductDTO product)
        {
            if (product == null)
                BadRequest();

            await ProductService.CreateAsync(product);

            return new CreatedAtRouteResult("GetCategory", new { id = product.Id }, product);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateProduct(int id, [FromBody] ProductDTO product)
        {
            if (product.Id != id)
                return BadRequest();

            if (product == null)
                return BadRequest("Invalid Data");

            await ProductService.UpdateAsync(product);

            return Ok(product);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ProductDTO>> DeleteProduct(int id)
        {
            var product = ProductService.GetByIdAsync(id);

            if (product == null)
                return NotFound("Product not found");

            await ProductService.RemoveAsync(id);

            return Ok(product);
        }
    }
}
