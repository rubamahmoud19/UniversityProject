using University.Entity.Dto;
using AutoMapper;
using University.Entity.Entities;
using University.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

using University.Entity.Enums;
using University.Services.Services;
//using Microsoft.Data.qlite;

namespace University.Services;


public class CourseService : ICourseService
{
  private readonly UniversityDbContext _context;
  private readonly IMapper _mapper;
  private readonly UserService _userService;

  public CourseService(UniversityDbContext context, AutoMapper.IMapper mapper, UserService userService)
  {
    _context = context;
    _mapper = mapper;
    _userService = userService;
  }

  public  string CreateCourse(CourseDto model)
  {
        // take the user university 
        var user = _userService.GetUser();
        var course = new Course
        {
            CourseName = model.CourseName,
            UniversityId = user.UniversityId
        };
        _context.Courses.Add(course);
    try
    {
      int result = _context.SaveChanges();
      return "success";
    }
    catch (Exception ex)
    {
            if (ex.InnerException is SqlException sqlEx && sqlEx.Number == 2601)
            {
                return "Course name has already been taken";
            }
            return $"{ex.Message}";
        }
    }

  public string DeleteCourse(Course course)
  {
    _context.Remove(course);
    int changesSaved = _context.SaveChanges();
    if (changesSaved > 0)
    {
      return "deleted";
    }
    else
    {
      return "An error occurred while saving the changes";
    }
  }

  public async Task<string> UpdateCourse(int id, CourseDto updatedCourseDto)
  {
    var existingCourse = await _context.Courses.FindAsync(id);
    if (existingCourse != null)
    {
      existingCourse.CourseName = updatedCourseDto.CourseName;
      try
      {
        int result = _context.SaveChanges();
        return "success";
      }
      catch (Exception ex)
      {
        return $"{ex.Message}";
      }
    }else{
      return "Not Found";
    }

  }

  public CourseDto GetCourse(int id)
  {
    Course? course = _context.Courses.Include(c => c.Users).FirstOrDefault(c => c.Id == id);
    var courseDto = _mapper.Map<CourseDto>(course);
    return courseDto;
  }

    public IQueryable<Course> GetCourses(string searchString)
  {
        var user = _userService.GetUser();
        var courses = from c in _context.Courses select c;
        courses = courses.Where(c => c.UniversityId == user.UniversityId);

        if (!string.IsNullOrEmpty(searchString))
        {
            courses = courses.Where(c => c.CourseName.Contains(searchString));
        }

        return courses;
    }

  public Course GetCourseWithStudents(int id)
  {
    var course = _context.Courses.Include(c => c.Users.Where(u => u.UserType == UserType.Student)).FirstOrDefault(c => c.Id == id);
    return course;
  }

}