using AutoMapper;
using MyBank.Domain.Entities;
using MyBank.Domain.Interfaces;
using MyBank.Application.DTOs.Auth;
using MyBank.Domain.Exceptions;
using MyBank.Domain.ValueObjects;
using MyBank.Domain.Services;
using MyBank.Application.Interfaces;
using MyBank.Core.ValueObjects;

namespace MyBank.Application.Services
{
    public class AuthAppService : IAuthAppService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ITokenService _tokenService;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IMapper _mapper;

        public AuthAppService(
            ICustomerRepository customerRepository,
            ITokenService tokenService,
            IPasswordHasher passwordHasher,
            IMapper mapper)
        {
            _customerRepository = customerRepository;
            _tokenService = tokenService;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
        }

        public async Task<AuthResponse> AuthenticateAsync(LoginRequest request)
        {
            var cpf = new Cpf(request.Cpf);
            var customer = await _customerRepository.GetByCpfAsync(cpf);

            if (customer == null || !_passwordHasher.Verify(request.Password, customer.PasswordHash))
                throw new DomainException("CPF ou senha inválidos");

            var token = _tokenService.GenerateToken(customer);
            var refreshToken = _tokenService.GenerateRefreshToken();

            customer.UpdateRefreshToken(refreshToken);
            await _customerRepository.UpdateAsync(customer);

            return new AuthResponse(
                token,
                DateTime.UtcNow.AddHours(2),
                refreshToken,
                customer.Id,
                customer.FullName);
        }

        public async Task RequestPasswordResetAsync(string email)
        {
            var customer = await _customerRepository.GetByEmailAsync(new Email(email));
            if (customer == null) return; // Segurança: não revelar que email não existe

            var resetToken = _tokenService.GeneratePasswordResetToken();
            customer.SetPasswordResetToken(resetToken);
            await _customerRepository.UpdateAsync(customer);

            // Implementar envio de email com o token
        }
    }
}