using Ecom.Application.Interfaces;
using Ecom.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ecom.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICart _cartRepository;

        public CartController(ICart cartRepository)
        {
            _cartRepository = cartRepository;
        }


        [HttpGet]
        [Route("api/[controller]/GetUserCart")]
        public async Task<ActionResult<Cart>> GetUserCart() 
        {
            var useremail = GetUserEmailFromToken(HttpContext);

            if (useremail == null) 
            {
            
            return Unauthorized();

                    }

            var cart = _cartRepository.GetCartByUserEmailAsync(useremail);

            if (cart == null)
            {
                cart = new Cart { UserEmail = useremail };
                await _cartRepository.CreateCartAsync(cart);
            }

            return Ok(cart);

        }





        private string? GetUserEmailFromToken(HttpContext httpContext)
        {
            return httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        }
    }
}
