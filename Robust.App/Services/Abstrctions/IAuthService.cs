using Robust.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robust.App.Services.Abstrctions
{
    public interface IAuthService
    {
        Task<AuthResponceDTO> RegisterAsync(RegisterDTO registerDto);
        Task<AuthResponceDTO> LoginAsync(LoginDTO loginDto);
        Task<AuthResponceDTO> RefreshTokenAsync(string refreshToken);
    }
}
