namespace Ebanx.Models;

public class EventDto
{
    public string Type { get; set; } = "";
    public int Destination { get; set; }
    public int Origin { get; set; }
    public int Amount { get; set; }
}
