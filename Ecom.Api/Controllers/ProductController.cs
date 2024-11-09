using Ecom.Application.Interfaces;
using Ecom.Domain.Dtos;
using Ecom.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.Api.Controllers
{

    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IProduct _productRepo;

        public ProductController(IProduct product)
        {
            _productRepo = product;

        }


        [AllowAnonymous]

        [HttpGet]
        [Route("api/[controller]/GetAllProducts")]
        public ActionResult<List<Product>> GetAllProducts()
        {
            var products = _productRepo.GetAllProducts();
            return Ok(products);
        }


        [HttpPost]
        [Route("api/[controller]/CreateProduct")]
        public ActionResult CreateProduct(ProductDto product)
        {


            return Ok(_productRepo.CreateProduct(product));
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/[controller]/EditProduct")]

        public ActionResult EditProduct(string id, [FromBody] ProductDto product)
        {


            return Ok(_productRepo.EditProduct(id, product));
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/[controller]/DeleteProduct")]

        public ActionResult DeleteProduct(string id)
        {


            return Ok(_productRepo.DeleteProduct(id));
        }
    }
}
