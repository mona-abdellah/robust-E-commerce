using Robust.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robust.App.Contracts
{
    public interface IUserRepo:IGenericRepo<User,int>
    {
        public Task<User> Login(User user);
        public Task<User> Register(User user);
        public Task<User> GetByEmailAsync(string email);

    }
}
