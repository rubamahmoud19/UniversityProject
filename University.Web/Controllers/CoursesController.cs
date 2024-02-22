using Microsoft.AspNetCore.Mvc;
using University.Data;
using University.Entity.Entities;
using University.Entity.Dto;
using University.Services;
using AutoMapper;

using University.Web;
using X.PagedList;
using University.Services.Services;


namespace Controllers
{

    public class CoursesController : Controller
    {
        private readonly UniversityDbContext _context;
        private readonly ICourseService _courseService;
        private readonly IMapper _mapper;
        private readonly AuthenitcationService _authenitcationService;
        private readonly UserService _userService;

        public CoursesController(UniversityDbContext context, CourseService courseService, IMapper mapper, AuthenitcationService authenticationService,UserService userService)
        {
            _authenitcationService = authenticationService;
            _context = context;
            _courseService = courseService;
            _mapper = mapper;
            _userService = userService;
        }

        [JwtAuthentication(roles: [1,0])]
        public IActionResult Index(string searchString, int? pageNumber)
        {
            var courses = _courseService.GetCourses(searchString);
            var courseIds = _userService.FetchCoursesId();
            ViewBag.courseIds = courseIds;
            ViewBag.CurrentFilter = searchString;
            var pagedCourses = courses.ToPagedList(pageNumber ?? 1, 1);

            return View(pagedCourses);
        }

        [JwtAuthentication(roles: [1])]
        [HttpGet]
        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CourseDto course)
        {
            if (ModelState.IsValid)
            {
                string result = _courseService.CreateCourse(course);
                if (result == "success")
                {
                    return RedirectToAction("Index", "Courses");
                }
                else
                {
                    ViewData["ErrorMessage"] = $"{result}";
                    return View("New", course);
                }
            }
            else
            {
                return View("New", course);
            }
        }
        [JwtAuthentication(roles: [1])]
        public IActionResult Edit(int id)
        {
            var existingCourse = _courseService.GetCourse(id);
            if (existingCourse == null)
            {
                return RedirectToAction("index");
            }
            else
            {
                return View(existingCourse);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, CourseDto updatedCourseDto)
        {
            var existingCourse = await _context.Courses.FindAsync(id);
            if (ModelState.IsValid)
            {
                var result = await _courseService.UpdateCourse(id, updatedCourseDto);
                if (result == "success")
                {
                    return RedirectToAction("Index", "Courses");
                }
                else
                {
                    ViewData["ErrorMessage"] = $"{result}";
                    return View("New", updatedCourseDto);
                }
            }
            else
            {
                return View("Edit");
            }
        }

        [JwtAuthentication(roles: [1])]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var course = await _context.Courses.FindAsync(id);

            if (course == null)
            {
                return RedirectToAction("index");
            }
            else
            {
                return View(course);
            }

        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            Course? course = _context.Courses.Find(id);
            if (course != null)
            {
                var DeleteSuccessfully = _courseService.DeleteCourse(course);
                if (DeleteSuccessfully == "deleted")
                {
                    ViewData["notice"] = "deleted successfully";
                }
                else
                {
                    ViewData["notice"] = "Error occured while deleting it";
                }
            }
            else
            {
                ViewData["notice"] = "this course is not exist";
            }
            return RedirectToAction("Index");
        }

        [JwtAuthentication(roles: [0,1])]
        public IActionResult Show(int id)

        {
            var course = _courseService.GetCourseWithStudents(id);

            if (course == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.course = course;
            return View(course);
        }

    }
}
