using Microsoft.AspNetCore.Mvc;
using Robust.App.Services.Abstrctions;
using Robust.App.Services.Implementation;
using Robust.DTO.User;

namespace Robust.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthService authService;
        public AuthController(IAuthService _authService)
        {
            authService = _authService;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            var result = await authService.RegisterAsync(registerDTO);
            return Ok(new { Message = "Registered Successfully" });
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var user=await authService.LoginAsync(loginDTO);
            if (user == null)
                return Unauthorized("invalid User");
            return Ok(user);
        }
        [HttpPost("RefershToken")]
        public async Task<IActionResult> RefreshToken([FromBody] RefershTokenDTO model)
        {
            var result = await authService.RefreshTokenAsync(model.Token,model.RefreshToken);
            if (result == null) return Unauthorized("Invalid or expired refresh token");
            return Ok(result);
        }
    }
}
