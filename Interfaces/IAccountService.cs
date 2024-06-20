using Ebanx.Models;

namespace Ebanx.Interfaces;

public interface IAccountService
{
    Task<AccountDto> Get(int id);
    Task<AccountDto> Post();
}
