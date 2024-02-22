using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace University.Entity.Entities
{
    public class Unversity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }

        public string? Location { get; set; }

        public ICollection<User>? Users { get; set; }
        public ICollection<Course>? Courses { get; set; }
    }
}
