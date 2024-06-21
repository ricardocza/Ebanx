using Ebanx.Interfaces;
using Ebanx.Models;

namespace Ebanx.Services;

public class AccountService : IAccountService
{
    private static ICollection<AccountDto> _accounts = new List<AccountDto>();

    public AccountDto? GetBalance(int id)
    {
        var account = _accounts.FirstOrDefault(a => a.Id == id);

        return account;
    }

    public Dictionary<string, object> Post(EventDto data)
    {
        if (data.Amount <= 0)
        {
            throw new InvalidOperationException("Amount must be greater than 0");
        }

        var type = data.Type.ToLower();

        switch (type)
        {
            case "deposit":
                var depositResult = Deposit(data);
                return depositResult;
            case "withdraw":
                var withdrawResult = Withdraw(data);
                return withdrawResult;
                
            case "transfer":
                var transferResult = Transfer(data);
                return transferResult;
            default:
                throw new InvalidDataException("Invalid type operation");                
        }
    }

    public bool Reset()
    {
        _accounts = new List<AccountDto>();

        return _accounts.Count == 0;
    }

    private Dictionary<string, object> Deposit(EventDto data)
    {
        var destination = _accounts.FirstOrDefault(a => a.Id == data.Destination);
        if (destination == null)
        {
            destination = new AccountDto { Id = data.Destination, Balance = data.Amount };            
            _accounts.Add(destination);
            return new Dictionary<string, object>() { {"destination", destination } };
        }
        
        destination.Balance += data.Amount;
        return new Dictionary<string, object>() { { "destination", destination } };
    }
    private Dictionary<string, object> Withdraw(EventDto data)
    {
        var origin = _accounts.FirstOrDefault(a => a.Id == data.Origin);
        
        if (origin == null)
        {
            throw new Exception();
        }

        ValidateAvailableBalance(origin, data.Amount);
        origin.Balance -= data.Amount;
        return new Dictionary<string, object>() { { "origin", origin } };
    }
    private Dictionary<string, object> Transfer(EventDto data)
    {
        var origin = _accounts.FirstOrDefault(a => a.Id == data.Origin);

        if (origin == null)
        {
            throw new Exception();
        }

        ValidateAvailableBalance(origin, data.Amount);
        var destination = _accounts.FirstOrDefault(a => a.Id == data.Destination);

        if (destination == null)
        {
            destination = new AccountDto { Id = data.Destination, Balance = data.Amount };
            _accounts.Add(destination);
            origin.Balance -= data.Amount;
        }
        else
        {
            origin.Balance -= data.Amount;
            destination.Balance += data.Amount;
        }

        return new Dictionary<string, object> { { "origin", origin }, { "destination", destination } };
    }

    private static void ValidateAvailableBalance(AccountDto account, decimal amount)
    {
        if (account.Balance < amount)
        {
            throw new InvalidOperationException("Insufficient funds");
        }
    }
}
