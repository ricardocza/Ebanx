using Ebanx.Interfaces;
using Ebanx.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace Ebanx.Controllers;

[Route("/")]
[ApiController]
[AllowAnonymous]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;    

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;        
    }

    [HttpPost("reset")]
    public async Task<IActionResult> Post()
    {
        var result = _accountService.Reset();

        if(result == false)            
            return StatusCode(StatusCodes.Status500InternalServerError);

        return Content("OK");
    }

    [HttpGet("balance")]
    public async Task<IActionResult> GetBalance([FromQuery] string account_id)
    {
        var account = _accountService.GetBalance(account_id);

        if (account == null)
            return NotFound(0);

        return Ok(account.Balance);
    }

    [HttpPost("event")]
    public IActionResult PostEvent([FromBody] EventDto data)
    {
        try
        {
            var result = _accountService.Post(data);
            return Created("", result);
        }
        catch (InvalidOperationException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception)
        {
            return NotFound(0);
        }
    }
}
