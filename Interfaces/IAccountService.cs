using Ebanx.Models;

namespace Ebanx.Interfaces;

public interface IAccountService
{
    bool Reset();
    AccountDto? GetBalance(string id);
    ResponseDto Post(EventDto data);
}
