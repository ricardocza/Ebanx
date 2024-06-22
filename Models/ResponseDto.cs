namespace Ebanx.Models;

public class ResponseDto
{
    public AccountDto? Destination { get; set; }
    public AccountDto? Origin { get; set; }

    public override string ToString()
    {
        if (Destination == null)
        {
            return $"{{\"origin\": {Origin}}}";
        }
        else if (Origin == null)
        {
            return $"{{\"destination\": {Destination}}}";
        }
        else
        {
            return $"{{\"origin\": {Origin}, \"destination\": {Destination}}}";
        }
    }
}
