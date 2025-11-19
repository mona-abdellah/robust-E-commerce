using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Robust.App.Contracts;
using Robust.App.Services.Abstrctions;
using Robust.DTO.User;
using Robust.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public Task<AuthResponceDTO> LoginAsync(LoginDTO loginDto)
        {
            throw new NotImplementedException();
        }

        public Task<AuthResponceDTO> RefreshTokenAsync(string refreshToken)
        {
            throw new NotImplementedException();
        }

        public async Task<AuthResponceDTO> RegisterAsync(RegisterDTO registerDTO)
        {
            AuthResponceDTO response = new();
            var checkUser = (await _userRepo.GetAllAsync(u => u.Email == registerDTO.Email)).FirstOrDefault();
            if (checkUser != null)
            {
                response.Email = checkUser.Email;
                response.Name = checkUser.Name;
                response.UserId = checkUser.Id;
                response.Role = checkUser.Role.ToString();
                return response;
            }
            var user=mapper.Map<User>(registerDTO);
            await _userRepo.Register(user);
            await _userRepo.SaveChangesAsync();
            var token = _tokenService.CreateToken(user);
            var refreshToken = _tokenService.CreateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _userRepo.Update(user);

            return new AuthResponseDto
            {
                Token = token,
                RefreshToken = refreshToken,
                UserId = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role.ToString()
            };
            return response;
        }
    }
}
