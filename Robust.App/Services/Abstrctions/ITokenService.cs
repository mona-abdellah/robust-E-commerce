using Robust.DTO.User;
using Robust.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robust.App.Services.Abstrctions
{
    public interface ITokenService
    {
        string CreateToken(User user);
        string CreateRefreshToken();
    }
}
