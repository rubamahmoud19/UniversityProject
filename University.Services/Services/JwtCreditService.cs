using Microsoft.Extensions.Configuration;
namespace University.Services.Services
{
    public class JwtCreditService
    {
        private readonly IConfiguration _configuration;
        public JwtCreditService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetJwtKey()
        {
            return _configuration["JwtTokenSettings:JwtKey"];
        }
        public string GetValidIssuer()
        {
            return _configuration["JwtTokenSettings:ValidIssuer"];
        }

        public string GetValidAudience()
        {
            return _configuration["JwtTokenSettings:ValidAudience"];
        }

        public string GetJwtExpireDays()
        {
            return _configuration["JwtTokenSettings:JwtExpireDays"];
        }
    }
}
