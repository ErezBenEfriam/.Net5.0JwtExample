using my_api.IdentityAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_api.Repository
{
  public  interface IRepository
    {
        ApplicationUser ValidateUser(string Name, String Password);

    }
}
