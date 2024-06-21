using Ebanx.Interfaces;
using Ebanx.Models;

namespace Ebanx.Services;

public class AccountService : IAccountService
{
    private static ICollection<AccountDto> _accounts = new List<AccountDto>()
    {
        new() { Id = 123, Balance = 123 }
    };

    public AccountDto? GetBalance(int id)
    {
        var account = _accounts.FirstOrDefault(a => a.Id == id);

        return account;
    }

    public AccountDto Post(EventDto data)
    {
        if (data.Amount <= 0)
        {
            throw new InvalidOperationException("Amount must be greater than 0");
        }

        var type = data.Type.ToLower();

        switch (type)
        {
            case "deposit":
                Deposit(data);
                break;
            case "withdraw":
                Withdraw(data);
                break;
            case "transfer":
                Transfer(data);
                break;
            default:
                throw new InvalidDataException("Invalid type operation");                
        }
        return new AccountDto();
    }

    public bool Reset()
    {
        _accounts = new List<AccountDto>();

        return _accounts.Count == 0;
    }

    private void Deposit(EventDto data)
    {
        var destination = _accounts.FirstOrDefault(a => a.Id == data.Destination);
        if (destination == null)
        {
            _accounts.Add(new AccountDto { Id = data.Destination, Balance = data.Amount });
        }
        else
        {
             destination.Balance += data.Amount;
        }        
    }
    private void Withdraw(EventDto data)
    {
        var origin = _accounts.FirstOrDefault(a => a.Id == data.Origin);
        
        if (origin == null)
        {
            throw new Exception();
        }
        else
        {
            origin.Balance -= data.Amount;
        }
    }
    private void Transfer(EventDto data)
    {
        var origin = _accounts.FirstOrDefault(a => a.Id == data.Origin);
        var destination = _accounts.FirstOrDefault(a => a.Id == data.Destination);

        if (origin == null)
        {
            throw new Exception();
        }
        else if (destination == null)
        {
            _accounts.Add(new AccountDto { Id = data.Destination, Balance = data.Amount });
        }
        else
        {
            origin.Balance -= data.Amount;
            destination.Balance += data.Amount;
        }
    }
}
