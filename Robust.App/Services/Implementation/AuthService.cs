using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Robust.App.Contracts;
using Robust.App.Services.Abstrctions;
using Robust.DTO.User;
using Robust.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Robust.App.Services.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepo _userRepo;
        private readonly ITokenService _tokenService;
        private readonly IMapper mapper;
        public AuthService(IUserRepo userRepo, ITokenService tokenService,IMapper _mapper)
        {
            _userRepo = userRepo;
            _tokenService = tokenService;
            mapper = _mapper;
        }
        public async Task<AuthResponceDTO> LoginAsync(LoginDTO loginDto)
        {
            var user = await _userRepo.Login(new User { Email = loginDto.Email, Password = loginDto.Password });
            if (user == null)
                return null;

            var token = _tokenService.CreateToken(user);
            var refreshToken = _tokenService.CreateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _userRepo.UpdateAsync(user);
            await _userRepo.SaveChangesAsync();
            return new AuthResponceDTO
            {
                Token = token,
                RefreshToken = refreshToken,
                UserId = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role.ToString()
            };
        }

        public async Task<AuthResponceDTO> RefreshTokenAsync(string token, string refreshToken)
        {
            var principal = _tokenService.GetPrincipalFromExpiredToken(token);

            int userId = int.Parse(principal.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);

            var user = await _userRepo.GetOneAsync(userId);
            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
                throw new Exception("Invalid or expired refresh token");

            var newToken = _tokenService.CreateToken(user);
            var newRefreshToken = _tokenService.CreateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            await _userRepo.UpdateAsync(user);
            await _userRepo.SaveChangesAsync();
            return new AuthResponceDTO
            {
                Token = newToken,
                RefreshToken = newRefreshToken
            };
        }

        public async Task<AuthResponceDTO> RegisterAsync(RegisterDTO registerDTO)
        {
            AuthResponceDTO response = new();
            var checkUser = (await _userRepo.GetAllAsync(u => u.Email == registerDTO.Email)).FirstOrDefault();
            if (checkUser != null)
                return null;
            var user=mapper.Map<User>(registerDTO);
            await _userRepo.Register(user);
            await _userRepo.SaveChangesAsync();
            var token = _tokenService.CreateToken(user);
            var refreshToken = _tokenService.CreateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _userRepo.UpdateAsync(user);

            return new AuthResponceDTO
            {
                Token = token,
                RefreshToken = refreshToken,
                UserId = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role.ToString()
            };
        }
    }
}
