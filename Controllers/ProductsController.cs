
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
        private IDapperProductRepository dapperProductRepository;
        private INHibernateProductRepository nHibernateProductRepository;
        private ICategoryRepository categoryRepository;
        private ISupplierRepository supplierRepository;


        public ProductController(IProductRepository productRepository, IDapperProductRepository dapperProductRepository, INHibernateProductRepository nHibernateProductRepository, ICategoryRepository categoryRepository,
    ISupplierRepository supplierRepository)
        {
            this.productRepository = productRepository;
            this.dapperProductRepository = dapperProductRepository;
            this.nHibernateProductRepository = nHibernateProductRepository;
            this.categoryRepository = categoryRepository;
            this.supplierRepository = supplierRepository;


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
            var category = await categoryRepository
                .GetByIdAsync(createProductDto.CategoryId);

            if (category == null)
            {
                return BadRequest("Invalid CategoryId.");
            }

            var supplier = await supplierRepository
                .GetByIdAsync(createProductDto.SupplierId);

            if (supplier == null)
            {
                return BadRequest("Invalid SupplierId.");
            }

            var product = new Product
            {
                Name = createProductDto.Name,
                ProductCode = createProductDto.ProductCode,
                Category = category,
                Supplier = supplier,
                Price = createProductDto.Price,
                Quantity = createProductDto.Quantity
            };

            product = await productRepository.CreateAsync(product);

            return CreatedAtAction(
                nameof(GetProductById),
                new { id = product.Id },
                product);
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProduct(
            int id,
            UpdateProductDto updateProductDto)
        {
            var category = await categoryRepository
                .GetByIdAsync(updateProductDto.CategoryId);

            if (category == null)
            {
                return BadRequest("Invalid CategoryId.");
            }

            var supplier = await supplierRepository
                .GetByIdAsync(updateProductDto.SupplierId);

            if (supplier == null)
            {
                return BadRequest("Invalid SupplierId.");
            }

            var product = new Product
            {
                Id = id,
                Name = updateProductDto.Name,
                ProductCode = updateProductDto.ProductCode,
                Category = category,
                Supplier = supplier,
                Price = updateProductDto.Price,
                Quantity = updateProductDto.Quantity
            };

            var updatedProduct =
                await productRepository.UpdateAsync(product);

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

        [HttpGet("Dapper")]

        public async Task<IActionResult> GetAllProductsUsingDapper()
        {
            var products = await dapperProductRepository.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("Dapper/{id:int}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var product = await dapperProductRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpGet("NHibernate")]
        public async Task<IActionResult> GetProductsUsingNHibernate()
        {
            var products = await nHibernateProductRepository.GetAllAsync();

            return Ok(products);

        }

        [HttpGet("NHibernate/{id:int}")]
        public async Task<IActionResult> GetProductsUsingNHibernateById(int id)
        {
            var product = await nHibernateProductRepository.GetByIdAsync(id);
            return Ok(product);
        }
    }

    
}

