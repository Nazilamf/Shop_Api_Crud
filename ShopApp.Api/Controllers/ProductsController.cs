using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopApi.Data;
using ShopApp.Api.Dtos.ProductDtos;
using ShopApp.Core.Entities;
using ShopApp.Core.Repositories;

namespace ShopApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IBrandRepository _brandRepository;

        public ProductsController(IProductRepository productRepository, IBrandRepository brandRepository)
        {
            _productRepository=productRepository;
            _brandRepository=brandRepository;
        }
        [HttpPost("")]
        public IActionResult Create(ProductCreateDto productDto)
        {
            if (!_brandRepository.IsExist(x => x.Id == productDto.BrandId))
                ModelState.AddModelError("BrandId", $"Brand Not Found by Id {productDto.BrandId}");


            return BadRequest(ModelState);

            Product product = new Product
            {
                BrandId = productDto.BrandId,
                Name = productDto.Name,
                SalePrice= productDto.SalePrice,
                CostPrice= productDto.CostPrice,
                CreatedAt= DateTime.UtcNow.AddHours(4),
                ModifiedAt= DateTime.UtcNow.AddHours(4),

            };

            _productRepository.Add(product);
            _productRepository.Commit();

            return StatusCode(201, new { id = product.Id });
        }

        [HttpGet("{id}")]
        public ActionResult<ProductGetDto> Get(int id)
        {
            Product product = _productRepository.Get(x => x.Id == id, "Brand");
            if (product== null) return NotFound();

            ProductGetDto productDto = new ProductGetDto()
            {
                Name= product.Name,
                CostPrice = product.CostPrice,
                SalePrice= product.SalePrice,
                brand = new BrandInProductGetDto
                {
                    Id = product.Id,
                    Name = product.Brand.Name
                }
            };
            return Ok(productDto);

        }

        [HttpGet("all")]
        public ActionResult<List<ProductGetAllItemDto>> GetAll()
        {
            var productDtos = _productRepository.GetQueryable(x => true).Select(x => new ProductGetAllItemDto
            {
                Id= x.Id,
                Name= x.Name,
                BrandName= x.Brand.Name,
            }).ToList();
            return Ok(productDtos);
        }
        [HttpDelete("{id}")]
        public IActionResult Edit(int id)
        {
            var product = _productRepository.Get(x => x.Id == id);

            if (product ==null)
                return NotFound();

            _productRepository.Remove(product);
            _productRepository.Commit();

            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult Edit(int id, ProductEditDto productDto)
        {
            Product product = _productRepository.Get(x => x.Id == id);

            if (product==null)
                return NotFound();

            if (product.BrandId!=productDto.BrandId && !_productRepository.IsExist(x => x.Id ==productDto.BrandId)) return NotFound();

            if (product.Name !=productDto.Name && _productRepository.IsExist(x => x.Name ==productDto.Name))

                ModelState.AddModelError("Name", "Name is already taken");
            return BadRequest(ModelState);

            product.Name = productDto.Name;
            product.CostPrice = productDto.CostPrice;
            product.SalePrice= productDto.SalePrice;
            product.BrandId= productDto.BrandId;

            _productRepository.Commit();

            return NoContent();




        }



    }
}
