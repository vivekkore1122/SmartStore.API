using Microsoft.AspNetCore.Mvc;
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
        public IActionResult GetallProduct()
        {
            var products = productRepository.GetAll();
            return Ok(products);

        }
    }
}

