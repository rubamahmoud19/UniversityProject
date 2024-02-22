using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using University.Data;
using University.Entity.Enums;
using University.Web;
using University.Services.Services;
using University.Entity.Dto;

namespace Controllers;

public class UsersController : Controller
{
    private readonly UniversityDbContext _context;
    private readonly UniversityService _universityService;
    private readonly UserService _userService;
    private readonly JwtCreditService _jwtCreditService;
    private readonly AuthenitcationService _authenitcation;

    public UsersController(UniversityDbContext context, UniversityService universityService, UserService userService, JwtCreditService jwtCreditService, AuthenitcationService authenitcation)
    {
        _context = context;
        _universityService = universityService;
        _userService = userService;
        _jwtCreditService = jwtCreditService;
        _authenitcation = authenitcation;
    }


    public IActionResult New()
    {
        PopulateUniversityListAndUserTypes();
        return View();
    }

    [HttpPost]
    public IActionResult Create(UserDto model)
    {
        if (ModelState.IsValid)
        {
            string result = _userService.CreateUser(model);
            if (result == "success")
            {
                return RedirectToAction("SignIn");
            }
            else
            {
                ViewData["ErrorMessage"] = $"{result}";
                PopulateUniversityListAndUserTypes();
                return View("New", model);
            }
        }
        else
        {
            PopulateUniversityListAndUserTypes();
            return View("New", model);
        }

    }
    [HttpGet("login")]
    public IActionResult SignIn()
    {
        return View();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserDto model)
    {
        if (string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password))
        {
            ModelState.AddModelError("", "Please enter valid username and password");

            return View("SignIn");
        }
        var appUserInfo = _context.Users.Where(u => u.Username == model.Username && u.Password == model.Password).FirstOrDefault();
        if (appUserInfo != null)
        {

            var role = appUserInfo.UserType;

            var jwtToken = _authenitcation.GenerateJWTAuthetication(appUserInfo.Username, role.ToString());
            var validUserName = _authenitcation.ValidateToken(jwtToken);
            
            if (string.IsNullOrEmpty(validUserName))
            {
                ModelState.AddModelError("", "Unauthorized login attempt");

                return View("SignIn");
            }
            Response.Cookies.Append("jwt", jwtToken);
            Response.Cookies.Append("userType", role.ToString());
            return RedirectToAction("index", "courses");
        }
        else
        {
            return View("SignIn");

        }
    }

    public IActionResult Logout()
    {
        HttpContext.Response.Cookies.Delete("jwt");
        return RedirectToAction("login");
    }

    [JwtAuthentication(roles: [0])]
    public IActionResult myCourses()
    {
        ViewBag.Courses = _userService.FetchCourses();
        return View();
    }

    private void PopulateUniversityListAndUserTypes()
    {
        ViewBag.UserTypes = new SelectList(Enum.GetValues(typeof(UserType)).Cast<UserType>());
        ViewBag.Universities = _universityService.GetUniversities();
    }

}