using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Mvc;
using MyBank.Application.DTOs.Accounts;
using MyBank.Application.Interfaces;
using System;
using System.Threading.Tasks;

namespace MyBank.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountAppService _accountAppService;

        public AccountsController(IAccountAppService accountAppService)
        {
            _accountAppService = accountAppService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var account = await _accountAppService.GetAccountByIdAsync(id);
            return Ok(account);
        }
    }
}