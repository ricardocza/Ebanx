using Ebanx.Enumerations;
using Ebanx.Interfaces;
using Ebanx.Models;

namespace Ebanx.Services;

public class AccountService : IAccountService
{
    private static ICollection<AccountDto> _accounts = new List<AccountDto>();
    
    public bool Reset()
    {
        _accounts = new List<AccountDto>();
        return _accounts.Count == 0;
    }

    public AccountDto? GetBalance(int id)
    {
        var account = _accounts.FirstOrDefault(a => a.Id == id);
        return account;
    }

    public Dictionary<string, object> Post(EventDto data)
    {        
        ValidataData(data);

        switch (data.Type)
        {
            case Enumerations.TypeEnum.Deposit:
                var depositResult = Deposit(data);
                return depositResult;
            case TypeEnum.Withdraw:
                var withdrawResult = Withdraw(data);
                return withdrawResult;
                
            case TypeEnum.Transfer:
                var transferResult = Transfer(data);
                return transferResult;
            default:
                throw new InvalidOperationException("Internal server error");
        }
    }

    private static Dictionary<string, object> Deposit(EventDto data)
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

    private static Dictionary<string, object> Withdraw(EventDto data)
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

    private static Dictionary<string, object> Transfer(EventDto data)
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

    private void ValidataData(EventDto data)
    {
        var errors = new List<string>();
        
        if (data.Amount <= 0)
        {
            errors.Add("Amount must be greater than 0");
        }
        if ((data.Type == Enumerations.TypeEnum.Deposit || data.Type == TypeEnum.Transfer) && data.Destination <= 0)
        {
            errors.Add("Invalid destination account number");
        }
        if ((data.Type == TypeEnum.Withdraw || data.Type == TypeEnum.Transfer) && data.Origin <= 0)
        {
            errors.Add("Invalid origin account number");
        }
        if (data.Type == TypeEnum.Transfer && data.Destination == data.Origin)
        {
            errors.Add("Origin and destination account number must be different");
        }        
        if (errors.Count > 0)
        {
            throw new InvalidOperationException(string.Join("\n", errors));
        }
    }
}
