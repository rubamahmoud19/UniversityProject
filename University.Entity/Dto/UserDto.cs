
using System.ComponentModel.DataAnnotations;
using University.Entity.Enums;

namespace University.Entity.Dto
{
    public class UserDto
    {
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public int? UniversityId { get; set; }
        [Required]
        public UserType UserType { get; set; }
    }
}
