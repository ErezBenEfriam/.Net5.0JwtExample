using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace my_api
{
  public  interface IJWTAuthenticationManager
    {
        string CreateToken(ClaimsIdentity Subject);
        string Authenticate(string username, string password);
        string AuthenticateWithRole(string username, string password);
        string AuthenticateWithCustomClaims(string username, string password);
    }
}
