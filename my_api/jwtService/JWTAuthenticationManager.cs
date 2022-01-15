using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace my_api
{
    public class JWTAuthenticationManager : IJWTAuthenticationManager
    {
        private readonly string key;
        private readonly IConfiguration configuration;

        public JWTAuthenticationManager(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.key = this.configuration.GetValue<string>("TokenKey"); ;
        }

        public string CreateToken( ClaimsIdentity Subject)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(key);
            var tokenDescripotor = new SecurityTokenDescriptor()
            {
                Subject = Subject,
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescripotor);
            return tokenHandler.WriteToken(token);
        }
        public string Authenticate(string username, string password)
        {
            var Subject = new ClaimsIdentity(new Claim[]
              {
                    new Claim(ClaimTypes.Name, username),
              });
            return CreateToken(Subject);
        }
 
        public string AuthenticateWithRole(string username, string password)
        {
            var Subject = new ClaimsIdentity(new Claim[]
               {
                   //in the AuthController only User that his token contain this role
                   //would be authorize to access method with  [Authorize(Roles = "admin")] above
                    new Claim(ClaimTypes.Role, "admin"), // defined the Role 
                    new Claim(ClaimTypes.Name, username)
               });
            return CreateToken(Subject);

        }
       
        public string AuthenticateWithCustomClaims(string username, string password)
        {
            var Subject = new ClaimsIdentity(new Claim[]
                {
                    //In Startup.cs we use those claims to create Policy. only user that his token contain both claims
                    // would be authorize to access method with  [Authorize(Policy = "{PolicyName}")] above 
                    new Claim("firstClaim", "selectedValue"),
                    new Claim("secondClaim", "selectedValue2"),
                    new Claim(ClaimTypes.Name, username)
                });
            return CreateToken(Subject);
        }
    }
}
