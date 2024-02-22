using University.Entity.Entities;
using University.Entity.Dto;

namespace University.Services;

public interface ICourseService
{
    public string CreateCourse(CourseDto course);
    public CourseDto GetCourse(int id);
    public Course GetCourseWithStudents(int id);
    public IQueryable<Course> GetCourses(String searchString);
    public Task<string> UpdateCourse(int id, CourseDto course);
    public string DeleteCourse(Course course);
}