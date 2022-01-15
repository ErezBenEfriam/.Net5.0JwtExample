using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_api.IdentityAuth
{
    public class ApplicationUser: IdentityUser
    {
        //Add additional fields to your user
        public string Name { get; set; }
        public string PassWord { get; set; } 
        public string Role { get; set; }

    }
}
