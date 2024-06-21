using Ebanx.Models;

namespace Ebanx.Interfaces;

public interface IAccountService
{
    bool Reset();
    AccountDto? GetBalance(int id);
    AccountDto Post(EventDto data);
}
