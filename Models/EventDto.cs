using Ebanx.Enumerations;

namespace Ebanx.Models;

public class EventDto
{
    public TypeEnum Type { get; set; }
    public string Destination { get; set; } = "";
    public string Origin { get; set; } = "";
    public int Amount { get; set; }
}
