using global::University.Data;
using global::University.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace University.Services.Services
{

        public class JwtAuthenticationAttribute : ActionFilterAttribute
        {
            //public AuthenitcationService _authenticationService;
            private int[] _roles;
            public JwtAuthenticationAttribute(int[] roles)
            {
                _roles = roles;
            }
            public override void OnActionExecuting(ActionExecutingContext context)
            {
                var authenticationService = context.HttpContext.RequestServices.GetService<AuthenitcationService>();
                var dbService = context.HttpContext.RequestServices.GetService<UniversityDbContext>();

                HttpContext httpContext = context.HttpContext;
                var request = context.HttpContext.Request;
                var token = request.Cookies["jwt"];

                if (token != null)
                {
                    string userName = authenticationService.ValidateToken(token);
                    if (userName == null)
                    {
                        context.Result = new RedirectToActionResult("Login", "Users", null);
                    }
                    //To check the Role
                    var user = dbService.Users.FirstOrDefault(u => u.Username == userName);
                    bool authorized = _roles.Any(x => x == (int)user.UserType);
                    if (!authorized)
                    {
                        context.Result = new RedirectToActionResult("Index", "Courses", null);
                    }

                }
                else
                {
                    context.Result = new RedirectToActionResult("Login", "Users", null);
                }

                base.OnActionExecuting(context);
            }
       
    }
}
