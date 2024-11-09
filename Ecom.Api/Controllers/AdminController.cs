using Ecom.Application.Interfaces;
using Ecom.Domain.Dtos;
using Ecom.Domain.Entities;
using Ecom.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IProduct _productRepo;

        public AdminController(IProduct product)
        {
            _productRepo = product;
            
        }





    }
}
