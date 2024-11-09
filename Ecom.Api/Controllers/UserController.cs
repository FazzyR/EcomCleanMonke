using Ecom.Application.Interfaces;
using Ecom.Domain.Entities;
using Ecom.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.Api.Controllers
{
        [Route("api/[controller]")]
         [ApiController]
    public class UserController : ControllerBase
    {

        public readonly IUser _userRepository;
        public UserController(IUser userRepository)
        {
            _userRepository = userRepository; 
        }


        [AllowAnonymous]
        [Route("authenticate")]
        [HttpPost]


        public ActionResult Login([FromBody] User user)
        {
            var token= _userRepository.Authenticate(user.Email, user.PasswordHash);

            if (token == null) 
            {
                return Unauthorized();
            }

            return Ok(new { token, user });

        }

        [HttpPost]
        [Route("assign-role")]
        public ActionResult AssignRole(string email, string role)
        {
            var user = _userRepository.GetUsers().FirstOrDefault(u => u.Email == email);
            if (user == null)
            {
                return NotFound("User not found");
            }

            if (!user.Roles.Contains(role))
            {
                user.Roles.Add(role);
                _userRepository.UpdateUser(user);
            }

            return Ok("Role assigned successfully");
        }



        [AllowAnonymous]
        [Route("Register")]
        [HttpPost]

        public ActionResult Register([FromBody] User user)
        {
            _userRepository.CreateUser(user);

            var token = _userRepository.Authenticate(user.Email, user.PasswordHash);

            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(new { token, user });

        }


        [HttpGet]   


        public ActionResult<List<User>> GetUsers() 
        {


            return _userRepository.GetUsers();


        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("admin-data")]
        public ActionResult GetAdminData()
        {
            return Ok("This is data only accessible by admins");
        }


    }
}
