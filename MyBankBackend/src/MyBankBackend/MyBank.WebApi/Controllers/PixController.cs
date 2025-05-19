// MyBankAPI.WebAPI/Controllers/PixController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBank.Application.Services;
using MyBank.Application.DTOs;
using MyBank.Application.Interfaces;

using System.Threading.Tasks;

namespace MyBank.WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PixController : ControllerBase
    {
        private readonly IPixService _pixService;

        public PixController(IPixService pixService)
        {
            _pixService = pixService;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePixTransaction([FromBody] PixRequest request)
        {
            try
            {
                var result = await _pixService.CreatePixTransaction(request);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}