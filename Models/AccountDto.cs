namespace Ebanx.Models;

public class AccountDto
{
    public string Id { get; set; } = "";
    public int Balance { get; set; }

    override public string ToString()
    {
        var result = $"{{\"id\":\"{Id}\", \"balance\":{Balance}}}";        
        return result;
    }
}
