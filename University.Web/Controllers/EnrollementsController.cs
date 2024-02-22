using Microsoft.AspNetCore.Mvc;


namespace Controllers
{
  public class EnrollementsController : Controller
    {
        private readonly EnrollmentService _enrollmentService;

        public EnrollementsController(EnrollmentService enrollmentService)
        {
          _enrollmentService = enrollmentService;
        }

        public async Task<IActionResult> Enroll(int courseId){
            await _enrollmentService.EnrollCourseAsync(courseId);
            return RedirectToAction("index", "courses");
        }


        public async Task<IActionResult> Disenroll(int courseId){
            await _enrollmentService.DisenrollCourseAsync(courseId);
            return RedirectToAction("index", "courses");
        }

  }
}