using University.Data;
using University.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using University.Services;
using University.Services.Services;

public class EnrollmentService : IEnrollmentService
{
    private readonly UniversityDbContext _context;
    private readonly UserService _userService;
    public EnrollmentService(UniversityDbContext context, UserService userService)
    {
        _context = context;
        _userService = userService;
    }

    public async Task EnrollCourseAsync(int courseId)
    {
        var user = _userService.GetUser();
        var course = await _context.Courses.FindAsync(courseId);
        

        if (course == null || user == null)
        {
            throw new ArgumentException("Invalid course or user ID.");
        }
        var courseAlreadyEnrolled = user.Courses?.FirstOrDefault(c => c.Id == courseId);
        if (courseAlreadyEnrolled == null)
        {
            user.Courses ??= new List<Course>();
            user.Courses.Add(course);
        }
        _ = await _context.SaveChangesAsync();
    }

    public async Task DisenrollCourseAsync(int courseId)
    {
        var user = _userService.GetUser();
        var CurrentUser = _context.Users.Include(u => u.Courses).FirstOrDefault(u => u.Id == user.Id);

        var courseToRemove = user.Courses.FirstOrDefault(c => c.Id == courseId);
        user.Courses.Remove(courseToRemove);
          await _context.SaveChangesAsync();

    }
}