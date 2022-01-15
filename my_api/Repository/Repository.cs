using my_api.DBContext;
using my_api.IdentityAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_api.Repository
{
    public class Repository : IRepository
    {
        private readonly ApllicationDbContext _context;

        public Repository(ApllicationDbContext context)
        {
            _context = context;
        }
        public ApplicationUser ValidateUser(string Name, string Password)
        {
            var user = _context.applicationUsers.FirstOrDefault(user => user.PassWord == Password && user.Name == Name);
            return user;
        }
    }
}
