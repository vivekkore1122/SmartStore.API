using Microsoft.AspNetCore.Mvc;
using SmartStore.API.Models.Domain;
using SmartStore.API.Models.DTO;
using SmartStore.API.Repository.Interfaces;

namespace SmartStore.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductRepository productRepository;

        public ProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;

        }

        [HttpGet]
        public async Task<IActionResult> GetallProduct()
        {
            var products = await productRepository.GetAllAsync();
            return Ok(products);

        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await productRepository.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto)
        {
            var product = new Product
            {
                Name = createProductDto.Name,
                ProductCode = createProductDto.ProductCode,
                CategoryId = createProductDto.CategoryId,
                SupplierId = createProductDto.SupplierId,
                Price = createProductDto.Price,
                Quantity = createProductDto.Quantity
            };

            product = await productRepository.CreateAsync(product); ;

            return CreatedAtAction(
                nameof(GetProductById),
                new { id = product.Id },
            product);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProduct(int id, UpdateProductDto updateProductDto)
        {
            var product = new Product
            {
                Id = id,
                Name = updateProductDto.Name,
                ProductCode = updateProductDto.ProductCode,
                CategoryId = updateProductDto.CategoryId,
                SupplierId = updateProductDto.SupplierId,
                Price = updateProductDto.Price,
                Quantity = updateProductDto.Quantity
            };

            var updatedProduct = await productRepository.UpdateAsync(product);

            if (updatedProduct == null)
            {
                return NotFound();
            }

            return Ok(updatedProduct);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var deletedProduct = await productRepository.DeleteAsync(id);

            if (deletedProduct == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}

