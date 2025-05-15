using MyBank.Application.DTOs.Accounts;
using MyBank.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

[ApiController]
[Route("api/accounts")]
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
        var account = await _accountAppService.GetAccountById(id);
        return Ok(account);
    }

    [HttpPost("transfer")]
    public async Task<IActionResult> Transfer([FromBody] TransferRequest request)
    {
        await _accountAppService.TransferFunds(request);
        return NoContent();
    }

    [HttpPost("pix/register")]
    public async Task<IActionResult> RegisterPixKey([FromBody] PixKeyRequest request)
    {
        await _accountAppService.RegisterPixKey(request);
        return NoContent();
    }
}