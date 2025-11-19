using Microsoft.EntityFrameworkCore;
using Robust.App.Contracts;
using Robust.Context;
using Robust.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robust.Infrastructure
{
    public class UserRepo : GenericRepository<User, int>, IUserRepo
    {
        private readonly RobustContext robustContext;
        public UserRepo(RobustContext _robustContext) : base(_robustContext)
        {
            robustContext = _robustContext;
        }

        public async Task<User> Login(User user)
        {
            var existingUser = await robustContext.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
            if (existingUser == null) return null;

            bool verified = BCrypt.Net.BCrypt.Verify(user.Password, existingUser.Password);
            return verified ? existingUser : null;
        }

        public async Task<User> Register(User user)
        {
            user.Role = Role.customer; 
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            user.CreatedDate = DateTime.UtcNow;

            await robustContext.Users.AddAsync(user);
            await robustContext.SaveChangesAsync();
            return user;
        }
    }
}
