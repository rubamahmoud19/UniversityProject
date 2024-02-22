using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Entity.Entities;

namespace University.Entity.Dto
{
    public class CourseDto
    {
        public int Id { get; set; }
        [Required]
        public string? CourseName { get; set; }

        public Unversity? University { get; set; }
    }
}
