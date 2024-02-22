using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.Services.Services
{
    public interface IAuthenticationService
    {
        public string GenerateJWTAuthetication(string userName, string role);
        public string ValidateToken(string token);
    }
}
