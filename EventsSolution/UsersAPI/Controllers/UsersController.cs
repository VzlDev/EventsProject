using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using UsersAPI.Data;
using UsersAPI.DTO;
using UsersAPI.Model;

namespace UsersAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UsersContext _context;
        public UsersController(UsersContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            var users = _context.Users.ToList();
            return users;
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetUser(Guid id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteUser(Guid id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            _context.Users.Remove(user);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPut("{id}")]
        public ActionResult<User> UpdateUser(int id, User user)
        {
            var existingUser = _context.Users.Find(id);
            if (existingUser == null)
            {
                return NotFound();
            }

            existingUser.Email = user.Email;
            existingUser.Userame = user.Userame;
            existingUser.FullName = user.FullName;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpPost("/signup")]
        public ActionResult<User> UserSignUp(User user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            user.Password = Utils.Utils.HashPassword(user.Password);

            _context.Users.Add(user);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPost("/login")]
        public ActionResult<User> UserLogIn(LoginDTO loginDTO)
        {
            if (loginDTO == null)
            {
                return BadRequest();
            }

            var user = _context.Users.FirstOrDefault(u => u.Userame.Equals(loginDTO.UserName));
            if (user == null)
            {
                return NotFound();
            }

            var loginPassword = Utils.Utils.HashPassword(loginDTO.Password);

            if (loginPassword.Equals(user.Password))
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
