using UsersAPI.Model;

namespace UsersAPI.DTO
{
    public class UserDTO
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string FullName { get; set; }

        public UserRole Role { get; set; }
    }
}
