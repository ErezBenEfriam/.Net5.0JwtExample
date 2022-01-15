using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using my_api.Models;
using my_api.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_api.Controllers
{
   /// <summary>
   /// users For Validate:  Name = "test1", PassWord = "12345"
   ///                 Name = "test2", PassWord = "12345" 
   /// </summary>
   ///    
              
[Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IJWTAuthenticationManager jwtAuthenticationMannager;
        private readonly IRepository repository;

        public AuthController(IJWTAuthenticationManager jwtAuthenticationMannager,IRepository repository)
        {
            this.jwtAuthenticationMannager = jwtAuthenticationMannager;
            this.repository = repository;
        }

        [Authorize]
        [HttpGet("Authorize")]
        public string GetAuthorize()
        {
            return "you are Authorize";
        }
        [Authorize(Roles = "admin")]
        [HttpGet("GetAuthorizeForByRole")]
        public string GetAuthorizeForByRole()
        {
            return "you are Authorize by Role";
        }

        [Authorize(Policy = "adminPolicy")]
        [HttpGet("GetAuthorizeByPolicy_Roles")]
        public string GetAuthorizeByPolicy_Roles()
        {
            return "you are Authorize by Policy that require admin role";
        }
       
        [Authorize(Policy = "PolicyWithTwoClaims")]
        [HttpGet("GetAuthorizeForByPolicy_Claims")]
        public string GetAuthorizeForByPolicy_Claims()
        {
            return "you are Authorize by Policy the require Claims ";
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] LoginModel userCred)
        {
         
    var user=  repository.ValidateUser(userCred.UserName, userCred.PassWord);
            if (user == null) return Unauthorized();

            var token = jwtAuthenticationMannager.Authenticate(userCred.UserName, userCred.PassWord);

            return Ok(new { token = token });
           
        }
     
        [AllowAnonymous]
        [HttpPost("AuthenticateWithRole")]
        public IActionResult AuthenticateWithRole([FromBody] LoginModel userCred)
        {
            var user = repository.ValidateUser(userCred.UserName, userCred.PassWord);
            if (user == null) return Unauthorized();

            var token = jwtAuthenticationMannager.AuthenticateWithRole(userCred.UserName, userCred.PassWord);

            return Ok(new { token = token });

        }
        [AllowAnonymous]
        [HttpPost("AuthenticateWithRole")]
        public IActionResult AuthenticateWithClaim([FromBody] LoginModel userCred)
        { 

            var user = repository.ValidateUser(userCred.UserName, userCred.PassWord);
            if (user == null) return Unauthorized();

            var token = jwtAuthenticationMannager.AuthenticateWithCustomClaims(userCred.UserName, userCred.PassWord);

          
            return Ok(new { token = token });

        }
    }
}
