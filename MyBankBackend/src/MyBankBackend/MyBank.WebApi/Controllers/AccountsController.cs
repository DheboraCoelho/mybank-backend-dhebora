using Microsoft.AspNetCore.Mvc;
using MyBank.Application.DTOs;
using MyBank.Application.Interfaces;
using MyBank.Application.Services;
using System;

namespace MyBank.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody] CreateAccountRequest request)
        {
            var account = await _accountService.CreateAccount(request);
            return CreatedAtAction(nameof(GetAccount), new { id = account.Id }, account);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccount(string id)
        {
            var account = await _accountService.GetAccount(id);
            if (account == null) return NotFound();

            return Ok(account);
        }

        [HttpPost("{accountId}/deposit")]
        public async Task<IActionResult> Deposit(string accountId, [FromBody] TransactionRequest request)
        {
            try
            {
                await _accountService.Deposit(accountId, request);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Adicionar outras ações conforme necessário
    }
}