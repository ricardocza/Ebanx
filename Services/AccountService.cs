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

    public AccountDto? GetBalance(string id)
    {
        var account = _accounts.FirstOrDefault(a => a.Id == id);
        return account;
    }

    public ResponseDto Post(EventDto data)
    {
        ValidateData(data);

        switch (data.Type)
        {
            case TypeEnum.Deposit:
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

    private static ResponseDto Deposit(EventDto data)
    {
        var destination = _accounts.FirstOrDefault(a => a.Id == data.Destination);

        if (destination == null)
        {
            destination = new AccountDto { Id = data.Destination, Balance = data.Amount };            
            _accounts.Add(destination);            
            return new ResponseDto() { Destination=destination };
        }
        
        destination.Balance += data.Amount;
        return new ResponseDto() { Destination = destination };
    }

    private static ResponseDto Withdraw(EventDto data)
    {
        var origin = _accounts.FirstOrDefault(a => a.Id == data.Origin);
        
        if (origin == null)
        {
            throw new Exception();
        }

        ValidateAvailableBalance(origin, data.Amount);
        origin.Balance -= data.Amount;
        return new ResponseDto() { Origin = origin };
    }

    private static ResponseDto Transfer(EventDto data)
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

        return new ResponseDto 
        { 
            Origin = origin, 
            Destination = destination 
        };
    }

    private static void ValidateAvailableBalance(AccountDto account, decimal amount)
    {
        if (account.Balance < amount)
        {
            throw new InvalidOperationException("Insufficient funds");
        }
    }

    private static void ValidateData(EventDto data)
    {
        var errors = new List<string>();
        
        if (data.Amount <= 0)
        {
            errors.Add("Amount must be greater than 0");
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
