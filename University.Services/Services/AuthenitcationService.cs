using Microsoft.AspNetCore.Components.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using University.Data;
using University.Services.Services;

namespace University.Web
{
    public class AuthenitcationService : IAuthenticationService
    {
        private readonly JwtCreditService _jwtCreditService;
        public AuthenitcationService(JwtCreditService jwtCreditService)
        {
            _jwtCreditService = jwtCreditService;
        }
        public  string GenerateJWTAuthetication(string userName, string role)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtHeaderParameterNames.Jku, userName),
                new Claim(JwtHeaderParameterNames.Kid, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, userName)
            };



            claims.Add(new Claim(ClaimTypes.Role, role));

            var JwtKey = _jwtCreditService.GetJwtKey();
            var JwtExpireDays = _jwtCreditService.GetJwtExpireDays();
            var JwtIssuer = _jwtCreditService.GetValidAudience();
            var JwtAudience = _jwtCreditService.GetValidIssuer();

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(JwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires =
                DateTime.Now.AddDays(
                    Convert.ToDouble(Convert.ToString(JwtExpireDays)));

            var token = new JwtSecurityToken(
                Convert.ToString(JwtIssuer),
                Convert.ToString(JwtAudience),
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);


        }


        public string ValidateToken(string token)
        {
            var JwtKey = _jwtCreditService.GetJwtKey();
            if (token == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Convert.ToString(JwtKey));
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero

                }, out SecurityToken validatedToken);

                // Corrected access to the validatedToken
                var jwtToken = (JwtSecurityToken)validatedToken;

                var jku = jwtToken.Claims.First(claim => claim.Type == "jku").Value;
                var userName = jwtToken.Claims.First(claim => claim.Type == "kid").Value;
              
                return jku;
            }
            catch
            {
                return null;
            }
        }

    }
}
