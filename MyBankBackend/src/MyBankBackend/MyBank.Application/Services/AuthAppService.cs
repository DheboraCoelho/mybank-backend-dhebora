using AutoMapper;
using MyBank.Domain.Entities;
using MyBank.Domain.Interfaces;
using MyBank.Application.DTOs.Auth;
using MyBank.Domain.Exceptions;
using MyBank.Application.Interfaces;

namespace MyBank.Application.Services
{
    public class AuthAppService : IAuthAppService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IEmailService _emailService;

        public AuthAppService(
            IUserRepository userRepository,
            ITokenService tokenService,
            IPasswordHasher passwordHasher,
            IEmailService emailService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _passwordHasher = passwordHasher;
            _emailService = emailService;
        }

        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            var user = await _userRepository.GetByUsernameAsync(request.Username);

            if (user == null || !_passwordHasher.Verify(request.Password, user.PasswordHash))
                throw new DomainException("Credenciais inválidas");

            // Geração de tokens
            var token = _tokenService.GenerateToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken();

            // Atualização do usuário (substitui UpdateRefreshToken)
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);
            await _userRepository.UpdateAsync(user);

            return new AuthResponse
            {
                Token = token,
                Expiration = DateTime.UtcNow.AddHours(2),
                RefreshToken = refreshToken,
                UserId = user.Id,
                Username = user.Username
            };
        }

        public async Task<AuthResponse> RefreshTokenAsync(RefreshTokenRequest request)
        {
            var user = await _userRepository.GetByRefreshTokenAsync(request.RefreshToken);

            if (user == null || user.RefreshTokenExpiry < DateTime.UtcNow)
                throw new DomainException("Token de refresh inválido ou expirado");

            var newToken = _tokenService.GenerateToken(user);
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            // Atualização do usuário
            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);
            await _userRepository.UpdateAsync(user);

            return new AuthResponse
            {
                Token = newToken,
                Expiration = DateTime.UtcNow.AddHours(2),
                RefreshToken = newRefreshToken,
                UserId = user.Id,
                Username = user.Username
            };
        }

        public async Task RequestPasswordResetAsync(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null) return;

            var resetToken = _tokenService.GeneratePasswordResetToken();

            // Configuração do token (substitui SetPasswordResetToken)
            user.PasswordResetToken = resetToken;
            user.PasswordResetTokenExpiry = DateTime.UtcNow.AddHours(1);
            await _userRepository.UpdateAsync(user);

            await _emailService.SendPasswordResetEmailAsync(email, resetToken);
        }

        public async Task ResetPasswordAsync(ResetPasswordRequest request)
        {
            var user = await _userRepository.GetByPasswordResetTokenAsync(request.Token);

            if (user == null || user.PasswordResetTokenExpiry < DateTime.UtcNow)
                throw new DomainException("Token de redefinição inválido ou expirado");

            // Atualização da senha (substitui UpdatePassword e ClearPasswordResetToken)
            user.PasswordHash = _passwordHasher.Hash(request.NewPassword);
            user.PasswordResetToken = null;
            user.PasswordResetTokenExpiry = null;
            await _userRepository.UpdateAsync(user);
        }
    }
}