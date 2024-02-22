using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Entity.Dto;
using University.Entity.Entities;

namespace University.Services.Services
{
    internal interface IUserService
    {
       public string CreateUser(UserDto userDto);
       public User GetUser();

        public int[] FetchCoursesId();

        public ICollection<Course> FetchCourses();

    }
}
