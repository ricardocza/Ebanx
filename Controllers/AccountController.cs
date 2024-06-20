using Ebanx.Interfaces;
using Ebanx.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ebanx.Controllers;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;
    private ICollection<AccountDto> _accounts; 

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
        _accounts = new List<AccountDto>() { new() { Id=1, Balance = 123} };
    }

    [HttpPost("reset")]
    public async Task<IActionResult> Post()
    {
        _accounts = new List<AccountDto>();
        return Ok();
    }

}
