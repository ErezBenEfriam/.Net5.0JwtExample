using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace my_api.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; }



        [Required(ErrorMessage ="PassWord is required")]
        public string PassWord { get; set; }
    }
}
