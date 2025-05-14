using System;
using System.Threading.Tasks;
using MyBank.Application.DTOs.Auth;
using MyBank.Application.Interfaces;
using MyBank.Domain.Entities;
using MyBank.Domain.Interfaces;

namespace MyBank.Application.Services
{
    public class AuthAppService : IAuthAppService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public AuthAppService(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<TokenResponse> LoginAsync(LoginRequest request)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user == null || !user.ValidatePassword(request.Password))
                return null;

            var token = _tokenService.GenerateToken(user);

            return new TokenResponse
            {
                Token = token,
                Expiration = DateTime.UtcNow.AddHours(1)
            };
        }
    }
}
