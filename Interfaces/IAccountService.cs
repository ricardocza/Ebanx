using Ebanx.Models;

namespace Ebanx.Interfaces;

public interface IAccountService
{
    bool Reset();
    AccountDto? GetBalance(int id);
    Dictionary<string, object> Post(EventDto data);
}
