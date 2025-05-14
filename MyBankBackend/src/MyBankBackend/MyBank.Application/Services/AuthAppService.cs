using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using MyBank.Domain.Entities;
using MyBank.Domain.Interfaces;
using MyBank.Application.DTOs.Auth;
using MyBank.Domain.Exceptions;
using MyBank.Domain.ValueObjects;
using MyBank.Application.DTOs.Account;
using MyBank.Core.ValueObjects;

namespace MyBank.Application.Services
{
    public class AuthAppService : IAuthAppService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AuthAppService(
            IUserRepository userRepository,
            ITokenService tokenService,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        public async Task<TokenResponse> LoginAsync(LoginRequest request)
        {
            var cpf = new Cpf(request.Cpf);
            var user = await _userRepository.GetByCpfAsync(cpf)
                ?? throw new DomainException("Credenciais inválidas");

            if (!user.ValidatePassword(request.Password))
                throw new DomainException("Credenciais inválidas");

            var token = _tokenService.GenerateToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken();

            user.UpdateRefreshToken(refreshToken);
            await _userRepository.UpdateAsync(user);

            return _mapper.Map<TokenResponse>(user) with
            {
                Token = token,
                RefreshToken = refreshToken
            };
        }

        public async Task<TokenResponse> RefreshTokenAsync(string refreshToken)
        {
            var user = await _userRepository.GetByRefreshTokenAsync(refreshToken)
                ?? throw new DomainException("Token inválido");

            var newToken = _tokenService.GenerateToken(user);
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            user.UpdateRefreshToken(newRefreshToken);
            await _userRepository.UpdateAsync(user);

            return new TokenResponse(
                newToken,
                DateTime.UtcNow.AddHours(2),
                newRefreshToken);
        }
    }
}
