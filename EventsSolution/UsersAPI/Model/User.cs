using System.ComponentModel.DataAnnotations;

namespace UsersAPI.Model
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; }

        public string Userame { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string FullName { get; set; }

        public UserRole Role { get; set; }
    }

    public enum UserRole
    {
        Admin,
        Member
    }

}
