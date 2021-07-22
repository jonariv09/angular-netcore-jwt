using angular_netcore_jwt.Models;
using angular_netcore_jwt.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace angular_netcore_jwt.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)    
        {
            var response = _userService.Authenticate(model);

            if (response == null)
                return BadRequest(new {message = "Username or password is incorrect"});
            return Ok(response);
        }

        [Authorize]
        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }

    }
}