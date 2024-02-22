using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace University.Entity.Entities
{
    public class Course
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string? CourseName { get; set; }
        public virtual ICollection<User>? Users { get; set; }

        public int UniversityId { get; set; }
        public virtual Unversity University { get; set; }
    }
}
