using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBank.Application.DTOs;
using MyBank.Application.Interfaces;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyBank.WebApi.Controllers
{
    [Authorize] 
    [ApiController]
    [Route("api/accounts")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(
            IAccountService accountService,
            ILogger<AccountController> logger)
        {
            _accountService = accountService;
            _logger = logger;
        }

        /// <summary>
        /// Cria uma nova conta bancária
        /// </summary>
        /// <param name="request">Dados da conta</param>
        /// <returns>Dados da conta criada</returns>
        [HttpPost]
        [Authorize(Roles = "Admin,Manager")] // Apenas admin/manager podem criar contas
        [ProducesResponseType(typeof(AccountResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> CreateAccount([FromBody] CreateAccountRequest request)
        {
            try
            {
                _logger.LogInformation("Iniciando criação de conta para o cliente {OwnerId}", request.OwnerId);

                var account = await _accountService.CreateAccount(request);

                _logger.LogInformation("Conta {AccountId} criada com sucesso", account.Id);

                return CreatedAtAction(
                    nameof(GetAccount),
                    new { id = account.Id },
                    account);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Erro ao criar conta");
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao criar conta");
                return StatusCode(500, new { Message = "Ocorreu um erro interno" });
            }
        }

        /// <summary>
        /// Obtém os dados de uma conta bancária
        /// </summary>
        /// <param name="id">ID da conta</param>
        /// <returns>Dados da conta</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AccountResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAccount(string id)
        {
            try
            {
                var account = await _accountService.GetAccount(id);

                if (account == null)
                {
                    _logger.LogWarning("Conta {AccountId} não encontrada", id);
                    return NotFound();
                }

                // Verifica se o usuário tem acesso à conta
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (account.OwnerId != userId && !User.IsInRole("Admin"))
                {
                    _logger.LogWarning("Usuário {UserId} não autorizado a acessar conta {AccountId}", userId, id);
                    return Forbid();
                }

                return Ok(account);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar conta {AccountId}", id);
                return StatusCode(500, new { Message = "Ocorreu um erro interno" });
            }
        }

        /// <summary>
        /// Realiza um depósito em uma conta
        /// </summary>
        /// <param name="accountId">ID da conta</param>
        /// <param name="request">Valor do depósito</param>
        [HttpPost("{accountId}/deposit")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Deposit(
            [FromRoute] string accountId,
            [FromBody] TransactionRequest request)
        {
            try
            {
                _logger.LogInformation("Iniciando depósito na conta {AccountId}", accountId);

                // Verifica se o usuário tem permissão
                var account = await _accountService.GetAccount(accountId);
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (account.OwnerId != userId && !User.IsInRole("Admin"))
                {
                    return Forbid();
                }

                await _accountService.Deposit(accountId, request);

                _logger.LogInformation("Depósito na conta {AccountId} realizado com sucesso", accountId);

                return NoContent();
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Erro ao realizar depósito");
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao realizar depósito");
                return StatusCode(500, new { Message = "Ocorreu um erro interno" });
            }
        }

        /// <summary>
        /// Realiza um saque em uma conta
        /// </summary>
        /// <param name="accountId">ID da conta</param>
        /// <param name="request">Valor do saque</param>
        [HttpPost("{accountId}/withdraw")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Withdraw(
            [FromRoute] string accountId,
            [FromBody] TransactionRequest request)
        {
            try
            {
                // Implementação similar ao depósito
                await _accountService.Withdraw(accountId, request);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao realizar saque");
                return StatusCode(500, new { Message = "Ocorreu um erro interno" });
            }
        }

    }

}