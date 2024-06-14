using System.ComponentModel.DataAnnotations;
using UsersAPI.DTO;

namespace UsersAPI.Model
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string FullName { get; set; }

        public UserRole Role { get; set; }

        public User(UserDTO userDto)
        {
            this.UserId = Guid.NewGuid();
            this.Username = userDto.Username;
            this.Email = userDto.Email;
            this.Password = Utils.Utils.HashPassword(userDto.Password);
            this.FullName = userDto.FullName;
            this.Role = userDto.Role;
        }

        public User() { }
    }

    public enum UserRole
    {
        Admin,
        Member
    }

}
