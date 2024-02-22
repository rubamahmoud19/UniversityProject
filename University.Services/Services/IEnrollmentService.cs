namespace University.Services;

public interface IEnrollmentService
{
  public Task EnrollCourseAsync(int courseId);
  public Task DisenrollCourseAsync(int courseId);
}