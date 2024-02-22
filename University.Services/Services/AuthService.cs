//using DataLayer;
//namespace University.Services;
//using Microsoft.IdentityModel.Tokens;
//using DataLayer;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;
//using System.Runtime.CompilerServices;
//using EntityLayer.Entities;
//using Microsoft.EntityFrameworkCore;
//using System.Threading.Tasks;

//public class AuthService : IAuthService
//{
//    private readonly UniversityDbContext _context;
//    public AuthService(UniversityDbContext context)
//    {
//    _context = context;
//}

//public string Authenticate(User user)
//{
//    if (user.Username != null)
//    {
//        var token = GenerateJwtToken(user.Username);
//        return token;
//    }
//    return "";

//}

//Task<string> IAuthService.Authenticate(User user)
//    {
//    throw new NotImplementedException();
//}

//private string GenerateJwtToken(string email)
//{
//    var key = Encoding.ASCII.GetBytes("your-secret-key");
//    var tokenHandler = new JwtSecurityTokenHandler();
//    var tokenDescriptor = new SecurityTokenDescriptor
//    {
//        Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Email, email) }),
//        Expires = DateTime.UtcNow.AddHours(1), // Set the expiration time as needed
//        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
//    };
//    var token = tokenHandler.CreateToken(tokenDescriptor);
//    return tokenHandler.WriteToken(token);
//}


//}
