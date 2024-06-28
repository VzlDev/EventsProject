using AuthorizationAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuthorizationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorizationController : ControllerBase
    {
        private readonly AuthorizationService _authService;

        public AuthorizationController(AuthorizationService authService)
        {
            _authService = authService;
        }

        [HttpPost("generateToken")]
        public IActionResult GenerateToken([FromBody] UserLogin userLogin)
        {
            var token = _authService.GenerateJWT(userLogin.Username);
            return Ok(new { Token = token });
        }
    }

    public class UserLogin
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}

