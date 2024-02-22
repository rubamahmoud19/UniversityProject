
using University.Data;
using University.Entity.Dto;
using University.Entity.Entities;
using System.Web;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Azure.Core;
using University.Web;
using Microsoft.EntityFrameworkCore;

namespace University.Services.Services
{
    public class UserService : IUserService
    {
        private readonly UniversityDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenitcationService _authenitcation;

        public UserService(UniversityDbContext context, IHttpContextAccessor httpContextAccessor, AuthenitcationService authenitcation)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _authenitcation = authenitcation; 
        }
        public string CreateUser(UserDto model)
        {
            User user = new User {      
                Username = model.Username,
                Password = model.Password,
                UniversityId = model.UniversityId.Value,
                UserType = model.UserType
            };

            try
            {
                _context.Users.Add(user);
                int result = _context.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public User GetUser()
        {
            var context = _httpContextAccessor.HttpContext;
            var jwtCookie = context.Request.Cookies["jwt"];
            var username = _authenitcation.ValidateToken(jwtCookie);
            User user = _context.Users.FirstOrDefault(x => x.Username == username);
            return user;
        }

        public  int[] FetchCoursesId()
        {
            User user = GetUser();
            var courses = _context.Users.Include(u => u.Courses).FirstOrDefault(u => u.Id == user.Id).Courses.Select(c => c.Id).ToArray();
            return courses;
        }

        private bool IsUserDtoValid(UserDto model)
        {
            return Validator.TryValidateObject(model, new ValidationContext(model), null, true);
        }

        public ICollection<Course> FetchCourses()
        {
            User user = GetUser();
            var courses = _context.Users.Include(u => u.Courses).FirstOrDefault(u => u.Id == user.Id).Courses;
            return courses;
        }
    }
}
