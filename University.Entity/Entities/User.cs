using University.Entity.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace University.Entity.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Name { get; set; }
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public UserType UserType { get; set; }

        public int UniversityId { get; set; }
        public virtual Unversity University { get; set; }

        public virtual ICollection<Course>? Courses { get; set; }
    }
}
